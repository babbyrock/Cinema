using Cinema.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTOs
{
    public class FilmeDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("duracao")]
        public string Duracao { get; set; }

        [JsonProperty("diretor")]
        public string Diretor { get; set; }

        [JsonProperty("salaId")]
        public int? SalaId { get; set; }
    }

    public class FilmeRequestDTO
    {
        [JsonProperty("nome")]
        public string? Nome { get; set; }

        [JsonProperty("duracao")]
        public string? Duracao { get; set; }

        [JsonProperty("diretor")]
        public string? Diretor { get; set; }

        [JsonProperty("salaId")]
        public int? SalaId { get; set; }
    }
}
