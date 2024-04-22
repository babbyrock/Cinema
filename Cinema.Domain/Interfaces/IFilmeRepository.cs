using Cinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Interfaces
{
    public interface IFilmeRepository
    {
        Task<IEnumerable<Filme>> GetAll(int pagina, int tamanhoPagina);
        Task<Filme> GetById(int? id);
        Task<Filme> Create(Filme sala);
        Task<Filme> Update(Filme sala);
        Task<Filme> Remove(Filme sala);
    }
}
