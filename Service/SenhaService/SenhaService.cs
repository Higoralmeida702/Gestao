using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Gestao.Service.AuthService.SenhaService
{
    public class SenhaService : ISenhaService
    {
     public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
     {
        using (var hmac = new HMACSHA512())
        {
            senhaSalt = hmac.Key;
            senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
        }
     }
    }

   
}