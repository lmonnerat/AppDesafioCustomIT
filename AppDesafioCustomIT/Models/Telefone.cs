using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDesafioCustomIT.Models
{
    public class Telefone
    {
        public int TelefoneId { get; set; }
        public string NumTelefone { get; set; }
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
