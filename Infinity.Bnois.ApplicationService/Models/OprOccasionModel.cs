using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class OprOccasionModel
    {
        public int OccasionId { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
