using System;
using System.Collections.Generic;
using System.Data;
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
	public class NominationService : INominationService
	{
		private readonly IBnoisRepository<Nomination> nominationRepository;
	    private readonly IBnoisRepository<vwNomination> vwGetNominationInfoRepository;
		public NominationService(IBnoisRepository<Nomination> nominationRepository, IBnoisRepository<vwNomination> vwGetNominationInfoRepository)
		{
			this.nominationRepository = nominationRepository;
			this.vwGetNominationInfoRepository = vwGetNominationInfoRepository;
		}


        public async Task<NominationModel> GetNomination(int id)
        {
            if (id <= 0)
            {
                return new NominationModel();
            }
            Nomination nomination = await nominationRepository.FindOneAsync(x => x.NominationId == id);
            if (nomination == null)
            {
                throw new InfinityNotFoundException("Nomination not found");
            }
            NominationModel model = ObjectConverter<Nomination, NominationModel>.Convert(nomination);
            return model;
        }




	    public  string GetNominationSchedule(int id, int type)
	    {

	        vwNomination nomination =  vwGetNominationInfoRepository.FindOne(x => x.EntityId == id && x.EnitityType==type );
	        if (nomination == null)
	        {
	            throw new InfinityNotFoundException("Nomination not found");
	        }
	        return nomination.Nomination;
	    }


        public IQueryable<vwNomination> GetNominations(int ps, int pn, string qs, out int total,int type)
        {
            IQueryable<vwNomination> nominations = vwGetNominationInfoRepository.FilterWithInclude(x => x.EnitityType==type && (x.Nomination.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = nominations.Count();
            nominations = nominations.OrderByDescending(x => x.NominationId).Skip((pn - 1) * ps).Take(ps);


            return nominations;
            
        }

        public async Task<NominationModel> SaveNomination(int id, NominationModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Nomination data missing");
            }
            //if (model.WithoutAppointment != true && model.MissoinAppointmentId == null)
            //{
            //    throw new InfinityArgumentMissingException("WithoutAppointment or Appoinment must be selected.");
            //}

            bool isExist = nominationRepository.Exists(x =>  x.EnitityType == model.EnitityType && x.EntityId==model.EntityId && x.MissoinAppointmentId==model.MissoinAppointmentId && x.WithoutTransfer==model.WithoutTransfer && x.NominationId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Nomination nomination = ObjectConverter<NominationModel, Nomination>.Convert(model);
            if (id > 0)
            {
                nomination = await nominationRepository.FindOneAsync(x => x.NominationId == id);
                if (nomination == null)
                {
                    throw new InfinityNotFoundException("Nomination not found !");
                }

                nomination.ModifiedDate = DateTime.Now;
                nomination.ModifiedBy = userId;
            }
            else
            {
                nomination.IsActive = true;
                nomination.CreatedDate = DateTime.Now;
                nomination.CreatedBy = userId;
            }


            nomination.EntryDate = model.EntryDate;
            nomination.EnitityType = model.EnitityType;
            nomination.EntityId = model.EntityId;
            nomination.WithoutTransfer = model.WithoutTransfer;
            nomination.WithoutAppointment = model.WithoutAppointment;
            nomination.MissoinAppointmentId = model.MissoinAppointmentId;
            nomination.Remarks = model.Remarks;
            await nominationRepository.SaveAsync(nomination);
            model.NominationId = nomination.NominationId;
            return model;
        }

        public async Task<bool> DeleteNomination(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Nomination nomination = await nominationRepository.FindOneAsync(x => x.NominationId == id);
            if (nomination == null)
            {
                throw new InfinityNotFoundException("Nomination not found");
            }
            else
            {
                return await nominationRepository.DeleteAsync(nomination);
            }
        }

	    public List<SelectModel> GetNominationTypeSelectModels()
	    {
	        List<SelectModel> selectModels =
	            Enum.GetValues(typeof(NominationType)).Cast<NominationType>()
	                .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt32(v) })
	                .ToList();
	        return selectModels;
        }


	    public async Task<List<SelectModel>> GetNominationSelectModels(int type)
        	    {
        	        ICollection<vwNomination> nominations = await vwGetNominationInfoRepository.FilterWithInclude(x => x.EnitityType==type && x.WithoutTransfer==false ).ToListAsync();
        
        	        if (type == 2)
        	        {
        	            List<SelectModel> selectModels = nominations.OrderByDescending(x=>x.FromDate).Select(x => new SelectModel
        	            {
        	                Text =x.Nomination + " [" + x.MissionAppointment + "]",
        	                Value = x.NominationId
        	            }).ToList();
        
        	            return selectModels;
                    }
        	        else
        	        {
        	            List<SelectModel> selectModels = nominations.OrderByDescending(x => x.FromDate).Select(x => new SelectModel
        	            {
        	                Text = x.Nomination,
	    	                Value = x.NominationId
	    	            }).ToList();
	    	            return selectModels;
	    
	                }
	    
	               
	    	    }



    }
}
