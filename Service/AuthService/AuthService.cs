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
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISenhaService _iSenhaService;
        public AuthService(ApplicationDbContext applicationDbContext, ISenhaService iSenhaService)
        {
            _applicationDbContext = applicationDbContext;
            _iSenhaService = iSenhaService;
        }


        public async Task<Response<AlunoDtos>> Registrar(AlunoDtos alunoDtos)
        {
            Response<AlunoDtos> respostaServico = new Response<AlunoDtos>();
            try
            {
                if(!VerificaSeEmaileUsuarioJaExiste(alunoDtos))
                {
                    respostaServico.Dados = null;
                    respostaServico.Status = false;
                    respostaServico.Mensagem = "Usuarios ja cadastrado";
                    return respostaServico;
                }

                _iSenhaService.CriarSenhaHash(alunoDtos.Senha,out byte[] senhaHash, out byte[] senhaSalt);

                Aluno usuario = new Aluno()
                {
                    Usuario = alunoDtos.Usuario,
                    Nome = alunoDtos.Nome,
                    Endereco = alunoDtos.Endereco,
                    Cargo = CargoEnum.Aluno,
                    Numero = alunoDtos.Numero,
                    DataNascimento = alunoDtos.DataNascimento,
                    Email = alunoDtos.Email,
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

        public bool VerificaSeEmaileUsuarioJaExiste(AlunoDtos alunoDtos)
        {
            var usuario = _applicationDbContext.Alunos.FirstOrDefault(userBanco => userBanco.Email == alunoDtos.Email || userBanco.Usuario == alunoDtos.Usuario);
            
            if (usuario != null) return false;

            return true;
        }

        
    }
}