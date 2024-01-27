using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ICountryService
    {
        List<CountryModel> GetCountries(int pageSize, int pageNumber, string queryText, out int total);
        Task<CountryModel> GetCountry(int countryId);
        Task<CountryModel> SaveCountry(int countryId, CountryModel model);
        Task<bool> DeleteCountry(int countryId);
        Task<List<SelectModel>> GetCountrySelectModels();
		Task<List<SelectModel>> GetCountriesTypeSelectModel();
	}
}
