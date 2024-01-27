using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class OfficeShipmentModel
    {
        public int OfficeMoveId { get; set; }
        public Nullable<int> OfficeId { get; set; }
        public Nullable<int> PreviousParentId { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public Nullable<int> PreZoneId { get; set; }
        public Nullable<int> PreAdminAuthorityId { get; set; }
        public string PreAddress { get; set; }

    }
}
