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
    public class InstituteService : IInstituteService
    {
        private readonly IBnoisRepository<Institute> instituteRepository;
        public InstituteService(IBnoisRepository<Institute> instituteRepository)
        {
            this.instituteRepository = instituteRepository;
        }
        
        public async Task<InstituteModel> GetInstitute(int id)
        {
            if (id <= 0)
            {
                return new InstituteModel();
            }
            Institute institute = await instituteRepository.FindOneAsync(x => x.InstituteId == id);
            if (institute == null)
            {
                throw new InfinityNotFoundException("Institute not found");
            }
            InstituteModel model = ObjectConverter<Institute, InstituteModel>.Convert(institute);
            return model;
        }

        //public List<InstituteModel> Institutes(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<Institute> institutes = instituteRepository.FilterWithInclude(x => x.IsActive&& (x.Name.Contains(qs) || String.IsNullOrEmpty(qs) || (x.Board.Name.Contains(qs) || String.IsNullOrEmpty(qs))),"Board");
        //    total = institutes.Count();
        //    institutes = institutes.OrderByDescending(x => x.InstituteId).Skip((pn - 1) * ps).Take(ps);
        //    List<InstituteModel> models = ObjectConverter<Institute, InstituteModel>.ConvertList(institutes.ToList()).ToList();
        //    models = models.Select(x =>
        //    {
        //        x.BoardTypeName= Enum.GetName(typeof(BoardType), x.BoardType);
        //        return x;
        //    }).ToList();
        //    return models;
        //}

        public async Task<InstituteModel> SaveInstitute(int id, InstituteModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Institute data missing");
            }

            bool isExistData = instituteRepository.Exists(x => x.Name == model.Name &&x.BoardId==model.BoardId && x.BoardType == model.BoardType && x.InstituteId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Institute institute = ObjectConverter<InstituteModel, Institute>.Convert(model);
            if (id > 0)
            {
                institute = await instituteRepository.FindOneAsync(x => x.InstituteId == id);
                if (institute == null)
                {
                    throw new InfinityNotFoundException("Institute not found !");
                }
                institute.ModifiedDate = DateTime.Now;
                institute.ModifiedBy = userId;
            }
            else
            {
                institute.IsActive = true;
                institute.CreatedDate = DateTime.Now;
                institute.CreatedBy = userId;
            }
            institute.Name = model.Name;
            institute.BoardType = model.BoardType;
            institute.Remarks = model.Remarks;
            await instituteRepository.SaveAsync(institute);
            model.InstituteId = institute.InstituteId;
            return model;
        }
        public async Task<bool> DeleteInstitute(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Institute institute = await instituteRepository.FindOneAsync(x => x.InstituteId == id);
            if (institute == null)
            {
                throw new InfinityNotFoundException("Institute not found");
            }
            else
            {
                return await instituteRepository.DeleteAsync(institute);
            }
        }

        public List<SelectModel> getBoardTypeSelectModel()
        {
            List<SelectModel> selectModels =
                 Enum.GetValues(typeof(BoardType)).Cast<BoardType>()
                      .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                      .ToList();
            return selectModels;
        }

        public async Task<List<SelectModel>> GetInstitutesSelectModelByBoard(long? boardId)
        {
            IQueryable<Institute> query = instituteRepository.FilterWithInclude(x => x.IsActive && x.BoardId == boardId);
            List<Institute> institutes = await query.OrderBy(x => x.Name).ToListAsync();
            List<SelectModel> instituteSelectModels = institutes.Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.InstituteId
            }).ToList();
            return instituteSelectModels;
        }
        public async Task<List<SelectModel>> GetInstitutesSelectModels()
        {
            IQueryable<Institute> query = instituteRepository.FilterWithInclude(x => x.IsActive );
            List<Institute> institutes = await query.OrderBy(x => x.Name).ToListAsync();
            List<SelectModel> instituteSelectModels = institutes.Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.InstituteId
            }).ToList();
            return instituteSelectModels;
        }

        public List<InstituteModel> Institutes(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}