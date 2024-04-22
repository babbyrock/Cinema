using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Cinema.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infra.Data.Repositories
{
    public class SalaRepository : ISalaRepository
    {
        ApplicationDbContext _context;
        public SalaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Sala> Create(Sala sala)
        {
            _context.Add(sala);
            await _context.SaveChangesAsync();
            return sala;
        }

        public async Task<Sala> GetById(int? id)
        {
            return await _context.Salas.FindAsync(id);
        }

        public async Task<IEnumerable<Sala>> GetAll(int pagina, int tamanhoPagina)
        {
            return await _context.Salas
                .Include(x => x.Filmes)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }




        public async Task<Sala> Update(Sala sala)
        {
            _context.Update(sala);
            await _context.SaveChangesAsync();
            return sala;
        }

        public async Task<Sala> Remove(Sala sala)
        {
            _context.Remove(sala);
            await _context.SaveChangesAsync();
            return sala;
        }
    }
}
