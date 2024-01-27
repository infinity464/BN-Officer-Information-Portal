using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class StatusChangeViewModel
    {
        public StatusChangeModel StatusChange { get; set; }
        public int CurrentStatusId { get; set; }
        public List<SelectModel> SelectModels { get; set; }
       
    }
}
