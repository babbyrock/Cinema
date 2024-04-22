
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
using Xunit.Extensions.Ordering;

namespace Cinema.Tests
{
    public class SalasTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly IMapper _mapper;

        public SalasTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sala, SalaDTO>();
                cfg.CreateMap<SalaDTO, Sala>();
                cfg.CreateMap<Filme, FilmeDTO>();
                cfg.CreateMap<FilmeDTO, Filme>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task A_Post_CadastrarNovaSala()
        {
            var controller = new SalasController(new SalaService(new SalaRepository(_fixture.Context), _mapper));

            var result = await controller.Post(new SalaRequestDTO { Descricao = "Nova Sala" });

            var createdResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<SalaDTO>(createdResult.Value);
            Assert.Equal("Nova Sala", returnValue.Descricao);
        }

        [Fact]
        public async Task Get_DeveRetornarTodasAsSalas()
        {
            var controller = new SalasController(new SalaService(new SalaRepository(_fixture.Context), _mapper));

            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<SalaDTO>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetById_DeveRetornarSalaExistente()
        {
            var existingSalaId = 1; 
            var controller = new SalasController(new SalaService(new SalaRepository(_fixture.Context), _mapper));

            var result = await controller.GetById(existingSalaId);
            var createdResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<SalaDTO>(createdResult.Value);
            Assert.Equal(existingSalaId, returnValue.Numero);
        }


        [Fact]
        public async Task Z_Put_AtualizarSalaExistente()
        {
            var existingSalaId = 1; 
            
            var sala = await _fixture.Context.Salas.FindAsync(existingSalaId);
            sala.Descricao = "Sala Atualizada";

            await _fixture.Context.SaveChangesAsync();
            var controller = new SalasController(new SalaService(new SalaRepository(_fixture.Context), _mapper));

            var result = await controller.Put(existingSalaId, new SalaRequestDTO { Descricao = "Sala Atualizada" });
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<SalaDTO>(createdResult.Value);
            Assert.Equal(sala.Descricao, returnValue.Descricao);
        }

        [Fact]
        public async Task RemoverSalaExistente()
        {
            var existingSalaId = 2;

            var controller = new SalasController(new SalaService(new SalaRepository(_fixture.Context), _mapper));

            var result = await controller.Remove(existingSalaId);

            var createdResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(createdResult);

            var sala = await _fixture.Context.Salas.FindAsync(existingSalaId);
            Assert.Null(sala);
        }




    }


}