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
    public class ProposalCandidateService : IProposalCandidateService
    {
        private readonly IBnoisRepository<ProposalCandidate> proposalCandidateRepository;
        public ProposalCandidateService(IBnoisRepository<ProposalCandidate> proposalCandidateRepository)
        {
            this.proposalCandidateRepository = proposalCandidateRepository;
        }

       
        public List<ProposalCandidateModel> GetProposalCandidates(int proposalDetailId)
        {
            IQueryable<ProposalCandidate> proposalCadidates = proposalCandidateRepository.FilterWithInclude(x => x.IsActive && x.ProposalDetailId == proposalDetailId, "ProposalDetail", "Employee");
            proposalCadidates = proposalCadidates.OrderByDescending(x => x.ProposalCandidateId);
            List<ProposalCandidateModel> models = ObjectConverter<ProposalCandidate, ProposalCandidateModel>.ConvertList(proposalCadidates.ToList()).ToList();
            return models;
        }

        //public async Task<ProposalCandidateModel> SaveProposalCadidate(int id, ProposalCandidateModel model)
        //{
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Proposal Candidate Officer data missing");
        //    }

        //    bool isExistData = proposalCandidateRepository.Exists(x => x.ProposalDetailId == model.ProposalDetailId && x.EmployeeId == model.EmployeeId && x.ProposalCandidateId != id);
        //    if (isExistData)
        //    {
        //        throw new InfinityInvalidDataException("Data already exists !");
        //    }
        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    ProposalCandidate proposalCadidate = ObjectConverter<ProposalCandidateModel, ProposalCandidate>.Convert(model);
            
        //    proposalCadidate.IsActive = true;
        //    proposalCadidate.CreatedDate = DateTime.Now;
        //    proposalCadidate.CreatedBy = userId;
        //    proposalCadidate.ProposalDetailId = model.ProposalDetailId;
        //    proposalCadidate.EmployeeId = model.EmployeeId;
        //    proposalCadidate.Employee = null;
        //    await proposalCandidateRepository.SaveAsync(proposalCadidate);
        //    model.ProposalCandidateId = proposalCadidate.ProposalCandidateId;
        //    return model;
        //}

        public async Task<bool> DeleteProposalCadidate(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ProposalCandidate proposalCadidate = await proposalCandidateRepository.FindOneAsync(x => x.ProposalCandidateId == id);
            if (proposalCadidate == null)
            {
                throw new InfinityNotFoundException("Proposal Cadidate not found");
            }
            else
            {
                return await proposalCandidateRepository.DeleteAsync(proposalCadidate);
            }
        }

        public Task<ProposalCandidateModel> SaveProposalCadidate(int id, ProposalCandidateModel model)
        {
            throw new NotImplementedException();
        }
    }
}
