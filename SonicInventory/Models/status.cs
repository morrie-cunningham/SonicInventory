namespace SonicInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("allenda8_designSite.status")]
    public partial class status
    {
        public int id { get; set; }

        [Column("Status")]
        [Required]
        [StringLength(45)]
        public string Status { get; set; }
    }
}
