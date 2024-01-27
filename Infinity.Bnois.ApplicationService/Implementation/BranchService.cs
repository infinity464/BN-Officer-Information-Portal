using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
	public class BranchService : IBranchService
	{	

		private readonly IBnoisRepository<Branch> branchRepository;
		public BranchService(IBnoisRepository<Branch> branchRepository)
		{
			this.branchRepository = branchRepository;
		}

		public async Task<bool> DeleteBranch(int id)
		{
			if (id <= 0)
			{
				throw new InfinityArgumentMissingException("Invalid Request");
			}
			Branch branch = await branchRepository.FindOneAsync(x => x.BranchId == id);
			if (branch == null)
			{
				throw new InfinityNotFoundException("Branch not found");
			}
			else
			{
				return await branchRepository.DeleteAsync(branch);
			}
		}

		public async Task<BranchModel> GetBranch(int id)
		{
			if (id <= 0)
			{
				return new BranchModel();
			}
			Branch branch = await branchRepository.FindOneAsync(x => x.BranchId == id);
			if (branch == null)
			{
				throw new InfinityNotFoundException("Branch not found");
			}
			BranchModel model = ObjectConverter<Branch, BranchModel>.Convert(branch);
			return model;
		}

		public List<BranchModel> GetBranchs(int ps, int pn, string qs, out int total)
		{
	

			IQueryable<Branch> branchs = branchRepository.FilterWithInclude(x => x.Active == true
				  && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs)) ||
				  (x.Description.Contains(qs) || String.IsNullOrEmpty(qs))));

			total = branchs.Count();
			branchs = branchs.OrderByDescending(x => x.BranchId).Skip((pn - 1) * ps).Take(ps);
			List<BranchModel> models = ObjectConverter<Branch, BranchModel>.ConvertList(branchs.ToList()).ToList();
			return models;
		}

     
        public async Task<BranchModel> SaveBranch(int id, BranchModel model)
		{
			string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
			if (model == null)
			{
				throw new InfinityArgumentMissingException("Branch data missing");
			}
			Branch branch = ObjectConverter<BranchModel, Branch>.Convert(model);
			if (id > 0)
			{
				branch = await branchRepository.FindOneAsync(x => x.BranchId == id);
				if (branch == null)
				{
					throw new InfinityNotFoundException("Branch not found !");
				}

			    branch.Modified = DateTime.Now;
			    branch.ModifiedBy = userId;
            }
			else
			{
				branch.Created = DateTime.Now;
				branch.CreatedBy = userId;
			}
			branch.Name = model.Name;
			branch.NameBan = model.NameBan;
			branch.ShortName = model.ShortName;
			branch.ShortNameBan = model.ShortNameBan;
			branch.Priority = model.Priority;
			branch.Description = model.Description;
		
			branch.Active = true;
			await branchRepository.SaveAsync(branch);
			model.BranchId = branch.BranchId;
			return model;
		}

        public async Task<List<SelectModel>> GetBranchSelectModels()
        {
            ICollection<Branch> branches = await branchRepository.FilterAsync(x => x.Active==true);
            List<SelectModel> selectModels = branches.OrderBy(x=>x.Priority).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.BranchId
            }).ToList();
            return selectModels;
        }

    }
}
