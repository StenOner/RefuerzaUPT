namespace RefuerzaUPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MedallaUsuario")]
    public partial class MedallaUsuario
    {
        public int medallaUsuarioID { get; set; }

        public int medallaID { get; set; }

        public int usuarioID { get; set; }

        public bool estado { get; set; }

        public virtual Medalla Medalla { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
