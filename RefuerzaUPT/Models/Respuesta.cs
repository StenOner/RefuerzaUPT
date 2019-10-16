namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
    }
}
