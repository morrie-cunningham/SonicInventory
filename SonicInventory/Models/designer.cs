namespace SonicInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("allenda8_designSite.designers")]
    public partial class designer
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
