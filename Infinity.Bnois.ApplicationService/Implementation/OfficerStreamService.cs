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
    public class OfficerStreamService : IOfficerStreamService
    {
        private readonly IBnoisRepository<OfficerStream> officerStreamRepository;
        public OfficerStreamService(IBnoisRepository<OfficerStream> officerStreamRepository)
        {
            this.officerStreamRepository = officerStreamRepository;
        }
        
        public List<OfficerStreamModel> GetOfficerStreams(int ps, int pn, string qs, out int total)
        {
            IQueryable<OfficerStream> officerStreams = officerStreamRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = officerStreams.Count();
            officerStreams = officerStreams.OrderByDescending(x => x.OfficerStreamId).Skip((pn - 1) * ps).Take(ps);
            List<OfficerStreamModel> models = ObjectConverter<OfficerStream, OfficerStreamModel>.ConvertList(officerStreams.ToList()).ToList();
            return models;
        }
        public async Task<OfficerStreamModel> GetOfficerStream(int id)
        {
            if (id <= 0)
            {
                return new OfficerStreamModel();
            }
            OfficerStream officerStream = await officerStreamRepository.FindOneAsync(x => x.OfficerStreamId == id);
            if (officerStream == null)
            {
                throw new InfinityNotFoundException("OfficerStream not found");
            }
            OfficerStreamModel model = ObjectConverter<OfficerStream, OfficerStreamModel>.Convert(officerStream);
            return model;
        }

        public async Task<OfficerStreamModel> SaveOfficerStream(int id, OfficerStreamModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("OfficerStream data missing");
            }
            bool isExist = officerStreamRepository.Exists(x => x.Name == model.Name && x.OfficerStreamId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("OfficerStream already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            OfficerStream officerStream = ObjectConverter<OfficerStreamModel, OfficerStream>.Convert(model);
            if (id > 0)
            {
                officerStream = await officerStreamRepository.FindOneAsync(x => x.OfficerStreamId == id);
                if (officerStream == null)
                {
                    throw new InfinityNotFoundException("OfficerStream not found !");
                }
                officerStream.ModifiedDate = DateTime.Now;
                officerStream.ModifiedBy = userId;
            }
            else
            {
                officerStream.IsActive = true;
                officerStream.CreatedDate = DateTime.Now;
                officerStream.CreatedBy = userId;
            }
            officerStream.Name = model.Name;      
   
            officerStream.Remarks = model.Remarks;
            await officerStreamRepository.SaveAsync(officerStream);
            model.OfficerStreamId = officerStream.OfficerStreamId;
            return model;
        }

        public async Task<bool> DeleteOfficerStream(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            OfficerStream officerStream = await officerStreamRepository.FindOneAsync(x => x.OfficerStreamId == id);
            if (officerStream == null)
            {
                throw new InfinityNotFoundException("OfficerStream not found");
            }
            else
            {
                return await officerStreamRepository.DeleteAsync(officerStream);
            }
        }

        public async Task<List<SelectModel>> GetOfficerStreamSelectModels()
        {
            ICollection<OfficerStream> officerStreams = await officerStreamRepository.FilterAsync(x => x.IsActive);
            List<OfficerStream> query =  officerStreams.OrderBy(x => x.Name).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.OfficerStreamId
            }).ToList();
            return selectModels;
        }
    }
}
