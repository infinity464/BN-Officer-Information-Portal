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
    public class BasicSearchService : IBasicSearchService
	{
		private readonly IBnoisRepository<SearchColumn> searchColumnRepository;
		public BasicSearchService(IBnoisRepository<SearchColumn> searchColumnRepository)
		{
			this.searchColumnRepository = searchColumnRepository;

		}

      
        public  List<SelectModel> GetColumnFilterSelectModels()
        {
	        List<SelectModel> items = new List<SelectModel>()
	        {
		        new SelectModel {Value = 1, Text = "Area"},
		        new SelectModel {Value = 2, Text = "Admin Authority"},
		        new SelectModel {Value = 3, Text = "Branch"},
	            new SelectModel {Value = 4, Text = "Sub Branch"},
                new SelectModel {Value = 5, Text = "Civil Education"},
	            new SelectModel {Value = 6, Text = "Comm Svc Length"},
	            new SelectModel {Value = 7, Text = "Commission Type"},
	            new SelectModel {Value = 8, Text = "Course"},
		        new SelectModel {Value = 9, Text = "Officer's Name"},
	            new SelectModel {Value = 10, Text = "P No"},
                new SelectModel {Value = 11, Text = "Rank"},
	            new SelectModel {Value = 12, Text = "Sea Service"},



		        
		       
	        };

			return items.Select(x => new SelectModel()
            {
                Text = x.Text,
                Value = x.Value
            }).ToList();
        }


	    public async Task<List<SelectModel>> GetColumnDisplaySelectModels()
	    {
	        ICollection<SearchColumn> searchColumns = await searchColumnRepository.FilterAsync(x => x.Id > 0);
	        var query = searchColumns.OrderBy(x => x.Label).ToList();
	        return query.Select(x => new SelectModel()
	        {
	            Text = x.Label,
	            Value = x.ColumnName
	        }).ToList();
	    }

	    public bool SaveCheckedValue(bool check, string value, string userId)
	    {
	        if (check)
	        {
	            string query = String.Format(@"insert into CheckedColumn values ('{0}','{1}')", value, userId);
	            return searchColumnRepository.ExecNoneQuery(query) > 0;

	        }
	        else
	        {
	            string query = String.Format(@"delete from CheckedColumn where  value='{0}' and  userId='{1}'", value, userId);
	            return searchColumnRepository.ExecNoneQuery(query) > 0;

	        }


	    }


	    public BasicSearchModel SearchOfficers(BasicSearchModel model)
	    {


	        return model;
	    }

	    public bool DeleteCheckedColumn()
	    {

	        string query = String.Format(@"truncate table CheckedColumn");

	        return searchColumnRepository.ExecNoneQuery(query) > 0;



	    }



    }
}
