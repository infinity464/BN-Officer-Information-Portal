using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class AdminFeedBackModel
    {
        public long AdminFeedBackId { get; set; }
        public long FeedBackId { get; set; }
        public string PNo { get; set; }
        public string Message { get; set; }
        public Nullable<bool> UserView { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
