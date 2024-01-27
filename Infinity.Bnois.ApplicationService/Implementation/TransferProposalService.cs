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
  public  class TransferProposalService: ITransferProposalService
    {
        private readonly IBnoisRepository<TransferProposal> transferProposalRepository;
        public TransferProposalService(IBnoisRepository<TransferProposal> transferProposalRepository)
        {
            this.transferProposalRepository = transferProposalRepository;
        }
        public List<TransferProposalModel> GetTransferProposals(int ps, int pn, string qs, out int total)
        {
            IQueryable<TransferProposal> transferProposals = transferProposalRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = transferProposals.Count();
            transferProposals = transferProposals.OrderByDescending(x => x.TransferProposalId).Skip((pn - 1) * ps).Take(ps);
            List<TransferProposalModel> models = ObjectConverter<TransferProposal, TransferProposalModel>.ConvertList(transferProposals.ToList()).ToList();
            return models;
        }
        public async Task<TransferProposalModel> GetTransferProposal(int id)
        {
            if (id <= 0)
            {
                return new TransferProposalModel();
            }
            TransferProposal transferProposal = await transferProposalRepository.FindOneAsync(x => x.TransferProposalId == id);
            if (transferProposal == null)
            {
                throw new InfinityNotFoundException("Transfer Proposal not found");
            }
            TransferProposalModel model = ObjectConverter<TransferProposal, TransferProposalModel>.Convert(transferProposal);
            return model;
        }

        public async Task<TransferProposalModel> SaveTransferProposal(int id, TransferProposalModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Transfer Proposal data missing");
            }
            bool isExist = transferProposalRepository.Exists(x => x.Name == model.Name && x.TransferProposalId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            TransferProposal transferProposal = ObjectConverter<TransferProposalModel, TransferProposal>.Convert(model);
            if (id > 0)
            {
                transferProposal = await transferProposalRepository.FindOneAsync(x => x.TransferProposalId == id);
                if (transferProposal == null)
                {
                    throw new InfinityNotFoundException("Transfer Proposal not found !");
                }

                transferProposal.ModifiedDate = DateTime.Now;
                transferProposal.ModifiedBy = userId;
            }
            else
            {
                transferProposal.IsActive = true;
                transferProposal.CreatedDate = DateTime.Now;
                transferProposal.CreatedBy = userId;
            }
             transferProposal.Name = model.Name;
            transferProposal.ProposalDate = model.ProposalDate ?? transferProposal.ProposalDate;
            transferProposal.LtCdrLevel = model.LtCdrLevel;
            transferProposal.WithPicture = model.WithPicture;
            transferProposal.Remarks = model.Remarks;


            await transferProposalRepository.SaveAsync(transferProposal);
            model.TransferProposalId = transferProposal.TransferProposalId;
            return model;
        }
        public async Task<bool> DeleteTransferProposal(int id)
        {

            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            TransferProposal transferProposal = await transferProposalRepository.FindOneAsync(x => x.TransferProposalId == id);
            if (transferProposal == null)
            {
                throw new InfinityNotFoundException("Division not found");
            }
            else
            {
                return await transferProposalRepository.DeleteAsync(transferProposal);
            }
        }
        
    }
}
