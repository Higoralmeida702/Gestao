    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestao.Data;
using Gestao.Data.Dtos;
using Gestao.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Service
{
    public class NotaService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NotaService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> AtribuirNota(NotaAlunoDto notaDto)
        {
            var aluno = await _applicationDbContext.Alunos.FirstOrDefaultAsync(a => a.RA == notaDto.RA);
            var materia = await _applicationDbContext.Materias.FirstOrDefaultAsync(m => m.Nome == notaDto.NomeMateria);
            var professor = await _applicationDbContext.Professores.FirstOrDefaultAsync(p => p.Nome == notaDto.NomeProfessor);

            if (materia == null || professor == null)
            {
                return new BadRequestObjectResult("Matéria ou Professor inválidos");
            }

            if (aluno == null)
            {
                return new BadRequestObjectResult($"Aluno com RA {notaDto.RA} não encontrado.");
            }

            var novaNotaAluno = new NotaAluno
            {
                AlunoId = aluno.Id,
                RA = aluno.RA,
                MateriaNome = materia.Nome,
                ProfessorNome = professor.Nome,
                Nota = notaDto.Nota,
                ProfessorId = professor.Id,
                MateriaId = materia.Id
            };

            try
            {
                _applicationDbContext.NotaAlunos.Add(novaNotaAluno);
                await _applicationDbContext.SaveChangesAsync();

                return new OkObjectResult("Nota atribuída com sucesso");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Erro ao atribuir a nota: {ex.Message}");
            }
        }

    public async Task<List<NotaAlunoDto>> BuscarNotasPorRA(int ra)
        {
            var aluno = await _applicationDbContext.Alunos.FirstOrDefaultAsync(a => a.RA == ra);

            if (aluno == null)
            {
                return new List<NotaAlunoDto>();
            }

            return await _applicationDbContext.NotaAlunos
                .Where(n => n.AlunoId == aluno.Id)
                .Include(n => n.Materia)
                .Include(n => n.Professor)
                .Select(n => new NotaAlunoDto
                {
                    NomeMateria = n.Materia.Nome,
                    NomeProfessor = n.Professor.Nome,
                    RA = n.Aluno.RA,
                    Nota = n.Nota
                })
                .ToListAsync();
        }
    }
}