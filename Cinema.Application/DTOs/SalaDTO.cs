using Cinema.Domain.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Application.DTOs
{
    public class SalaDTO
    {
        [JsonProperty("numero")]
        public int Numero { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("filmes")]
        public IEnumerable<FilmeDTO> Filmes { get; set; }
    }

    public class SalaRequestDTO
    {
        [JsonProperty("descricao")]
        public string Descricao { get; set; }
    }
}
