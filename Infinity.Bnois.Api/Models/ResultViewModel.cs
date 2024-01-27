using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;


namespace Infinity.Bnois.Api.Models
{
    public class ResultViewModel
    {
        public List<SelectModel> ExamCategories { get; set; }
        public ResultModel Result { get; set; }
    }
}
