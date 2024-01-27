using Infinity.Bnois;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IMaritalTypeService
    {
        List<MaritalTypeModel> GetMaritalTypes(int pageSize, int pageNumber, string searchText, out int total);
        Task<MaritalTypeModel> GetMaritalType(int maritalTypeId);
        Task<MaritalTypeModel> SaveMaritalType(int maritalTypeId, MaritalTypeModel model);
        Task<bool> DeleteMaritalType(int maritalTypeId);
        Task<List<SelectModel>> GetMaritalTypeSelectModels();
    }
}
