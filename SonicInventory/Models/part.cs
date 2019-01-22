namespace SonicInventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("allenda8_designSite.parts")]
    public partial class part
    {
        public int id { get; set; }

        [Display(Name = "Project")]
        public int project_id { get; set; }

        [Display(Name = "Sub Assembly")]
        public int? subAssy_id { get; set; }

        [Display(Name = "Drawing #")]
        [StringLength(50)]
        public string drawingNo { get; set; }

        [Required]
        [Display(Name = "Drawing Type")]
        public int drawingType { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Detail #")]
        public string detailNo { get; set; }

        [StringLength(50)]
        [Display(Name = "Rev #")]
        public string revNo { get; set; }

        [Required]
        public bool wasModified { get; set; }

        [Display(Name = "Designer")]
        public int? designer_id { get; set; }

        [Display(Name = "Fabricator")]
        public int? fabricator_id { get; set; }

        [StringLength(255)]
        [Display(Name = "File")]
        public string file { get; set; }
    }
}
