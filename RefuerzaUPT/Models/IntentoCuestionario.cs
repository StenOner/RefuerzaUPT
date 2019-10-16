namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        public int nota { get; set; }

        public int tiempoResolucion { get; set; }

        public bool estado { get; set; }

        public virtual Cuestionario Cuestionario { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
}
