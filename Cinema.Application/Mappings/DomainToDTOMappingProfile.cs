using Cinema.Application.DTOs;
using Cinema.Domain.Entities;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile() {
            CreateMap<Sala, SalaDTO>().ReverseMap();
            CreateMap<SalaDTO, Sala>().ReverseMap();
            CreateMap<Filme, FilmeDTO>().ReverseMap();
            CreateMap<Filme, FilmeRequestDTO>().ReverseMap();
        }
        
    }
}
