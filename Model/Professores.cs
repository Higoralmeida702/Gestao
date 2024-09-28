using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Gestao.Enum;

namespace Gestao.Model
{
    public class Professores
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "Digite um nome!")]
        public string Nome {get; set;}
        [Required(ErrorMessage = "Digite um email valido!")]
        [EmailAddress]
        public string Email {get; set;}
        [Required(ErrorMessage = "Digite um endereço!")]
        public string Endereco {get; set;}
        [Required(ErrorMessage = "Digite o numero de telefone valido!")]
        public string Numero {get; set;}
        [Required(ErrorMessage = "Necessario espeficicar que materia lesiona!")]
        public string MateriaResponsavel {get; set;}
        
        [Required(ErrorMessage = "Insira um nome de usuario valido!")] 
        public string Usuario {get; set;}
        public CargoEnum Cargo {get; set;}
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
    }
}