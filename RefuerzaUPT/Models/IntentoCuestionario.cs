namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("IntentoCuestionario")]
    public partial class IntentoCuestionario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntentoCuestionario()
        {
            Respuesta = new HashSet<Respuesta>();
        }

        public int intentoCuestionarioID { get; set; }

        public int cuestionarioID { get; set; }

        public int usuarioID { get; set; }

        public double nota { get; set; }

        public int tiempoResolucion { get; set; }

        public bool estado { get; set; }

        public virtual Cuestionario Cuestionario { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }

        public List<IntentoCuestionario> Listar(int _id)
        {
            var listaIntentoCuestionario = new List<IntentoCuestionario>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaIntentoCuestionario = db.IntentoCuestionario
                        .Include("Respuesta")
                        .Where(x => x.intentoCuestionarioID == _id && x.estado == true)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaIntentoCuestionario;
        }

        public List<IntentoCuestionario> ListarPorCuestionario(int _id)
        {
            var listaIntentoCuestionario = new List<IntentoCuestionario>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaIntentoCuestionario = db.IntentoCuestionario
                        .Include("Respuesta")
                        .Where(x => x.cuestionarioID == _id && x.estado == true)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaIntentoCuestionario;
        }

        public List<IntentoCuestionario> ListarPorUsuario(int _id)
        {
            var listaIntentoCuestionario = new List<IntentoCuestionario>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaIntentoCuestionario = db.IntentoCuestionario
                        .Include("Respuesta")
                        .Where(x => x.usuarioID == _id && x.estado == true)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaIntentoCuestionario;
        }

        public IntentoCuestionario Obtener(int _id)
        {
            var objetoIntentoCuestionario = new IntentoCuestionario();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    objetoIntentoCuestionario = db.IntentoCuestionario
                        .Include("Respuesta")
                        .Where(x => x.intentoCuestionarioID == _id && x.estado == true)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return objetoIntentoCuestionario;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    if (this.intentoCuestionarioID > 0)
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
