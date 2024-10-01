using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Gestao.Enum;

namespace Gestao.Data.Dtos
{
    public class AlunoDto
    {
       [Required(ErrorMessage = "Digite um nome!")]
       public string Nome {get; set;}

       [Required(ErrorMessage = "Insira um nome de usuario valido!")] 
       public string Usuario {get; set;}
       [Required(ErrorMessage = "Digite um email valido!")]
       [EmailAddress(ErrorMessage = "Endereço de email inválido.")]
       public string Email {get; set;}
       [Required(ErrorMessage = "Digite uma senha valida")]
       public string Senha {get; set;}
       [Required]
       [Compare("Senha", ErrorMessage = "Senha nao coincidem")]
       public string ConfirmacaoSenha {get; set;} 
       [Required(ErrorMessage = "Digite um numero valido!")]
       public string Numero {get; set;}
       [Required(ErrorMessage = "Digite um endereço valido!")]
       public string Endereco {get; set;}
       [Required(ErrorMessage = "Coloque uma data valida")]
       public DateTime DataNascimento {get; set;}
       public CargoEnum Cargo {get; set;}
    }
}