using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IRelationService
    {
        List<RelationModel> GetRelations(int ps, int pn, string qs, out int total);
        Task<RelationModel> GetRelation(int id);
        Task<RelationModel> SaveRelation(int v, RelationModel model);
        Task<bool> DeleteRelation(int id);
        Task<List<SelectModel>> GetRelationSelectModels();
    }
}
