using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestao.Model;

namespace Gestao.Service.AuthService.SenhaService
{
    public interface ISenhaService
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
        string CriarToken<T>(T usuario) where T : class;
    }
}