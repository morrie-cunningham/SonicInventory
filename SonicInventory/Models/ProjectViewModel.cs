using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SonicInventory.Models
{
    public class ProjectViewModel
    {
        public project Project { get; set; }

        public List<project> Projects { get; set; }

        public part Part { get; set; }

        public List<part> Parts { get; set; }

        public List<designer> DesignerList { get; set; }

        public List<fabricator> FabricatorList { get; set; }

        public List<status> StatusList { get; set; }

        public List<subAssembly> SubAssemblyList { get; set; }

        public List<drawingType> DrawingTypeList { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase PartFile { get; set; }
    }
}