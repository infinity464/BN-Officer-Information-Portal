using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class EducationModel
    {
        public long EducationId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> ExamCategoryId { get; set; }
        public Nullable<int> ExaminationId { get; set; }
        public Nullable<int> BoardId { get; set; }
        public Nullable<long> InstituteId { get; set; }
        public Nullable<int> Subjectid { get; set; }
        public string Roll { get; set; }
        public string RegNo { get; set; }
        public Nullable<int> ResultId { get; set; }
        public Nullable<int> ResultGradeId { get; set; }
        public Nullable<double> Gpa { get; set; }
        public Nullable<double> Marks { get; set; }
        public string Distiction { get; set; }
        public string PassingYear { get; set; }
        public string CourseDuration { get; set; }
        public string OtherInstituteName { get; set; }
        public string OtherSubjectName { get; set; }
        public System.DateTime ResultPublishDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ResultText { get; set; }
        public Nullable<double> Percentage { get; set; }
        public bool IsVarified { get; set; }
        public string FileName { get; set; }

        public virtual BoardModel Board { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual ExamCategoryModel ExamCategory { get; set; }
        public virtual ExaminationModel Examination { get; set; }
        public virtual InstituteModel Institute { get; set; }
        public virtual ResultModel Result { get; set; }
        public virtual ResultGradeModel ResultGrade { get; set; }
        public virtual SubjectModel Subject { get; set; }
    }
}
