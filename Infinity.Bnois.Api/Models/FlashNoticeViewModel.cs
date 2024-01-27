using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class FlashNoticeViewModel
    {
        public List<FlashNoticeModel> ActiveFlashNotice { get; set; }
        public List<FlashNoticeModel> InActiveFlashNotice { get; set; }
    }
}
