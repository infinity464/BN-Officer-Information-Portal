using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IColorService
    {
        List<ColorModel> GetColors(int ps, int pn, string qs, out int total);
        Task<ColorModel> GetColor(int id);
        Task<ColorModel> SaveColor(int id, ColorModel model);
        Task<bool> DeleteColor(int id);
        List<SelectModel> GetColorTypeSelectModels();
        Task<List<SelectModel>> GetColorSelectModel(int colorType);
    }
}
