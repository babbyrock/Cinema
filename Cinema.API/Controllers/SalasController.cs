using Cinema.Application.DTOs;
using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : Controller
    {
        private readonly ISalaService _salaService;

        public SalasController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaDTO>>> Get()
        {
            var salas = await _salaService.GetAll();
            if(salas == null)
            {
                return NotFound("Salas não encontradas!");
            }
            return Ok(salas);
        }


        [HttpGet("{id}", Name = "GetSala")]
        public async Task<ActionResult<SalaDTO>> GetById(int id)
        {
            var sala = await _salaService.GetById(id);
            if (sala == null)
            {
                return NotFound("Sala não encontrada!");
            }
            return Ok(sala);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalaRequestDTO salaDTO)
        {
            if (salaDTO == null)
                return BadRequest("Invalid Data");

            var  newSalaDTO = new SalaDTO
            {
                Descricao = salaDTO.Descricao
            };

            return Ok(await _salaService.Add(newSalaDTO));
        }



        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] SalaRequestDTO salaDTO)
        {
            if (salaDTO == null)
                return BadRequest("Invalid Data");

            var newSalaDTO = new SalaDTO
            {
                Numero = id,
                Descricao = salaDTO.Descricao
            };

            var response = await _salaService.Update(newSalaDTO);

            if (response == null)
                return BadRequest("Invalid Data");

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<SalaDTO>> Remove(int id)
        {
           var response = await _salaService.Remove(id);

            if (response == null)
                return NotFound("Sala não encontrada");

            return Ok(response);
        }
    }
}
