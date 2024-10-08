using Gestao.Data;
using Gestao.Data.Dtos;
using Gestao.Enum;
using Gestao.Model;
using Gestao.Service.AuthService.SenhaService;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Service.AuthService
{
    public class ProfessorAuthService : IProfessorAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISenhaService _iSenhaService;

        public ProfessorAuthService (ApplicationDbContext applicationDbContext, ISenhaService senhaService)
        {
            _applicationDbContext = applicationDbContext;
            _iSenhaService = senhaService;
        }

        public async Task<Response<ProfessorDto>> Registrar(ProfessorDto professorDto)
        {
            Response<ProfessorDto> respostaServico = new Response<ProfessorDto>();
            try
            {
                if(!VerificaSeEmaileUsuarioJaExistem(professorDto))
                {
                    respostaServico.Dados = null;
                    respostaServico.Status = false;
                    respostaServico.Mensagem = "Usuario ja cadastrado";
                    return respostaServico;
                }

            _iSenhaService.CriarSenhaHash(professorDto.Senha,out byte[] senhaHash, out byte[] senhaSalt);

            var materias = await _applicationDbContext.Materias
                .Where(m => professorDto.MateriasId.Contains(m.Id))
                .ToListAsync();

            Professores usuario = new Professores()
            {
                Usuario = professorDto.Usuario,
                Nome = professorDto.Nome,
                Endereco = professorDto.Endereco,
                Cargo = CargoEnum.Professor,
                Numero = professorDto.Numero,
                DataNascimento = professorDto.DataNascimento,
                Email = professorDto.Email,
                Materias = materias,
                PasswordHash = senhaHash,
                PasswordSalt = senhaSalt
            };   
            
            _applicationDbContext.Professores.Add(usuario);
            await _applicationDbContext.SaveChangesAsync();


            respostaServico.Mensagem = "Professor criado com sucesso";
            respostaServico.Status = true;
            respostaServico.Dados = professorDto;

            }
            catch (Exception error)
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
                var usuario = await _applicationDbContext.Professores.FirstOrDefaultAsync(userBanco => userBanco.Email == loginDto.Email);
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
                respostaServico.Mensagem = "Usuario logado com sucesso!";
            }
            catch (Exception error)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = error.Message;
                respostaServico.Status = false;
            }
            return respostaServico;
        }


        public bool VerificaSeEmaileUsuarioJaExistem(ProfessorDto professorDto)
        {
            var usuario = _applicationDbContext.Professores.FirstOrDefault(userBanco => userBanco.Email == professorDto.Email || userBanco.Usuario == professorDto.Usuario);
            if (usuario != null) return false;
            return true;
        }
    }
}
