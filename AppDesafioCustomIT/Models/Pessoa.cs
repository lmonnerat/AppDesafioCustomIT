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
        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} tamanho deve ser entre {2} e {1}")]

        public string Nome { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime DataNasc { get; set; }
        [Required(ErrorMessage = "{0} required")]
        public string CPF { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public string Bairro { get; set; }        
        public string Cidade { get; set; }
        public string UF { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Entre com um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public virtual ICollection<Telefone> Telefones { get; set; }

        //public int Idade { get; set; }

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
