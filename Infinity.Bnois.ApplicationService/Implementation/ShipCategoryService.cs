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
    public class ShipCategoryService : IShipCategoryService
    {
        private readonly IBnoisRepository<ShipCategory> shipCategoryRepository;
        public ShipCategoryService(IBnoisRepository<ShipCategory> shipCategoryRepository)
        {
            this.shipCategoryRepository = shipCategoryRepository;
        }

    
        public List<ShipCategoryModel> GetShipCategories(int ps, int pn, string qs, out int total)
        {
            IQueryable<ShipCategory> shipCategories = shipCategoryRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = shipCategories.Count();
	        shipCategories = shipCategories.OrderByDescending(x => x.ShipCategoryId).Skip((pn - 1) * ps).Take(ps);
            List<ShipCategoryModel> models = ObjectConverter<ShipCategory, ShipCategoryModel>.ConvertList(shipCategories.ToList()).ToList();
            return models;
        }
        public async Task<ShipCategoryModel> GetShipCategory(int id)
        {
            if (id <= 0)
            {
                return new ShipCategoryModel();
            }
            ShipCategory shipCategory = await shipCategoryRepository.FindOneAsync(x => x.ShipCategoryId == id);
            if (shipCategory == null)
            {
                throw new InfinityNotFoundException("Ship Category not found");
            }
            ShipCategoryModel model = ObjectConverter<ShipCategory, ShipCategoryModel>.Convert(shipCategory);
            return model;
        }

        public async Task<ShipCategoryModel> SaveShipCategory(int id, ShipCategoryModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Ship Category data missing");
            }
            bool isExist = shipCategoryRepository.Exists(x => x.Name == model.Name && x.ShipCategoryId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ShipCategory shipCategory = ObjectConverter<ShipCategoryModel, ShipCategory>.Convert(model);
            if (id > 0)
            {
	            shipCategory = await shipCategoryRepository.FindOneAsync(x => x.ShipCategoryId == id);
                if (shipCategory == null)
                {
                    throw new InfinityNotFoundException("Ship Category not found !");
                }

	            shipCategory.ModifiedDate = DateTime.Now;
	            shipCategory.ModifiedBy = userId;
            }
            else
            {
	            shipCategory.IsActive = true;
	            shipCategory.CreatedDate = DateTime.Now;
	            shipCategory.CreatedBy = userId;
            }
	        shipCategory.Name = model.Name;
	        shipCategory.Priority = model.Priority;
	        shipCategory.ShortName = model.ShortName;
	        shipCategory.Purpose = model.Purpose;
        
            await shipCategoryRepository.SaveAsync(shipCategory);
            model.ShipCategoryId = shipCategory.ShipCategoryId;
            return model;
        }

        public async Task<bool> DeleteShipCategory(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ShipCategory shipCategory = await shipCategoryRepository.FindOneAsync(x => x.ShipCategoryId == id);
            if (shipCategory == null)
            {
                throw new InfinityNotFoundException("Ship Category not found");
            }
            else
            {
                return await shipCategoryRepository.DeleteAsync(shipCategory);
            }
        }

        public async Task<List<SelectModel>> GetShipCategorySelectModels()
        {
            ICollection<ShipCategory> shipCategories = await shipCategoryRepository.FilterAsync(x => x.IsActive);
            return shipCategories.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.ShipCategoryId
            }).ToList();
        }
    }
}
