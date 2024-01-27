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
    public class ExtraAppointmentService : IExtraAppointmentService
    {
        private readonly IBnoisRepository<ExtraAppointment> ExtraAppointmentRepository;

        public ExtraAppointmentService(IBnoisRepository<ExtraAppointment> ExtraAppointmentRepository)
        {
            this.ExtraAppointmentRepository = ExtraAppointmentRepository;
        }

        //public List<ExtraAppointmentModel> GetExtraAppointments(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<ExtraAppointment> extraAppointments = ExtraAppointmentRepository.FilterWithInclude(x => x.IsActive
        //        && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "Transfer", "Office", "OfficeAppointment");
        //    total = extraAppointments.Count();
        //    extraAppointments = extraAppointments.OrderByDescending(x => x.ExtraAppointmentId).Skip((pn - 1) * ps).Take(ps);
        //    List<ExtraAppointmentModel> models = ObjectConverter<ExtraAppointment, ExtraAppointmentModel>.ConvertList(extraAppointments.ToList()).ToList();
        //    return models;
        //}

        public async Task<ExtraAppointmentModel> GetExtraAppointment(int id)
        {
            if (id <= 0)
            {
                return new ExtraAppointmentModel();
            }
            ExtraAppointment extraAppointment = await ExtraAppointmentRepository.FindOneAsync(x => x.ExtraAppointmentId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch","Office" });
            if (extraAppointment == null)
            {
                throw new InfinityNotFoundException("Extra Appointment not found");
            }
            ExtraAppointmentModel model = ObjectConverter<ExtraAppointment, ExtraAppointmentModel>.Convert(extraAppointment);
            return model;
        }

       

        public async Task<ExtraAppointmentModel> SaveExtraAppointment(int id, ExtraAppointmentModel model)
        {
           
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Extra Appointment  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ExtraAppointment extraAppointment = ObjectConverter<ExtraAppointmentModel, ExtraAppointment>.Convert(model);
            if (id > 0)
            {
                extraAppointment = await ExtraAppointmentRepository.FindOneAsync(x => x.ExtraAppointmentId == id);
                if (extraAppointment == null)
                {
                    throw new InfinityNotFoundException("Extra Appointment not found !");
                }

                extraAppointment.ModifiedDate = DateTime.Now;
                extraAppointment.ModifiedBy = userId;
            }
            else
            {
                extraAppointment.IsActive = true;
                extraAppointment.CreatedDate = DateTime.Now;
                extraAppointment.CreatedBy = userId;
            }
            extraAppointment.EmployeeId = model.EmployeeId;
            extraAppointment.TransferId = model.Employee.TransferId;
            extraAppointment.OfficeId = model.OfficeId;
            extraAppointment.AppointmentId = model.AppointmentId;
            extraAppointment.AssignDate = model.AssignDate ?? extraAppointment.AssignDate;
            extraAppointment.EndDate = model.EndDate;
            extraAppointment.Remarks = model.Remarks;
            //extraAppointment.Employee = null;

            await ExtraAppointmentRepository.SaveAsync(extraAppointment);
            model.ExtraAppointmentId = extraAppointment.ExtraAppointmentId;


            return model;
        }


        public async Task<bool> DeleteExtraAppointment(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ExtraAppointment extraAppointment = await ExtraAppointmentRepository.FindOneAsync(x => x.ExtraAppointmentId == id);
            if (extraAppointment == null)
            {
                throw new InfinityNotFoundException("Extra Appointment not found");
            }

            return await ExtraAppointmentRepository.DeleteAsync(extraAppointment); ;
           
        }

        public List<ExtraAppointmentModel> GetExtraAppointments(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
