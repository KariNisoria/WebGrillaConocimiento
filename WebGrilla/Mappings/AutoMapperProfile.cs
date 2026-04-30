using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;

namespace WebGrilla.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ===== EVALUACION =====
            CreateMap<Evaluacion, EvaluacionDTO>()
                .ForMember(dest => dest.NombreRecurso, 
                    opt => opt.MapFrom(src => src.Recurso != null 
                        ? $"{src.Recurso.Nombre} {src.Recurso.Apellido}" 
                        : null))
                .ForMember(dest => dest.NombreGrilla, 
                    opt => opt.MapFrom(src => src.Grilla != null 
                        ? src.Grilla.Nombre 
                        : null));
            
            CreateMap<EvaluacionDTO, Evaluacion>()
                .ForMember(dest => dest.Recurso, opt => opt.Ignore())
                .ForMember(dest => dest.Grilla, opt => opt.Ignore())
                .ForMember(dest => dest.Resultados, opt => opt.Ignore())
                .ForMember(dest => dest.Conocimientos, opt => opt.Ignore());

            // ===== RECURSO =====
            CreateMap<Recurso, RecursoDTO>().ReverseMap();

            // ===== GRILLA =====
            CreateMap<Grilla, GrillaDTO>().ReverseMap();

            // ===== CONOCIMIENTO RECURSO =====
            CreateMap<ConocimientoRecurso, ConocimientoRecursoDTO>().ReverseMap();

            // ===== TEMA =====
            CreateMap<Tema, TemaDTO>().ReverseMap();

            // ===== SUBTEMA =====
            CreateMap<Subtema, SubtemaDTO>().ReverseMap();

            // ===== GRILLA TEMA =====
            CreateMap<GrillaTema, GrillaTemaDTO>().ReverseMap();

            // ===== GRILLA SUBTEMA =====
            CreateMap<GrillaSubtema, GrillaSubtemaDTO>().ReverseMap();

            // ===== ROL =====
            CreateMap<Rol, RolDTO>().ReverseMap();

            // ===== CLIENTE =====
            CreateMap<Cliente, ClienteDTO>().ReverseMap();

            // ===== EQUIPO DESARROLLO =====
            CreateMap<EquipoDesarrollo, EquipoDesarrolloDTO>().ReverseMap();

            // Agrega aquí otros mapeos según los necesites...
        }
    }
}