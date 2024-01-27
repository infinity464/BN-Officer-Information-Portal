using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface ISubjectService
	{
		List<SubjectModel> GetSubjects(int ps, int pn, string qs, out int total);
		Task<SubjectModel> GetSubjects(int id);
		Task<SubjectModel> SaveSubject(int v, SubjectModel model);
		Task<bool> DeleteSubject(int id);
        Task<List<SelectModel>> GetSubjectSelectModels();
        Task<List<SelectModel>> FilterSubjects(string searchStr);
        Task<List<SelectModel>> GetSubjectsSelectModelByExamination(int? examinationId);
    }
}
