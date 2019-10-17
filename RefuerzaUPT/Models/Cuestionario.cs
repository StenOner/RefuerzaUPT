namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Cuestionario")]
    public partial class Cuestionario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cuestionario()
        {
            CuestionarioBloqueado = new HashSet<CuestionarioBloqueado>();
            CuestionarioBloqueado1 = new HashSet<CuestionarioBloqueado>();
            IntentoCuestionario = new HashSet<IntentoCuestionario>();
            Pregunta = new HashSet<Pregunta>();
            Respuesta = new HashSet<Respuesta>();
        }

        public int cuestionarioID { get; set; }

        public int temaID { get; set; }

        public int duracion { get; set; }

        public int intentos { get; set; }

        public bool estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CuestionarioBloqueado> CuestionarioBloqueado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CuestionarioBloqueado> CuestionarioBloqueado1 { get; set; }

        public virtual Tema Tema { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntentoCuestionario> IntentoCuestionario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pregunta> Pregunta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }

        public List<Cuestionario> Listar()
        {
            var objCuestionario = new List<Cuestionario>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    objCuestionario = db.Cuestionario.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return objCuestionario;
        }

        public Cuestionario Obtener(int id)
        {
            var objCuestionario = new Cuestionario();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    objCuestionario = db.Cuestionario
                        .Where(x => x.cuestionarioID == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return objCuestionario;
        }

        public List<Cuestionario> ObtenerPorCurso(int id)
        {
            var objCuestionario = new List<Cuestionario>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    objCuestionario = db.Cuestionario.Include("Tema.Curso")
                              .Where(x => x.Tema.Curso.cursoID == id)
                              .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return objCuestionario;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    if (this.cuestionarioID > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Eliminar()
        {
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
