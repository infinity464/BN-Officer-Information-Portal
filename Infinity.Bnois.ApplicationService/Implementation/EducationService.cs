using Infinity.Bnois;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity.Ers.ApplicationService
{
    public class EducationService : IEducationService
    {
        private readonly IBnoisRepository<Education> educationRepository;
        private readonly IBnoisRepository<Result> resultRepository;
        private readonly IBnoisRepository<ExamCategory> examCategoryRepository;
        private readonly IBnoisRepository<Employee> employeeRepository;
        private readonly IBnoisRepository<Subject> subjectRepository;
        public EducationService(IBnoisRepository<Employee> employeeRepository,
            IBnoisRepository<ExamCategory> examCategoryRepository,
            IBnoisRepository<Result> resultRepository,
            IBnoisRepository<Education> educationRepository,
            IBnoisRepository<Subject> subjectRepository)
        {
            this.educationRepository = educationRepository;
            this.resultRepository = resultRepository;
            this.examCategoryRepository = examCategoryRepository;
            this.employeeRepository = employeeRepository;
            this.employeeRepository = employeeRepository;
            this.subjectRepository = subjectRepository;
        }

        public List<EducationModel> GetEducations(int employeeId)
        {
            List<Education> educations = educationRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "ExamCategory", "Examination", "Subject", "Result","Board","Institute","ResultGrade").OrderBy(x=>x.PassingYear).ToList();
            List<EducationModel> models = ObjectConverter<Education, EducationModel>.ConvertList(educations.ToList()).ToList();
            return models;
        }
        public async Task<EducationModel> GetEducation(int educationId)
        {
            if (educationId <= 0)
            {
                return new EducationModel();
            }
            Education education = await educationRepository.FindOneAsync(x => x.EducationId == educationId, new List<string> { "ExamCategory" });

            if (education == null)
            {
                throw new InfinityNotFoundException("Education not found!");
            }
            EducationModel model = ObjectConverter<Education, EducationModel>.Convert(education);
            return model;
        }

        public async Task<EducationModel> SaveEducation(int educationId, EducationModel model)
        {
            Result result = await resultRepository.FindOneAsync(x => x.ResultId == model.ResultId);
            CheckResult(result, model);


            if (model == null)
            {
                throw new InfinityArgumentMissingException("Education data missing");
            }
            Education education = ObjectConverter<EducationModel, Education>.Convert(model);

            if (educationId > 0)
            {
                education = await educationRepository.FindOneAsync(x => x.EducationId == educationId);
                if (education == null)
                {
                    throw new InfinityNotFoundException("Education not found!");
                }
                education.ModifiedBy = model.EmployeeId.ToString();
                education.ModifiedDate = DateTime.Now;
            }
            else
            {
                education.CreatedDate = DateTime.Now;
            }
            education.EmployeeId = model.EmployeeId;
            education.ExamCategoryId = model.ExamCategoryId;
            education.ExaminationId = model.ExaminationId;
            education.BoardId = model.BoardId;
            education.InstituteId = model.InstituteId;
            education.Subjectid = model.Subjectid;
            education.Roll = model.Roll;
            education.RegNo = model.RegNo;
            education.ResultId = model.ResultId;
            education.Gpa = model.Gpa;
            education.ResultGradeId = model.ResultGradeId;
            education.Marks = model.Marks;
            education.Distiction = model.Distiction;
            education.IsVarified = model.IsVarified;
            education.ModifiedDate = model.ModifiedDate;
            education.PassingYear = model.PassingYear;
            education.CourseDuration = model.CourseDuration ?? "N/A";
            education.OtherInstituteName = model.OtherInstituteName;
            education.OtherSubjectName = model.OtherSubjectName;
            education.ResultPublishDate = DateTime.Now;
            education.Percentage = model.Percentage;
            if (education.Gpa <= 0.0)
            {
                education.Gpa = GetAvgGpa(result.MinGPA, result.MaxGPA);
                education.ResultText = result.Name;
            }
            else
            {
                education.ResultText = education.Gpa.ToString();
            }
            //string[] dvCodes = new[] { "1", "2", "3" };
            //bool exist = !dvCodes.Contains(result.ResultCode);
            //if (exist)
            //{
            //    result = resultRepository.FindOne(x => (x.MinGPA <= education.Gpa || x.MinGPA >= education.Gpa) && dvCodes.Contains(x.ResultCode));
            //    if (result != null)
            //    {

            //        education.QResultCode = result.ResultCode;
            //    }
            //    else
            //    {
            //        education.QResultCode = "1";
            //    }
            //}
            //else
            //{
            //    education.QResultCode = result.ResultCode;
            //}
            await educationRepository.SaveAsync(education);
            model.EducationId = education.EducationId;
            return model;
        }
       

        public List<SelectModel> GetYearSelectModel()
        {
            int fromYear = PortalConfig.YearFrom;
            int toYear = DateTime.Now.Year;
            List<SelectModel> years = new List<SelectModel>();
            for (int year = fromYear; year <= toYear; year++)
            {
                SelectModel model = new SelectModel();
                model.Text = year.ToString();
                model.Value = year.ToString();
                years.Add(model);
            }
            return years.OrderByDescending(x=>x.Value).ToList();
        }

        public async Task<bool> DeleteEducation(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Education education = await educationRepository.FindOneAsync(x => x.EducationId == id);
            if (education == null)
            {
                throw new InfinityNotFoundException("Education not found");
            }
            else
            {
                return await educationRepository.DeleteAsync(education);
            }
        }


        public List<SelectModel> GetDurationSelectModel()
        {
            List<SelectModel> durations = new List<SelectModel>();
            for (int i = 1; i <= 5; i++)
            {
                SelectModel duration = new SelectModel();
                duration.Text = i.ToString();
                duration.Value = i.ToString();
                durations.Add(duration);
            }
            return durations;
        }

        private double GetAvgGpa(double min, double max)
        {
            return (min + min) / 2;
        }

  

        private void CheckResult(Result result, EducationModel model)
        {
            if (result.ResultCode == "4" || result.ResultCode == "5")
            {
                if (model.Gpa < result.MinGPA || model.Gpa > result.MaxGPA)
                {
                    throw new InfinityInvalidDataException("Please enter valid GPA !");
                }
            }
        }

        public async Task<EducationModel> UpdateEducation(EducationModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Education data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Education education = ObjectConverter<EducationModel, Education>.Convert(model);

            education = await educationRepository.FindOneAsync(x => x.EducationId == model.EducationId);
            if (education == null)
            {
                throw new InfinityNotFoundException("Education not found !");
            }

            if (model.FileName != null)
            {
                education.FileName = model.FileName;
            }

            education.ModifiedDate = DateTime.Now;
            education.ModifiedBy = userId;
            await educationRepository.SaveAsync(education);
            model.EducationId = education.EducationId;
            return model;
        }
    }
}
