using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class RemarkService : IRemarkService
    {
        private readonly IBnoisRepository<Remark> RemarkRepository;

        public RemarkService(IBnoisRepository<Remark> RemarkRepository)
        {
            this.RemarkRepository = RemarkRepository;
        }

        //public List<RemarkModel> GetRemarks( string pNo,int type)
        //{
        //    IQueryable<Remark> remarks = RemarkRepository.FilterWithInclude(x => x.IsActive && x.Type==type && x.Employee.PNo == pNo, "Employee");

        //    List<RemarkModel> models = ObjectConverter<Remark, RemarkModel>.ConvertList(remarks.ToList()).ToList();
        //    return models;
        //}

        public async Task<RemarkModel> GetRemark(int id)
        {
            if (id <= 0)
            {
                return new RemarkModel();
            }
            Remark remark = await RemarkRepository.FindOneAsync(x => x.RemarkId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (remark == null)
            {
                throw new InfinityNotFoundException("Remark not found");
            }
            RemarkModel model = ObjectConverter<Remark, RemarkModel>.Convert(remark);
            return model;
        }

       

        //public async Task<RemarkModel> SaveRemark(int id, RemarkModel model)
        //{
           
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Remark  data missing");
        //    }

        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    Remark remark = ObjectConverter<RemarkModel, Remark>.Convert(model);
        //    if (id > 0)
        //    {
        //        remark = await RemarkRepository.FindOneAsync(x => x.RemarkId == id);
        //        if (remark == null)
        //        {
        //            throw new InfinityNotFoundException("Remark not found !");
        //        }

        //        remark.ModifiedDate = DateTime.Now;
        //        remark.ModifiedBy = userId;
        //    }
        //    else
        //    {
        //        remark.IsActive = true;
        //        remark.CreatedDate = DateTime.Now;
        //        remark.CreatedBy = userId;
        //    }
        //    remark.EmployeeId = model.EmployeeId;
        //    remark.TransferId = model.TransferId;
        //    remark.NoteType = model.NoteType;
        //    remark.Remarks = model.Remarks;
        //    remark.Date = model.Date;
        //    remark.Type = model.Type;
        //    remark.Employee = null;

        //    await RemarkRepository.SaveAsync(remark);
        //    model.RemarkId = remark.RemarkId;

        //    return model;
        //}


        public async Task<bool> DeleteRemark(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Remark remark = await RemarkRepository.FindOneAsync(x => x.RemarkId == id);
            if (remark == null)
            {
                throw new InfinityNotFoundException("Remark not found");
            }

            return await RemarkRepository.DeleteAsync(remark); ;
           
        }

        public List<RemarkModel> GetRemarks(string pNo, int type)
        {
            throw new NotImplementedException();
        }

        public Task<RemarkModel> SaveRemark(int v, RemarkModel model)
        {
            throw new NotImplementedException();
        }
    }
}
