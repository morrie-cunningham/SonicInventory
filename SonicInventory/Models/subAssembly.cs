namespace SonicInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("allenda8_designSite.subAssembly")]
    public partial class subAssembly
    {
        public int id { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

        [StringLength(2)]
        public string Abbreviation { get; set; }
    }
}
