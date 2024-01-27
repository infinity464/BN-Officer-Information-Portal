using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class OfficeViewModel
    {
        public OfficeModel Office { get; set; }
        public List<SelectModel> Countries { get; set; }
        public List<SelectModel> Zones { get; set; }
        public List<SelectModel> ShipCategories { get; set; }
        public List<SelectModel> ShipTypes { get; set; }
        public List<SelectModel> Objectives { get; set; }
        public List<SelectModel> Patterns { get; set; }
        public List<SelectModel> AdminAuthorities  { get; set; }
        public List<SelectModel> BornOffices  { get; set; }
        public List<SelectModel> ParentOffices  { get; set; }
        public List<Object> AppointedOfficers  { get; set; }
        public List<Object> VacantAppointments  { get; set; }
        public string OfficeName  { get; set; }
    }
}
