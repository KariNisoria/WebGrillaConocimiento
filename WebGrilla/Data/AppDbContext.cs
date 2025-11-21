using Microsoft.EntityFrameworkCore;
using WebGrilla.Models;

namespace WebGrilla.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        // DBSets del modelo
        // Inicial
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EquipoDesarrollo> Equipos { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
        // Primera
        public DbSet<Rol> Roles { get; set; }
        // Segunda
        public DbSet<Tema> Temas { get; set; }
        public DbSet<Subtema>  Subtemas { get; set; } 
        // Tercera
        public DbSet<Grilla> Grillas { get; set; }
        public DbSet<GrillaTema> GrillaTemas { get; set; }
        public DbSet<GrillaSubtema> GrillaSubtemas { get; set; }
        // Cuarta - Evaluaciones y Conocimientos
        public DbSet<Evaluacion> Evaluacion { get; set; }
        public DbSet<ConocimientoRecurso> ConocimientoRecurso { get; set; }
        // Sexta
        public DbSet<TipoDocumento> TiposDocumentos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*----------------------------------*/
            /*Inicial + Cliente, Equipo, Recurso*/
            /*----------------------------------*/

            modelBuilder.Entity<Cliente>(entity =>
            {
                //Inicial

                entity.HasKey(x => x.IdCliente);
                //Agregar requeridos, longitudes maximas, indices, relaciones (solo M:N)
                entity.Property(x => x.Nombre).IsRequired().HasMaxLength(100); 
            });

            modelBuilder.Entity<EquipoDesarrollo>(entity =>
            {
                entity.HasKey(x => x.IdEquipoDesarrollo);
                //Agregar requeridos, longitudes maximas, indices, relaciones (solo M:N)
                entity.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<EquipoDesarrollo>()
                .HasOne(x => x.Cliente)
                .WithMany(x => x.Equipos)
                .HasForeignKey(x => x.IdCliente);

            modelBuilder.Entity<Recurso>(entity =>
            {
                entity.HasKey(x => x.IdRecurso);
                //Agregar requeridos, longitudes maximas, indices, relaciones (solo M:N)
                entity.Property(x => x.Nombre).IsRequired().HasMaxLength(100); ;
                entity.Property(x => x.Apellido).IsRequired().HasMaxLength(100); 
                entity.Property(x => x.NumeroDocumento) .IsRequired();
            });

            modelBuilder.Entity<Recurso>()
                .HasOne(x => x.EquipoDesarrollo)
                .WithMany(x => x.Recursos)
                .HasForeignKey(x => x.IdEquipoDesarrollo);

            /*Primera + Rol*/

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(x => x.IdRol);
                //Agregar requeridos, longitudes maximas, indices, relaciones (solo M:N)
                entity.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Recurso>()
                .HasOne(x => x.Rol)
                .WithMany(x => x.Recursos)
                .HasForeignKey(x => x.IdRol);

            /*------------------------*/
            /*Segunda + Tema y SubTema*/
            /*------------------------*/

            modelBuilder.Entity<Tema>(entity =>
            {
                entity.HasKey(x => x.IdTema);
                //Agregar requeridos, longitudes maximas, indices, relaciones (solo M:N)
                entity.Property(x => x.Nombre).IsRequired().HasMaxLength(100); 
            });

            modelBuilder.Entity<Subtema>(entity =>
            {
                entity.HasKey(x => x.IdSubtema);
                //Agregar requeridos, longitudes maximas, indices, relaciones (solo M:N)
                entity.Property(x => x.Nombre).IsRequired().HasMaxLength(100); 
            });

            modelBuilder.Entity<Subtema>()
                .HasOne(x => x.Tema)
                .WithMany(x => x.Subtemas)
                .HasForeignKey(x => x.IdTema)
                .OnDelete(DeleteBehavior.NoAction);

            /*--------------------------------------------*/
            /*Tercera + Grilla, GrillaTema y GrillaSubtema*/
            /*--------------------------------------------*/

            modelBuilder.Entity<Grilla>(entity =>
            {
                entity.HasKey(x => x.IdGrilla);
                //Agregar requeridos, longitudes maximas, indices, relaciones (solo M:N)
                entity.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(x=>x.Descripcion).HasMaxLength(100);
            });

            modelBuilder.Entity<GrillaTema>(entity =>
            {
                entity.HasKey(x => x.IdGrillaTema);
                entity.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Ponderacion).IsRequired().HasPrecision(5,2);
            });

            modelBuilder.Entity<GrillaTema>()
                .HasOne(x => x.Grilla)
                .WithMany(x => x.GrillaTemas)
                .HasForeignKey(x => x.IdGrilla);

            modelBuilder.Entity<GrillaTema>()
                .HasOne(x => x.Tema)
                .WithMany(x => x.GrillaTemas);

            modelBuilder.Entity<GrillaSubtema>(entity =>
            {
                entity.Property("Ponderacion")
                .IsRequired()
                .HasPrecision(5, 2);
            });

            /*Cuarta + Evaluacion, ResultadoConocmiento, ConocimientoRecurso*/

            modelBuilder.Entity<Evaluacion>(entity =>
            {
                entity.HasKey(x => x.IdEvaluacion);
                entity.Property("Descripcion").IsRequired().HasMaxLength(100);
                entity.Property("FechaInicio").IsRequired();
                entity.Property("FechaFin").IsRequired();
                entity.Property("IdRecurso").IsRequired();
                entity.Property("IdGrilla").IsRequired();
            });

            modelBuilder.Entity<Evaluacion>()
                .HasOne(x => x.Recurso)
                .WithMany(x => x.Evaluaciones)
                .HasForeignKey(x => x.IdRecurso);

            modelBuilder.Entity<Evaluacion>()
                .HasOne(x => x.Grilla)
                .WithMany(x => x.Evaluaciones)
                .HasForeignKey(x => x.IdGrilla);

            modelBuilder.Entity<ResultadoConocimiento>(entity =>
            {
                entity.HasKey(x => x.IdResultadoConocimiento);
                entity.Property("ValorFuncional").IsRequired().HasPrecision(5, 2);
                entity.Property("ValorTecnico").IsRequired().HasPrecision(5, 2);
            });

            modelBuilder.Entity<ResultadoConocimiento>()
                .HasOne(x => x.Grilla)
                .WithMany(x => x.Resultados)
                .HasForeignKey(x => x.IdGrilla);

            modelBuilder.Entity<ResultadoConocimiento>()
                .HasOne(x=>x.Recurso)
                .WithMany(x=>x.Resultados)
                .HasForeignKey(x=>x.IdRecurso);

            modelBuilder.Entity<ResultadoConocimiento>()
                .HasOne(x => x.Evaluacion)
                .WithMany(x => x.Resultados)
                .HasForeignKey(x => x.IdEvaluacion);

            modelBuilder.Entity<ResultadoConocimiento>()
                .HasOne(x => x.GrillaSubtema)
                .WithMany(x => x.Resultados)
                .HasForeignKey(x => x.IdSubtema)
                .OnDelete(DeleteBehavior.NoAction); ;

            modelBuilder.Entity<ConocimientoRecurso>(entity =>
            {
                entity.HasKey(x=>x.IdConocimientoRecurso);
                entity.Property("ValorFuncional").IsRequired();
                entity.Property("ValorTecnico").IsRequired();
                entity.Property("ValorFuncionalVerif");
                entity.Property("ValorTecnicoVerif");
            });

            modelBuilder.Entity<ConocimientoRecurso>()
                .HasOne(x => x.Recurso)
                .WithMany(x => x.Conocimientos)
                .HasForeignKey(x => x.IdRecurso);

            modelBuilder.Entity<ConocimientoRecurso>()
                .HasOne(x => x.Evaluacion)
                .WithMany(x => x.Conocimientos)
                .HasForeignKey(x => x.IdEvaluacion);

            modelBuilder.Entity<ConocimientoRecurso>()
                .HasOne(x => x.Grilla)
                .WithMany(x => x.Conocimientos)
                .HasForeignKey(x => x.IdGrilla);

            /*Quinta - Cambio de tipo de dato de Nombre.Rol a string*/

            /*Sexta - Cambio de nombre a TipoDocumento + Armado de tabla TipoDocumento*/

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(x => x.IdTipoDocumento);
                //Agregar requeridos, longitudes maximas, indices, relaciones (solo M:N)
                entity.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<Recurso>()
                .HasOne(x => x.TipoDocumento)
                .WithMany(x => x.Recursos)
                .HasForeignKey(x => x.IdTipoDocumento);


            base.OnModelCreating(modelBuilder);
        }
    }
}
