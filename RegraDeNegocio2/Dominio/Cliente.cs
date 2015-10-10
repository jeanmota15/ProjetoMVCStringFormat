using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Dominio
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo Sobrenome")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Preencha o campo Email")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DataCadastro { get; set; }

        public Boolean Ativo { get; set; }
    }
}
