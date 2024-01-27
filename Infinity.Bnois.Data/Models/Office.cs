using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Data
{
    public partial class Office
    {
        public List<Office> Items { get; set; }

        public List<Office> Children { get; set; }
        public string Count { get; set; }

    }
}
