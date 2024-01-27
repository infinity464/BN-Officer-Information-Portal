using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class DownloadService: IDownloadService
    {
        BnoisDbContext db = new BnoisDbContext();
        private readonly IBnoisRepository<Download> _downloadRepository;

        public DownloadService(IBnoisRepository<Download> downloadRepository)
        {
            _downloadRepository = downloadRepository;
        }



        public async Task<bool> DeleteDownload(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Download download = await _downloadRepository.FindOneAsync(x => x.DownloadId == id);
            if (download == null)
            {
                throw new InfinityNotFoundException("File not found");
            }
            else
            {
                return await _downloadRepository.DeleteAsync(download);
            }
        }

        public async Task<DownloadModel> GetDownload(int id)
        {
            if (id <= 0)
            {
                return new DownloadModel();
            }
            Download download= await _downloadRepository.FindOneAsync(x => x.DownloadId == id);
            if (download == null)
            {
                throw new InfinityNotFoundException("File not found");
            }
            DownloadModel model = ObjectConverter<Download, DownloadModel>.Convert(download);
            return model;
        }

        public List<DownloadModel> GetDownloads(int ps, int pn, string qs, out int total)
        {
            IQueryable<Download> downloads =  _downloadRepository.FilterWithInclude(x => x.IsActive && (x.FileName == qs || String.IsNullOrEmpty(qs)));
            total = downloads.Count();
            downloads = downloads.OrderByDescending(x => x.DownloadId).Skip((pn - 1) * ps).Take(ps);
            List<DownloadModel> models = ObjectConverter<Download, DownloadModel>.ConvertList(downloads.ToList()).ToList();
            return models;
        }

        public List<DownloadModel> GetDownloadsForView()
        {
            IQueryable<Download> downloads = _downloadRepository.FilterWithInclude(x => x.IsActive);
            List<DownloadModel> models = ObjectConverter<Download, DownloadModel>.ConvertList(downloads.ToList()).ToList();
            return models;
        }

        public async Task<DownloadModel> SaveDownload(int id, DownloadModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Data is missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Download download = ObjectConverter<DownloadModel, Download>.Convert(model);
            if (id > 0)
            {
                download = await _downloadRepository.FindOneAsync(x => x.DownloadId == id);
                if (download == null)
                {
                    throw new InfinityNotFoundException("File not found !");
                }

                download.ModifiedDate = DateTime.Now;
                download.ModifiedBy = userId;
            }
            else
            {
                download.IsActive = true;
                download.CreatedDate = DateTime.Now;
                download.CreatedBy = userId;
            }
            download.FileName = model.FileName;
            download.Source = model.Source;
            download.ContentType = model.ContentType;
            download.FileExtension = model.FileExtension;
            
            await _downloadRepository.SaveAsync(download);
            model.DownloadId = download.DownloadId;
            return model;
        }
        #region data migration start
        public string DataMigration(string fileURL)
        {
            if (File.Exists(fileURL))
            {
                try
                {
                    DataTable dataTable = _downloadRepository.ExecWithSqlQuery(String.Format("exec DataMigrationScript '{0}'", fileURL));
                    string isDatabaseExist = db.Database.SqlQuery<string>("SELECT name FROM master.sys.databases WHERE name = N'Bnoisdb'").SingleOrDefault();
                    if(isDatabaseExist == "Bnoisdb")
                    {
                        try
                        {
                            DataTable dataTable1 = _downloadRepository.ExecWithSqlQuery(String.Format("exec BnoisToBnoipDataMigrationScript"));
                            try
                            {
                                DataTable dataTable2 = _downloadRepository.ExecWithSqlQuery(String.Format("exec BnoisDataDelete"));
                                return "Migration Successfull.";
                            }
                            catch
                            {
                                return "Cannot delete data from BNOIS DB.";
                            }
                        }
                        catch
                        {
                            return "Data migration from BNOIS to BNOIP failed. ";
                        }
                    }
                    else
                    {
                        return "BNOIS DB doesnot found";
                    }
                }
                catch
                {
                    return "Can not migrated Data to BNOIS";
                }
            }
            else
            {
                return "File Does not found.";
            }
        }
        
        #endregion
    }
}
