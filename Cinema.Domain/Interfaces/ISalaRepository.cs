using Cinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Interfaces
{
    public interface ISalaRepository
    {
        Task<IEnumerable<Sala>> GetAll(int pagina, int tamanhoPagina);
        Task<Sala> GetById(int? id);
        Task<Sala> Create(Sala sala);
        Task<Sala> Update(Sala sala);
        Task<Sala> Remove(Sala sala);
    }
}
