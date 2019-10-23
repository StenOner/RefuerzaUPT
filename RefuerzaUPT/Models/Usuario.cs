namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Curso = new HashSet<Curso>();
            IntentoCuestionario = new HashSet<IntentoCuestionario>();
            MedallaUsuario = new HashSet<MedallaUsuario>();
            Respuesta = new HashSet<Respuesta>();
        }

        public int usuarioID { get; set; }

        public int tipoUsuarioID { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string codigo { get; set; }

        [Required]
        [StringLength(30)]
        public string correo { get; set; }

        [Required]
        [StringLength(30)]
        public string clave { get; set; }

        public bool estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Curso> Curso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntentoCuestionario> IntentoCuestionario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedallaUsuario> MedallaUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }

        public ResponseModel ValidarLogin(string user, string password)
        {
            var rm = new ResponseModel();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    var usuario = db.Usuario
                        .Where(x => x.nombre.Equals(user) &&
                                x.clave.Equals(password)).SingleOrDefault();

                    if (usuario != null)
                    {
                        if (usuario.estado)
                        {
                            SessionHelper.AddUserToSession(usuario.usuarioID.ToString());
                            rm.SetResponse(true);
                        }
                        else
                        {
                            rm.SetResponse(false, "Usuario desactivado, comuniquese con el administrador.");
                        }
                    }
                    else
                    {
                        rm.SetResponse(false, "Usuario o Contraseña incorrectos.");
                    }
                }
            }
            catch (Exception)
            {

            }
            return rm;
        }
    }
}
