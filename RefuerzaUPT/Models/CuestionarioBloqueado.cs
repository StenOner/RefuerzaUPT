namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CuestionarioBloqueado")]
    public partial class CuestionarioBloqueado
    {
        public int cuestionarioBloqueadoID { get; set; }

        public int cuestionarioDesbloquearID { get; set; }

        public int cuestionarioRequeridoID { get; set; }

        public virtual Cuestionario Cuestionario1 { get; set; }

        public virtual Cuestionario Cuestionario2 { get; set; }

        public CuestionarioBloqueado ObtenerCuestionarioDesbloquear(int _id)
        {
            var cuestionarioBloqueado = new CuestionarioBloqueado();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    cuestionarioBloqueado = db.CuestionarioBloqueado
                        .Where(x => x.cuestionarioDesbloquearID == _id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            if (cuestionarioBloqueado == null)
                return new CuestionarioBloqueado();
            else
                return cuestionarioBloqueado;
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
                    if (this.cuestionarioBloqueadoID > 0)
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
