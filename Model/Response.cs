using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestao.Model
{
    public class Response<T>
    {
        public T? Dados {get; set;}
        public string Mensagem {get; set;} = string.Empty;
        public bool Status {get; set;} = true;
    }
}