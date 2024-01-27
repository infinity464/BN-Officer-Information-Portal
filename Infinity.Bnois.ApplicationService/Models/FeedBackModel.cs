using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class FeedBackModel
    {
        public long FeedBackId { get; set; }
        public string PNo { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Nullable<bool> AdminView { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool Active { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
