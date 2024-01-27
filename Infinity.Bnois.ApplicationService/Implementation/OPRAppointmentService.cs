using System;
using System.Collections.Generic;
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
	public class OPRAppointmentService : IOPRAppointmentService
	{
		private readonly IBnoisRepository<OprAptSuitability> OPRAppointmentRepository;
		public OPRAppointmentService(IBnoisRepository<OprAptSuitability> OPRAppointmentRepository)
		{
			this.OPRAppointmentRepository = OPRAppointmentRepository;
		}



        public List<OprAptSuitabilityModel> GetOPRAppointments(int id)
        {
            List<OprAptSuitability> OPRAppointments = OPRAppointmentRepository.FilterWithInclude(x => x.EmployeeOprId == id , "SpecialAptType","Suitability").ToList();
            List<OprAptSuitabilityModel> models = ObjectConverter<OprAptSuitability, OprAptSuitabilityModel>.ConvertList(OPRAppointments.ToList()).ToList();
            return models;

        }



        public async Task<OprAptSuitabilityModel> SaveOPRAppointment(int id, OprAptSuitabilityModel model)
        {
           
            if (model == null)
            {
                throw new InfinityArgumentMissingException("OPR Special Appointment data missing");
            }
            bool isExist = OPRAppointmentRepository.Exists(x => x.EmployeeOprId == model.EmployeeOprId && x.SpecialAptTypeId==model.SpecialAptTypeId && x.SuitabilityId == model.SuitabilityId && x.Id != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }


            OprAptSuitability OPRAppointment = ObjectConverter<OprAptSuitabilityModel, OprAptSuitability>.Convert(model);
            if (id > 0)
            {
                OPRAppointment = await OPRAppointmentRepository.FindOneAsync(x => x.Id == id);
                if (OPRAppointment == null)
                {
                    throw new InfinityNotFoundException("OPR Special Appointment not found !");
                }
               
            }
           
            OPRAppointment.EmployeeOprId = model.EmployeeOprId;
            OPRAppointment.SpecialAptTypeId = model.SpecialAptTypeId;
            OPRAppointment.SuitabilityId = model.SuitabilityId;
            
            OPRAppointment.Note = model.Note;
            await OPRAppointmentRepository.SaveAsync(OPRAppointment);
            model.Id = OPRAppointment.Id;
            return model;
        }


	 
        public async Task<bool> DeleteOPRAppointment(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            OprAptSuitability OPRAppointment = await OPRAppointmentRepository.FindOneAsync(x => x.Id == id);
            if (OPRAppointment == null)
            {
                throw new InfinityNotFoundException("OPR Special Appointment not found");
            }
            else
            {
                return await OPRAppointmentRepository.DeleteAsync(OPRAppointment);
            }
        }

    }
}
