using AutoMapper;
using Cinema.Application.DTOs;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Services
{
    public class SalaService : ISalaService
    {
        private ISalaRepository _salaRepository;
        private readonly IMapper _mapper;
        public SalaService(ISalaRepository salaRepository, IMapper mapper)
        {
            _salaRepository = salaRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<SalaDTO>> GetAll(int pagina, int tamanhoPagina)
        {
            try
            {
                var salas = await _salaRepository.GetAll(pagina, tamanhoPagina);
                return _mapper.Map<IEnumerable<SalaDTO>>(salas);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<SalaDTO> GetById(int? id)
        {
            try
            {
                var salaEntity = await _salaRepository.GetById(id);
                return _mapper.Map<SalaDTO>(salaEntity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<SalaDTO> Add(SalaDTO salaDto)
        {
            var salaEntity =  await _salaRepository.Create(_mapper.Map<Sala>(salaDto));
            return _mapper.Map<SalaDTO>(salaEntity);
        }

        public async Task<SalaDTO> Update(SalaDTO salaDto)
        {
            var entity = await _salaRepository.GetById(salaDto.Numero);
            if (entity != null)
            {
                var propertiesWithValue = salaDto.GetType().GetProperties()
                    .Where(prop => prop.GetValue(salaDto) != null);

                foreach (var prop in propertiesWithValue)
                {
                    entity.GetType().GetProperty(prop.Name)?.SetValue(entity, prop.GetValue(salaDto));
                }

                await _salaRepository.Update(entity);
                return _mapper.Map<SalaDTO>(entity);
            }
            return null;
        }

        public async Task<SalaDTO> Remove(int id)
        {
            var salaEntity = await _salaRepository.GetById(id);
            if(salaEntity != null){
                await _salaRepository.Remove(salaEntity);
                return _mapper.Map<SalaDTO>(salaEntity);
            }
            return null;
        }


    }
}
