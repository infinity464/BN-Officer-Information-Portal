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
    public class SubBranchService : ISubBranchService
    {
        private readonly IBnoisRepository<SubBranch> subBranchRepository;
        public SubBranchService(IBnoisRepository<SubBranch> subBranchRepository)
        {
            this.subBranchRepository = subBranchRepository;
        }
        
        public List<SubBranchModel> GetSubBranches(int ps, int pn, string qs, out int total)
        {
            IQueryable<SubBranch> subBraches = subBranchRepository.FilterWithInclude(x => x.IsActive && (x.ShortName.Contains(qs) || String.IsNullOrEmpty(qs)) || (x.FullName.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = subBraches.Count();
            subBraches = subBraches.OrderByDescending(x => x.SubBranchId).Skip((pn - 1) * ps).Take(ps);
            List<SubBranchModel> models = ObjectConverter<SubBranch, SubBranchModel>.ConvertList(subBraches.ToList()).ToList();
            return models;
        }

        public async Task<SubBranchModel> GetSubBranch(int id)
        {
            if (id <= 0)
            {
                return new SubBranchModel();
            }
            SubBranch subBranch = await subBranchRepository.FindOneAsync(x => x.SubBranchId == id);
            if (subBranch == null)
            {
                throw new InfinityNotFoundException("SubBranch not found");
            }
            SubBranchModel model = ObjectConverter<SubBranch, SubBranchModel>.Convert(subBranch);
            return model;
        }

        public async Task<SubBranchModel> SaveSubBranch(int id, SubBranchModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("SubBranch data missing");
            }
            bool isExist = subBranchRepository.Exists(x => x.FullName == model.FullName && x.ShortName == model.ShortName && x.SubBranchId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("SubBranch already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            SubBranch subBranch = ObjectConverter<SubBranchModel, SubBranch>.Convert(model);
            if (id > 0)
            {
                subBranch = await subBranchRepository.FindOneAsync(x => x.SubBranchId == id);
                if (subBranch == null)
                {
                    throw new InfinityNotFoundException("SubBranch not found !");
                }

                subBranch.ModifiedDate = DateTime.Now;
                subBranch.ModifiedBy = userId;
            }
            else
            {
                subBranch.IsActive = true;
                subBranch.CreatedDate = DateTime.Now;
                subBranch.CreatedBy = userId;
            }
            subBranch.FullName = model.FullName;
            subBranch.ShortName = model.ShortName;
            subBranch.FullNameBan = model.FullNameBan;
            subBranch.ShortNameBan = model.ShortNameBan;
       
            subBranch.Description = model.Description;
            await subBranchRepository.SaveAsync(subBranch);
            model.SubBranchId = subBranch.SubBranchId;
            return model;
        }

        public async Task<bool> DeleteSubBranch(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            SubBranch subBranch = await subBranchRepository.FindOneAsync(x => x.SubBranchId == id);
            if (subBranch == null)
            {
                throw new InfinityNotFoundException("SubBranch not found");
            }
            else
            {
                return await subBranchRepository.DeleteAsync(subBranch);
            }
        }

        public async Task<List<SelectModel>> GetSubBranchSelectModels()
        {
            ICollection<SubBranch> subBranches = await subBranchRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = subBranches.OrderBy(x=>x.ShortName).Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.SubBranchId
            }).ToList();
            return selectModels;
        }
    }
}
