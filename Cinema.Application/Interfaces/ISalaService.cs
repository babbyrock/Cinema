using Cinema.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface ISalaService
    {
        Task<IEnumerable<SalaDTO>> GetAll(int pagina, int tamanhoPagina);
        Task<SalaDTO> GetById(int? id);
        Task<SalaDTO> Add(SalaDTO salaDto);
        Task<SalaDTO> Update(SalaDTO salaDto);
        Task<SalaDTO> Remove(int id);
    }
}
