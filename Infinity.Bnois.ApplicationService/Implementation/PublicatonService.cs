using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   public class PublicationService: IPublicationService
    {

        private readonly IBnoisRepository<Publication> publicationRepository;
        public PublicationService(IBnoisRepository<Publication> publicationRepository)
        {
            this.publicationRepository = publicationRepository;
        }

        //public List<PublicationModel> GetPublications(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<Publication> publications = publicationRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.PublicationCategory.Name.Contains(qs) || String.IsNullOrEmpty(qs))), "PublicationCategory");
        //    total = publications.Count();
	       // publications = publications.OrderByDescending(x => x.PublicationId).Skip((pn - 1) * ps).Take(ps);
        //    List<PublicationModel> models = ObjectConverter<Publication, PublicationModel>.ConvertList(publications.ToList()).ToList();
        //    return models;
        //}

        public async Task<PublicationModel> GetPublication(int id)
        {
            if (id <= 0)
            {
                return new PublicationModel();
            }
            Publication publication = await publicationRepository.FindOneAsync(x => x.PublicationId == id);
            if (publication == null)
            {
                throw new InfinityNotFoundException("Publication not found");
            }
            PublicationModel model = ObjectConverter<Publication, PublicationModel>.Convert(publication);
            return model;
        }

    
        public async Task<PublicationModel> SavePublication(int id, PublicationModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Publication data missing");
            }

            bool isExistData = publicationRepository.Exists(x => x.PublicationCategoryId == model.PublicationCategoryId && x.Name == model.Name && x.PublicationId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Publication publication = ObjectConverter<PublicationModel, Publication>.Convert(model);
            if (id > 0)
            {
	            publication = await publicationRepository.FindOneAsync(x => x.PublicationId == id);
                if (publication == null)
                {
                    throw new InfinityNotFoundException("Publication not found !");
                }

	            publication.ModifiedDate = DateTime.Now;
	            publication.ModifiedBy = userId;
            }
            else
            {
	            publication.IsActive = true;
	            publication.CreatedDate = DateTime.Now;
	            publication.CreatedBy = userId;
            }
	        publication.Name = model.Name;
	        publication.ShortName = model.ShortName;
	        publication.PublicationCategoryId = model.PublicationCategoryId;
            await publicationRepository.SaveAsync(publication);
            model.PublicationId = publication.PublicationId;
            return model;
        }


        public async Task<bool> DeletePublication(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Publication publication = await publicationRepository.FindOneAsync(x => x.PublicationId == id);
            if (publication == null)
            {
                throw new InfinityNotFoundException("Publication not found");
            }
            else
            {
                return await publicationRepository.DeleteAsync(publication);
            }
        }


        public async Task<List<SelectModel>> GetPublicationSelectModelsByPublicationCategory(int id)
        {
            ICollection<Publication> publications = await publicationRepository.FilterAsync(x => x.IsActive && x.PublicationCategoryId == id);
            List<SelectModel> selectModels = publications.OrderBy(x => x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.PublicationId
            }).ToList();
            return selectModels;

        }

        public async Task<List<SelectModel>> GetPublicationSelectModels()
        {
            ICollection<Publication> publications = await publicationRepository.FilterAsync(x => x.IsActive );
            List<SelectModel> selectModels = publications.OrderBy(x=>x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.PublicationId
            }).ToList();
            return selectModels;

        }

        public List<PublicationModel> GetPublications(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
