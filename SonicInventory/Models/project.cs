namespace SonicInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("allenda8_designSite.projects")]
    public partial class project
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [StringLength(255)]
        [Display(Name = "Description")]
        public string description { get; set; }
    }
}
