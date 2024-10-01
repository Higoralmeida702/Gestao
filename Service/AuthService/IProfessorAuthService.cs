using Gestao.Data.Dtos;
using Gestao.Model;

namespace Gestao.Service.AuthService
{
    public interface IProfessorAuthService
    {
        Task<Response<ProfessorDto>> Registrar (ProfessorDto professorDto);
        Task<Response<string>> Login (LoginDto loginDto);
    }
}
