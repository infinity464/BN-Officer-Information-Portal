using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.Data.Models;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class AdvanceSearchService : IAdvanceSearchService
	{
		private readonly IBnoisRepository<SearchColumn> searchColumnRepository;
		private readonly ISearchRepository searchRepository;
		public AdvanceSearchService(IBnoisRepository<SearchColumn> searchColumnRepository, ISearchRepository searchRepository)
		{
			this.searchColumnRepository = searchColumnRepository;
			this.searchRepository = searchRepository;

		}

        public  List<SelectModel> GetColumnFilterSelectModels()
        {
	        List<SelectModel> items = new List<SelectModel>()
	        {
//		        new SelectModel {Value = 1, Text = "Area"},
		        new SelectModel {Value = 2, Text = "Service Extension"},
		        new SelectModel {Value = 3, Text = "Age"},
		        new SelectModel {Value = 4, Text = "Batch"},
		        new SelectModel {Value = 5, Text = "Branch"},
		        new SelectModel {Value = 6, Text = "Sub Branch"},
		        new SelectModel {Value = 7, Text = "Blood group"},
		        new SelectModel {Value = 8, Text = "Civil Education"},
		        new SelectModel {Value = 9, Text = "Comm Svc Length"},
		        new SelectModel {Value = 10, Text = "Commission Type"},
		        new SelectModel {Value = 11, Text = "Course"},
		        new SelectModel {Value = 12, Text = "Current Status"},
		        new SelectModel {Value = 13, Text = "District (Permanent)"},
		        new SelectModel {Value = 14, Text = "Driving License"},
		        new SelectModel {Value = 15, Text = "Foreign Visit"},
		        new SelectModel {Value = 16, Text = "Gender"},
		        new SelectModel {Value = 17, Text = "Height"},
		        new SelectModel {Value = 18, Text = "Joining Date"},
		        new SelectModel {Value = 19, Text = "LPR Date"},
		        new SelectModel {Value = 20, Text = "Marital Status"},
		        new SelectModel {Value = 21, Text = "Medical Category"},
		        new SelectModel {Value = 22, Text = "Officer Category"},
		        new SelectModel {Value = 23, Text = "Officer Service From"},
		        new SelectModel {Value = 24, Text = "Officer's Name"},
		        new SelectModel {Value = 25, Text = "OPR"},
		        new SelectModel {Value = 26, Text = "Passport"},
		        new SelectModel {Value = 27, Text = "Physical Structure"},
		        new SelectModel {Value = 28, Text = "Promotion Status"},
		        new SelectModel {Value = 29, Text = "Rank"},
		        new SelectModel {Value = 30, Text = "Religion"},
		        new SelectModel {Value = 31, Text = "Retirement Date"},
		        new SelectModel {Value = 32, Text = "Return From Abroad"},
		        new SelectModel {Value = 33, Text = "Self Exam"},
		        new SelectModel {Value = 34, Text = "Sea Service"},
		        new SelectModel {Value = 35, Text = "Subject"},
		        new SelectModel {Value = 36, Text = "Transfer"},
		        new SelectModel {Value = 37, Text = "UN Mission"},
		        new SelectModel {Value = 38, Text = "Freedom Fighter"},
		        new SelectModel {Value = 39, Text = "Punishment"},
		        new SelectModel {Value = 40, Text = "Medal"},
		        new SelectModel {Value = 41, Text = "Award"},
		        new SelectModel {Value = 42, Text = "Publication"},
		   //     new SelectModel {Value = 43, Text = "Commendation"},
		        new SelectModel {Value = 44, Text = "Achievement"},
		        new SelectModel {Value = 45, Text = "EX-BD Leave"},
		        new SelectModel {Value = 46, Text = "Security Clearance"},
		        new SelectModel {Value = 47, Text = "ISSB"},
		        new SelectModel {Value = 48, Text = "Ship Wise State"},
		        new SelectModel {Value = 49, Text = "RL Due Date"},
		        new SelectModel {Value = 50, Text = "Status Change"}

            };

			return items.OrderBy(x=>x.Text).Select(x => new SelectModel()
            {
                Text = x.Text,
                Value = x.Value
            }).ToList();
        }
		public async Task<List<SelectModel>> GetColumnDisplaySelectModels()
		{
			ICollection<SearchColumn> searchColumns = await searchColumnRepository.FilterAsync(x => x.Id > 0);
		    var query = searchColumns.OrderBy(x => x.Label).ToList();
			return query.Select(x => new SelectModel()
            {
                Text = x.Label,
                Value = x.ColumnName
            }).ToList();
        }


		public bool SaveCheckedValue(bool check,string value, string userId)
		{
			if (check)
			{
				string query = String.Format(@"insert into CheckedColumn values ('{0}','{1}')", value, userId);
				return searchColumnRepository.ExecNoneQuery(query) > 0;

			}
			else
			{
				string query = String.Format(@"delete from CheckedColumn where  value='{0}' and  userId='{1}'", value, userId);
				return searchColumnRepository.ExecNoneQuery(query) > 0;

			}

        }


        public List<string> SelectCheckedColumn(string userId)
        {
          return  searchRepository.SelectCheckedColumn(userId);
        }

	    public bool ExecuteAdvanceSearch()
	    {
	      


	        //insert multiple result/Record
	        searchRepository.InsertMultipleResult();

            searchRepository.ExecWithSqlQuery(String.Format("exec [spInsertIntoSearchDataWarehouse]"));
	        return true;
	    }
	 
	    public bool UpdateSeaService()
	    {
	        searchRepository.ExecWithSqlQuery(String.Format("update [dbo].[SearchDataWarehouse] set [SeaService]=dbo.fnGetSeaServiceByEmployeeId(EmployeeId)"));
	        return true;
	    }
	    public bool UpdateSeaCmdService()
	    {
	        searchRepository.ExecWithSqlQuery(String.Format("update [dbo].[SearchDataWarehouse] set [SeaCmdService]=dbo.fnGetCommandServiceDurationByEmployeeId(EmployeeId)"));
	        return true;
	    }
	    public bool UpdateSeaServiceDays()
	    {
	        searchRepository.ExecWithSqlQuery(String.Format("update [dbo].[SearchDataWarehouse] set [SeaServiceDays]=dbo.fnGetSeaServiceDurationInDaysYear(1,EmployeeId)"));
	        return true;
	    }
	    public bool UpdateSeaServiceYears()
	    {
	        searchRepository.ExecWithSqlQuery(String.Format("update [dbo].[SearchDataWarehouse] set [SeaServiceYears]=dbo.fnGetSeaServiceDurationInDaysYear(2,EmployeeId)"));
	        return true;
	    }

        public int SearchOfficers( AdvanceSearchModel model, string userId)
	    {
	        // Delete all search params
	        searchRepository.DeleteAllSearchParams(userId);

            //Insert Single Search Param
            searchRepository.SaveSingleParams(userId,model.Gender,model.FromAge,model.ToAge,model.DrivingLicense,model.Passport,model.JoiningFromDate,model.JoiningToDate,model.LprFromDate,model.LprToDate,model.MaritalStatus,model.RetirementFromDate,model.RetirementToDate,
	            model.CommissionFromDate,model.CommissionToDate,model.CommissionFromDuration,model.CommissionToDuration,model.CommissionService,model.CommissionDurationType,model.OfficerName,model.FromFt,model.FromIn,model.ToFt,model.ToIn,model.SeaServiceType,model.NoOfOfficer,model.SeaFromYear,model.SeaToYear,
	            model.Mission,model.MissionDate,model.MissionDuration,model.ServiceExamCategory,model.ExamResult,model.ReturnFromAbroad,model.ReturnFromDate,model.ReturnToDate,model.ForeignVisit,model.GoneAbroad,model.FromYear,model.ToYear,model.Result,model.CourseFromDate,model.CourseToDate,model.FromResult,model.ToResult,
		        model.OprCount,model.LastValue,model.OprAverage,model.OprCheck,model.OprGrade,model.TransferArea,model.ShipType,model.DoneMission,model.MissionTime,model.BeforeDate,model.ServingType,model.HodService,model.CommandService,model.TransferFromDate,model.TransferToDate,model.TransferMode,
	            model.ServedOffice,model.NotServedOffice,model.ServingOffice,model.DoneCourse,model.NotDoneCourse,model.DoingCourse,model.NotServedHodService,model.NotServedCommandService,model.FreedomFighter,model.Issb,model.IssbResult,model.OprToGrade,model.FromBatch,model.ToBatch,model.TransferNo,model.ServingHodService,
	            model.ServingCommandService,model.PromotionFromDate,model.PromotionToDate,model.ServiceExtension,model.RlFromDate,model.RlToDate,model.StatusType,model.VisitFromDate,model.VisitToDate,model.LeaveFromDate,model.LeaveToDate,model.GpaFrom,model.GpaTo);

            //Insert Multiple Dropdown Selected Item

	        if (model.DoingCourseCountriesSelected.Length > 0)
	        {
	            foreach (var item in model.DoingCourseCountriesSelected)
	            {
	                searchRepository.SaveDoingCourseCountrySelected(item.Value, userId);
	            }
	        }

	        if (model.NotDoneCourseCountriesSelected.Length > 0)
	        {
	            foreach (var item in model.NotDoneCourseCountriesSelected)
	            {
	                searchRepository.SaveNotDoneCourseCountrySelected(item.Value, userId);
	            }
	        }
            if (model.ShipsSelected.Length > 0)
	        {
	            foreach (var item in model.ShipsSelected)
	            {
	                searchRepository.SaveShipSelected(item.Value, userId);
	            }
	        }
	        if (model.EducationSubjectsSelected.Length > 0)
	        {
	            foreach (var item in model.EducationSubjectsSelected)
	            {
	                searchRepository.SaveEducationSubjectSelected(item.Value, userId);
	            }
	        }

	        if (model.ClearancesSelected.Length > 0)
	        {
	            foreach (var item in model.ClearancesSelected)
	            {
	                searchRepository.SaveClearanceSelected(item.Value, userId);
	            }
	        }



            if (model.AwardsSelected.Length > 0)
	        {
	            foreach (var item in model.AwardsSelected)
	            {
	                searchRepository.SaveAwardsSelected(item.Value, userId);
	            }
	        }


	        if (model.MedalsSelected.Length > 0)
	        {
	            foreach (var item in model.MedalsSelected)
	            {
	                searchRepository.SaveMedalsSelected(item.Value, userId);
	            }
	        }

	        if (model.PublicationCategoriesSelected.Length > 0)
	        {
	            foreach (var item in model.PublicationCategoriesSelected)
	            {
	                searchRepository.SavePublicationCategoriesSelected(item.Value, userId);
	            }
	        }



	        if (model.PublicationsSelected.Length > 0)
	        {
	            foreach (var item in model.PublicationsSelected)
	            {
	                searchRepository.SavePublicationsSelected(item.Value, userId);
	            }
	        }


	        if (model.CommendationsSelected.Length > 0)
	        {
	            foreach (var item in model.CommendationsSelected)
	            {
	                searchRepository.SaveCommendationsSelected(item.Value, userId);
	            }
	        }


	        if (model.AppreciationsSelected.Length > 0)
	        {
	            foreach (var item in model.AppreciationsSelected)
	            {
	                searchRepository.SaveAppreciationsSelected(item.Value, userId);
	            }
	        }
	        if (model.LeaveCountriesSelected.Length > 0)
	        {
	            foreach (var item in model.LeaveCountriesSelected)
	            {
	                searchRepository.SaveLeaveCountriesSelected(item.Value, userId);
	            }
	        }







            if (model.AreasSelected.Length > 0)
	        {
	            foreach (var item in model.AreasSelected)
	            {
	                searchRepository.SaveAreasSelected(item.Value, userId);
	            }
	        }


	        if (model.PunishmentCategoriesSelected.Length > 0)
	        {
	            foreach (var item in model.PunishmentCategoriesSelected)
	            {
	                searchRepository.SavePunishmentCategoriesSelected(item.Value, userId);
	            }
	        }

	        if (model.PunishmentSubCategoriesSelected.Length > 0)
	        {
	            foreach (var item in model.PunishmentSubCategoriesSelected)
	            {
	                searchRepository.SavePunishmentSubCategoriesSelected(item.Value, userId);
	            }
	        }



            if (model.TransferAreasSelected.Length > 0)
	        {
	            foreach (var item in model.TransferAreasSelected)
	            {
	                searchRepository.SaveTransferAreaSelected(item.Value, userId);
	            }
	        }


		    if (model.TransferAdminAuthoritiesSelected.Length > 0)
		    {
			    foreach (var item in model.TransferAdminAuthoritiesSelected)
			    {
				    searchRepository.SaveTransferAdminAuthoritySelected(item.Value, userId);
			    }
		    }


			if (model.AreaTypesSelected.Length > 0)
            {
                foreach (var item in model.AreaTypesSelected)
                {
                    searchRepository.SaveAreaTypesSelected(item.Value, userId);
                }
            }


            if (model.TrainingInstitutesSelected.Length > 0)
	        {
	            foreach (var item in model.TrainingInstitutesSelected)
	            {
	                searchRepository.SaveTrainingInstitutesSelected(item.Value, userId);
	            }
	        }


	        if (model.CourseCountriesSelected.Length > 0)
	        {
	            foreach (var item in model.CourseCountriesSelected)
	            {
	                searchRepository.SaveCourseCountriesSelected(item.Value, userId);
	            }
	        }

	        if (model.CoursesDoneSelected.Length > 0)
	        {
	            foreach (var item in model.CoursesDoneSelected)
	            {
	                searchRepository.SaveCoursesDoneSelected(item.Value, userId);
	            }
	        }

	        if (model.CoursesNotDoneSelected.Length > 0)
	        {
	            foreach (var item in model.CoursesNotDoneSelected)
	            {
	                searchRepository.SaveCoursesNotDoneSelected(item.Value, userId);
	            }
	        }

	        if (model.CoursesDoingSelected.Length > 0)
	        {
	            foreach (var item in model.CoursesDoingSelected)
	            {
	                searchRepository.SaveCoursesDoingSelected(item.Value, userId);
	            }
	        }

	        if (model.CourseSubCategoriesDoneSelected.Length > 0)
	        {
	            foreach (var item in model.CourseSubCategoriesDoneSelected)
	            {
	                searchRepository.SaveCourseSubCategoriesDoneSelected(item.Value, userId);
	            }
	        }

	        if (model.CourseSubCategoriesNotDoneSelected.Length > 0)
	        {
	            foreach (var item in model.CourseSubCategoriesNotDoneSelected)
	            {
	                searchRepository.SaveCourseSubCategoriesNotDoneSelected(item.Value, userId);
	            }
	        }

	        if (model.CourseSubCategoriesDoingSelected.Length > 0)
	        {
	            foreach (var item in model.CourseSubCategoriesDoingSelected)
	            {
	                searchRepository.SaveCourseSubCategoriesDoingSelected(item.Value, userId);
	            }
	        }

	        if (model.CourseCategoriesDoneSelected.Length > 0)
	        {
	            foreach (var item in model.CourseCategoriesDoneSelected)
	            {
	                searchRepository.SaveCourseCategoriesDoneSelected(item.Value, userId);
	            }
	        }

	        if (model.CourseCategoriesNotDoneSelected.Length > 0)
	        {
	            foreach (var item in model.CourseCategoriesNotDoneSelected)
	            {
	                searchRepository.SaveCourseCategoriesNotDoneSelected(item.Value, userId);
	            }
	        }

	        if (model.CourseCategoriesDoingSelected.Length > 0)
	        {
	            foreach (var item in model.CourseCategoriesDoingSelected)
	            {
	                searchRepository.SaveCourseCategoriesDoingSelected(item.Value, userId);
	            }
	        }


	        if (model.OprRanksSelected.Length > 0)
	        {
	            foreach (var item in model.OprRanksSelected)
	            {
	                searchRepository.SaveOprRanksSelected(item.Value, userId);
	            }
	        }


	        if (model.OccasionsSelected.Length > 0)
	        {
	            foreach (var item in model.OccasionsSelected)
	            {
	                searchRepository.SaveOccasionsSelected(item.Value, userId);
	            }
	        }

	        if (model.OfficerServicesSelected.Length > 0)
	        {
	            foreach (var item in model.OfficerServicesSelected)
	            {
	                searchRepository.SaveOfficerServicesSelected(item.Value, userId);
	            }
	        }

	        if (model.ReligionCastsSelected.Length > 0)
	        {
	            foreach (var item in model.ReligionCastsSelected)
	            {
	                searchRepository.SaveReligionCastsSelected(item.Value, userId);
	            }
	        }

	        if (model.ReligionsSelected.Length > 0)
	        {
	            foreach (var item in model.ReligionsSelected)
	            {
	                searchRepository.SaveReligionsSelected(item.Value, userId);
	            }
	        }

	        if (model.SubCategoriesSelected.Length > 0)
	        {
	            foreach (var item in model.SubCategoriesSelected)
	            {
	                searchRepository.SaveSubCategoriesSelected(item.Value, userId);
	            }
	        }

	        if (model.CategoriesSelected.Length > 0)
	        {
	            foreach (var item in model.CategoriesSelected)
	            {
	                searchRepository.SaveCategoriesSelected(item.Value, userId);
	            }
	        }

	        if (model.PromotionStatusSelected.Length > 0)
	        {
	            foreach (var item in model.PromotionStatusSelected)
	            {
	                searchRepository.SavePromotionStatusSelected(item.Value, userId);
	            }
	        }

	        if (model.PhysicalStructuresSelected.Length > 0)
	        {
	            foreach (var item in model.PhysicalStructuresSelected)
	            {
	                searchRepository.SavePhysicalStructuresSelected(item.Value, userId);
	            }
	        }

	        if (model.VisitCategoriesSelected.Length > 0)
	        {
	            foreach (var item in model.VisitCategoriesSelected)
	            {
	                searchRepository.SaveVisitCategoriesSelected(item.Value, userId);
	            }
	        }

	        if (model.CountriesSelected.Length > 0)
	        {
	            foreach (var item in model.CountriesSelected)
	            {
	                searchRepository.SaveCountriesSelected(item.Value, userId);
	            }
	        }

	        if (model.SubjectsSelected.Length > 0)
	        {
	            foreach (var item in model.SubjectsSelected)
	            {
	                searchRepository.SaveSubjectsSelected(item.Value, userId);
	            }
	        }

	        if (model.RanksSelected.Length > 0)
	        {
	            foreach (var item in model.RanksSelected)
	            {
	                searchRepository.SaveRanksSelected(item.Value, userId);
	            }
	        }

	        if (model.MedicalCategoriesSelected.Length > 0)
	        {
	            foreach (var item in model.MedicalCategoriesSelected)
	            {
	                searchRepository.SaveMedicalCategoriesSelected(item.Value, userId);
	            }
	        }

	        if (model.DistrictsSelected.Length > 0)
	        {
	            foreach (var item in model.DistrictsSelected)
	            {
	                searchRepository.SaveDistrictsSelected(item.Value, userId);
	            }
	        }

	        if (model.CurrentStatusSelected.Length > 0)
	        {
	            foreach (var item in model.CurrentStatusSelected)
	            {
	                searchRepository.SaveCurrentStatusSelected(item.Value, userId);
	            }
	        }

	        if (model.CommissionTypesSelected.Length > 0)
	        {
	            foreach (var item in model.CommissionTypesSelected)
	            {
	                searchRepository.SaveCommissionTypesSelected(item.Value, userId);
	            }
	        }

	        if (model.InstitutesSelected.Length > 0)
	        {
	            foreach (var item in model.InstitutesSelected)
	            {
	                searchRepository.SaveInstitutesSelected(item.Value, userId);
	            }
	        }

	        if (model.ExamsSelected.Length > 0)
	        {
	            foreach (var item in model.ExamsSelected)
	            {
	                searchRepository.SaveExamsSelected(item.Value, userId);
	            }
	        }

	        if (model.BloodGroupsSelected.Length > 0)
	        {
	            foreach (var item in model.BloodGroupsSelected)
	            {
	                searchRepository.SaveBloodGroupsSelected(item.Value, userId);
	            }
	        }

	        if (model.SubBranchesSelected.Length > 0)
	        {
	            foreach (var item in model.SubBranchesSelected)
	            {
	                searchRepository.SaveSubBranchesSelected(item.Value, userId);
	            }
	        }

	        if (model.BranchesSelected.Length > 0)
	        {
	            foreach (var item in model.BranchesSelected)
	            {
	                searchRepository.SaveBranchesSelected(item.Value, userId);
	            }
	        }

	        if (model.BatchesSelected.Length > 0)
	        {
	            foreach (var item in model.BatchesSelected)
	            {
	                searchRepository.SaveBatchesSelected(item.Value, userId);
	            }
	        }

	        if (model.AdminAuthoritiesSelected.Length > 0)
	        {
	            foreach (var item in model.AdminAuthoritiesSelected)
	            {
	                searchRepository.SaveAdminAuthoritiesSelected(item.Value, userId);
	            }
	        }

	        if (model.VisitCountriesSelected.Length > 0)
	        {
	            foreach (var item in model.VisitCountriesSelected)
	            {
	                searchRepository.SaveVisitCountriesSelected(item.Value, userId);
	            }
	        }

	        if (model.MissionCountriesSelected.Length > 0)
	        {
	            foreach (var item in model.MissionCountriesSelected)
	            {
	                searchRepository.SaveMissionCountriesSelected(item.Value, userId);
	            }
	        }
	        if (model.ResultsSelected.Length > 0)
	        {
	            foreach (var item in model.ResultsSelected)
	            {
	                searchRepository.SaveResultsSelected(item.Value, userId);
	            }
	        }

	        if (model.OfficesSelected.Length > 0)
	        {
	            foreach (var item in model.OfficesSelected)
	            {
	                searchRepository.SaveOfficesSelected(item.Value, userId);
	            }
	        }

	        if (model.AppointmentsSelected.Length > 0)
	        {
	            foreach (var item in model.AppointmentsSelected)
	            {
	                searchRepository.SaveAppointmentsSelected(item.Value, userId);
	            }
	        }
	        if (model.NotServedOfficesSelected.Length > 0)
	        {
	            foreach (var item in model.NotServedOfficesSelected)
	            {
	                searchRepository.SaveNotServedOfficesSelected(item.Value, userId);
	            }
	        }


	        if (model.NotServedAppointmentsSelected.Length > 0)
	        {
	            foreach (var item in model.NotServedAppointmentsSelected)
	            {
	                searchRepository.SaveNotServedAppointmentsSelected(item.Value, userId);
	            }
	        }


	        if (model.NotServedAppointmentsSelected.Length > 0)
	        {
	            foreach (var item in model.NotServedAppointmentsSelected)
	            {
	                searchRepository.SaveNotServedAppointmentsSelected(item.Value, userId);
	            }
	        }


	        if (model.ServingOfficesSelected.Length > 0)
	        {
	            foreach (var item in model.ServingOfficesSelected)
	            {
	                searchRepository.SaveServingOfficesSelected(item.Value, userId);
	            }
	        }

	        if (model.ServingAppointmentsSelected.Length > 0)
	        {
	            foreach (var item in model.ServingAppointmentsSelected)
	            {
	                searchRepository.SaveServingAppointmentsSelected(item.Value, userId);
	            }
	        }


	        if (model.VisitSubCategoriesSelected.Length > 0)
	        {
	            foreach (var item in model.VisitSubCategoriesSelected)
	            {
	                searchRepository.SaveVisitSubCategoriesSelected(item.Value, userId);
	            }
	        }

	        if (model.AppointmentCategoriesSelected.Length > 0)
	        {
	            foreach (var item in model.AppointmentCategoriesSelected)
	            {
	                searchRepository.SaveAppointmentCategoriesSelected(item.Value, userId);
	            }
	        }


            return searchRepository.UpdateSearchResult(userId);	       
	    }
       public Dictionary<string, dynamic> SearchOfficersResult(string userId)
        {
            

            OfficerViewModel obj = searchRepository.GetSearchOfficers(userId);
           
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            foreach (var type in obj.GetOfficerGroupModels)
            {
                if (type.Seniority == null || type.Seniority == "") continue;
                DataTable results = obj.DataTable.AsEnumerable().Where(r => r.Field<int>("Seniority") == Convert.ToInt16(type.Seniority)).CopyToDataTable();
                 results.Columns.RemoveAt(0);
                    keyValues.Add(type.Seniority, results.ToJson().ToList());                  
              
            }
            return keyValues;
        }

        public bool DeleteCheckedColumn(string userId)
		{

		    string query = String.Format(@"delete from CheckedColumn where userId='{0}'",userId);

		    return searchColumnRepository.ExecNoneQuery(query) > 0;



		}





	}
}
