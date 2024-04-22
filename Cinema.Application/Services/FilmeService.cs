using AutoMapper;
using Cinema.Application.DTOs;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Services
{
    public class FilmeService : IFilmeService
    {
        private IFilmeRepository _filmeRepository;
        private readonly IMapper _mapper;
        public FilmeService(IFilmeRepository filmeRepository, IMapper mapper)
        {
            _filmeRepository = filmeRepository;
            _mapper = mapper;
        }

        public async Task<FilmeDTO> Add(FilmeRequestDTO filmeDto)
        {
            var filmeEntity = await _filmeRepository.Create(_mapper.Map<Filme>(filmeDto));
            return _mapper.Map<FilmeDTO>(filmeEntity);
        }


        public async Task<FilmeDTO> Update(int id, FilmeRequestDTO filmeDto)
        {
            var entity = await _filmeRepository.GetById(id);
            if (entity != null)
            {

                var propertiesWithValue = filmeDto.GetType().GetProperties()
                    .Where(prop => prop.GetValue(filmeDto) != null);

                foreach (var prop in propertiesWithValue)
                {
                    entity.GetType().GetProperty(prop.Name)?.SetValue(entity, prop.GetValue(filmeDto));
                }

                await _filmeRepository.Update(entity);
                return _mapper.Map<FilmeDTO>(entity);
            }
            return null;
        }

        public async Task<IEnumerable<FilmeDTO>> GetAll()
        {
            try
            {
                var entity = await _filmeRepository.GetFilmes();
                return _mapper.Map<IEnumerable<FilmeDTO>>(entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<FilmeDTO> GetById(int? id)
        {
            try
            {
                var entity = await _filmeRepository.GetById(id);
                return _mapper.Map<FilmeDTO>(entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<FilmeDTO> Remove(int id)
        {
            var entity = await _filmeRepository.GetById(id);
            if (entity != null)
            {
                await _filmeRepository.Remove(entity);
                return _mapper.Map<FilmeDTO>(entity);
            }
            return null;
        }
    }
}
