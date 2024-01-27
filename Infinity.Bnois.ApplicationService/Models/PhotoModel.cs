using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class PhotoModel
    {
        public long PhotoId { get; set; }
        public int PhotoType { get; set; }
        public int EmployeeId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FileExtension { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
