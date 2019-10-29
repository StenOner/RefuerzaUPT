namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Pregunta")]
    public partial class Pregunta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pregunta()
        {
            Alternativa = new HashSet<Alternativa>();
            Respuesta = new HashSet<Respuesta>();
        }

        public int preguntaID { get; set; }

        public int tipoPreguntaID { get; set; }

        public int cuestionarioID { get; set; }

        [Column("pregunta")]
        [Required]
        [StringLength(200)]
        public string enunciado { get; set; }

        [StringLength(200)]
        public string imagen { get; set; }

        public bool estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alternativa> Alternativa { get; set; }

        public virtual Cuestionario Cuestionario { get; set; }

        public virtual TipoPregunta TipoPregunta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }

        public List<Pregunta> ListarPorCuestionario(int _id)
        {
            var listaPregunta = new List<Pregunta>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaPregunta = db.Pregunta
                        .Where(x => x.cuestionarioID == _id)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaPregunta;
        }

        public Pregunta Obtener(int _id)
        {
            var listaPregunta = new Pregunta();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaPregunta = db.Pregunta
                        .Where(x => x.preguntaID == _id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaPregunta;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    if (this.preguntaID > 0)
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
