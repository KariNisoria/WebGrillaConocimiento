using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.DTOs.MapaCalor;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class MapaCalorRepository : IMapaCalorRepository
    {
        private readonly AppDbContext _context;

        public MapaCalorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MapaCalorGrillaDTO?> GetGrillaAsync(int idGrilla)
        {
            return await _context.Grillas
                .AsNoTracking()
                .Where(x => x.IdGrilla == idGrilla)
                .Select(x => new MapaCalorGrillaDTO
                {
                    IdGrilla = x.IdGrilla,
                    NombreGrilla = x.Nombre
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<MapaCalorTemaDTO>> GetTemasAsync(int idGrilla)
        {
            return await (
                from gt in _context.GrillaTemas
                join t in _context.Temas
                    on gt.IdTema equals t.IdTema
                where gt.IdGrilla == idGrilla
                orderby t.Orden
                select new MapaCalorTemaDTO
                {
                    IdGrillaTema = gt.IdGrillaTema,
                    IdTema = t.IdTema,
                    NombreTema = t.Nombre,
                    Orden = t.Orden,
                    PonderacionTema = gt.Ponderacion < 0m ? 0m : gt.Ponderacion
                }
            )
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<List<MapaCalorRecursoDTO>> GetRecursosAsync(int idGrilla)
        {
            return await (
                from e in _context.Evaluacion

                join r in _context.Set<Recurso>()
                    on e.IdRecurso equals r.IdRecurso

                where e.IdGrilla == idGrilla

                orderby r.Apellido, r.Nombre

                select new MapaCalorRecursoDTO
                {
                    IdRecurso = r.IdRecurso,
                    IdEvaluacion = e.IdEvaluacion,
                    NombreRecurso = r.Nombre + " " + r.Apellido,
                    IdRol = r.IdRol
                }
            )
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<List<MapaCalorDetalleDTO>> GetDetalleAsync(int idGrilla)
        {
            return await (
                from gt in _context.GrillaTemas

                join gs in _context.GrillaSubtemas
                    on gt.IdGrillaTema equals gs.IdGrillaTema

                join cr in _context.ConocimientoRecurso
                    on gs.IdSubtema equals cr.IdSubtema

                where gt.IdGrilla == idGrilla
                      && cr.IdGrilla == idGrilla

                select new MapaCalorDetalleDTO
                {
                    IdGrillaTema = gt.IdGrillaTema,
                    IdTema = gt.IdTema,
                    IdSubtema = gs.IdSubtema,

                    IdRecurso = cr.IdRecurso,
                    IdEvaluacion = cr.IdEvaluacion,

                    PonderacionSubtema = gs.Ponderacion,

                    ValorFuncionalVerif = cr.ValorFuncionalVerif,
                    ValorTecnicoVerif = cr.ValorTecnicoVerif
                }
            )
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<List<MapaCalorValorDTO>> GetValoresAsync(int idGrilla)
        {
            var detalle =
                from gt in _context.GrillaTemas

                join gs in _context.GrillaSubtemas
                    on gt.IdGrillaTema equals gs.IdGrillaTema

                join cr in _context.ConocimientoRecurso
                    on gs.IdSubtema equals cr.IdSubtema

                where gt.IdGrilla == idGrilla
                   && cr.IdGrilla == idGrilla

                // Evitar ponderaciones negativas
                let ponderacion =
                    gs.Ponderacion < 0m ? 0m : gs.Ponderacion

                // Valores verificados: NULL o negativo = 0
                let funcionalVerif =
                    (decimal?)cr.ValorFuncionalVerif ?? 0m

                let tecnicoVerif =
                    (decimal?)cr.ValorTecnicoVerif ?? 0m

                let funcionalNormalizado =
                    funcionalVerif < 0m ? 0m : funcionalVerif

                let tecnicoNormalizado =
                    tecnicoVerif < 0m ? 0m : tecnicoVerif

                // Si existe el resultado guardado, utilizarlo.
                // Si es NULL, calcularlo desde los valores verificados.
                let funcional =
                    cr.ResultadoFuncionalPonderado
                    ?? (ponderacion * funcionalNormalizado / 5m)

                let tecnico =
                    cr.ResultadoTecnicoPonderado
                    ?? (ponderacion * tecnicoNormalizado / 5m)

                select new
                {
                    gt.IdGrillaTema,
                    gt.IdTema,
                    cr.IdRecurso,
                    cr.IdEvaluacion,

                    ResultadoFuncional = funcional < 0m ? 0m : funcional,
                    ResultadoTecnico = tecnico < 0m ? 0m : tecnico
                };

            var consulta = detalle
                .GroupBy(x => new
                {
                    x.IdGrillaTema,
                    x.IdTema,
                    x.IdRecurso,
                    x.IdEvaluacion
                })
                .Select(g => new MapaCalorValorDTO
                {
                    IdGrillaTema = g.Key.IdGrillaTema,
                    IdTema = g.Key.IdTema,
                    IdRecurso = g.Key.IdRecurso,
                    IdEvaluacion = g.Key.IdEvaluacion,

                    ResultadoFuncional = g.Sum(x => x.ResultadoFuncional),
                    ResultadoTecnico = g.Sum(x => x.ResultadoTecnico)
                });

            return await consulta.ToListAsync();
        }
    }
}
