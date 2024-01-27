using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IBasicSearchService
	{
		List<SelectModel> GetColumnFilterSelectModels();
	    Task<List<SelectModel>> GetColumnDisplaySelectModels();
	    BasicSearchModel SearchOfficers(BasicSearchModel model);

	    bool SaveCheckedValue(bool check, string value, string userId);
	    bool DeleteCheckedColumn();
    }
}