namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
        public string alternativa1 { get; set; }

        public bool respuestaCorrecta { get; set; }

        public bool estado { get; set; }

        public virtual Pregunta Pregunta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
}
