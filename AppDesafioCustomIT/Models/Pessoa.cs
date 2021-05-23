using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDesafioCustomIT.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} tamanho deve ser entre {2} e {1}")]

        public string Nome { get; set; }
        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime DataNasc { get; set; }
        [Required]
        public string CPF { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public string Bairro { get; set; }        
        public string Cidade { get; set; }
        [MaxLength(2)]
        public string UF { get; set; }        
        [EmailAddress(ErrorMessage = "Entre com um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public virtual ICollection<Telefone> Telefones { get; set; }

        public int CalculaIdade()
        {
            int idade = DateTime.Today.Year - DataNasc.Year;
            if (DateTime.Now.DayOfYear < DataNasc.DayOfYear)
            {
                idade--;
            }
            return idade;
        }
    }
}
