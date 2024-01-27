using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IRemarkService
    {
        List<RemarkModel> GetRemarks(string pNo,int type);
        Task<RemarkModel> GetRemark(int id);
        Task<RemarkModel> SaveRemark(int v, RemarkModel model);
        Task<bool> DeleteRemark(int id);


    }
}
