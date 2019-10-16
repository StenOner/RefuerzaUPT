namespace RefuerzaUPT.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModeloCuestionario : DbContext
    {
        public ModeloCuestionario()
            : base("name=ModeloCuestionario")
        {
        }

        public virtual DbSet<Alternativa> Alternativa { get; set; }
        public virtual DbSet<Cuestionario> Cuestionario { get; set; }
        public virtual DbSet<CuestionarioBloqueado> CuestionarioBloqueado { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<IntentoCuestionario> IntentoCuestionario { get; set; }
        public virtual DbSet<Medalla> Medalla { get; set; }
        public virtual DbSet<MedallaUsuario> MedallaUsuario { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<Respuesta> Respuesta { get; set; }
        public virtual DbSet<Tema> Tema { get; set; }
        public virtual DbSet<TipoPregunta> TipoPregunta { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alternativa>()
                .Property(e => e.alternativa1)
                .IsUnicode(false);

            modelBuilder.Entity<Alternativa>()
                .HasMany(e => e.Respuesta)
                .WithRequired(e => e.Alternativa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cuestionario>()
                .HasMany(e => e.CuestionarioBloqueado)
                .WithRequired(e => e.Cuestionario)
                .HasForeignKey(e => e.cuestionarioDesbloquearID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cuestionario>()
                .HasMany(e => e.CuestionarioBloqueado1)
                .WithRequired(e => e.Cuestionario1)
                .HasForeignKey(e => e.cuestionarioRequeridoID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cuestionario>()
                .HasMany(e => e.IntentoCuestionario)
                .WithRequired(e => e.Cuestionario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cuestionario>()
                .HasMany(e => e.Pregunta)
                .WithRequired(e => e.Cuestionario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cuestionario>()
                .HasMany(e => e.Respuesta)
                .WithRequired(e => e.Cuestionario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.Tema)
                .WithRequired(e => e.Curso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IntentoCuestionario>()
                .HasMany(e => e.Respuesta)
                .WithRequired(e => e.IntentoCuestionario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medalla>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Medalla>()
                .Property(e => e.imagen)
                .IsUnicode(false);

            modelBuilder.Entity<Medalla>()
                .HasMany(e => e.MedallaUsuario)
                .WithRequired(e => e.Medalla)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pregunta>()
                .Property(e => e.pregunta1)
                .IsUnicode(false);

            modelBuilder.Entity<Pregunta>()
                .Property(e => e.imagen)
                .IsUnicode(false);

            modelBuilder.Entity<Pregunta>()
                .HasMany(e => e.Alternativa)
                .WithRequired(e => e.Pregunta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pregunta>()
                .HasMany(e => e.Respuesta)
                .WithRequired(e => e.Pregunta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tema>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Tema>()
                .HasMany(e => e.Cuestionario)
                .WithRequired(e => e.Tema)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoPregunta>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoPregunta>()
                .HasMany(e => e.Pregunta)
                .WithRequired(e => e.TipoPregunta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoUsuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoUsuario>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.TipoUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.clave)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.IntentoCuestionario)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.MedallaUsuario)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Respuesta)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);
        }
    }
}
