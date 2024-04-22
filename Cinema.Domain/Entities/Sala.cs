using Cinema.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Entities
{
    public class Sala 
    {
        public int Numero { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Filme> Filmes { get; set; }

        public Sala() { }

        public Sala(int Numero, string Descricao, ICollection<Filme> Filmes)
        {
            DomainExceptionValidation.When(Numero < 0, "Numero de sala inválida");
            this.Numero = Numero;
            this.Descricao = Descricao;
            this.Filmes = Filmes;
        }

    }
}
