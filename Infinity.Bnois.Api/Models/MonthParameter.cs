using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public class MonthParameter
    {
        public MonthParameter(string Value, string Text)
        {
            this.Text = Text;
            this.Value = Value;
        }

        public string Value { get; set; }
        public string Text { get; set; }
    }
}
