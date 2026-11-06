using ClosedXML.Excel;
using WebGrilla.DTOs.MapaCalor;
using Microsoft.EntityFrameworkCore;
using WebGrilla.Models;
using WebGrilla.Data;

namespace WebGrilla.Services
{
    public class MapaCalorExcelService
    {
        private readonly AppDbContext _context;

        public MapaCalorExcelService(AppDbContext context)
        {
            _context = context;
        }
        public byte[] GenerarExcel(MapaCalorDTO mapa, int? idRol, string nombreRol)
        {
            var recursos = mapa.Recursos
                .Where(r => !idRol.HasValue || r.IdRol == idRol.Value)
                .ToList();
            // Índice para buscar los valores sin recorrer
            // toda la colección por cada celda.
            var valores = mapa.Valores.ToDictionary(
                x => (x.IdGrillaTema, x.IdRecurso, x.IdEvaluacion)
            );
            using var libro = new XLWorkbook();
            CrearHoja(libro, mapa, recursos, valores, nombreRol, false);
            CrearHoja(libro, mapa, recursos, valores, nombreRol, true);
            using var stream = new MemoryStream();
            libro.SaveAs(stream);
            return stream.ToArray();
        }
        private static void CrearHoja(
            XLWorkbook libro,
            MapaCalorDTO mapa,
            List<MapaCalorRecursoDTO> recursos,
            Dictionary<(int, int, int), MapaCalorValorDTO> valores,
            string nombreRol,
            bool tecnico)
        {
            string nombreHoja = tecnico ? "Tecnico" : "Funcional";
            var hoja = libro.Worksheets.Add(nombreHoja);
            int ultimaColumna = Math.Max(2, recursos.Count + 1);
            int filaEncabezado = 4;
            int fila = 5;
            // Título e información de filtros
            hoja.Cell(1, 1).Value = $"Mapa de calor - {nombreHoja}";
            hoja.Range(1, 1, 1, ultimaColumna).Merge();
            hoja.Cell(2, 1).Value = $"Grilla: {mapa.NombreGrilla}";
            hoja.Range(2, 1, 2, ultimaColumna).Merge();
            hoja.Cell(3, 1).Value = $"Rol: {nombreRol}";
            hoja.Range(3, 1, 3, ultimaColumna).Merge();
            // Encabezados
            hoja.Cell(filaEncabezado, 1).Value = "Tema";
            for (int i = 0; i < recursos.Count; i++)
            {
                hoja.Cell(filaEncabezado, i + 2).Value =
                    recursos[i].NombreRecurso;
            }
            if (recursos.Count == 0)
            {
                hoja.Cell(filaEncabezado, 2).Value =
                    "Sin recursos para el filtro seleccionado";
            }
            // Temas y valores
            foreach (var tema in mapa.Temas.OrderBy(x => x.Orden))
            {
                hoja.Cell(fila, 1).Value = tema.NombreTema;
                for (int i = 0; i < recursos.Count; i++)
                {
                    var recurso = recursos[i];
                    valores.TryGetValue(
                        (tema.IdGrillaTema,
                         recurso.IdRecurso,
                         recurso.IdEvaluacion),
                        out var valor
                    );
                    decimal resultado = 0m;
                    if (valor != null)
                    {
                        resultado = tecnico
                            ? valor.ResultadoTecnico
                            : valor.ResultadoFuncional;
                    }
                    decimal porcentaje = tema.PonderacionTema > 0
                        ? resultado / tema.PonderacionTema
                        : 0m;
                    var celda = hoja.Cell(fila, i + 2);
                    // Excel almacena 0.2299 para mostrar 22.99%
                    celda.Value = (double)porcentaje;
                    celda.Style.NumberFormat.Format = "0.00%";
                    celda.Style.Fill.BackgroundColor =
                        ObtenerColor(porcentaje * 100m);
                    celda.Style.Alignment.Horizontal =
                        XLAlignmentHorizontalValues.Center;
                }
                hoja.Row(fila).Height = 32;
                fila++;
            }
            // Estilos de título
            hoja.Cell(1, 1).Style.Font.Bold = true;
            hoja.Cell(1, 1).Style.Font.FontSize = 16;
            // Encabezados
            var encabezado = hoja.Range(
                filaEncabezado, 1, filaEncabezado, ultimaColumna
            );
            encabezado.Style.Fill.BackgroundColor =
                XLColor.FromHtml("#1F4E78");
            encabezado.Style.Font.FontColor = XLColor.White;
            encabezado.Style.Font.Bold = true;
            encabezado.Style.Alignment.WrapText = true;
            encabezado.Style.Alignment.Horizontal =
                XLAlignmentHorizontalValues.Center;
            encabezado.Style.Alignment.Vertical =
                XLAlignmentVerticalValues.Center;
            hoja.Row(filaEncabezado).Height = 48;
            // Anchos
            hoja.Column(1).Width = 32;
            for (int i = 2; i <= ultimaColumna; i++)
            {
                hoja.Column(i).Width = 24;
            }
            // Bordes
            var tabla = hoja.Range(
                filaEncabezado, 1, Math.Max(fila - 1, filaEncabezado),
                ultimaColumna
            );
            tabla.Style.Border.OutsideBorder =
                XLBorderStyleValues.Thin;
            tabla.Style.Border.InsideBorder =
                XLBorderStyleValues.Thin;
            // Congelar encabezados y primera columna
            hoja.SheetView.Freeze(4, 1);
        }
        private static XLColor ObtenerColor(decimal porcentaje)
        {
            if (porcentaje <= 10m)
                return XLColor.FromHtml("#B9D9F5");

            if (porcentaje <= 20m)
                return XLColor.FromHtml("#A5C9F0");

            if (porcentaje <= 30m)
                return XLColor.FromHtml("#9CDCEB");

            if (porcentaje <= 40m)
                return XLColor.FromHtml("#96DDD5");

            if (porcentaje <= 50m)
                return XLColor.FromHtml("#ADE2BA");

            if (porcentaje <= 60m)
                return XLColor.FromHtml("#CFE59C");

            if (porcentaje <= 70m)
                return XLColor.FromHtml("#F6E89A");

            if (porcentaje <= 80m)
                return XLColor.FromHtml("#F8C789");

            if (porcentaje <= 90m)
                return XLColor.FromHtml("#F3A182");

            return XLColor.FromHtml("#E97878");
        }
        public async Task<string> ObtenerNombreRolAsync(int? idRol)
        {
            if (!idRol.HasValue || idRol.Value == 0)
                return "Todos los roles";

            var nombreRol = await _context.Set<Rol>()
                .AsNoTracking()
                .Where(r => r.IdRol == idRol.Value)
                .Select(r => r.Nombre)
                .FirstOrDefaultAsync();

            return nombreRol
                ?? throw new InvalidOperationException(
                    $"No existe el rol con ID {idRol.Value}."
                );
        }
    }
}
