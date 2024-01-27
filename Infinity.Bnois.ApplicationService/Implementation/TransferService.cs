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
    public class TransferService : ITransferService
    {
        private readonly IBnoisRepository<Transfer> transferRepository;
        private readonly IBnoisRepository<vwTransfer> vwTransferRepository;
        private readonly IBnoisRepository<Employee> employeeRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        private readonly IBnoisRepository<OfficeAppRank> officeAppRankRepository;
        private readonly IBnoisRepository<OfficeAppBranch> officeAppBranchRepository;
        public TransferService(IBnoisRepository<Transfer> transferRepository, IBnoisRepository<vwTransfer> vwTransferRepository, IBnoisRepository<Employee> employeeRepository,
            IBnoisRepository<EmployeeGeneral> employeeGeneralRepository, IBnoisRepository<OfficeAppRank> officeAppRankRepository, IBnoisRepository<OfficeAppBranch> officeAppBranchRepository)
        {
            this.transferRepository = transferRepository;
            this.vwTransferRepository = vwTransferRepository;
            this.employeeRepository = employeeRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
            this.officeAppRankRepository = officeAppRankRepository;
            this.officeAppBranchRepository = officeAppBranchRepository;

        }

        public List<vwTransfer> GetTransfers(int employeeId, int type, int mode)
        {
            var transfers = vwTransferRepository.FilterWithInclude(x => x.EmployeeId == employeeId && x.TransferFor == type && x.TransferMode == mode).OrderBy(x => x.FromDate).ToList();

            return transfers;
        }


        public async Task<TransferModel> GetTransfer(int id)
        {

            if (id <= 0)
            {
                return new TransferModel();
            }
            Transfer transfer = await transferRepository.FindOneAsync(x => x.TransferId == id, new List<string> { "Employee" });
            if (transfer == null)
            {
                throw new InfinityNotFoundException("Transfer not found");
            }
            TransferModel model = ObjectConverter<Transfer, TransferModel>.Convert(transfer);
            return model;
        }

        //public async Task<TransferModel> SaveTransfer(int id, TransferModel model)
        //{
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Transfer  data missing");
        //    }

        //    if (model.FromDate > model.ToDate)
        //    {
        //        throw new InfinityInvalidDataException("FromDate is greater than ToDate");
        //    }

        //    if (model.TransferMode == (int)TransferMode.Permanent && model.CurrentBornOfficeId == null)
        //    {
        //        throw new InfinityInvalidDataException("Please enter born office.");
        //    }


        //    if (model.TransferFor == (int)TransferFor.Office && model.TransferMode == (int)TransferMode.Permanent && model.AttachOfficeId == null)
        //    {
        //        throw new InfinityInvalidDataException("Please enter attach office.");
        //    }


        //    if (model.TransferFor != (int)TransferFor.Office)
        //    {
        //        bool isExistData = transferRepository.Exists(x => x.TransferFor == model.TransferFor && x.NominationId == model.NominationId && x.EmployeeId == model.EmployeeId && x.TransferId != id);
        //        if (isExistData)
        //        {
        //            throw new InfinityInvalidDataException("Data already exists !");
        //        }
        //    }

        //    if (model.TransferFor == (int)TransferFor.Office && model.TransferMode == (int)TransferMode.Permanent)
        //    {
        //        bool isExistData = transferRepository.Exists(x => x.TransferFor == model.TransferFor && x.TransferMode==(int)TransferMode.Permanent && x.FromDate == model.FromDate && x.EmployeeId == model.EmployeeId && x.TransferId != id);
        //        if (isExistData)
        //        {
        //            throw new InfinityInvalidDataException("Data already exists !");
        //        }
        //    }



        //    if (model.TransferFor == (int)TransferFor.Office && model.AppointmentType == 1 && model.AppointmentId != null && model.TransferMode == (int)TransferMode.Permanent && id == 0)
        //    {
        //        EmployeeGeneral employeeGeneral =
        //            await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);

        //        bool officeAppRank =
        //            officeAppRankRepository.Exists(x => x.OffAppId == model.AppointmentId && x.RankId == model.Employee.RankId);


        //        bool officeAppBranch =
        //            officeAppBranchRepository.Exists(x => x.OffAppId == model.AppointmentId && x.BranchId == employeeGeneral.BranchId);

        //        if (!officeAppBranch && !officeAppRank)
        //        {
        //            throw new InfinityNotFoundException("Officer Not Allowed");
        //        }
        //        else if (officeAppBranch && !officeAppRank)
        //        {
        //            throw new InfinityNotFoundException("Officer Not Allowed");
        //        }
        //        else if (officeAppRank && !officeAppBranch)
        //        {
        //            throw new InfinityNotFoundException("Officer Not Allowed");
        //        }
        //    }


        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    Transfer transfer = ObjectConverter<TransferModel, Transfer>.Convert(model);
        //    if (id > 0)
        //    {
        //        transfer = await transferRepository.FindOneAsync(x => x.TransferId == id);
        //        if (transfer == null)
        //        {
        //            throw new InfinityNotFoundException("Transfer not found !");
        //        }

        //        transfer.ModifiedDate = DateTime.Now;
        //        transfer.ModifiedBy = userId;
        //    }
        //    else
        //    {
        //        transfer.IsActive = false;
        //        transfer.CreatedDate = DateTime.Now;
        //        transfer.CreatedBy = userId;

        //    }



        //    transfer.Employee = null;


        //    if (model.IsBackLog && (model.TransferMode == (int)TransferMode.Permanent))
        //    {
        //        transfer.RankId = model.RankId;
        //        //var previousTransfer = transferRepository.Where(x => x.EmployeeId == model.EmployeeId && x.TransferMode == (int)TransferMode.Permanent && x.FromDate < model.FromDate).OrderByDescending(x => x.FromDate).FirstOrDefault();
        //        var nextTransfer = transferRepository.Where(x => x.EmployeeId == model.EmployeeId && x.TransferMode == (int)TransferMode.Permanent && x.FromDate > model.FromDate).OrderBy(x => x.FromDate).FirstOrDefault();

        //        if (nextTransfer == null)
        //        {
        //            throw new InfinityNotFoundException("Back Log Entry Not Allow.");
        //        }

        //        //if (previousTransfer != null)
        //        //{
        //        //    previousTransfer.ToDate = Convert.ToDateTime(model.FromDate).AddDays(-1);
        //        //    await transferRepository.SaveAsync(previousTransfer);

        //        //}
        //        transfer.ToDate = nextTransfer.FromDate.AddDays(-1);

        //    }
        //    else
        //    {
        //        transfer.RankId = model.Employee.RankId;

        //        if (model.TransferMode == (int)TransferMode.Permanent && id == 0)
        //        {
        //            var transferInfo = transferRepository.Where(x => x.EmployeeId == model.EmployeeId && x.TransferMode == (int)TransferMode.Permanent).OrderByDescending(x => x.FromDate).FirstOrDefault();
        //            if (transferInfo != null)
        //            {

        //                if (model.FromDate < transferInfo.FromDate)
        //                {
        //                    throw new InfinityNotFoundException("From Date is less than Last From Date.");
        //                    //transferInfo.ToDate = Convert.ToDateTime(model.FromDate).AddDays(-1);
        //                    //await transferRepository.SaveAsync(transferInfo);
        //                }                      

        //            }

        //        }
        //        else
        //        {
        //            transfer.ToDate = model.ToDate;
        //        }
        //    }

        //    if (model.TransferMode == (int)TransferMode.Temporary && id == 0)
        //    {
        //        var tyTransferInfo = transferRepository.Where(x => x.EmployeeId == model.EmployeeId && x.TransferMode == (int)TransferMode.Temporary && x.TempTransferType==(int)TemporaryTransferType.TY_Attachment).OrderByDescending(x => x.FromDate).FirstOrDefault();
        //        if (tyTransferInfo != null)
        //        {

        //            if (tyTransferInfo.ToDate == null)
        //            {
        //                if (tyTransferInfo.TransferFor == (int)TransferFor.Office)
        //                {
        //                    if (model.TempTransferType==(int)TemporaryTransferType.TY_Attachment)
        //                    {
        //                        throw new InfinityNotFoundException("Please Close Last Office TY.");
        //                    }
        //                }
        //                else if (tyTransferInfo.TransferFor == (int)TransferFor.Course)
        //                {
        //                    throw new InfinityNotFoundException("Please Close Last Course TY.");
        //                }
        //                else
        //                {
        //                    throw new InfinityNotFoundException("Please Close Last Mission TY.");

        //                }

        //            }



        //        }
        //    }


        //    transfer.EmployeeId = model.EmployeeId;
        //    transfer.DistrictId = model.DistrictId;
        //    transfer.CurrentBornOfficeId = model.CurrentBornOfficeId;
        //    transfer.AttachOfficeId = model.AttachOfficeId;
        //    transfer.NominationId = model.NominationId;
        //    transfer.TransferMode = model.TransferMode;
        //    transfer.TranferType = model.TranferType;
        //    transfer.TempTransferType = model.TempTransferType;
        //    transfer.AppointmentId = model.AppointmentId;
        //    transfer.FromDate = model.FromDate ?? transfer.FromDate;
        //    transfer.TransferFor = model.TransferFor;
        //    transfer.FileName = model.FileName;
        //    transfer.AppointmentType = model.AppointmentType;
        //    transfer.Remarks = model.Remarks;

        //    await transferRepository.SaveAsync(transfer);
        //    model.TransferId = transfer.TransferId;


  


        //    return model;
        //}

        public async Task<bool> DeleteTransfer(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Transfer transfer = await transferRepository.FindOneAsync(x => x.TransferId == id);
            if (transfer == null)
            {
                throw new InfinityNotFoundException("Transfer not found");
            }
            else
            {

                var deletedTransferInfo = await transferRepository.DeleteAsync(transfer);

                if (deletedTransferInfo)
                {
                    var permanentTransferInfo = transferRepository.Where(x => x.EmployeeId == transfer.EmployeeId && x.TransferMode == (int)TransferMode.Permanent && x.IsActive).OrderByDescending(x => x.FromDate).FirstOrDefault();

                    var employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == transfer.EmployeeId);
                    if (employee != null)
                    {
                        employee.ModifiedDate = DateTime.Now;
                        employee.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();

                        if (permanentTransferInfo != null)
                        {
                            employee.TransferId = permanentTransferInfo.TransferId;

                        }



                        await employeeRepository.SaveAsync(employee);
                    }

                }

                return deletedTransferInfo;
            }
        }

        public List<SelectModel> GetTransferModeSelectModels()
        {
            List<SelectModel> selectModels =
           Enum.GetValues(typeof(TransferMode)).Cast<TransferMode>()
               .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt32(v) })
               .ToList();
            return selectModels;
        }

        public List<SelectModel> GetTransferTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(TransferType)).Cast<TransferType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt32(v) })
                    .ToList();
            return selectModels;
        }

        public List<SelectModel> GetTemporaryTransferTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(TemporaryTransferType)).Cast<TemporaryTransferType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt32(v) })
                    .ToList();
            return selectModels;
        }
        public List<SelectModel> GetTransferForSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(TransferFor)).Cast<TransferFor>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt32(v) })
                    .ToList();
            return selectModels;
        }

        public vwTransfer GetLastTransfer(int employeeId)
        {
            var transfer = vwTransferRepository.Where(x => x.EmployeeId == employeeId && x.TransferMode == (int)TransferMode.Permanent && x.isActive).OrderByDescending(x => x.FromDate).FirstOrDefault();

            return transfer;


        }


        public async Task<List<SelectModel>> GetTransferHistory(int employeeId)
        {
            IQueryable<vwTransfer> query = vwTransferRepository.FilterWithInclude(x => x.EmployeeId == employeeId && x.TransferMode == (int)TransferMode.Permanent && x.isActive);
            List<vwTransfer> transfers = await query.OrderByDescending(x => x.FromDate).ToListAsync();
            List<SelectModel> transferSelectModels = transfers.OrderByDescending(x => x.FromDate).Select(x => new SelectModel()
            {
                Text = x.BornOffice + '/' + x.CurrentAttach + '/' + x.Appointment,
                Value = x.TransferId
            }).ToList();
            return transferSelectModels;


        }

        public bool ExecuteTransfer()
        {
            transferRepository.ExecWithSqlQuery(String.Format("exec [spUpdateEmployeeTransfer]"));
            return true;
        }

        public Task<TransferModel> SaveTransfer(int v, TransferModel model)
        {
            throw new NotImplementedException();
        }
    }
}
