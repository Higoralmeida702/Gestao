using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Gestao.Enum;

namespace Gestao.Model
{
    public class Aluno
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "Digite um nome!")]
        public string Nome {get; set;}

        [Required(ErrorMessage = "Insira um nome de usuario valido!")] 
        public string Usuario {get; set;}
        [Required(ErrorMessage = "Digite um email valido!")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email {get; set;}
        [Required(ErrorMessage = "Digite um endereço!")]
        public string Endereco {get; set;}
       
        [Required(ErrorMessage = "Digite o numero de telefone valido!")]
        public string Numero {get; set;}

        [Required(ErrorMessage = "Insira a data de aniversario")]
        public DateTime DataNascimento {get; set;}
        public DateTime DataMatricula {get; set;} = DateTime.Now;
        public CargoEnum Cargo {get; set;}

        public int RA {get; set;}
        public List<NotaAluno> NotaAlunos {get; set;}

        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
    }
}