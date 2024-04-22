using Cinema.Application.DTOs;
using Cinema.Application.Interfaces;
using Cinema.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeService _filmeService;

        public FilmesController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FilmeRequestDTO filmeDTO)
        {
            if (filmeDTO == null)
                return BadRequest("Invalid Data");


            return Ok(await _filmeService.Add(filmeDTO));
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] FilmeRequestDTO filmeDTO)
        {
            if (filmeDTO == null)
                return BadRequest("Invalid Data");


            var response = await _filmeService.Update(id, filmeDTO);

            if (response == null)
                return BadRequest("Invalid Data");

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeDTO>>> GetAll([FromQuery] int pagina = 1 , [FromQuery] int tamanhoPagina = 10)
        {
            var filmes = await _filmeService.GetAll(pagina, tamanhoPagina);
            if (filmes == null)
            {
                return NotFound("Filmes não encontrados!");
            }
            return Ok(filmes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeDTO>> GetById(int id)
        {
            var filme = await _filmeService.GetById(id);
            if (filme == null)
            {
                return NotFound("Filme não encontrada!");
            }
            return Ok(filme);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FilmeDTO>> Remove(int id)
        {
            var response = await _filmeService.Remove(id);

            if (response == null)
                return NotFound("Sala não encontrada");

            return Ok(response);
        }
    }
}
