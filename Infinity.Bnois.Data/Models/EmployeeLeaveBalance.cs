using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Infinity.Bnois.Data.Models
{
    [JsonObject(IsReference = false)]
    public class EmployeeLeaveBalance
    {
        public int LeaveBalanceId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> LeaveTypeId { get; set; }
        public Nullable<int> LeaveYear { get; set; }
	    public Nullable<int> TotalForeignLeave { get; set; }
		public int TotalLeave { get; set; }
        public int TotalConsume { get; set; }
        public int Balance { get; set; }
        public int Slot { get; set; }
	    public string WATP { get; set; }
	    public Nullable<DateTime> CommissionDate { get; set; }

		public Nullable<bool> IsAssigned { get; set; }
    }
}
