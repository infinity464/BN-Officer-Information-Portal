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
   public class MemberRoleService : IMemberRoleService
    {
        private readonly IBnoisRepository<MemberRole> memberRoleRepository;
        public MemberRoleService(IBnoisRepository<MemberRole> memberRoleRepository)
        {
            this.memberRoleRepository = memberRoleRepository;
        }
        
        public List<MemberRoleModel> GetMemberRoles(int ps, int pn, string qs, out int total)
        {
            IQueryable<MemberRole> memberRoles = memberRoleRepository
                .FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || string.IsNullOrEmpty(qs)));
            total = memberRoles.Count();
            memberRoles = memberRoles.OrderBy(x => x.MemberRoleId).Skip((pn - 1) * ps).Take(ps);
            List<MemberRoleModel> models = ObjectConverter<MemberRole, MemberRoleModel>.ConvertList(memberRoles.ToList()).ToList();
            return models;
        }

        public async Task<MemberRoleModel> GetMemberRole(int id)
        {
            if (id == 0)
            {
                return new MemberRoleModel();
            }
            MemberRole memberRole = await memberRoleRepository.FindOneAsync(x => x.MemberRoleId == id);
            if (memberRole == null)
            {
                throw new InfinityNotFoundException("Board Member Role not found !");
            }
            MemberRoleModel model = ObjectConverter<MemberRole, MemberRoleModel>.Convert(memberRole);
            return model;
        }

        public async Task<MemberRoleModel> SaveMemberRole(int id, MemberRoleModel model)
        {
            bool isExist = await memberRoleRepository.ExistsAsync(x => (x.Name == model.Name) && x.MemberRoleId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Board Member Role data already exist !");
            }

            if (model == null)
            {
                throw new InfinityArgumentMissingException("Board Member Role data missing");
            }
            MemberRole memberRole = ObjectConverter<MemberRoleModel, MemberRole>.Convert(model);
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();

            if (id > 0)
            {
                memberRole = await memberRoleRepository.FindOneAsync(x => x.MemberRoleId == id);
                if (memberRole == null)
                {
                    throw new InfinityNotFoundException("Board Member Role not found !");
                }


                memberRole.ModifiedDate = DateTime.Now;
                memberRole.ModifiedBy = userId;
            }
            else
            {
                memberRole.CreatedDate = DateTime.Now;
                memberRole.CreatedBy = userId;
                memberRole.IsActive = true;
            }
            memberRole.Name = model.Name;
            memberRole.Remarks = model.Remarks;
            await memberRoleRepository.SaveAsync(memberRole);
            model.MemberRoleId = memberRole.MemberRoleId;
            return model;
        }

        public async Task<bool> DeleteMemberRole(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            MemberRole memberRole = await memberRoleRepository.FindOneAsync(x => x.MemberRoleId == id);
            if (memberRole == null)
            {
                throw new InfinityNotFoundException("MemberRole not found");
            }
            else
            {
                return await memberRoleRepository.DeleteAsync(memberRole);
            }
        }

        public async Task<List<SelectModel>> GetMemberRoleSelectModels()
        {
            ICollection<MemberRole> memberRoles = await memberRoleRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = memberRoles.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.MemberRoleId
            }).ToList();
            return selectModels;
        }
    }
}
