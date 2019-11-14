namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("MedallaUsuario")]
    public partial class MedallaUsuario
    {
        public int medallaUsuarioID { get; set; }

        public int medallaID { get; set; }

        public int usuarioID { get; set; }

        public bool estado { get; set; }

        public virtual Medalla Medalla { get; set; }

        public virtual Usuario Usuario { get; set; }

        public List<MedallaUsuario> ListarPorUsuario(int _id)
        {
            var listaMedallaUsuario = new List<MedallaUsuario>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaMedallaUsuario = db.MedallaUsuario
                        .Where(x => x.usuarioID == _id && x.estado == true)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaMedallaUsuario;
        }

        public MedallaUsuario Obtener(int _id)
        {
            var objetoMedallaUsuario = new MedallaUsuario();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    objetoMedallaUsuario = db.MedallaUsuario
                        .Where(x => x.medallaUsuarioID == _id && x.estado == true)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return objetoMedallaUsuario;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    if (this.medallaUsuarioID > 0)
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
