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
    public class FilmeRepository : IFilmeRepository
    {
        ApplicationDbContext _context;
        public FilmeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Filme> Create(Filme filme)
        {
            _context.Add(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task<Filme> GetById(int? id)
        {
            return await _context.Filmes.FindAsync(id);
        }

        public async Task<Filme> GetByFilmeSala(int? id)
        {
            return await _context.Filmes.Include(x => x.Sala).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Filme>> GetAll(int pagina, int tamanhoPagina)
        {
            return await _context.Filmes
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task<Filme> Remove(Filme filme)
        {
            _context.Remove(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task<Filme> Update(Filme filme)
        {
            _context.Update(filme);
            await _context.SaveChangesAsync();
            return filme;
        }
    }
}
