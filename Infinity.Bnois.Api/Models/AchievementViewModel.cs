using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class AchievementViewModel
    {
        public AchievementModel Achievement { get; set; }
        public List<SelectModel> Commendations { get; set; }
        public List<SelectModel> Appreciations { get; set; }
        public List<SelectModel> Patterns { get; set; }
        public List<SelectModel> Offices { get; set; }
        public List<SelectModel> InsideOffices { get; set; }
        public List<SelectModel> InsideAppointments { get; set; }
        public List<SelectModel> GivenByTypes { get; set; }
        public List<SelectModel> AchievementComTypes { get; set; }
    }
}
