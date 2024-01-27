using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class HeirNextOfKinInfoViewModel
    {
        public HeirNextOfKinInfoModel HeirNextOfKinInfo { get; set; }
        public FileModel File { get; set; }
        public List<SelectModel> Genders { get; set; }
        public List<SelectModel> Occupations { get; set; }
        public List<SelectModel> Relations { get; set; }
        public List<SelectModel> HeirTypes { get; set; }
        public List<SelectModel> HeirKinTypes { get; set; }



    }
}