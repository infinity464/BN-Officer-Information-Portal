using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.Api.Models
{
    public class TransferViewModel
    {
        public TransferModel Transfer { get; set; }
        public string PreBornOffice { get; set; }
        public string PreAppointment { get; set; }
        public string PreAttachOffice { get; set; }
        public List<SelectModel> TransferModes { get; set; }
        public List<SelectModel> TransferTypes { get; set; }
        public List<SelectModel> TemporaryTransferTypes { get; set; }
        public List<SelectModel> NewBorns { get; set; }
        public List<SelectModel> NewAttaches { get; set; }
        public List<SelectModel> Appointments { get; set; }
     
        public List<SelectModel> Ranks { get; set; }
        public List<SelectModel> Districts { get; set; }

        public  List<vwTransfer> OfficerTransfers { get; set; }
        public  List<vwTransfer> OfficerTemporaryTransfers { get; set; }
    }
}
