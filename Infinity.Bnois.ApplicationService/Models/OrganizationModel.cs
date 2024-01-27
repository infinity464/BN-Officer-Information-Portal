using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class OrganizationModel
    {
        public int OrganizationId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Remarks { get; set; }
    }
}
