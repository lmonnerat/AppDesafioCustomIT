using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [MaxLength(20)]
        public string CPF { get; set; }
        [Display(Name = "Endereço")]
        [MaxLength(100)]
        public string Endereco { get; set; }
        [MaxLength(60)]
        public string Bairro { get; set; }
        [MaxLength(60)]
        public string Cidade { get; set; }
        [MaxLength(2)]
        public string UF { get; set; }
        [EmailAddress(ErrorMessage = "Entre com um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(60)]
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
