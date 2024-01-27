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
    public class PreviousTransferService : IPreviousTransferService
    {
        private readonly IBnoisRepository<PreviousTransfer> previousTransferRepository;
        public PreviousTransferService(IBnoisRepository<PreviousTransfer> previousTransferRepository)
        {
            this.previousTransferRepository = previousTransferRepository;
        }

        public async Task<PreviousTransferModel> GetPreviousTransfer(int previousTransferId)
        {
            if (previousTransferId <= 0)
            {
                return new PreviousTransferModel();
            }
            PreviousTransfer previousTransfer = await previousTransferRepository.FindOneAsync(x => x.PreviousTransferId == previousTransferId);
            if (previousTransfer == null)
            {
                return new PreviousTransferModel();
            }

            PreviousTransferModel model = ObjectConverter<PreviousTransfer, PreviousTransferModel>.Convert(previousTransfer);
            return model;
        }

        public List<PreviousTransferModel> GetPreviousTransfers(int employeeId)
        {
            List<PreviousTransfer> previousTransfers = previousTransferRepository.FilterWithInclude(x => x.EmployeeId == employeeId,"Rank").ToList();
            List<PreviousTransferModel> models = ObjectConverter<PreviousTransfer, PreviousTransferModel>.ConvertList(previousTransfers.ToList()).ToList();
            return models;
        }

        public async Task<PreviousTransferModel> SavePreviousTransfer(int previousTransferId, PreviousTransferModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer Transfer data missing!");
            }

            PreviousTransfer previousTransfer = ObjectConverter<PreviousTransferModel, PreviousTransfer>.Convert(model);
            string userId= ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            if (previousTransferId > 0)
            {
                previousTransfer = await previousTransferRepository.FindOneAsync(x => x.PreviousTransferId == previousTransferId);
                if (previousTransfer == null)
                {
                    throw new InfinityNotFoundException("Transfer Not found !");
                }

                previousTransfer.ModifiedDate = DateTime.Now;
                previousTransfer.ModifiedBy = userId;
            }
            else
            {
                previousTransfer.EmployeeId = model.EmployeeId;
                previousTransfer.CreatedBy = userId;
                previousTransfer.CreatedDate = DateTime.Now;
                previousTransfer.IsActive = true;
            }

            previousTransfer.RankId = model.RankId;
            previousTransfer.Billet = model.Billet;
            previousTransfer.FromDate = model.FromDate;
            previousTransfer.ToDate = model.ToDate;
            previousTransfer.Remarks = model.Remarks;
            await previousTransferRepository.SaveAsync(previousTransfer);
            model.PreviousTransferId = previousTransfer.PreviousTransferId;
            return model;
        }


        public async Task<bool> DeletePreviousTransfer(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PreviousTransfer previousTransfer = await previousTransferRepository.FindOneAsync(x => x.PreviousTransferId == id);
            if (previousTransfer == null)
            {
                throw new InfinityNotFoundException("Transfer not found");
            }
            else
            {
                return await previousTransferRepository.DeleteAsync(previousTransfer);
            }
        }
    }
}
