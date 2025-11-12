using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;

namespace WebGrilla.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tema, TemaDTO>().ReverseMap();
            CreateMap<Subtema, SubtemaDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<TipoDocumento, TipoDocumentoDTO>().ReverseMap();
            CreateMap<Recurso, RecursoDTO>().ReverseMap();
            CreateMap<EquipoDesarrollo, EquipoDesarrolloDTO>().ReverseMap();
            CreateMap<Grilla, GrillaDTO>().ReverseMap();
            CreateMap<GrillaTema, GrillaTemaDTO>().ReverseMap();
        }

    }
}
