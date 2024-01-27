using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infinity.Bnois
{
    [JsonObject(IsReference = false)]
    public class Chart
    {
        public object labels { get; set; }
        public List<Datasets> datasets { get; set; }
    }
    [JsonObject(IsReference = false)]
    public class Datasets
    {
        public object label { get; set; }
        public object backgroundColor { get; set; }
        public object borderColor { get; set; }
        public string borderWidth { get; set; }
        public object data { get; set; }
        public bool fill { get; set; }
    }
}
