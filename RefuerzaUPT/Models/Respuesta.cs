namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Respuesta")]
    public partial class Respuesta
    {
        public int respuestaID { get; set; }

        public int cuestionarioID { get; set; }

        public int intentoCuestionarioID { get; set; }

        public int preguntaID { get; set; }

        public int alternativaID { get; set; }

        public int usuarioID { get; set; }

        public bool estado { get; set; }

        public virtual Alternativa Alternativa { get; set; }

        public virtual Cuestionario Cuestionario { get; set; }

        public virtual IntentoCuestionario IntentoCuestionario { get; set; }

        public virtual Pregunta Pregunta { get; set; }

        public virtual Usuario Usuario { get; set; }

        public List<Respuesta> ListarPorIntentoCuestionario(int _id)
        {
            var listaRespuesta = new List<Respuesta>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaRespuesta = db.Respuesta
                        .Where(x => x.intentoCuestionarioID == _id)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaRespuesta;
        }

        public List<Respuesta> ListarPorPregunta(int _id)
        {
            var listaRespuesta = new List<Respuesta>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaRespuesta = db.Respuesta
                        .Where(x => x.preguntaID == _id)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaRespuesta;
        }

        public CuestionarioBloqueado Obtener(int _id)
        {
            var objetoCuestionarioBloqueado = new CuestionarioBloqueado();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    objetoCuestionarioBloqueado = db.CuestionarioBloqueado
                        .Where(x => x.cuestionarioBloqueadoID == _id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return objetoCuestionarioBloqueado;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    if (this.respuestaID > 0)
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
