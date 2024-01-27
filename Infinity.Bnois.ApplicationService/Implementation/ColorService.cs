using Infinity.Bnois.ApplicationService.Implementation;
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
    public class ColorService : IColorService
    {
        private readonly IBnoisRepository<Color> colorRepository;

        public ColorService(IBnoisRepository<Color> colorRepository)
        {
            this.colorRepository = colorRepository;
        }

        public async Task<bool> DeleteColor(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Color color = await colorRepository.FindOneAsync(x => x.ColorId == id);
            if (color == null)
            {
                throw new InfinityNotFoundException("Color not found");
            }
            else
            {
                return await colorRepository.DeleteAsync(color);
            }
        }

        public List<SelectModel> GetColorTypeSelectModels()
        {
            List<SelectModel> selectModels = 
                Enum.GetValues(typeof(ColorType)).Cast<ColorType>()
                     .Select(v => new SelectModel{ Text = v.ToString(), Value = Convert.ToInt16(v)})
                     .ToList();
            return selectModels;
        }


        public async Task<ColorModel> GetColor(int id)
        {
            if (id <= 0)
            {
                return new ColorModel();
            }
            Color color = await colorRepository.FindOneAsync(x => x.ColorId == id);
            if (color == null)
            {
                throw new InfinityNotFoundException("Color not found");
            }
            ColorModel model = ObjectConverter<Color, ColorModel>.Convert(color);
            return model;
        }

        public List<ColorModel> GetColors(int ps, int pn, string qs, out int total)
        {
            IQueryable<Color> colors = colorRepository.FilterWithInclude(x => x.IsActive
              && ((x.Name.Contains(qs)
              || String.IsNullOrEmpty(qs))));
            total = colors.Count();
            colors = colors.OrderByDescending(x => x.ColorId).Skip((pn - 1) * ps).Take(ps);
            List<ColorModel> models = ObjectConverter<Color, ColorModel>.ConvertList(colors.ToList()).ToList();
            models = models.Select(x =>
            {
                x.ColorTypeName = Enum.GetName(typeof(ColorType), x.ColorType);
                return x;
            }).ToList();

           
            return models;
        }

        public async Task<ColorModel> SaveColor(int id, ColorModel model)
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Color data missing");
            }
            Color color = ObjectConverter<ColorModel, Color>.Convert(model);
            if (id > 0)
            {
                color = await colorRepository.FindOneAsync(x => x.ColorId == id);
                if (color == null)
                {
                    throw new InfinityNotFoundException("Color not found !");
                }

                color.ModifiedDate = DateTime.Now;
                color.ModifiedBy = userId;
            }
            else
            {
                color.CreatedDate = DateTime.Now;
                color.CreatedBy = userId;
                color.IsActive = true;
            }
            color.Name = model.Name;
            color.ColorType = model.ColorType;
          
            await colorRepository.SaveAsync(color);
            model.ColorId = color.ColorId;
            return model;
        }

        public async Task<List<SelectModel>> GetColorSelectModel(int colorType)
        {

            ICollection<Color> colors = await colorRepository.FilterAsync(x => x.IsActive && x.ColorType==colorType);
            List<SelectModel> selectModels = colors.OrderBy(x => x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.ColorId
            }).ToList();
            return selectModels;
        }
    }
}
