namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
        public string pregunta1 { get; set; }

        [StringLength(200)]
        public string imagen { get; set; }

        public bool estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alternativa> Alternativa { get; set; }

        public virtual Cuestionario Cuestionario { get; set; }

        public virtual TipoPregunta TipoPregunta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
}
