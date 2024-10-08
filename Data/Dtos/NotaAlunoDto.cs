using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gestao.Data.Dtos
{
    public class NotaAlunoDto
    {
        public string NomeProfessor {get; set;}
        public string NomeMateria {get; set;}
        public int RA {get; set;}
        [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public double Nota {get; set;}
    }
}