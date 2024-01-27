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
    public class RelationService : IRelationService
    {
        private readonly IBnoisRepository<Relation> relationRepository;
        public RelationService(IBnoisRepository<Relation> relationRepository)
        {
            this.relationRepository = relationRepository;
        }

    
        public List<RelationModel> GetRelations(int ps, int pn, string qs, out int total)
        {
            IQueryable<Relation> relations = relationRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = relations.Count();
            relations = relations.OrderByDescending(x => x.RelationId).Skip((pn - 1) * ps).Take(ps);
            List<RelationModel> models = ObjectConverter<Relation, RelationModel>.ConvertList(relations.ToList()).ToList();
            return models;
        }
        public async Task<RelationModel> GetRelation(int id)
        {
            if (id <= 0)
            {
                return new RelationModel();
            }
            Relation relation = await relationRepository.FindOneAsync(x => x.RelationId == id);
            if (relation == null)
            {
                throw new InfinityNotFoundException("Relation not found");
            }
            RelationModel model = ObjectConverter<Relation, RelationModel>.Convert(relation);
            return model;
        }

        public async Task<RelationModel> SaveRelation(int id, RelationModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Relation data missing");
            }
            bool isExist = relationRepository.Exists(x => x.Name == model.Name && x.RelationId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Relation relation = ObjectConverter<RelationModel, Relation>.Convert(model);
            if (id > 0)
            {
                relation = await relationRepository.FindOneAsync(x => x.RelationId == id);
                if (relation == null)
                {
                    throw new InfinityNotFoundException("Relation not found !");
                }

                relation.ModifiedDate = DateTime.Now;
                relation.ModifiedBy = userId;
            }
            else
            {
                relation.IsActive = true;
                relation.CreatedDate = DateTime.Now;
                relation.CreatedBy = userId;
            }
            relation.Name = model.Name;
            relation.Remarks = model.Remarks;
        
            await relationRepository.SaveAsync(relation);
            model.RelationId = relation.RelationId;
            return model;
        }

        public async Task<bool> DeleteRelation(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Relation Relation = await relationRepository.FindOneAsync(x => x.RelationId == id);
            if (Relation == null)
            {
                throw new InfinityNotFoundException("Relation not found");
            }
            else
            {
                return await relationRepository.DeleteAsync(Relation);
            }
        }

        public async Task<List<SelectModel>> GetRelationSelectModels()
        {
            ICollection<Relation> relations = await relationRepository.FilterAsync(x => x.IsActive);
            List<Relation> query = relations.OrderBy(x => x.Name).ToList();
            return query.Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.RelationId
            }).ToList();
        }
    }
}
