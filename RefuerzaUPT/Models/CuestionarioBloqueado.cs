namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CuestionarioBloqueado")]
    public partial class CuestionarioBloqueado
    {
        public int cuestionarioBloqueadoID { get; set; }

        public int cuestionarioDesbloquearID { get; set; }

        public int cuestionarioRequeridoID { get; set; }

        public virtual Cuestionario Cuestionario { get; set; }

        public virtual Cuestionario Cuestionario1 { get; set; }
    }
}
