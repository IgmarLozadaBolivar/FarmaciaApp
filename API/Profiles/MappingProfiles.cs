using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pais,PaisDto>().ReverseMap();
            CreateMap<Pais,PaisxDepDto>().ReverseMap();

            CreateMap<Departamento,DepDto>().ReverseMap();
            CreateMap<Departamento,DepxCiuDto>().ReverseMap();

            CreateMap<Ciudad,CiuDto>().ReverseMap();
            CreateMap<Ciudad,CiuxPerDto>().ReverseMap();

            CreateMap<Persona,PerDto>().ReverseMap();
            CreateMap<Persona,PerxManyDto>().ReverseMap();

            CreateMap<Genero,GenDto>().ReverseMap();
            CreateMap<Genero,GenxPerDto>().ReverseMap();

            CreateMap<TipoPersona,TipoPerDto>().ReverseMap();
            CreateMap<TipoPersona,TipoPerxPerDto>().ReverseMap();
        }
    }
}