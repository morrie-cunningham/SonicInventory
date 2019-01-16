namespace SonicInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("allenda8_designSite.fabricators")]
    public partial class fabricator
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
