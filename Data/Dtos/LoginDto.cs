using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gestao.Data.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O campo email é obrigatório"), EmailAddress(ErrorMessage = "Email inválido")]
        public string Email {get; set;}

        [Required(ErrorMessage = "O senha é obrigatório")]
        public string Senha {get; set;}
    }
}