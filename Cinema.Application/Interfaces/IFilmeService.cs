using Cinema.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IFilmeService
    {
        Task<FilmeDTO> Add(FilmeRequestDTO salaDto);
        Task<FilmeDTO> Update(int id, FilmeRequestDTO salaDto);
        Task<IEnumerable<FilmeDTO>> GetAll(int pagina, int tamanhoPagina);
        Task<FilmeDTO> GetById(int? id);
        Task<FilmeDTO> Remove(int id);
    }
}
