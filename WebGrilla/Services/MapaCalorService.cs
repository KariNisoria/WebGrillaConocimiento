using WebGrilla.DTOs.MapaCalor;
using WebGrilla.Repository;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public class MapaCalorService : IMapaCalorService
    {
        private readonly IMapaCalorRepository _repository;

        public MapaCalorService(IMapaCalorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<MapaCalorDTO>> GetMapaCalor(int idGrilla)
        {
            var grilla = await _repository.GetGrillaAsync(idGrilla);

            if (grilla == null)
            {
                return Result<MapaCalorDTO>.Failure(
                    "No se encontró la grilla."
                );
            }

            var temas = await _repository.GetTemasAsync(idGrilla);

            var recursos = await _repository.GetRecursosAsync(idGrilla);

            // SQL Server devuelve los resultados ya calculados y agrupados.
            var valoresSql = await _repository.GetValoresAsync(idGrilla);

            var indiceValores = valoresSql.ToDictionary(
                x => (
                    x.IdGrillaTema,
                    x.IdRecurso,
                    x.IdEvaluacion
                )
            );

            var valores = new List<MapaCalorValorDTO>();

            foreach (var tema in temas)
            {
                foreach (var recurso in recursos)
                {
                    var clave = (
                        tema.IdGrillaTema,
                        recurso.IdRecurso,
                        recurso.IdEvaluacion
                    );

                    if (indiceValores.TryGetValue(clave, out var valor))
                    {
                        valores.Add(valor);
                    }
                    else
                    {
                        valores.Add(new MapaCalorValorDTO
                        {
                            IdGrillaTema = tema.IdGrillaTema,
                            IdTema = tema.IdTema,
                            IdRecurso = recurso.IdRecurso,
                            IdEvaluacion = recurso.IdEvaluacion,
                            ResultadoFuncional = 0m,
                            ResultadoTecnico = 0m
                        });
                    }
                }
            }

            var mapa = new MapaCalorDTO
            {
                IdGrilla = grilla.IdGrilla,
                NombreGrilla = grilla.NombreGrilla,
                Temas = temas,
                Recursos = recursos,
                Valores = valores
            };

            return Result<MapaCalorDTO>.Success(mapa);
        }

        //public async Task<Result<MapaCalorDTO>> GetMapaCalor(int idGrilla)
        //{
        //    var grilla = await _repository.GetGrillaAsync(idGrilla);

        //    if (grilla == null)
        //    {
        //        return Result<MapaCalorDTO>.Failure(
        //            $"No se encontró la grilla {idGrilla}"
        //        );
        //    }
        //    var temas = await _repository.GetTemasAsync(idGrilla);

        //    var recursos = await _repository.GetRecursosAsync(idGrilla);

        //    var detalle = await _repository.GetDetalleAsync(idGrilla);
        //    // Validamos que los valores estén entre 0 y 5.
        //    var valorInvalido = detalle.FirstOrDefault(x =>
        //        (x.ValorFuncionalVerif.HasValue &&
        //         (x.ValorFuncionalVerif.Value < 0 ||
        //          x.ValorFuncionalVerif.Value > 5))
        //        ||
        //        (x.ValorTecnicoVerif.HasValue &&
        //         (x.ValorTecnicoVerif.Value < 0 ||
        //          x.ValorTecnicoVerif.Value > 5))
        //    );
        //    // Calculamos el resultado de cada Tema/Recurso/Evaluación.
        //    var valoresCalculados = detalle
        //        .GroupBy(x => new
        //        {
        //            x.IdGrillaTema,
        //            x.IdTema,
        //            x.IdRecurso,
        //            x.IdEvaluacion
        //        })
        //        .Select(g => new MapaCalorValorDTO
        //        {
        //            IdGrillaTema = g.Key.IdGrillaTema,
        //            IdTema = g.Key.IdTema,
        //            IdRecurso = g.Key.IdRecurso,
        //            IdEvaluacion = g.Key.IdEvaluacion,

        //            ResultadoFuncional = g.Sum(x =>
        //                x.PonderacionSubtema *
        //                (x.ValorFuncionalVerif ?? 0m) /
        //                5m
        //            ),

        //            ResultadoTecnico = g.Sum(x =>
        //                x.PonderacionSubtema *
        //                (x.ValorTecnicoVerif ?? 0m) /
        //                5m
        //            )
        //        })
        //        .ToList();
        //    // Se arma un diccionario para encontrar rápidamente
        //    // Tema + Recurso + Evaluación.
        //    var diccionario = valoresCalculados.ToDictionary(
        //        x => (
        //            x.IdGrillaTema,
        //            x.IdRecurso,
        //            x.IdEvaluacion
        //        )
        //    );
        //    var valoresFinales = new List<MapaCalorValorDTO>();

        //    foreach (var tema in temas)
        //    {
        //        foreach (var recurso in recursos)
        //        {
        //            var clave = (
        //                tema.IdGrillaTema,
        //                recurso.IdRecurso,
        //                recurso.IdEvaluacion
        //            );

        //            if (diccionario.TryGetValue(clave, out var valor))
        //            {
        //                valoresFinales.Add(valor);
        //            }
        //            else
        //            {
        //                valoresFinales.Add(
        //                    new MapaCalorValorDTO
        //                    {
        //                        IdGrillaTema = tema.IdGrillaTema,
        //                        IdTema = tema.IdTema,

        //                        IdRecurso = recurso.IdRecurso,
        //                        IdEvaluacion = recurso.IdEvaluacion,

        //                        ResultadoFuncional = 0m,
        //                        ResultadoTecnico = 0m
        //                    }
        //                );
        //            }
        //        }
        //    }
        //    var resultado = new MapaCalorDTO
        //    {
        //        IdGrilla = grilla.IdGrilla,
        //        NombreGrilla = grilla.NombreGrilla,

        //        Temas = temas,
        //        Recursos = recursos,
        //        Valores = valoresFinales
        //    };


        //    return Result<MapaCalorDTO>.Success(resultado);
        //}
    }
}
