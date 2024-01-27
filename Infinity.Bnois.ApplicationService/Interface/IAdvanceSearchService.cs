using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IAdvanceSearchService
	{
		List<SelectModel> GetColumnFilterSelectModels();
		Task<List<SelectModel>> GetColumnDisplaySelectModels();
        int SearchOfficers(AdvanceSearchModel model,string userId);

        Dictionary<string, dynamic> SearchOfficersResult(string userId);

	    bool ExecuteAdvanceSearch();
	    bool UpdateSeaService();
	    bool UpdateSeaCmdService();
	    bool UpdateSeaServiceDays();
	    bool UpdateSeaServiceYears();

        bool SaveCheckedValue(bool check,string value, string userId);
		bool DeleteCheckedColumn(string userId);
        List<string> SelectCheckedColumn(string userId);

    }
}