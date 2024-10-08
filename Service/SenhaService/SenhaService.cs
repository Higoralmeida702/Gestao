using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Gestao.Model;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Gestao.Data.Dtos;

namespace Gestao.Service.AuthService.SenhaService
{
    public class SenhaService : ISenhaService
    {
        private readonly IConfiguration _config;

        public SenhaService(IConfiguration config)
        {
            _config = config;
        }

        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(senhaHash);
            }
        }

        public string CriarToken<T>(T usuario) where T : class
        {
            if (usuario is Aluno aluno)
            {
                return CriarTokenBase(aluno.Cargo.ToString(), aluno.Email, aluno.Usuario);
            }
            else if (usuario is Professores professor)
            {
                return CriarTokenBase(professor.Cargo.ToString(), professor.Email, professor.Usuario);
            }
            else
            {
                throw new ArgumentException("Tipo de usuário inválido");
            }
        }

        private string CriarTokenBase(string cargo, string email, string username)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Cargo", cargo),
                new Claim("Email", email),
                new Claim("Username", username),
                new Claim(ClaimTypes.Role, cargo)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
