using Cinema.Domain.Entities;
using Cinema.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cinema.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            Context = new ApplicationDbContext(options);

            // Adicione as salas
            var sala1 = new Sala { Descricao = "Sala de Teste 1" };
            var sala2 = new Sala { Descricao = "Sala de Teste 2" };
            Context.Salas.Add(sala1);
            Context.Salas.Add(sala2);
            Context.SaveChanges();

            // Adicione os filmes relacionados às salas
            Context.Filmes.Add(new Filme { Nome = "Filme 1", Duracao = "120", Diretor = "Diretor 1", SalaId = sala1.Numero });
            Context.Filmes.Add(new Filme { Nome = "Filme 2", Duracao = "90", Diretor = "Diretor 2", SalaId = sala2.Numero });
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
