using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gestao.Model
{
    public class NotaAluno
    {
        public int Id {get; set;}
        public int MateriaId {get; set;}
        public int ProfessorId {get; set;}
        public int AlunoId {get; set;}

        public double Nota {get; set;}
        public int RA {get; set;}
        public string MateriaNome {get; set;}
        public string ProfessorNome {get; set;}
        public Aluno Aluno {get; set;}
        public Professores Professor {get; set;}
        public Materia Materia {get; set;}
    }
}