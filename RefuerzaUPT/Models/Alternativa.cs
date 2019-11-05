namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Alternativa")]
    public partial class Alternativa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alternativa()
        {
            Respuesta = new HashSet<Respuesta>();
        }

        public int alternativaID { get; set; }

        public int preguntaID { get; set; }

        [Column("alternativa")]
        [Required]
        [StringLength(200)]
        public string enunciadoAlternativa { get; set; }

        public bool respuestaCorrecta { get; set; }

        public bool estado { get; set; }

        public virtual Pregunta Pregunta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }

        public List<Alternativa> ListarPorPregunta(int _id)
        {
            var listaAlternativa = new List<Alternativa>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaAlternativa = db.Alternativa
                        .Include("Pregunta")
                        .Where(x => x.preguntaID == _id)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaAlternativa;
        }

        public Alternativa Obtener(int _id)
        {
            var objetoAlternativa = new Alternativa();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    objetoAlternativa = db.Alternativa
                        .Include("Pregunta")
                        .Where(x => x.alternativaID == _id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return objetoAlternativa;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    if (this.alternativaID > 0)
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
