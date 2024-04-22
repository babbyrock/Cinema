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
        Task<IEnumerable<Filme>> GetFilmes();
        Task<Filme> GetById(int? id);
        Task<Filme> Create(Filme sala);
        Task<Filme> Update(Filme sala);
        Task<Filme> Remove(Filme sala);
    }
}
