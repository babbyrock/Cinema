using Cinema.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Cinema.Domain.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CreateSala_WithInvalidFilmeId_ThrowsDomainException()
        {
            Filme filme = new Filme(1, "Invocação do mal", "3 horas", null, "James Wan");
            Action action = () => new Sala(1, "Terror", new List<Filme> { filme });

            action.Should()
                .NotThrow<Cinema.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreatSala_NegativeIdValue_DomainExceptionInvalidId()
        {
            Filme filme = new Filme(1, "Invocação do mal", "3 horas", null, "James Wan");
            Action action = () => new Sala(-1, "Terror", new List<Filme> { filme });
            action.Should()
                .NotThrow<Cinema.Domain.Validation.DomainExceptionValidation>();
        }
    }
}