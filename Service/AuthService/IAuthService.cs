using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestao.Data.Dtos;
using Gestao.Model;

namespace Gestao.Service.AuthService
{
    public interface IAuthService 
    {
        Task<Response<AlunoDto>> Registrar(AlunoDto alunoDto);
        Task<Response<string>> Login(LoginDto loginDto);
    }
}