using Infinity.Bnois.Api;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.Data.Models;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class EmployeeLeaveData
    {
        public int EmpLeaveId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferId { get; set; }
        public int LeaveTypeId { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> Duration { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> ExBdLeave { get; set; }
        public string AccompanyBy { get; set; }
        public Nullable<int> Purpose { get; set; }
        public string FileName { get; set; }
        public Nullable<int> ApprovedStatus { get; set; }
        public bool Active { get; set; }
        public Nullable<int> BOffCd { get; set; }
        public Nullable<int> AOffCd { get; set; }
        public Nullable<int> OrgCd { get; set; }
        public Nullable<bool> PingDeskApproveStatus { get; set; }
        public string PingDeskRemark { get; set; }
        public Nullable<bool> NominationApproveStatus { get; set; }
        public string NominationRemark { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class EmployeeLeaveService : IEmployeeLeaveService
    {
        BnoisDbContext db = new BnoisDbContext();
        private readonly IEmployeeLeaveRepository employeeLeaveRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        private readonly IBnoisRepository<Employee> employeeRepository;
        private readonly IBnoisRepository<EmployeeLeaveCountry> employeeLeaveCountryRepository;
        private readonly IBnoisRepository<EmployeeLeaveYear> employeeLeaveYearRepository;
        private readonly IBnoisRepository<LeavePolicy> leavePolicyRepository;
        private readonly IBnoisRepository<EmpLeaveDetail> empLeaveDetailRepository;
        private readonly IBnoisRepository<OfficeAppointment> officeAppointmentRepository;
        private readonly IBnoisRepository<Transfer> transferRepository;

        public EmployeeLeaveService(IBnoisRepository<Employee> employeeRepository, IEmployeeLeaveRepository employeeLeaveRepository, IBnoisRepository<EmployeeLeaveCountry> employeeLeaveCountryRepository, IBnoisRepository<EmployeeLeaveYear> employeeLeaveYearRepository, IBnoisRepository<EmployeeGeneral> employeeGeneralRepository, IBnoisRepository<LeavePolicy> leavePolicyRepository, IBnoisRepository<EmpLeaveDetail> empLeaveDetailRepository, IBnoisRepository<OfficeAppointment> officeAppointmentRepository, IBnoisRepository<Transfer> transferRepository)
        {
            this.employeeLeaveRepository = employeeLeaveRepository;
            this.employeeLeaveCountryRepository = employeeLeaveCountryRepository;
            this.employeeLeaveYearRepository = employeeLeaveYearRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
            this.leavePolicyRepository = leavePolicyRepository;
            this.empLeaveDetailRepository = empLeaveDetailRepository;
            this.employeeRepository = employeeRepository;
            this.officeAppointmentRepository = officeAppointmentRepository;
            this.transferRepository = transferRepository;
        }

        public async Task<bool> DeleteEmployeeLeave(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeLeave employeeLeave = await employeeLeaveRepository.FindOneAsync(x => x.EmpLeaveId == id);
            List<EmployeeLeaveYear> employeeLeaveYear = employeeLeaveYearRepository.Where(x => x.EmpLeaveId == employeeLeave.EmpLeaveId).ToList();
            List<EmployeeLeaveCountry> employeeLeaveCountry = employeeLeaveCountryRepository.Where(x => x.EmpLeaveId == employeeLeave.EmpLeaveId).ToList();

            if (employeeLeave == null)
            {
                throw new InfinityNotFoundException("Leave Info not found!");
            }
            else
            {
                employeeLeaveYearRepository.RemoveRange(employeeLeaveYear);
                employeeLeaveCountryRepository.RemoveRange(employeeLeaveCountry);
                return await employeeLeaveRepository.DeleteAsync(employeeLeave);
            }
        }

        //public async Task<EmployeeLeaveModel> GetEmployeeLeave(int id)
        //{
        //    if (id <= 0)
        //    {

        //        return new EmployeeLeaveModel() { LeaveBalances = new List<EmployeeLeaveBalance>() };
        //    }

        //    EmployeeLeave employeeLeave = await employeeLeaveRepository.FindOneAsync(x => x.EmpLeaveId == id, new List<string> { "Employee", "Employee.EmployeeGeneral", "Employee.Rank", "Employee.Batch", "EmployeeLeaveYear" });

        //    if (employeeLeave == null)
        //    {
        //        throw new InfinityNotFoundException("Employee Leave not found");
        //    }

        //    EmployeeLeaveModel model = ObjectConverter<EmployeeLeave, EmployeeLeaveModel>.Convert(employeeLeave);
        //    if (model.LeaveTypeId == CodeValue.PL_LeaveType)
        //    {
        //        model.LeaveBalances = await GetEmployeeLeaveBalanceBySlot(employeeLeave.EmployeeId, model);
        //    }
        //    model.CountryIds = employeeLeaveCountryRepository.Where(x => x.EmpLeaveId == employeeLeave.EmpLeaveId).Select(x => x.CountryId).ToArray();
        //    //var employeeLeaveInfo = employeeLeaveRepository.Where(x =>x.EmployeeId == employeeLeave.EmployeeId && x.LeaveTypeId == employeeLeave.LeaveTypeId && x.EmpLeaveId != employeeLeave.EmpLeaveId).ToList();
        //    string due = await GetEmployeeLeaveDue(model.EmployeeId, model.LeaveTypeId);
        //    model.LeaveDueCount = (Convert.ToInt16(due) + Convert.ToInt16(model.Duration)).ToString();
        //    model.OfficeAppointmentId= db.Database.SqlQuery<int>("select top(1) ReportingOfficeId from EmpLeaveDetail where EmpLeaveId="+ id).SingleOrDefault();

        //    return model;
        //}

        private async Task<List<EmployeeLeaveBalance>> GetEmployeeLeaveBalanceBySlot(int employeeId, EmployeeLeaveModel employeeLeave)
        {
            List<EmployeeLeaveBalance> totalLeaveBalance = await employeeLeaveRepository.GetEmployeeRepository(employeeId, employeeLeave.LeaveTypeId);
            List<EmployeeLeaveBalance> leaveBalancesForEdit = totalLeaveBalance.Where(x => x.LeaveYear >= employeeLeave.EmployeeLeaveYears.Min(y => y.YearText)).ToList();
            int slot = totalLeaveBalance.Select(x => x.Slot).FirstOrDefault();
            List<EmployeeLeaveBalance> filterLeaveBalance = leaveBalancesForEdit.OrderBy(x => x.LeaveYear).Take(slot).ToList();

            var yearr = from leaveYear in employeeLeave.EmployeeLeaveYears
                        group leaveYear by leaveYear.YearText into yearDetails
                        select new
                        {
                            Year = yearDetails.Key,
                            dayCount = yearDetails.Count()
                        };

            foreach (var year in yearr)
            {
                foreach (var leaveBalance in filterLeaveBalance)
                {
                    if (leaveBalance.LeaveYear.ToString().Contains(year.Year.ToString()))
                    {
                        leaveBalance.IsAssigned = true;
                        leaveBalance.TotalConsume = (int)(leaveBalance.TotalConsume - year.dayCount);
                        leaveBalance.Balance = (int)(leaveBalance.Balance + year.dayCount);

                    }
                }
            }
            return filterLeaveBalance.OrderBy(x => x.LeaveYear).ToList();
        }

        public async Task<List<EmployeeLeaveBalance>> GetEmployeeLeaveBalance(int employeeId, int leaveType)
        {
            if (employeeId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalide Officers Pno!");
            }
            List<EmployeeLeaveBalance> totalLeaveBalance = await employeeLeaveRepository.GetEmployeeRepository(employeeId, leaveType);
            int slot = totalLeaveBalance.Select(x => x.Slot).FirstOrDefault();
            List<EmployeeLeaveBalance> filterLeaveBalance = totalLeaveBalance.OrderByDescending(x => x.LeaveYear).Take(slot).ToList();
            return filterLeaveBalance.OrderBy(x => x.LeaveYear).ToList();
        }
        public async Task<List<SpGetEmployeeLeaveInfoByPNo>> GetEmployeeLeaveDetailsByPNo(string employeePNo)
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeeePNo;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && employeePNo == "undefined")
            {
                employeeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if (RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && employeePNo != "undefined")
            {
                employeeePNo = employeePNo;
            }
            else
            {
                throw new Exception("Some thing went wrong.");
            }
            int employeeStatus = 0;
            employeeStatus = db.Database.SqlQuery<int>("Select top(1) EmployeeStatusId from Employee where PNo='" + employeeePNo + "'").SingleOrDefault();
            if (employeeStatus != 1)
            {
                throw new InfinityNotFoundException("Officer is not active.");
            }
            if (employeeePNo != null)
            {
                var leaveBreakDowns = await employeeLeaveRepository.GetLeaveDetailsByPno(employeeePNo);
                List<SpGetEmployeeLeaveInfoByPNo> leaveInfo = leaveBreakDowns.GroupBy(x => x.LeaveTypeId).Select(x => x.First()).Select(x => new SpGetEmployeeLeaveInfoByPNo()
                {
                    AccompanyBy = x.AccompanyBy,
                    EmpLeaveId = x.EmpLeaveId,
                    EmployeeId = x.EmployeeId,
                    ShartName = x.ShartName,
                    FileName = x.FileName,
                    LeaveTypeName = x.LeaveTypeName,
                    LeaveTypeId = x.LeaveTypeId,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    Duration = x.Duration,
                    Remarks = x.Remarks,
                    Slot = x.Slot,
                    Country = x.Country,
                    CreatedDate = x.CreatedDate,
                    ApprovedStatus = x.ApprovedStatus

                }).ToList();

                leaveInfo = leaveInfo.Select(x =>
                {
                    int id = Convert.ToInt32(x.LeaveTypeId);
                    x.SpGetEmployeeLeaveInfoes = leaveBreakDowns.Where(y => y.LeaveTypeId == id).Select(y => new SpGetEmployeeLeaveInfoByPNo()
                    {
                        AccompanyBy = y.AccompanyBy,
                        EmpLeaveId = y.EmpLeaveId,
                        EmployeeId = y.EmployeeId,
                        ShartName = y.ShartName,
                        FileName = y.FileName,
                        LeaveTypeName = y.LeaveTypeName,
                        LeaveTypeId = y.LeaveTypeId,
                        FromDate = y.FromDate,
                        ToDate = y.ToDate,
                        Duration = y.Duration,
                        Remarks = y.Remarks,
                        Slot = y.Slot,
                        Country = y.Country,
                        CreatedDate = y.CreatedDate
                    }).ToList();
                    return x;
                }).ToList();

                return ObjectConverter<SpGetEmployeeLeaveInfoByPNo, SpGetEmployeeLeaveInfoByPNo>.ConvertList(leaveInfo).OrderBy(x => x.LeaveTypeId).ThenBy(x => x.FromDate).ToList();

            }
            else
            {
                throw new Exception("Missed the P No.");
            }

        }
        public async Task<List<SpGetEmployeeLeaveInfoByPNo>> GetEmployeeLeaveDetailsByPNo()
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();

            var leaveBreakDowns = await employeeLeaveRepository.GetLeaveDetailsByPno(employeePNo);
            List<SpGetEmployeeLeaveInfoByPNo> leaveInfo = leaveBreakDowns.GroupBy(x => x.LeaveTypeId).Select(x => x.First()).Select(x => new SpGetEmployeeLeaveInfoByPNo()
            {
                AccompanyBy = x.AccompanyBy,
                EmpLeaveId = x.EmpLeaveId,
                EmployeeId = x.EmployeeId,
                ShartName = x.ShartName,
                FileName = x.FileName,
                LeaveTypeName = x.LeaveTypeName,
                LeaveTypeId = x.LeaveTypeId,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                Duration = x.Duration,
                Remarks = x.Remarks,
                Slot = x.Slot,
                Country = x.Country,
                CreatedDate = x.CreatedDate,
                ApprovedStatus = x.ApprovedStatus


            }).ToList();

            leaveInfo = leaveInfo.Select(x =>
            {
                int id = Convert.ToInt32(x.LeaveTypeId);
                x.SpGetEmployeeLeaveInfoes = leaveBreakDowns.Where(y => y.LeaveTypeId == id).Select(y => new SpGetEmployeeLeaveInfoByPNo()
                {
                    AccompanyBy = y.AccompanyBy,
                    EmpLeaveId = y.EmpLeaveId,
                    EmployeeId = y.EmployeeId,
                    ShartName = y.ShartName,
                    FileName = y.FileName,
                    LeaveTypeName = y.LeaveTypeName,
                    LeaveTypeId = y.LeaveTypeId,
                    FromDate = y.FromDate,
                    ToDate = y.ToDate,
                    Duration = y.Duration,
                    Remarks = y.Remarks,
                    Slot = y.Slot,
                    Country = y.Country,
                    CreatedDate = y.CreatedDate,
                    ApprovedStatus = y.ApprovedStatus
                }).ToList();
                return x;
            }).ToList();

            return ObjectConverter<SpGetEmployeeLeaveInfoByPNo, SpGetEmployeeLeaveInfoByPNo>.ConvertList(leaveInfo).OrderBy(x => x.LeaveTypeId).ThenBy(x => x.FromDate).ToList();
        }

        //public List<EmployeeLeaveModel> GetEmployeeLeaves(int ps, int pn, string qs, int? leaveType, out int total)
        //{
        //    IQueryable<EmployeeLeave> employeeLeave = null;
        //    if (qs != null || leaveType > 0)
        //        employeeLeave = employeeLeaveRepository.FilterWithInclude(x => x.Active && x.LeaveTypeId == leaveType || x.Employee.PNo.Contains(qs), "Employee", "LeaveType");
        //    else
        //        employeeLeave = employeeLeaveRepository.FilterWithInclude(x => x.Active && (x.Employee.PNo == qs || String.IsNullOrEmpty(qs)), "Employee", "LeaveType");

        //    total = employeeLeave.Count();
        //    employeeLeave = employeeLeave.OrderByDescending(x => x.EmployeeId).Skip((pn - 1) * ps).Take(ps);
        //    List<EmployeeLeaveModel> models = ObjectConverter<EmployeeLeave, EmployeeLeaveModel>.ConvertList(employeeLeave.ToList()).ToList();
        //    return models;

        //}

        public async Task<List<LeaveBreakDown>> GetLeaveBreakDowns(int emId)
        {
            var leaveBreakDows = await employeeLeaveRepository.GetLeaveBreakDown(emId);
            return leaveBreakDows.OrderBy(x => x.YearText).ToList();
        }

        //public async Task<EmployeeLeaveModel> SaveEmployeeLeave(int id, EmployeeLeaveModel model)
        //{
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Leave data missing");
        //    }

        //    model.EmployeeId = model.EmployeeId;
        //    string LeaveDueCount = model.LeaveDueCount;
        //    // filter Leave select value

        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();

        //    EmployeeLeave employeeLeave = ObjectConverter<EmployeeLeaveModel, EmployeeLeave>.Convert(model);
        //    employeeLeave.Employee = null;
        //    EmployeeLeave virtualEmloyeeInfo = new EmployeeLeave();
        //    EmpLeaveDetail empLeaveDetail = new EmpLeaveDetail();


        //    string category = model.Employee.EmployeeGeneral.Select(x => x.CategoryId).FirstOrDefault().ToString();


        //    if (category == CodeValue.Deputed_Officer_Type || employeeLeave.LeaveTypeId == CodeValue.Medical_LeaveType)
        //    {
        //        DateTime? DateFrom = employeeLeave.FromDate;
        //        //DateTime ToDate = Convert.ToDateTime(employeeLeave.ToDate);
        //        List<EmployeeLeaveYear> EmployeeLeaveYears = new List<EmployeeLeaveYear>();
        //        for (int j = 0; DateFrom <= employeeLeave.ToDate; j++)
        //        {

        //            EmployeeLeaveYear leaveYear = new EmployeeLeaveYear();
        //            leaveYear.YearText = DateTime.Now.Date.Year;
        //            leaveYear.LeaveDate = (DateTime)DateFrom;
        //            EmployeeLeaveYears.Add(leaveYear);
        //            DateFrom = Convert.ToDateTime(DateFrom).Date.AddDays(1);
        //        }
        //        virtualEmloyeeInfo.EmployeeLeaveYear = EmployeeLeaveYears;
        //    }
        //    else
        //    {
        //        virtualEmloyeeInfo.EmployeeLeaveYear = await GetEmployeeLeaveYearAsync(LeaveDueCount, id, model.Employee.GenderId, model.EmployeeId, model.LeaveTypeId, employeeLeave.FromDate, employeeLeave.ToDate, model.LeaveBalances);

        //    }

        //    if (model.EmployeeId <= 0)
        //    {
        //        throw new InfinityArgumentMissingException("Invalide Officers Pno!");
        //    }
        //    if (id > 0)
        //    {
        //        int EditPermission=db.Database.SqlQuery<int>("select count(el.EmpLeaveId) from  EmployeeLeave el left join EmpLeaveDetail eld on eld.EmpLeaveId=el.EmpLeaveId  where el.EmpLeaveId="+ id + "and (el.ApprovedStatus=1 or eld.ApprovedStatus=1)").SingleOrDefault();
        //        if (EditPermission >0 )
        //        {
        //            throw new InfinityInvalidDataException("Not Eiddited!");
        //        }
        //        employeeLeave = await employeeLeaveRepository.FindOneAsync(x => x.EmpLeaveId == id);

        //        if (employeeLeave == null)
        //        {
        //            throw new InfinityNotFoundException("Employee Leave not found !");
        //        }
        //        employeeLeave.ModifiedDate = DateTime.Now;
        //        //employeeLeave.CreatedDate = model.CreatedDate;
        //        employeeLeave.ModifiedBy = userId;
        //        // All Employee Leave Country Child Delete 
        //        await employeeLeaveCountryRepository.DeleteAsync(x => x.EmpLeaveId == id);
        //        // All Employee Leave Year Child Delete 
        //        await employeeLeaveYearRepository.DeleteAsync(x => x.EmpLeaveId == id);
        //    }
        //    else
        //    {
        //        //IQueryable<EmployeeLeaveYear> employeeLeaveYears = employeeLeaveRepository.FilterWithInclude(x =>x.EmployeeId == model.EmployeeId );
        //        IQueryable<EmployeeLeaveYear> employeeLeaves = employeeLeaveYearRepository.FilterWithInclude(x => x.EmployeeLeave.EmployeeId == model.EmployeeId && x.EmployeeLeave.LeaveTypeId == model.LeaveTypeId);
        //        var isSameDateExist = employeeLeaves.ToList().FirstOrDefault(x => x.LeaveDate.Date == Convert.ToDateTime(model.FromDate).Date || x.LeaveDate.Date == Convert.ToDateTime(model.ToDate).Date);
        //        if (isSameDateExist != null) throw new InfinityArgumentMissingException("Date Already Exist.");
        //        employeeLeave.ApprovedStatus = 3;
        //        employeeLeave.Active = true;
        //        employeeLeave.CreatedDate = DateTime.Now.Date;
        //        employeeLeave.CreatedBy = userId;

        //        empLeaveDetail.CreatedDate = DateTime.Now.Date;
        //        empLeaveDetail.CreatedBy = userId;

        //    }
        //    employeeLeave.EmployeeLeaveYear = virtualEmloyeeInfo.EmployeeLeaveYear;

        //    // Employee Leave Country Value added in Employee Country List
        //    if (model.CountryIds != null) employeeLeave.EmployeeLeaveCountry = model.CountryIds.Select(x => new EmployeeLeaveCountry() { CountryId = x }).ToList();
        //    employeeLeave.EmployeeId = model.EmployeeId;
        //    employeeLeave.LeaveTypeId = model.LeaveTypeId;
        //    employeeLeave.FromDate = model.FromDate;
        //    employeeLeave.ToDate = model.ToDate;
        //    employeeLeave.Duration = model.Duration;
        //    employeeLeave.Remarks = model.Remarks;
        //    employeeLeave.ExBdLeave = model.ExBdLeave;
        //    employeeLeave.AccompanyBy = model.AccompanyBy;
        //    employeeLeave.Purpose = model.Purpose;
        //    model.FileName = model.FileName;
        //    model.FromDate = employeeLeave.FromDate;
        //    model.ToDate = employeeLeave.ToDate;
        //    employeeLeave.BOffCd = Convert.ToInt32(LeaveDueCount);

        //    employeeLeave.RankId = model.Employee.RankId; ;
        //    employeeLeave.TransferId = model.Employee.TransferId;



        //    employeeLeave.Employee = null;
        //    await employeeLeaveRepository.SaveAsync(employeeLeave);
        //    model.EmpLeaveId = employeeLeave.EmpLeaveId;
        //    int reportingOfficeId = model.OfficeAppointmentId;
        //    int? nextReportingOfficeId = null;
        //    bool IsOfficeHead = db.Database.SqlQuery<bool>("select dbo.fnIsOfficeHead(" + model.EmployeeId + ")").SingleOrDefault();
        //    bool IsOfficeSubHead = db.Database.SqlQuery<bool>("select dbo.fnIsOfficeSubHead(" + model.EmployeeId + ")").SingleOrDefault();
        //    bool IsAreaCommender = db.Database.SqlQuery<bool>("select dbo.fnIsAreaCommander(" + model.EmployeeId + ")").SingleOrDefault();
        //    int OfficeId = officeAppointmentRepository.Where(x => x.OffAppId == reportingOfficeId).Select(x => x.OfficeId).FirstOrDefault();
        //    if (IsOfficeHead == true && !IsAreaCommender)
        //    {
        //        nextReportingOfficeId = 10;
        //    }
        //    else if (IsAreaCommender)
        //    {
        //        nextReportingOfficeId = null;
        //    }
        //    else if (IsOfficeSubHead)
        //    {
        //        nextReportingOfficeId = 10;
        //    }
        //    else
        //    {

        //        nextReportingOfficeId = officeAppointmentRepository.Where(x => x.OfficeId == OfficeId && x.OfficeHead == true).Select(x => x.OffAppId).FirstOrDefault();

        //        nextReportingOfficeId = nextReportingOfficeId >= 0 && nextReportingOfficeId != reportingOfficeId  ? nextReportingOfficeId : 10;

        //    }

        //    empLeaveDetail.EmpLeaveId = employeeLeave.EmpLeaveId;
        //    empLeaveDetail.ReportingOfficeId = reportingOfficeId;
        //    empLeaveDetail.NextReportingOfficeId = nextReportingOfficeId;
        //    empLeaveDetail.ApprovedStatus = 3;
        //    await empLeaveDetailRepository.SaveAsync(empLeaveDetail);
        //    return model;
        //}
        public async Task<string> employeeLeaveApprove(int leaveId, int leaveDetailId)
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            int employeeId = db.Database.SqlQuery<int>("select EmployeeId from Employee where PNo=(select PNo from Administration.AspNetUsers where Id='" + userId + "')").SingleOrDefault();
            string pno = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + userId + "'").SingleOrDefault();
            EmpLeaveDetail oldData = empLeaveDetailRepository.Where(x => x.EmpLeaveDetailId == leaveDetailId).FirstOrDefault();
            oldData.ApprovedStatus = 1;
            oldData.ApprovedPNo = pno;
            int? reportingOfficeId = oldData.NextReportingOfficeId;
            int? nextReportingOfficeId = null;
            if (reportingOfficeId == 10)
            {
                nextReportingOfficeId = null;
            }
            else
            {
                nextReportingOfficeId = db.Database.SqlQuery<int>("select ParentId from Office where OfficeId=(select OfficeId from OfficeAppointment where OffAppId=" + reportingOfficeId + ")").SingleOrDefault();
                int count = empLeaveDetailRepository.Where(x => x.EmpLeaveId == leaveId).Count();
                if (count > 0)
                {
                    nextReportingOfficeId = 10;
                }
                else
                {
                    nextReportingOfficeId = db.Database.SqlQuery<int>("select OffAppId from OfficeAppointment where OfficeId=" + nextReportingOfficeId + "and OfficeHead=1").SingleOrDefault();
                }

            }
            EmpLeaveDetail empLeaveDetail = new EmpLeaveDetail
            {
                EmpLeaveId = leaveId,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                ApprovedStatus = 3
            };
            empLeaveDetail.ReportingOfficeId = reportingOfficeId ?? 0;
            empLeaveDetail.NextReportingOfficeId = nextReportingOfficeId;
            await empLeaveDetailRepository.SaveAsync(oldData);
            await empLeaveDetailRepository.SaveAsync(empLeaveDetail);

            return "Approved successfully";

        }
        public async Task<List<object>> leaveTrackingByLeaveId(int leaveId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spLeaveStatusRecordByLeaveId] {0}", leaveId));
            return dataTable.ToJson().ToList();
        }
        private async Task<ICollection<EmployeeLeaveYear>> GetEmployeeLeaveYearAsync(string leaveduecount, int id, int genderid, int employeeId, int leaveTypeId, DateTime? fromDate, DateTime? toDate, List<EmployeeLeaveBalance> leaveBalances)
        {
            List<EmployeeLeaveYear> employeeLeaveYears = new List<EmployeeLeaveYear>();
            int leavedue = Convert.ToInt16(leaveduecount);
            switch (leaveTypeId)
            {
                case CodeValue.PL_LeaveType: // Privileadge Leave Balance Check
                    List<EmployeeLeaveBalance> leaveBalanceList = leaveBalances;

                    leaveBalances = leaveBalances.Where(x => x.IsAssigned == true).ToList();
                    if (!leaveBalances.Any())
                    {
                        throw new InfinityArgumentMissingException("Select Leave Slot");
                    }
                    else
                    {
                        int balanceSum = 0;
                        if (leaveBalanceList != null)
                        {
                            foreach (var item in leaveBalanceList.OrderBy(x => x.LeaveYear))
                            {
                                if (item.IsAssigned != true)
                                    balanceSum += item.Balance;
                                else break;
                            }
                            if (balanceSum != 0)
                            {
                                throw new InfinityArgumentMissingException("Please Select consecutive years");
                            }
                        }
                        // Employee Leave Year Value added in Employee Year List
                        int leaveEntryComplete = 0;

                        DateTime? DateFrom1 = fromDate;

                        foreach (var item in leaveBalances)
                        {
                            int leaveBalanced;
                            int totalLeaveBalanced;
                            int leaveEntryBySlot = 0;
                            int leaveDaysDeclared = (int)(Convert.ToDateTime(toDate).Date - Convert.ToDateTime(fromDate).Date).TotalDays + 1;
                            leaveBalanced = item.Balance;
                            totalLeaveBalanced = (leaveBalances.Sum(x => x.Balance));
                            if (totalLeaveBalanced < leaveDaysDeclared)
                            {
                                throw new InfinityArgumentMissingException("Your Leave Not Available");
                            }
                            for (int i = 0; leaveBalanced > leaveEntryBySlot && toDate >= DateFrom1 && leaveDaysDeclared >= leaveEntryComplete; i++)
                            {

                                EmployeeLeaveYear leaveYear = new EmployeeLeaveYear();
                                leaveYear.YearText = item.LeaveYear;
                                leaveYear.LeaveDate = (DateTime)DateFrom1;
                                employeeLeaveYears.Add(leaveYear);
                                DateFrom1 = Convert.ToDateTime(DateFrom1).Date.AddDays(1);

                                leaveEntryBySlot++;
                                leaveEntryComplete++;
                            }
                        }
                    }
                    break;
                case CodeValue.RL_LeaveType:
                    //DateTime? rlDate = GetRlDate(CodeValue.RL_LeaveType, employeeId, toDate, fromDate);
                    //rlDate = Convert.ToDateTime(rlDate).AddYears(CodeValue.AfterLprYears).Date;
                    DateTime? rlDateFrom = fromDate;
                    int leaveDaysDeclared1 = (int)(Convert.ToDateTime(toDate).Date - Convert.ToDateTime(fromDate).Date).TotalDays + 1;


                    if (leavedue < leaveDaysDeclared1)
                    {
                        throw new InfinityArgumentMissingException("Your Leave Not Available");
                    }
                    for (int i = 0; Convert.ToDateTime(rlDateFrom).Date <= Convert.ToDateTime(toDate).Date; i++)
                    {

                        EmployeeLeaveYear leaveYear = new EmployeeLeaveYear();
                        leaveYear.YearText = DateTime.Now.Date.Year;
                        leaveYear.LeaveDate = (DateTime)rlDateFrom;
                        employeeLeaveYears.Add(leaveYear);
                        rlDateFrom = Convert.ToDateTime(rlDateFrom).Date.AddDays(1);
                    }

                    break;
                case CodeValue.FL_LeaveType:
                    // Furlough Leave Balance Check

                    // List<EmployeeLeaveBalance> leaveDays;
                    DateTime? DateFrom = fromDate;
                    //leaveDays = await employeeLeaveRepository.GetEmployeeRepository(employeeId, leaveTypeId);
                    //int filterBalance = leaveDays.Count > 0 ? (leaveDays.Sum(x => x.Balance)) : 0;
                    int leaveDaysDeclared2 = (int)(Convert.ToDateTime(toDate).Date - Convert.ToDateTime(fromDate).Date).TotalDays + 1;
                    if (leavedue < leaveDaysDeclared2)
                    {
                        throw new InfinityArgumentMissingException("Your Leave Not Available");
                    }
                    for (int i = 0; Convert.ToDateTime(DateFrom).Date <= Convert.ToDateTime(toDate).Date; i++)
                    {
                        EmployeeLeaveYear leaveYear = new EmployeeLeaveYear();
                        leaveYear.YearText = DateTime.Now.Date.Year;
                        leaveYear.LeaveDate = (DateTime)DateFrom;
                        employeeLeaveYears.Add(leaveYear);
                        DateFrom = Convert.ToDateTime(DateFrom).Date.AddDays(1);
                    }
                    break;
                default:
                    if (leaveTypeId == CodeValue.Materny_LeaveType && genderid == CodeValue.Male)
                    {
                        throw new InfinityArgumentMissingException("Your are not able for this leave.");
                    }
                    int leaveDaysDeclared3 = (int)(Convert.ToDateTime(toDate).Date - Convert.ToDateTime(fromDate).Date).TotalDays + 1;
                    DateTime? DateFromcc = fromDate;

                    if (leavedue < leaveDaysDeclared3)
                    {
                        throw new InfinityArgumentMissingException("Your Leave Not Available");
                    }

                    for (int i = 0; Convert.ToDateTime(DateFromcc).Date <= Convert.ToDateTime(toDate).Date; i++)
                    {
                        EmployeeLeaveYear leaveYear = new EmployeeLeaveYear();
                        leaveYear.YearText = DateTime.Now.Date.Year;
                        leaveYear.LeaveDate = (DateTime)DateFromcc;
                        employeeLeaveYears.Add(leaveYear);
                        DateFromcc = Convert.ToDateTime(DateFromcc).Date.AddDays(1);
                    }
                    break;
            }
            return employeeLeaveYears;

        }

        private DateTime? GetRlDate(int rlLeaveType, int EmployeeId, DateTime? employeeLeaveToDate, DateTime? employeeLeaveFromDate)
        {
            var employeeGeneral = employeeGeneralRepository.FindOne(x => x.EmployeeId == EmployeeId);
            LeavePolicy lp = leavePolicyRepository.FindOne(x =>
                x.CommissionTypeId == employeeGeneral.CommissionTypeId && x.LeaveTypeId == rlLeaveType);
            int lpDays = lp.LeaveDurationType == "1" ? lp.LeaveDuration * 30 : lp.LeaveDuration;
            int rlLeaveDays = (int)(Convert.ToDateTime(employeeLeaveToDate).Date - Convert.ToDateTime(employeeLeaveFromDate).Date).TotalDays + 1;
            if (lpDays < rlLeaveDays)
            {
                throw new InfinityArgumentMissingException("Your Leave Not Available");
            }
            var lastRlDate = employeeLeaveRepository.Where(x => x.LeaveTypeId == rlLeaveType && x.EmployeeId == EmployeeId).ToList();

            return lastRlDate.Count() > 0 ? lastRlDate.Max(x => x.ToDate) : employeeGeneral.CategoryId == CodeValue.PromotedOfficer || employeeGeneral.CategoryId == CodeValue.HonoraryOfficer ? employeeGeneral.LastRLAvailedDate
                : employeeGeneral.CommissionDate;
        }
        public async Task<LeaveSummaryModel> GetLeaveSummary(string pno)
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeePNo;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && pno == "undefined")
            {
                employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if (RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && pno != "undefined")
            {
                employeePNo = pno;
            }
            else
            {
                throw new Exception("Some thing went wrong.");
            }
            int employeeStatus = 0;
            employeeStatus = db.Database.SqlQuery<int>("Select top(1) EmployeeStatusId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
            if (employeeStatus != 1)
            {
                throw new InfinityNotFoundException("Officer is not active.");
            }
            if (employeePNo != null)
            {
                try
                {
                    int employeeId = db.Database.SqlQuery<int>("Select top(1) EmployeeId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
                    LeaveSummaryModel leaveSummaryModel = new LeaveSummaryModel();
                    List<EmployeeLeaveBalance> totaleaveBalance = await employeeLeaveRepository.GetTotalEmployeeRepository(employeePNo);
                    if (!totaleaveBalance.Any()) return leaveSummaryModel;
                    int maternyLeaveGet = employeeLeaveRepository.Count(x => x.EmployeeId == employeeId &&  x.LeaveTypeId == CodeValue.Materny_LeaveType);
                    int flLeaveGet = employeeLeaveRepository.Where(x => x.EmployeeId == employeeId && x.LeaveTypeId == CodeValue.FL_LeaveType).Sum(x => x.Duration) ?? 0;
                    DateTime commisionDate = (DateTime)totaleaveBalance.Select(x => x.CommissionDate).FirstOrDefault();
                    int yy = ((int)(DateTime.Now - commisionDate).TotalDays / 365);
                    int fltotal = totaleaveBalance.Where(x => x.LeaveTypeId == CodeValue.FL_LeaveType).Sum(x => x.Balance);
                    int empid = totaleaveBalance.Select(x => x.EmployeeId).FirstOrDefault();
                    leaveSummaryModel.PlAvailable = totaleaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.PL_LeaveType).Select(x => x.Balance).FirstOrDefault();
                    //leaveSummaryModel.TotalPlAvailable = totaleaveBalance.Where(x => x.LeaveTypeId == CodeValue.PL_LeaveType).Sum(x => x.Balance);
                    var total = await employeeLeaveRepository.GetEmployeeDueRepository(empid, CodeValue.PL_LeaveType);
                    leaveSummaryModel.TotalPlAvailable = total.Sum(x => x.Balance);
                    leaveSummaryModel.LastRlAvailed = GetRlDateForSummery(CodeValue.RL_LeaveType, empid);
                    if (leaveSummaryModel.LastRlAvailed != null)
                    {
                        leaveSummaryModel.RlDue = Convert.ToDateTime(leaveSummaryModel.LastRlAvailed).AddYears(CodeValue.AfterLprYears);
                        leaveSummaryModel.IsTrue = Convert.ToDateTime(leaveSummaryModel.RlDue).Date < DateTime.Now.Date;
                    }
                    leaveSummaryModel.RecreationLeaveDue = leaveSummaryModel.RlDue < DateTime.Now.Date ? totaleaveBalance.Where(x => x.LeaveTypeId == CodeValue.RL_LeaveType).Select(x => x.TotalLeave).LastOrDefault() : 0;

                    leaveSummaryModel.CasualLeave = totaleaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.CL_LeaveType).Select(x => x.Balance).FirstOrDefault();
                    leaveSummaryModel.SickLeave = totaleaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.Sick_LeaveType).Select(x => x.Balance).FirstOrDefault();
                    leaveSummaryModel.MedicalLeave = totaleaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.Medical_LeaveType).Select(x => x.Balance).FirstOrDefault();
                    leaveSummaryModel.TotalFurloughLeave = (yy * 30) > fltotal ? fltotal - flLeaveGet : (yy * 30) - flLeaveGet;
                    leaveSummaryModel.TerminalLeave = totaleaveBalance.Where(x => x.LeaveTypeId == CodeValue.Terminal_LeaveType).Select(x => x.Balance).FirstOrDefault();
                    leaveSummaryModel.MaternyLeave = totaleaveBalance.Where(x => x.LeaveTypeId == CodeValue.Materny_LeaveType).Count() > maternyLeaveGet ? totaleaveBalance.Where(x => x.LeaveTypeId == CodeValue.Materny_LeaveType).Select(x => x.TotalLeave).FirstOrDefault() : 0;
                    leaveSummaryModel.SurveyLeave = totaleaveBalance.Where(x => x.LeaveTypeId == CodeValue.Survey_LeaveType).Select(x => x.Balance).FirstOrDefault();
                    leaveSummaryModel.WoundLeave = totaleaveBalance.Where(x => x.LeaveTypeId == CodeValue.Wonded_LeaveType).Select(x => x.Balance).FirstOrDefault();

                    return leaveSummaryModel;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("Missed the P No.");
            }
        }

        private DateTime? GetRlDateForSummery(int rlLeaveType, int employeeid)
        {
            var employeeGeneral = employeeGeneralRepository.FindOne(x => x.EmployeeId == employeeid);
            //Select* from EmployeeLeave where EmployeeId = 2523 and LeaveTypeId = 6
            var lastRlDate = db.Database.SqlQuery<EmployeeLeaveData>("Select * from EmployeeLeave where EmployeeId = " + employeeid + " and LeaveTypeId = " + rlLeaveType).ToList();


            //employeeLeaveRepository.Where(x => x.LeaveTypeId == rlLeaveType && x.Employee.EmployeeId == employeeid).ToList();

            DateTime? date = lastRlDate.Count() != 0 ? lastRlDate.Max(x => x.FromDate) : employeeGeneral.CategoryId == CodeValue.PromotedOfficer || employeeGeneral.CategoryId == CodeValue.HonoraryOfficer ? employeeGeneral.LastRLAvailedDate
                : employeeGeneral.CommissionDate;
            return date;
        }

        public async Task<string> GetEmployeeLeaveDue(int employeeId, int leaveType)
        {
            if (employeeId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalide Officers Pno!");
            }
            //await employeeLeaveRepository.GetTotalEmployeeRepository(pno);
            Employee employee = employeeRepository.FindOne(x => x.EmployeeId == employeeId);
            List<EmployeeLeaveBalance> totalLeaveBalance = await employeeLeaveRepository.GetTotalEmployeeRepository(employee.PNo);
            //List<EmployeeLeaveBalance> totalLeaveBalance = await employeeLeaveRepository.GetEmployeeDueRepository(employeeId, leaveType);
            //////////////////
            int maternyLeaveGet = employeeLeaveRepository.Count(x => x.EmployeeId == employeeId && x.LeaveTypeId == CodeValue.Materny_LeaveType);
            int flLeaveGet = employeeLeaveRepository.Where(x => x.EmployeeId == employeeId && x.LeaveTypeId == CodeValue.FL_LeaveType).Sum(x => x.Duration) ?? 0;
            DateTime commisionDate = (DateTime)totalLeaveBalance.Select(x => x.CommissionDate).FirstOrDefault();
            int yy = ((int)(DateTime.Now - commisionDate).TotalDays / 365);
            int fltotal = totalLeaveBalance.Where(x => x.LeaveTypeId == CodeValue.FL_LeaveType).Sum(x => x.Balance);


            //leaveSummaryModel.PlAvailable = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.PL_LeaveType).Select(x => x.Balance).FirstOrDefault();
            //leaveSummaryModel.TotalPlAvailable = totalLeaveBalance.Where(x => x.LeaveTypeId == CodeValue.PL_LeaveType).Sum(x => x.Balance);
            //leaveSummaryModel.LastRlAvailed = (DateTime)GetRlDateForSummery(CodeValue.RL_LeaveType, pno);
            //leaveSummaryModel.RlDue = leaveSummaryModel.LastRlAvailed.AddYears(CodeValue.AfterLprYears);
            //leaveSummaryModel.RecreationLeaveDue = leaveSummaryModel.RlDue < DateTime.Now.Date ? totaleaveBalance.Where(x => x.LeaveTypeId == CodeValue.RL_LeaveType).Select(x => x.TotalLeave).LastOrDefault() : 0;



            string result = null;
            switch (leaveType)
            {
                case CodeValue.PL_LeaveType:
                    result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.PL_LeaveType).Select(x => x.Balance).FirstOrDefault().ToString();
                    break;
                case CodeValue.RL_LeaveType:
                    DateTime rldate = (DateTime)GetRlDateForSummery(CodeValue.RL_LeaveType, employeeId);
                    DateTime rldue = rldate.AddYears(CodeValue.AfterLprYears);
                    result = rldue < DateTime.Now.Date ? totalLeaveBalance.Where(x => x.LeaveTypeId == CodeValue.RL_LeaveType).Select(x => x.TotalLeave).LastOrDefault().ToString() : "0";
                    break;
                case CodeValue.CL_LeaveType:
                    result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.CL_LeaveType).Select(x => x.Balance).FirstOrDefault().ToString();
                    break;
                case CodeValue.Sick_LeaveType:
                    result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.Sick_LeaveType).Select(x => x.Balance).FirstOrDefault().ToString();
                    break;
                case CodeValue.Medical_LeaveType:
                    result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.Medical_LeaveType).Select(x => x.Balance).FirstOrDefault().ToString();
                    break;
                case CodeValue.FL_LeaveType:
                    result = (yy * 30) > fltotal ? (fltotal - flLeaveGet).ToString() : ((yy * 30) - flLeaveGet).ToString();
                    break;
                case CodeValue.Terminal_LeaveType:
                    result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.Terminal_LeaveType).Select(x => x.Balance).FirstOrDefault().ToString();
                    break;
                case CodeValue.Materny_LeaveType:
                    result = totalLeaveBalance.Where(x => x.LeaveTypeId == CodeValue.Materny_LeaveType).Count() > maternyLeaveGet ? totalLeaveBalance.Where(x => x.LeaveTypeId == CodeValue.Materny_LeaveType).Select(x => x.TotalLeave).FirstOrDefault().ToString() : "0";
                    break;
                case CodeValue.Survey_LeaveType:
                    result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.Survey_LeaveType).Select(x => x.Balance).FirstOrDefault().ToString();
                    break;
                case CodeValue.Wonded_LeaveType:
                    result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year && x.LeaveTypeId == CodeValue.Wonded_LeaveType).Select(x => x.Balance).FirstOrDefault().ToString();
                    break;
            }
            return result;

            ///////////////
            //totalLeaveBalance = totalLeaveBalance.Where(x => x.LeaveYear <= DateTime.Now.Year).ToList();
            //int maternyLeaveGet = employeeLeaveRepository.Count(x => x.EmployeeId == employeeId && x.LeaveTypeId == CodeValue.Materny_LeaveType);
            //string result = null;
            //switch (leaveType)
            //{
            //    case CodeValue.PL_LeaveType:
            //        result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year).Select(x => x.Balance).FirstOrDefault().ToString();
            //        break;
            //    case CodeValue.CL_LeaveType:
            //        result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year).Select(x => x.Balance).FirstOrDefault().ToString();
            //        break;
            //    case CodeValue.Sick_LeaveType:
            //        result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year).Select(x => x.Balance).FirstOrDefault().ToString();
            //        break;
            //    case CodeValue.Medical_LeaveType:
            //        result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year).Select(x => x.Balance).FirstOrDefault().ToString();
            //        break;
            //    case CodeValue.FL_LeaveType:
            //        result = totalLeaveBalance.Sum(x => x.Balance).ToString();
            //        break;
            //    case CodeValue.Terminal_LeaveType:
            //        result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year).Select(x => x.Balance).FirstOrDefault().ToString();
            //        break;
            //    case CodeValue.Materny_LeaveType:
            //        result = totalLeaveBalance.Count() > maternyLeaveGet ? totalLeaveBalance.Select(x => x.TotalLeave).FirstOrDefault().ToString() : "0";
            //        break;
            //    case CodeValue.Survey_LeaveType:
            //        result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year).Select(x => x.Balance).FirstOrDefault().ToString();
            //        break;
            //    case CodeValue.Wonded_LeaveType:
            //        result = totalLeaveBalance.Where(x => x.LeaveYear == DateTime.Now.Year).Select(x => x.Balance).FirstOrDefault().ToString();
            //        break;
            //}
            //return result;
        }

        public async Task<List<EmployeeLeaveBalance>> GetDefaultEmployeeLeaveBalance(int employeeId, int leaveType, int fromDate)
        {
            if (fromDate < 0)
            {
                throw new InfinityArgumentMissingException("Invalide Date!");
            }
            List<EmployeeLeaveBalance> totalLeaveBalance = await employeeLeaveRepository.GetEmployeeRepository(employeeId, leaveType);
            //int slot = totalLeaveBalance.Select(x => x.Slot).FirstOrDefault();
            //List<EmployeeLeaveBalance> filterLeaveBalance = totalLeaveBalance.OrderByDescending(x => x.LeaveYear).Take(slot).ToList();
            return totalLeaveBalance.Where(x => x.LeaveYear == (fromDate - 2) || x.LeaveYear == (fromDate - 1) || x.LeaveYear == (fromDate) || x.LeaveYear == (fromDate + 1) || x.LeaveYear == (fromDate + 2)).OrderBy(x => x.LeaveYear).ToList();
        }


        public int CheckUserOfficeHead(string id)
        {
            int userCurrentAppoinmentId = db.Database.SqlQuery<int>("select dbo.[fnLastAppointmentFinder]('" + id + "')").SingleOrDefault();
            return userCurrentAppoinmentId;
        }

        public List<object> GetEmployeeLeavesForAuthorizedUser(int ps, int pn, string qs, int? leaveType)
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            int userCurrentAppoinmentId = CheckUserOfficeHead(userId);
            DataTable dataTable;
            if (qs != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetEmployeeLeaveDataWithPaging] {0},{1},{2},{3},{4},'{5}'", userCurrentAppoinmentId, 3, ps, pn, leaveType, qs));
            }
            else
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetEmployeeLeaveDataWithPaging] {0},{1},{2},{3},{4}", userCurrentAppoinmentId, 3, ps, pn, leaveType));
            }


            return dataTable.ToJson().ToList();

        }

        public List<object> GetEmployeeApprovedLeavesForAuthorizedUser(int ps, int pn, string qs, int? leaveType)
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            int reportingOfficeId = CheckUserOfficeHead(userId);
            DataTable dataTable;
            if (reportingOfficeId == 0)
            {
                throw new Exception("You Are not allowed to view any Leave data here........!");
            }
            else
            {
                if (qs != null)
                {
                    dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetEmployeeLeaveDataWithPaging] {0},{1},{2},{3},{4},'{5}'", reportingOfficeId, 1, ps, pn, leaveType, qs));
                }
                else
                {
                    dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetEmployeeLeaveDataWithPaging] {0},{1},{2},{3},{4}", reportingOfficeId, 1, ps, pn, leaveType));
                }
                //spGetEmployeeLeaveDataWithPaging 51,1,50,1,null,null
                //dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetEmployeeLeaveDataWithPaging] {0},{1},{2},{3},'{4}',{5}",reportingOfficeId,1,ps,pn,qs,leaveType));
            }


            return dataTable.ToJson().ToList();
        }

        public List<object> GetEmployeeLeaveStatusData(int empLeaveId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetEmployeeLeaveStatusDatas] {0}", empLeaveId));
            return dataTable.ToJson().ToList();
        }

        public List<object> GetLeavesForNs(string pno, int? leaveType)
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            DataTable dataTable;
            if (leaveType != 0)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetLeaveInfoByUserId] '{0}','{1}',{2}", userId, pno, leaveType));
            }
            else
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetLeaveInfoByUserId] '{0}','{1}'", userId, pno));
            }
            return dataTable.ToJson().ToList();
        }

        public List<object> GetEmployeeLeaveStatusDatabyIndividualStatusId(int empLeaveId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetLeaveInfoByIndividualLeaveId] {0}", empLeaveId));
            return dataTable.ToJson().ToList();
        }

        public string ApproveEmployeeLeaveByNs(int empLeaveId, string remark)
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string userRole = db.Database.SqlQuery<string>("select top(1) Remarks from NSOfficeMapping where USerId='"+userId+"'").SingleOrDefault();
            string msg;
            if(userRole == "Ping Desk ")
            {
                //update EmployeeLeave set PingDeskApproveStatus = 1, PingDeskRemark = 'remarks'where EmpLeaveId = 56578
                var query = $"update EmployeeLeave set PingDeskApproveStatus = 1, PingDeskRemark = '" + remark + "' where EmpLeaveId = " + empLeaveId;
                int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                msg = "Succesfully Approved";
            }
            else if (userRole == "Nomination Desk")
            {
                //update EmployeeLeave set PingDeskApproveStatus = 1, PingDeskRemark = 'remarks'where EmpLeaveId = 56578
                var query = $"update EmployeeLeave set NominationApproveStatus = 1, NominationRemark ='"+ remark + "' where EmpLeaveId = " + empLeaveId;
                int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                msg = "Succesfully Approved";
            }
            else
            {
                var query = $"update EmployeeLeave set ApprovedStatus=1 where EmpLeaveId="+empLeaveId;
                int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);

                var query1 = $"update EmpLeaveDetail set ApprovedStatus=1, ModifiedDate=GETDATE(), Remarks= '"+remark+"' where EmpLeaveId="+empLeaveId+" and ReportingOfficeId=10 and NextReportingOfficeId is null";
                int noOfRowUpdated1 = db.Database.ExecuteSqlCommand(query1);
                msg = "Succesfully Approved";
            }
            return msg;
        }

        public string CancelEmployeeLeaveByNs(int empLeaveId, string remark)
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string userRole = db.Database.SqlQuery<string>("select top(1) Remarks from NSOfficeMapping where USerId='" + userId + "'").SingleOrDefault();
            string msg;
            if (userRole == "Ping Desk ")
            {
                //update EmployeeLeave set PingDeskApproveStatus = 1, PingDeskRemark = 'remarks'where EmpLeaveId = 56578
                var query = $"update EmployeeLeave set PingDeskApproveStatus = 0, PingDeskRemark = '" + remark + "' where EmpLeaveId = " + empLeaveId;
                int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                msg = "cancelled Succesfully";
            }
            else if (userRole == "Nomination Desk")
            {
                //update EmployeeLeave set PingDeskApproveStatus = 1, PingDeskRemark = 'remarks'where EmpLeaveId = 56578
                var query = $"update EmployeeLeave set NominationApproveStatus = 0, NominationRemark ='" + remark + "' where EmpLeaveId = " + empLeaveId;
                int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                msg = "cancelled Succesfully";
            }
            else
            {
                var query = $"update EmployeeLeave set ApprovedStatus=2 where EmpLeaveId=" + empLeaveId;
                int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);

                var query1 = $"update EmpLeaveDetail set ApprovedStatus=2, ModifiedDate=GETDATE(), Remarks= '" + remark + "' where EmpLeaveId=" + empLeaveId + " and ReportingOfficeId=10 and NextReportingOfficeId is null";
                int noOfRowUpdated1 = db.Database.ExecuteSqlCommand(query1);
                msg = "cancelled Succesfully";
            }
            return msg;
        }

        public string GetEmployeeLeaveNotifications()
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string userRole = db.Database.SqlQuery<string>("select top(1) Remarks from NSOfficeMapping where USerId='" + userId + "'").SingleOrDefault();
            List<object> list = new List<object>();
            if (userRole != null)
            {
                list = GetLeavesForNs(null, 0);
            }
            else
            {
                list = GetEmployeeLeavesForAuthorizedUser(1000, 1, null, 0);
            }

                
            string a= Convert.ToString(list.Count());
            return a;
        }

        public string CancelEmployeeLeave(int empLeaveId, string remark)
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            int empLeaveID = db.Database.SqlQuery<int>("select top(1) EmpLeaveId from EmpLeaveDetail where EmpLeaveDetailId="+empLeaveId).SingleOrDefault();
            string pno = (db.Database.SqlQuery<string>("select top(1)PNo from Administration.AspNetUsers where Id='"+userId+"'").SingleOrDefault());

            string msg;
            if(empLeaveID == null)
            {
                throw new Exception("some thing Went wrong......!");
            }
            try
            {
                var query = $"update EmployeeLeave set ApprovedStatus=2 where EmpLeaveId=" + empLeaveID;
                int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);

                var query1 = $"update EmpLeaveDetail set ApprovedStatus=2, ApprovedPNo="+ pno + ", Remarks='"+ remark +"' where EmpLeaveDetailId="+ empLeaveId;
                db.Database.ExecuteSqlCommand(query1);
                msg = "cancelled Succesfully";
            }
            catch
            {
                throw new Exception("some thing Went wrong......!");
            }
            return msg;            
        }

        public List<object> GetCurrentEmployeeInformation()
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            int employeeId = db.Database.SqlQuery<int>("select EmployeeId from Employee where PNo=(select PNo from Administration.AspNetUsers where Id='" + userId + "')").SingleOrDefault();
            bool IsOfficeHead = db.Database.SqlQuery<bool>("select dbo.fnIsOfficeHead(" + employeeId + ")").SingleOrDefault();
            bool IsOfficeSubHead = db.Database.SqlQuery<bool>("select dbo.fnIsOfficeSubHead(" + employeeId + ")").SingleOrDefault();
            bool IsAreaCommender = db.Database.SqlQuery<bool>("select dbo.fnIsAreaCommander(" + employeeId + ")").SingleOrDefault();
            DataTable dataTable=null;
            if (IsOfficeHead == true || IsOfficeSubHead == true || IsAreaCommender == true)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [FindEmployeeListForUserId] '{0}'", userId));
                return dataTable.ToJson().ToList();
            }
            else
            {
                List<object> list = new List<object>();
                list.Add(1);
                return list;
            }

        }

        public List<EmployeeLeaveModel> GetEmployeeLeaves(int ps, int pn, string qs, int? leaveType, out int total)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeLeaveModel> GetEmployeeLeave(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeLeaveModel> SaveEmployeeLeave(int v, EmployeeLeaveModel model)
        {
            throw new NotImplementedException();
        }

        //public Task<LeaveSummaryModel> GetLeaveSummary(string pno)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> GetEmployeeLeaveDue(int employeeId, int leaveType)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
