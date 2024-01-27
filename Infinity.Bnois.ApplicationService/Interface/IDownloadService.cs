using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IDownloadService
    {
        List<DownloadModel> GetDownloads(int ps, int pn, string qs, out int total);
        List<DownloadModel> GetDownloadsForView();
        Task<DownloadModel> GetDownload(int id);
        Task<DownloadModel> SaveDownload(int id, DownloadModel model);
        Task<bool> DeleteDownload(int id);
        string DataMigration (string fileURL);
    }
}
