using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestao.Data;
using Gestao.Data.Dtos;
using Gestao.Enum;
using Gestao.Model;
using Gestao.Service.AuthService.SenhaService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Service.AuthService
{
    public class AlunoAuthService : IAlunoAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISenhaService _iSenhaService;
        public AlunoAuthService(ApplicationDbContext applicationDbContext, ISenhaService iSenhaService)
        {
            _applicationDbContext = applicationDbContext;
            _iSenhaService = iSenhaService;
        }


        public async Task<Response<AlunoDto>> Registrar(AlunoDto alunoDto)
        {
            Response<AlunoDto> respostaServico = new Response<AlunoDto>();
            try
            {
                if(!VerificaSeEmaileUsuarioJaExiste(alunoDto))
                {
                    respostaServico.Dados = null;
                    respostaServico.Status = false;
                    respostaServico.Mensagem = "Usuarios ja cadastrado";
                    return respostaServico;
                }

                _iSenhaService.CriarSenhaHash(alunoDto.Senha,out byte[] senhaHash, out byte[] senhaSalt);

                Aluno usuario = new Aluno()
                {
                    Usuario = alunoDto.Usuario,
                    Nome = alunoDto.Nome,
                    Endereco = alunoDto.Endereco,
                    Cargo = CargoEnum.Aluno,
                    Numero = alunoDto.Numero,
                    DataNascimento = alunoDto.DataNascimento,
                    Email = alunoDto.Email,
                    RA = GerarNumeroAleatorio(),
                    PasswordHash = senhaHash,
                    PasswordSalt = senhaSalt
                };
                _applicationDbContext.Add(usuario);
                await _applicationDbContext.SaveChangesAsync();
                respostaServico.Mensagem = "Usuario criado com sucesso";


            }catch (Exception error)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = error.Message;
                respostaServico.Status = false;
            }
            return respostaServico;
        }

        public async Task<Response<string>> Login(LoginDto loginDto)
        {
            Response<string> respostaServico = new Response<string>();

            try
            {
                var usuario = await _applicationDbContext.Alunos.FirstOrDefaultAsync(userBanco => userBanco.Email == loginDto.Email);
                if (usuario == null)
                {
                    respostaServico.Mensagem = "Crendencias inválidas!";
                    respostaServico.Status = false;
                    return respostaServico;
                }

                if(!_iSenhaService.VerificaSenhaHash(loginDto.Senha, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    respostaServico.Mensagem = "Credenciais inválidas!";
                    respostaServico.Status = false;
                    return respostaServico;
                }

                var token = _iSenhaService.CriarToken(usuario);
                respostaServico.Dados = token;
                respostaServico.Mensagem = "Usuário logado com sucesso!";

            }
            catch (Exception error)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = error.Message;
                respostaServico.Status = false;
            }

            return respostaServico;
        }

        public bool VerificaSeEmaileUsuarioJaExiste(AlunoDto alunoDto)
        {
            var usuario = _applicationDbContext.Alunos.FirstOrDefault(userBanco => userBanco.Email == alunoDto.Email || userBanco.Usuario == alunoDto.Usuario);
            
            if (usuario != null) return false;

            return true;
        }

        private int GerarNumeroAleatorio()
        {
            Random random = new Random();
            return random.Next(1000000000, 2000000000);
        }

    }
}