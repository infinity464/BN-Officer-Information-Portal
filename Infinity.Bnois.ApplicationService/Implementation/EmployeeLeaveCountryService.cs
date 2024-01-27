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
	public class EmployeeLeaveCountryService : IEmployeeLeaveCountryService
	{
		private readonly IBnoisRepository<EmployeeLeaveCountry> employeeLeaveCountryRepository;
		public EmployeeLeaveCountryService(IBnoisRepository<EmployeeLeaveCountry> employeeLeaveCountryRepository)
		{
			this.employeeLeaveCountryRepository = employeeLeaveCountryRepository;
		}

		public async Task<EmployeeLeaveCountryModel> SaveEmployeeLeaveCountry(int id, EmployeeLeaveCountryModel employeeLeaveCountryModel, int empLeaveId)
		{
			if (employeeLeaveCountryModel == null)
			{
				throw new InfinityArgumentMissingException("Employee Leave Country missing");
			}

			string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
			EmployeeLeaveCountry employeeLeave = ObjectConverter<EmployeeLeaveCountryModel, EmployeeLeaveCountry>.Convert(employeeLeaveCountryModel);
			if (id > 0)
			{
				await employeeLeaveCountryRepository.DeleteAsync(x => x.EmpLeaveId == empLeaveId);


			}

			employeeLeave.CountryId = employeeLeaveCountryModel.CountryId;
			employeeLeave.EmpLeaveId = empLeaveId;

			await employeeLeaveCountryRepository.SaveAsync(employeeLeave);			
			return employeeLeaveCountryModel;
		}
	}
}
