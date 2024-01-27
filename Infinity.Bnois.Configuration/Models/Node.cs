using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Configuration.Models
{
   public class Node
    {
        public object Id { get; set; }
        public string Text { get; set; }
        public bool IsChaild { get; set; }
        public List<Node> Nodes { get; set; }

    }
}
