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
                        : null))
                .ForMember(dest => dest.NombreRecursoSupervisor,
                    opt => opt.MapFrom(src => src.RecursoSupervisor != null
                        ? $"{src.RecursoSupervisor.Nombre} {src.RecursoSupervisor.Apellido}"
                        : null));

            CreateMap<EvaluacionDTO, Evaluacion>()
                .ForMember(dest => dest.Recurso, opt => opt.Ignore())
                .ForMember(dest => dest.Grilla, opt => opt.Ignore())
                .ForMember(dest => dest.RecursoSupervisor, opt => opt.Ignore())
                .ForMember(dest => dest.Resultados, opt => opt.Ignore())
                .ForMember(dest => dest.Conocimientos, opt => opt.Ignore());

            // ===== RECURSO =====
            //CreateMap<Recurso, RecursoDTO>().ReverseMap();
            // configuracin ms completa:
            CreateMap<Recurso, RecursoDTO>()
                .ForMember(dest => dest.NombreEquipo,
                    opt => opt.MapFrom(src => src.EquipoDesarrollo != null ? src.EquipoDesarrollo.Nombre : null))
                .ForMember(dest => dest.NombreRol,
                    opt => opt.MapFrom(src => src.Rol != null ? src.Rol.Nombre : null))
                .ForMember(dest => dest.NombreTipoDocumento,
                    opt => opt.MapFrom(src => src.TipoDocumento != null ? src.TipoDocumento.Nombre : null));

            CreateMap<RecursoDTO, Recurso>()
                .ForMember(dest => dest.EquipoDesarrollo, opt => opt.Ignore())
                .ForMember(dest => dest.Rol, opt => opt.Ignore())
                .ForMember(dest => dest.TipoDocumento, opt => opt.Ignore())
                .ForMember(dest => dest.Resultados, opt => opt.Ignore())
                .ForMember(dest => dest.Conocimientos, opt => opt.Ignore())
                .ForMember(dest => dest.Evaluaciones, opt => opt.Ignore())
                .ForMember(dest => dest.RecursosSupervisados, opt => opt.Ignore())
                .ForMember(dest => dest.Supervisores, opt => opt.Ignore());
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

            // ===== TIPOS DE DOCUMENTO =====
            CreateMap<TipoDocumento, TipoDocumentoDTO>().ReverseMap();

            
        }
    }
}