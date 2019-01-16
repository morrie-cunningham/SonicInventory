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

        public int project_id { get; set; }

        public int? subAssy_id { get; set; }

        [StringLength(50)]
        public string drawingNo { get; set; }

        [Required]
        [StringLength(1)]
        public string drawingType { get; set; }

        [Required]
        [StringLength(4)]
        public string detailNo { get; set; }

        [StringLength(50)]
        public string revNo { get; set; }

        [Required]
        public bool? wasModified { get; set; }

        public int? designer_id { get; set; }

        public int? fabricator_id { get; set; }

        [StringLength(255)]
        public string file { get; set; }
    }
}
