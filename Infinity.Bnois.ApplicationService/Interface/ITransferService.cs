using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ITransferService
    {
        List<vwTransfer> GetTransfers(int employeeId,int type, int mode);
        Task<TransferModel> GetTransfer(int id);
        Task<TransferModel> SaveTransfer(int v, TransferModel model);
        Task<bool> DeleteTransfer(int id);
        List<SelectModel> GetTransferModeSelectModels();  
        List<SelectModel> GetTransferTypeSelectModels();
        List<SelectModel> GetTransferForSelectModels();
        List<SelectModel> GetTemporaryTransferTypeSelectModels();

        vwTransfer GetLastTransfer(int employeeId);
        Task<List<SelectModel>> GetTransferHistory(int employeeId);

        bool ExecuteTransfer();


    }
}
