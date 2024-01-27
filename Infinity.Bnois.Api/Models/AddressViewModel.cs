using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class AddressViewModel
    {
        public AddressModel Address { get; set; }
        public List<SelectModel> Divisions { get; set; }
        public List<SelectModel> Districts { get; set; }
        public List<SelectModel> Upazilas { get; set; }
        public List<SelectModel> AddressTypes { get; set; }
    }
}
