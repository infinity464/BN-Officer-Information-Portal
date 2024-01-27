using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public int RankCategoryId { get; set; }
        public int OfficerTypeId { get; set; }
        public int CountryId { get; set; }
        public string ReferenceId { get; set; }
        public string PNo { get; set; }
        public string BnNo { get; set; }
        public string FullNameEng { get; set; }
        public string Name { get; set; }

        public string FullNameBan { get; set; }
        public Nullable<int> BatchId { get; set; }
        public string BatchPosition { get; set; }
        public int GenderId { get; set; }
        public int RankId { get; set; }
        public int EmployeeStatusId { get; set; }
        public Nullable<int> SLCode { get; set; }
        public bool HasDollarSign { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> DateOfDollarSign { get; set; }
        public Nullable<int> TransferId { get; set; }
        public string Notification { get; set; }
        public Nullable<int> ExecutionRemarkId { get; set; }
        public Nullable<System.DateTime> BExecutionDate { get; set; }
        public string BSpRemarks { get; set; }
        public string BranchName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool Active { get; set; }

        public virtual BatchModel Batch { get; set; }
        public virtual CountryModel Country { get; set; }
        public virtual EmployeeStatusModel EmployeeStatus { get; set; }
        public virtual GenderModel Gender { get; set; }
        public virtual OfficerTypeModel OfficerType { get; set; }
        public virtual RankModel Rank { get; set; }
        public virtual RankCategoryModel RankCategory { get; set; }
        public virtual MaritalTypeModel MaritalType { get; set; }
        public virtual ExecutionRemarkModel ExecutionRemark { get; set; }
        public virtual ICollection<EmployeeGeneralModel> EmployeeGeneral { get; set; }
       

    }
}
