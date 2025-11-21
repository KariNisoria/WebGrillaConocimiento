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
            CreateMap<GrillaSubtema, GrillaSubtemaDTO>().ReverseMap();
            
            // Mapeos para evaluaciones y conocimientos
            CreateMap<Evaluacion, EvaluacionDTO>().ReverseMap();
            CreateMap<ConocimientoRecurso, ConocimientoRecursoDTO>()
                .ForMember(dest => dest.NombreRecurso, opt => opt.MapFrom(src => src.Recurso.Nombre + " " + src.Recurso.Apellido))
                .ForMember(dest => dest.NombreSubtema, opt => opt.MapFrom(src => src.Subtema.Nombre))
                .ForMember(dest => dest.NombreTema, opt => opt.MapFrom(src => src.Subtema.Tema.Nombre))
                .ReverseMap();
        }
    }
}
