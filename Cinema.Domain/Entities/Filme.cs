using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Entities
{
    public class Filme
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Duracao { get; set; }
        public string Diretor { get; set; }
        public int? SalaId { get; set; }
        public virtual Sala? Sala { get; set; }

        public Filme()
        {
            
        }

        public Filme(int Id, string Nome,string Duracao, int? SalaId, string Diretor)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Duracao = Duracao;
            this.Diretor = Diretor;
            this.SalaId = SalaId;
        }
    }
}
