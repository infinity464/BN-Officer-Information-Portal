using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class EmployeeChildrenModel
    {
        public int EmployeeChildrenId { get; set; }
        public int EmployeeId { get; set; }
        public int ChildrenType { get; set; }
        public string ChildrenTypeName { get; set; }//extra field
        public Nullable<int> Child { get; set; }
        public string FileName { get; set; }
        public string GenFormName { get; set; }
        public string Name { get; set; }
        public string NameBan { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string LivingWith { get; set; }
        public string ContactAddress { get; set; }
        public Nullable<int> OccupationId { get; set; }
        public string OfficeAddress { get; set; }
        public bool IsDead { get; set; }
        public Nullable<System.DateTime> DeadDate { get; set; }
        public string DeadReason { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual OccupationModel Occupation { get; set; }
    }
}
