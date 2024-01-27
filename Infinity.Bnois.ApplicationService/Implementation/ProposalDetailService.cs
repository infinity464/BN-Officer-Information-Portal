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
    public class ProposalDetailService : IProposalDetailService
    {
        private readonly IBnoisRepository<ProposalDetail> proposalDetailRepository;
        public ProposalDetailService(IBnoisRepository<ProposalDetail> proposalDetailRepository)
        {
            this.proposalDetailRepository = proposalDetailRepository;
        }

        public async Task<bool> DeleteProposalDetails(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ProposalDetail proposal = await proposalDetailRepository.FindOneAsync(x => x.ProposalDetailId == id);
            if (proposal == null)
            {
                throw new InfinityNotFoundException("Proposal Detail not found");
            }
            else
            {
                return await proposalDetailRepository.DeleteAsync(proposal);
            }
        }

        public async Task<ProposalDetailModel> GetProposalDetail(int id)
        {
            if (id <= 0)
            {
                return new ProposalDetailModel();
            }
            ProposalDetail proposalDetail = await proposalDetailRepository.FindOneAsync(x => x.ProposalDetailId == id,new List<string>() { "Office"});
            if (proposalDetail == null)
            {
                throw new InfinityNotFoundException("Proposal Detail not found");
            }
            ProposalDetailModel model = ObjectConverter<ProposalDetail, ProposalDetailModel>.Convert(proposalDetail);
            return model;
        }

        public List<ProposalDetailModel> GetProposalDetails(int transferProposalId, int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        //public List<ProposalDetailModel> GetProposalDetails(int transferProposalId,int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<ProposalDetail> proposalDetails = proposalDetailRepository.FilterWithInclude(x => x.IsActive && x.TransferProposalId==transferProposalId
        //        && ((x.Office.ShortName.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.OfficeAppointment.Name.Contains(qs) || String.IsNullOrEmpty(qs))), "Office", "OfficeAppointment", "TransferProposal");
        //    total = proposalDetails.Count();
        //    proposalDetails = proposalDetails.OrderByDescending(x => x.ProposalDetailId).Skip((pn - 1) * ps).Take(ps);
        //    List<ProposalDetailModel> models = ObjectConverter<ProposalDetail, ProposalDetailModel>.ConvertList(proposalDetails.ToList()).ToList();
        //    return models;
        //}

        public async Task<ProposalDetailModel> SaveProposalDetail(int id, ProposalDetailModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Proposal Detail data missing");
            }

            bool isExistData = proposalDetailRepository.Exists(x => x.TransferProposalId == model.TransferProposalId && x.TransferType == model.TransferType && x.AttachOfficeId == model.AttachOfficeId && x.AppointmentId == model.AppointmentId && x.ProposalDetailId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ProposalDetail proposalDetail = ObjectConverter<ProposalDetailModel, ProposalDetail>.Convert(model);
            if (id > 0)
            {
                proposalDetail = await proposalDetailRepository.FindOneAsync(x => x.ProposalDetailId == id);
                if (proposalDetail == null)
                {
                    throw new InfinityNotFoundException("Proposal Detail not found !");
                }

                proposalDetail.ModifiedDate = DateTime.Now;
                proposalDetail.ModifiedBy = userId;
            }
            else
            {
                proposalDetail.IsActive = true;
                proposalDetail.CreatedDate = DateTime.Now;
                proposalDetail.CreatedBy = userId;
            }
            proposalDetail.TransferProposalId = model.TransferProposalId;
            proposalDetail.TransferType = model.TransferType;
            proposalDetail.AttachOfficeId = model.AttachOfficeId;
            proposalDetail.AppointmentId = model.AppointmentId;
            await proposalDetailRepository.SaveAsync(proposalDetail);
            model.ProposalDetailId = proposalDetail.ProposalDetailId;
            return model;
        }
    }
}
