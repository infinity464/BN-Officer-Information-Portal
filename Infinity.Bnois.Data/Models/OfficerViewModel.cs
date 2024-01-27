using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Data.Models
{
   public class OfficerViewModel
    {
        public List<OfficerGroupModel> GetOfficerGroupModels { get; set; }
        public DataTable DataTable { get; set; }
    }
}
