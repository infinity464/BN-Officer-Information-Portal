using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPunishmentAccidentService
    {
        List<PunishmentAccidentModel> GetPunishmentAccidents(int ps, int pn, string qs, out int total);
        Task<PunishmentAccidentModel> GetPunishmentAccident(int id);
        Task<PunishmentAccidentModel> SavePunishmentAccident(int id, PunishmentAccidentModel model);
        Task<bool> DeletePunishmentAccident(int id);
        List<SelectModel> GetAccidentTypeSelectModels();
        List<SelectModel> GetPunishmentAccidentTypeSelectModels();
        Task<bool> ExecutePunishmentProcess();
    }
}
