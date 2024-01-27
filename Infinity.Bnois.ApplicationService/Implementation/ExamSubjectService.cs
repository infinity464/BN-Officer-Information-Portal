using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class ExamSubjectService : IExamSubjectService
    {
        private readonly IBnoisRepository<ExamSubject> examSubjectRepository;
        public ExamSubjectService(IBnoisRepository<ExamSubject> examSubjectRepository)
        {
            this.examSubjectRepository = examSubjectRepository;
        }


        //public List<ExamSubjectModel> GetExamSubjects(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<ExamSubject> queryable = examSubjectRepository.FilterWithInclude(x => x.IsActive && ((x.Examination.Name.Contains(qs) || String.IsNullOrEmpty(qs)) 
        //    || ((x.Examination.ExamCategory.Name.Contains(qs) || String.IsNullOrEmpty(qs))) 
        //    || (x.Subject.Name.Contains(qs) || String.IsNullOrEmpty(qs))), "Subject", "Examination.ExamCategory");
        //    total = queryable.Count();
        //    queryable = queryable.OrderByDescending(x => x.ExamSubjectId).Skip((pn - 1) * ps).Take(ps);
        //    List<ExamSubjectModel> models = ObjectConverter<ExamSubject, ExamSubjectModel>.ConvertList(queryable.ToList()).ToList();
        //    return models;
        //}

        public async Task<ExamSubjectModel> GetExamSubject(int examSubjectId)
        {
            if (examSubjectId <= 0)
            {
                return new ExamSubjectModel();
            }
            ExamSubject examSubject = await examSubjectRepository.FindOneAsync(x => x.ExamSubjectId == examSubjectId, new List<string>() { "Subject", "Examination" });

            if (examSubject == null)
            {
                throw new InfinityNotFoundException("ExamSubject not found!");
            }
            ExamSubjectModel model = ObjectConverter<ExamSubject, ExamSubjectModel>.Convert(examSubject);
            return model;
        }

        public async Task<ExamSubjectModel> SaveExamSubject(int examSubjectId, ExamSubjectModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("ExamSubject data missing!");
            }
            bool isExist = examSubjectRepository.Exists(x => x.ExamSubjectId != model.ExamSubjectId && (x.ExaminationId == model.ExaminationId && x.SubjectId == model.SubjectId));
            if (isExist)
            {
                throw new InfinityInvalidDataException("Exam Subject already Exists !");
            }

            ExamSubject examSubject = ObjectConverter<ExamSubjectModel, ExamSubject>.Convert(model);

            if (examSubjectId > 0)
            {
                examSubject = await examSubjectRepository.FindOneAsync(x => x.ExamSubjectId == examSubjectId);
                if (examSubject == null)
                {
                    throw new InfinityNotFoundException("ExamSubject not found!");
                }
                examSubject.ModifiedDate = DateTime.Now;
                examSubject.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                examSubject.CreatedBy = model.CreatedBy;
                examSubject.CreatedDate = DateTime.Now;
                examSubject.IsActive = true;
            }
            examSubject.ExaminationId = model.ExaminationId;
            examSubject.SubjectId = model.SubjectId;
            await examSubjectRepository.SaveAsync(examSubject);
            model.ExamSubjectId = examSubject.ExamSubjectId;
            return model;
        }

        public async Task<bool> DeleteExamSubject(int examSubjectId)
        {
            if (examSubjectId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ExamSubject examSubject = await examSubjectRepository.FindOneAsync(x => x.ExamSubjectId == examSubjectId);
            if (examSubject == null)
            {
                throw new InfinityNotFoundException("ExamSubject not found!");
            }
            else
            {
                return await examSubjectRepository.DeleteAsync(examSubject);
            }
        }

        public List<ExamSubjectModel> GetExamSubjects(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
