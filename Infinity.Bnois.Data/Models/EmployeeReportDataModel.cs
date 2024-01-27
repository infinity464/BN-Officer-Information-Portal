namespace Infinity.Bnois.Data.Models
{
    public class EmployeeReportDataModel
    {
        public double? OprGrade { get; set; }
        public int? OPR_MONTH { get; set; }
        public int? TDay { get; set; }
        public int? SeaTDay { get; set; }
        public string PNo { get; set; }
        public string TDayInTex { get; set; }
        public string SeaTDayInTex { get; set; }
        public string SubCategory { get; set; }
        public double Percentage { get; set; }

        public long? Row_Num { get; set; }
    }
}