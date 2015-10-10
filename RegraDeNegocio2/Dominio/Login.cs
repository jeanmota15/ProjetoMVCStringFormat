using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Dominio
{
    public class Login
    {
        [Key]
        public int LoginId { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }
    }
}
