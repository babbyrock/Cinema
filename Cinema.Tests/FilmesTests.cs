using AutoMapper;
using Cinema.API.Controllers;
using Cinema.Application.DTOs;
using Cinema.Application.Interfaces;
using Cinema.Application.Services;
using Cinema.Domain.Entities;
using Cinema.Infra.Data.Context;
using Cinema.Infra.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cinema.Tests
{
    public class FilmesControllerTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly IMapper _mapper;

        public FilmesControllerTests(DatabaseFixture fixture)
        {
            _fixture = fixture;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sala, SalaDTO>();
                cfg.CreateMap<SalaDTO, Sala>();
                cfg.CreateMap<Filme, FilmeDTO>();
                cfg.CreateMap<FilmeDTO, Filme>();
                cfg.CreateMap<FilmeRequestDTO, Filme>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Post_AdicionarNovoFilme()
        {
            var controller = new FilmesController(new FilmeService(new FilmeRepository(_fixture.Context), _mapper));
            var filmeRequestDTO = new FilmeRequestDTO { Nome = "Novo Filme", Duracao = "120", Diretor = "Diretor 3", SalaId = 1 };

            var result = await controller.Post(filmeRequestDTO);

            var createdResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<FilmeDTO>(createdResult.Value);
            Assert.Equal("Novo Filme", returnValue.Nome);
            Assert.Equal("120", returnValue.Duracao);
            Assert.Equal("Diretor 3", returnValue.Diretor);
            Assert.Equal(1, returnValue.SalaId);
        }


        [Fact]
        public async Task Get_DeveRetornarTodosOsFilmes()
        {
            var controller = new FilmesController(new FilmeService(new FilmeRepository(_fixture.Context), _mapper));

            var result = await controller.GetAll(1, 3);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<FilmeDTO>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetById_DeveRetornarFilmeExistente()
        {
            var existingFilmeId = 1;
            var controller = new FilmesController(new FilmeService(new FilmeRepository(_fixture.Context), _mapper));

            var result = await controller.GetById(existingFilmeId);
            var createdResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<FilmeDTO>(createdResult.Value);
            Assert.Equal(existingFilmeId, returnValue.Id);
        }

        [Fact]
        public async Task Z_Put_AtualizarFilmeExistente()
        {
            var existingFilmeId = 1;

            var filme = await _fixture.Context.Filmes.FindAsync(existingFilmeId);
            filme.Nome = "Filme Atualizado";

            await _fixture.Context.SaveChangesAsync();
            var controller = new FilmesController(new FilmeService(new FilmeRepository(_fixture.Context), _mapper));

            var result = await controller.Put(existingFilmeId, new FilmeRequestDTO { Nome = "Filme Atualizado" });
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<FilmeDTO>(createdResult.Value);
            Assert.Equal(filme.Nome, returnValue.Nome);
        }

        [Fact]
        public async Task RemoverFilmeExistente()
        {
            var existingFilmeId = 2;

            var controller = new FilmesController(new FilmeService(new FilmeRepository(_fixture.Context), _mapper));

            var result = await controller.Remove(existingFilmeId);

            var createdResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(createdResult);

            var filme = await _fixture.Context.Filmes.FindAsync(existingFilmeId);
            Assert.Null(filme);
        }

    }
}
