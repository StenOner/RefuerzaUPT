namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Curso")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            Tema = new HashSet<Tema>();
        }

        public int cursoID { get; set; }

        public int? usuarioID { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        public bool estado { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tema> Tema { get; set; }

        public List<Curso> ListarMisCursos(int _id)
        {
            var listaCurso = new List<Curso>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaCurso = db.Curso
                        .Where(x => x.usuarioID == _id && x.estado == true)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaCurso;
        }

        public List<Curso> Listar()
        {
            var listaCurso = new List<Curso>();
            try
            {
                using (var db = new ModeloCuestionario())
                {
                    listaCurso = db.Curso
                        .Where(x => x.estado == true)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaCurso;
        }
    }
}
