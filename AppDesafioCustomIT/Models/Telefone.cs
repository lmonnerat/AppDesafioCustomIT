using System.ComponentModel.DataAnnotations;

namespace AppDesafioCustomIT.Models
{
    public class Telefone
    {
        public int TelefoneId { get; set; }
        [Display(Name = "Telefone")]
        [MaxLength(30)]
        public string NumTelefone { get; set; }
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
