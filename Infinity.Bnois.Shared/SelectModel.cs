using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infinity.Bnois
{


    [JsonObject(IsReference = false)]
    public class SelectModel
    {
        public object Value { set; get; }
        public object Text { set; get; }
    }
    

}
