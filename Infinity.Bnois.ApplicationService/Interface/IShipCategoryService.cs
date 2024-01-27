using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IShipCategoryService
    {
        List<ShipCategoryModel> GetShipCategories(int ps, int pn, string qs, out int total);
        Task<ShipCategoryModel> GetShipCategory(int id);
        Task<ShipCategoryModel> SaveShipCategory(int v, ShipCategoryModel model);
        Task<bool> DeleteShipCategory(int id);
        Task<List<SelectModel>> GetShipCategorySelectModels();
    }
}
