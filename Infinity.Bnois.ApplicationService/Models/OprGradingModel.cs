using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class OprGradingModel
    {
        public int Id { get; set; }
        public double MinGrade { get; set; }
        public double MaxGrade { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
