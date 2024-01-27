using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPreviousTransferService
    {
        Task<PreviousTransferModel> SavePreviousTransfer(int previousTransferId, PreviousTransferModel model);
        Task<PreviousTransferModel> GetPreviousTransfer(int previousTransferId);
        List<PreviousTransferModel> GetPreviousTransfers(int employeeId);
        Task<bool> DeletePreviousTransfer(int id);
    }
}
