using AutoMapper;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Office = Infinity.Bnois.Data.Office;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModelMapper'
    public partial class ModelMapper
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModelMapper'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModelMapper.SetUp()'
        public static void SetUp()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModelMapper.SetUp()'
        {
            Mapper.Initialize(cfg =>
            {
                //*****************Data to API*********************

                cfg.CreateMap<Configuration.Models.Module, Configuration.ServiceModel.ModuleModel>();
                cfg.CreateMap<Configuration.Models.Feature, Configuration.ServiceModel.FeatureModel>()
                .ForMember(d => d.Module, opt => opt.MapFrom(s => s.Module));

      //          cfg.CreateMap<RankCategory, RankCategoryModel>();
      //          cfg.CreateMap<Rank, RankModel>();
      //          cfg.CreateMap<RankMap, RankMapModel>();
      //          //.ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank))
      //          //.ForMember(d => d.Rank1, opt => opt.MapFrom(s => s.Rank1))
      //          //.ForMember(d => d.Rank2, opt => opt.MapFrom(s => s.Rank2));

      //          cfg.CreateMap<Color, ColorModel>();
      //          cfg.CreateMap<LeaveType, LeaveTypeModel>();

      //          cfg.CreateMap<EyeVision, EyeVisionModel>();
      //          cfg.CreateMap<BloodGroup, BloodGroupModel>();
      //          cfg.CreateMap<MedicalCategory, MedicalCategoryModel>();
      //          cfg.CreateMap<PhysicalStructure, PhysicalStructureModel>();

      //          cfg.CreateMap<ExamCategory, ExamCategoryModel>();
      //          cfg.CreateMap<Examination, ExaminationModel>();

      //          cfg.CreateMap<InstituteType, InstituteTypeModel>();
      //          cfg.CreateMap<Institute, InstituteModel>()
      //          //.ForMember(d => d.Board, opt => opt.MapFrom(s => s.Board));

      //          cfg.CreateMap<Board, BoardModel>();
      //          cfg.CreateMap<MemberRole, MemberRoleModel>();

      //          cfg.CreateMap<ExamSubject, ExamSubjectModel>();
      //          //.ForMember(d => d.Examination, opt => opt.MapFrom(s => s.Examination));

      //          cfg.CreateMap<Result, ResultModel>();

      //          cfg.CreateMap<ExtracurricularType, ExtracurricularTypeModel>();

      //          cfg.CreateMap<PhysicalCondition, PhysicalConditionModel>();
      //          //.ForMember(d => d.Color, opt => opt.MapFrom(s => s.Color))
      //          //.ForMember(d => d.Color1, opt => opt.MapFrom(s => s.Color1))
      //          //.ForMember(d => d.Color2, opt => opt.MapFrom(s => s.Color2))
      //          //.ForMember(d => d.EyeVision, opt => opt.MapFrom(s => s.EyeVision))
      //          //.ForMember(d => d.PhysicalStructure, opt => opt.MapFrom(s => s.PhysicalStructure))
      //          //.ForMember(d => d.MedicalCategory, opt => opt.MapFrom(s => s.MedicalCategory));

      //          cfg.CreateMap<Education, EducationModel>();
      //          //.ForMember(d => d.ExamCategory, opt => opt.MapFrom(s => s.ExamCategory))
      //          //.ForMember(d => d.Examination, opt => opt.MapFrom(s => s.Examination))
      //          //.ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject))
      //          //.ForMember(d => d.Result, opt => opt.MapFrom(s => s.Result))
      //          //.ForMember(d => d.Board, opt => opt.MapFrom(s => s.Board));

      //          cfg.CreateMap<Address, AddressModel>();
      //          //.ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //          //.ForMember(d => d.Division, opt => opt.MapFrom(s => s.Division))
      //          //.ForMember(d => d.District, opt => opt.MapFrom(s => s.District))
      //          //  .ForMember(d => d.Upazila, opt => opt.MapFrom(s => s.Upazila));



      //          cfg.CreateMap<SocialAttribute, SocialAttributeModel>();
      //          cfg.CreateMap<Sport, SportModel>();

      //          cfg.CreateMap<Extracurricular, ExtracurricularModel>();
      //          //.ForMember(d => d.ExtracurricularType, opt => opt.MapFrom(s => s.ExtracurricularType));

      //          cfg.CreateMap<PromotionNomination, PromotionNominationModel>();
      //          //.ForMember(d => d.PromotionBoard, opt => opt.MapFrom(s => s.PromotionBoard))
      //          //.ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //          //.ForMember(d => d.ExecutionRemark, opt => opt.MapFrom(s => s.ExecutionRemark));

      //          cfg.CreateMap<Parent, ParentModel>();
      //          //.ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //          //.ForMember(d => d.Nationality, opt => opt.MapFrom(s => s.Nationality))
      //          //.ForMember(d => d.Religion, opt => opt.MapFrom(s => s.Religion))
      //          //  .ForMember(d => d.ReligionCast, opt => opt.MapFrom(s => s.ReligionCast))
      //          //    .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation));

      //          cfg.CreateMap<Category, CategoryModel>();
      //          cfg.CreateMap<SubCategory, SubCategoryModel>();
      //          //.ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category));
      //          cfg.CreateMap<CommissionType, CommissionTypeModel>();
      //          cfg.CreateMap<Branch, BranchModel>();
      //          cfg.CreateMap<SubBranch, SubBranchModel>();
      //          cfg.CreateMap<OfficerStream, OfficerStreamModel>();
      //          cfg.CreateMap<Batch, BatchModel>();
      //          cfg.CreateMap<ExecutionRemark, ExecutionRemarkModel>();
      //          cfg.CreateMap<Country, CountryModel>();
      //          cfg.CreateMap<MaritalType, MaritalTypeModel>();
      //          cfg.CreateMap<Subject, SubjectModel>();
      //          cfg.CreateMap<Religion, ReligionModel>();

      //          cfg.CreateMap<ReligionCast, ReligionCastModel>();
      //          //.ForMember(d => d.Religion, opt => opt.MapFrom(s => s.Religion));
      //          cfg.CreateMap<Nationality, NationalityModel>();

      //          cfg.CreateMap<Gender, GenderModel>();
      //          cfg.CreateMap<OfficerType, OfficerTypeModel>();
      //          cfg.CreateMap<EmployeeStatus, EmployeeStatusModel>();

      //          cfg.CreateMap<Employee, EmployeeModel>();
      //          //.ForMember(d => d.Batch, opt => opt.MapFrom(s => s.Batch))
      //          //.ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender))
      //          //.ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank))
      //          //.ForMember(d => d.RankCategory, opt => opt.MapFrom(s => s.RankCategory))
      //          //.ForMember(d => d.OfficerType, opt => opt.MapFrom(s => s.OfficerType))
      //          //.ForMember(d => d.EmployeeStatus, opt => opt.MapFrom(s => s.EmployeeStatus));

      //          cfg.CreateMap<EmployeeGeneral, EmployeeGeneralModel>();
      //          //.ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
      //          //.ForMember(d => d.SubCategory, opt => opt.MapFrom(s => s.SubCategory))
      //          //.ForMember(d => d.Branch, opt => opt.MapFrom(s => s.Branch))
      //          //.ForMember(d => d.SubBranch, opt => opt.MapFrom(s => s.SubBranch))
      //          //.ForMember(d => d.CommissionType, opt => opt.MapFrom(s => s.CommissionType))
      //          //.ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject))
      //          //.ForMember(d => d.Nationality, opt => opt.MapFrom(s => s.Nationality))
      //          //       .ForMember(d => d.MaritalType, opt => opt.MapFrom(s => s.MaritalType))
      //          //    .ForMember(d => d.Religion, opt => opt.MapFrom(s => s.Religion))
      //          //    .ForMember(d => d.ReligionCast, opt => opt.MapFrom(s => s.ReligionCast));

      //          cfg.CreateMap<EmployeeOther, EmployeeOtherModel>();
      //          //.ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<Division, DivisionModel>();
      //          cfg.CreateMap<District, DistrictModel>();
      //          //.ForMember(d => d.Division, opt => opt.MapFrom(s => s.Division));

      //          cfg.CreateMap<Upazila, UpazilaModel>();
      //          //.ForMember(d => d.District, opt => opt.MapFrom(s => s.District));

      //          cfg.CreateMap<TerminationType, TerminationTypeModel>();

      //          cfg.CreateMap<AgeServicePolicy, AgeServicePolicyModel>();
      //          //.ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
      //          //.ForMember(d => d.SubCategory, opt => opt.MapFrom(s => s.SubCategory))
      //          //.ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank));

      //          cfg.CreateMap<RetiredAge, RetiredAgeModel>();
      //          //.ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
      //          //.ForMember(d => d.SubCategory, opt => opt.MapFrom(s => s.SubCategory))
      //          //.ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank));




      //          cfg.CreateMap<EmployeeServiceExt, EmployeeServiceExtensionModel>();
      //              //.ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<CourseCategory, CourseCategoryModel>();
      //          cfg.CreateMap<CourseSubCategory, CourseSubCategoryModel>()
      //              //.ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory));

      //          cfg.CreateMap<EmployeeLeave, EmployeeLeaveModel>()
      //              //.ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              //.ForMember(d => d.LeaveType, opt => opt.MapFrom(s => s.LeaveType));


      //          cfg.CreateMap<EmpRunMissing, EmpRunMissingModel>()
      //              //.ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<EmpRejoin, EmpRejoinModel>()
      //              //.ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              //.ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank));

      //          cfg.CreateMap<ResultType, Result>();

      //          cfg.CreateMap<Course, CourseModel>()
      //              //.ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory))
      //              //.ForMember(d => d.CourseSubCategory, opt => opt.MapFrom(s => s.CourseSubCategory))
      //              //.ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));


      //          cfg.CreateMap<TrainingInstitute, TrainingInstituteModel>()
      //              //.ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));

      //          cfg.CreateMap<TrainingPlan, TrainingPlanModel>()
      //              //.ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory))
      //              //.ForMember(d => d.CourseSubCategory, opt => opt.MapFrom(s => s.CourseSubCategory))
      //              //.ForMember(d => d.Course, opt => opt.MapFrom(s => s.Course))
      //              //.ForMember(d => d.TrainingInstitute, opt => opt.MapFrom(s => s.TrainingInstitute))
      //              //.ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));


      //          cfg.CreateMap<TrainingResult, TrainingResultModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.ResultType, opt => opt.MapFrom(s => s.ResultType))
      //              .ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory))
      //              .ForMember(d => d.CourseSubCategory, opt => opt.MapFrom(s => s.CourseSubCategory))
      //              .ForMember(d => d.TrainingPlan, opt => opt.MapFrom(s => s.TrainingPlan));

      //          cfg.CreateMap<Occupation, OccupationModel>();
      //          cfg.CreateMap<ResultGrade, ResultGradeModel>();

      //          cfg.CreateMap<Sibling, SiblingModel>()
      //              .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<Spouse, SpouseModel>()
      //              .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<PreCommissionRank, PreCommissionRankModel>();

      //          cfg.CreateMap<PreCommissionCourse, PreCommissionCourseModel>();
      //          cfg.CreateMap<PreCommissionCourseDetail, PreCommissionCourseDetailModel>();

      //          cfg.CreateMap<Relation, RelationModel>();
      //          cfg.CreateMap<HeirType, HeirTypeModel>();

      //          cfg.CreateMap<HeirNextOfKinInfo, HeirNextOfKinInfoModel>()
      //              .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender))
      //              .ForMember(d => d.Relation, opt => opt.MapFrom(s => s.Relation))
      //              .ForMember(d => d.HeirType, opt => opt.MapFrom(s => s.HeirType));
      //          cfg.CreateMap<AptCat, AptCatModel>();
      //          cfg.CreateMap<AptNat, AptNatModel>();
      //          cfg.CreateMap<Zone, ZoneModel>();
      //          cfg.CreateMap<ShipCategory, ShipCategoryModel>();
      //          cfg.CreateMap<Pattern, PatternModel>();

      //          cfg.CreateMap<Office, OfficeModel>()
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //              .ForMember(d => d.Country1, opt => opt.MapFrom(s => s.Country1))
      //              .ForMember(d => d.Zone, opt => opt.MapFrom(s => s.Zone))
      //              .ForMember(d => d.Pattern, opt => opt.MapFrom(s => s.Pattern))
      //              .ForMember(d => d.ShipCategory, opt => opt.MapFrom(s => s.ShipCategory));
      //          cfg.CreateMap<LprCalculateInfo, LprCalculateInfoModel>();

      //          cfg.CreateMap<OfficeAppointment, OfficeAppointmentModel>()
      //              .ForMember(d => d.AptCat, opt => opt.MapFrom(s => s.AptCat))
      //              .ForMember(d => d.AptNat, opt => opt.MapFrom(s => s.AptNat))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office));

      //          cfg.CreateMap<Medal, MedalModel>();
      //          cfg.CreateMap<Award, AwardModel>();
      //          cfg.CreateMap<PublicationCategory, PublicationCategoryModel>();

      //          cfg.CreateMap<Publication, PublicationModel>()
      //              .ForMember(d => d.PublicationCategory, opt => opt.MapFrom(s => s.PublicationCategory));


      //          cfg.CreateMap<PromotionBoard, PromotionBoardModel>()
      //    .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank));

      //          cfg.CreateMap<BoardMember, BoardMemberModel>()
      //    .ForMember(d => d.PromotionBoard, opt => opt.MapFrom(s => s.PromotionBoard))
      //    .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //    .ForMember(d => d.MemberRole, opt => opt.MapFrom(s => s.MemberRole));

      //          cfg.CreateMap<PunishmentCategory, PunishmentCategoryModel>();

      //          cfg.CreateMap<PunishmentSubCategory, PunishmentSubCategoryModel>()
      //              .ForMember(d => d.PunishmentCategory, opt => opt.MapFrom(s => s.PunishmentCategory));

      //          cfg.CreateMap<Commendation, CommendationModel>();
      //          cfg.CreateMap<PunishmentNature, PunishmentNatureModel>();

      //          cfg.CreateMap<Achievement, AchievementModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Employee1, opt => opt.MapFrom(s => s.Employee1))
      //              .ForMember(d => d.Commendation, opt => opt.MapFrom(s => s.Commendation))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office));


      //          cfg.CreateMap<MedalAward, MedalAwardModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Award, opt => opt.MapFrom(s => s.Award))
      //              .ForMember(d => d.Medal, opt => opt.MapFrom(s => s.Medal))
      //              .ForMember(d => d.Publication, opt => opt.MapFrom(s => s.Publication))
      //              .ForMember(d => d.PublicationCategory, opt => opt.MapFrom(s => s.PublicationCategory));

      //          cfg.CreateMap<ObservationIntelligent, ObservationIntelligentModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Employee1, opt => opt.MapFrom(s => s.Employee1));


      //          cfg.CreateMap<PunishmentAccident, PunishmentAccidentModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.PunishmentCategory, opt => opt.MapFrom(s => s.PunishmentCategory))
      //              .ForMember(d => d.PunishmentSubCategory, opt => opt.MapFrom(s => s.PunishmentSubCategory))
      //              .ForMember(d => d.PunishmentNature, opt => opt.MapFrom(s => s.PunishmentNature));

      //          cfg.CreateMap<Nomination, NominationModel>()
      //              .ForMember(d => d.MissionAppointment, opt => opt.MapFrom(s => s.MissionAppointment));


      //          cfg.CreateMap<NominationDetail, NominationDetailModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Nomination, opt => opt.MapFrom(s => s.Nomination));


      //          cfg.CreateMap<Transfer, TransferModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Nomination, opt => opt.MapFrom(s => s.Nomination))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office))
      //              .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank))
      //              .ForMember(d => d.OfficeAppointment, opt => opt.MapFrom(s => s.OfficeAppointment))
      //              .ForMember(d => d.District, opt => opt.MapFrom(s => s.District));


      //          cfg.CreateMap<ExtraAppointment, ExtraAppointmentModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office))
      //              .ForMember(d => d.OfficeAppointment, opt => opt.MapFrom(s => s.OfficeAppointment))
      //              .ForMember(d => d.Transfer, opt => opt.MapFrom(s => s.Transfer));


      //          cfg.CreateMap<NominationSchedule, NominationScheduleModel>()
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //              .ForMember(d => d.VisitCategory, opt => opt.MapFrom(s => s.VisitCategory))
      //              .ForMember(d => d.VisitSubCategory, opt => opt.MapFrom(s => s.VisitSubCategory));


      //          cfg.CreateMap<VisitCategory, VisitCategoryModel>();

      //          cfg.CreateMap<VisitSubCategory, VisitSubCategoryModel>()
      //              .ForMember(d => d.VisitCategory, opt => opt.MapFrom(s => s.VisitCategory));


      //          cfg.CreateMap<MissionAppointment, MissionAppointmentModel>()
      //              .ForMember(d => d.AptCat, opt => opt.MapFrom(s => s.AptCat))
      //              .ForMember(d => d.AptNat, opt => opt.MapFrom(s => s.AptNat))
      //              .ForMember(d => d.NominationSchedule, opt => opt.MapFrom(s => s.NominationSchedule));


      //          cfg.CreateMap<PreviousExperience, PreviousExperienceModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
      //              .ForMember(d => d.PreCommissionRank, opt => opt.MapFrom(s => s.PreCommissionRank));

      //          cfg.CreateMap<EmployeeDollarSign, EmployeeDollarSignModel>()
      //             .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<SecurityClearanceReason, SecurityClearanceReasonModel>();

      //          cfg.CreateMap<EmployeeSecurityClearance, EmployeeSecurityClearanceModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.SecurityClearanceReason, opt => opt.MapFrom(s => s.SecurityClearanceReason));


      //          cfg.CreateMap<ServiceExamCategory, ServiceExamCategoryModel>();

      //          cfg.CreateMap<ServiceExam, ServiceExamModel>()
      //              .ForMember(d => d.Branch, opt => opt.MapFrom(s => s.Branch))
      //              .ForMember(d => d.ServiceExamCategory, opt => opt.MapFrom(s => s.ServiceExamCategory));

      //          cfg.CreateMap<EmployeeServiceExamResult, EmployeeServiceExamResultModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.ServiceExamCategory, opt => opt.MapFrom(s => s.ServiceExamCategory))
      //              .ForMember(d => d.ServiceExam, opt => opt.MapFrom(s => s.ServiceExam));

      //          cfg.CreateMap<PftType, PftTypeModel>();

      //          cfg.CreateMap<PftResult, PftResultModel>();

      //          cfg.CreateMap<EmployeePft, EmployeePftModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.PftType, opt => opt.MapFrom(s => s.PftType))
      //              .ForMember(d => d.PftResult, opt => opt.MapFrom(s => s.PftResult));


      //          cfg.CreateMap<EmployeeChildren, EmployeeChildrenModel>()
      //         .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //         .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation));

      //          cfg.CreateMap<TraceSetting, TraceSettingModel>();


      //          cfg.CreateMap<OprOccasion, OprOccasionModel>();
      //          cfg.CreateMap<OprGrading, OprGradingModel>();
      //          cfg.CreateMap<Suitability, SuitabilityModel>();
      //          cfg.CreateMap<SpecialAptType, SpecialAptTypeModel>();

      //          cfg.CreateMap<EmployeeOpr, EmployeeOprModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee)) 
      //              .ForMember(d => d.OprOccasion, opt => opt.MapFrom(s => s.OprOccasion))
      //              .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank))
      //              .ForMember(d => d.RecomandationType, opt => opt.MapFrom(s => s.RecomandationType))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office));

      //          cfg.CreateMap<OprAptSuitability, OprAptSuitabilityModel>()
      //              .ForMember(d => d.EmployeeOpr, opt => opt.MapFrom(s => s.EmployeeOpr))
      //              .ForMember(d => d.Suitability, opt => opt.MapFrom(s => s.Suitability))
      //              .ForMember(d => d.SpecialAptType, opt => opt.MapFrom(s => s.SpecialAptType));


      //          cfg.CreateMap<CoursePoint, CoursePointModel>()
      //    .ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory));

      //          cfg.CreateMap<PtDeductPunishment, PtDeductPunishmentModel>()
      //             .ForMember(d => d.PunishmentSubCategory, opt => opt.MapFrom(s => s.PunishmentSubCategory));

      //          cfg.CreateMap<BonusPtMedal, BonusPtMedalModel>()
      //    .ForMember(d => d.Medal, opt => opt.MapFrom(s => s.Medal));

      //          cfg.CreateMap<BonusPtAward, BonusPtAwardModel>()
      //    .ForMember(d => d.Award, opt => opt.MapFrom(s => s.Award));

      //          cfg.CreateMap<BonusPtPublic, BonusPtPublicModel>();

      //          cfg.CreateMap<BonusPtComApp, BonusPtComAppModel>()
      //   .ForMember(d => d.Commendation, opt => opt.MapFrom(s => s.Commendation));

	     //       cfg.CreateMap<SeaService, SeaServiceModel>()
		    //        .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
		    //        .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));

      //          cfg.CreateMap<EmployeeCourseFuturePlan, EmployeeCourseFuturePlanModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Course, opt => opt.MapFrom(s => s.Course))
      //              .ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory))
      //              .ForMember(d => d.CourseSubCategory, opt => opt.MapFrom(s => s.CourseSubCategory));

      //          cfg.CreateMap<EmployeeTransferFuturePlan, EmployeeTransferFuturePlanModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.AptNat, opt => opt.MapFrom(s => s.AptNat))
      //              .ForMember(d => d.AptCat, opt => opt.MapFrom(s => s.AptCat))
      //              .ForMember(d => d.Pattern, opt => opt.MapFrom(s => s.Pattern))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office))
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));



      //          cfg.CreateMap<RetiredEmployee, RetiredEmployeeModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<EmployeeReport, EmployeeReportModel>();

	     //       cfg.CreateMap<Certificate, CertificateModel>();

      //          cfg.CreateMap<TransferProposal, TransferProposalModel>();
      //          cfg.CreateMap<ProposalDetail, ProposalDetailModel>()
      //              .ForMember(d =>d.Office, opt => opt.MapFrom(s => s.Office))
      //              .ForMember(d => d.OfficeAppointment, opt => opt.MapFrom(s => s.OfficeAppointment))
      //              .ForMember(d => d.TransferProposal, opt => opt.MapFrom(s => s.TransferProposal));

      //          cfg.CreateMap<ProposalCandidate, ProposalCandidateModel>()
      //              .ForMember(d => d.ProposalDetail, opt => opt.MapFrom(s => s.ProposalDetail))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<UsedStoreProcedure, UsedStoreProcedureModel>()
      //              .ForMember(d => d.UsedReport, opt => opt.MapFrom(s => s.UsedReport));


      //          cfg.CreateMap<UsedReport, UsedReportModel>();
      //          cfg.CreateMap<OfficeShipment, OfficeShipmentModel>();

      //          cfg.CreateMap<StatusChange, StatusChangeModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<Photo, PhotoModel>();

      //          cfg.CreateMap<Remark, RemarkModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<ForeignProject, ForeignProjectModel>()
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<PreviousLeave, PreviousLeaveModel>()
      //              .ForMember(d => d.LeaveType, opt => opt.MapFrom(s => s.LeaveType))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));



      //          cfg.CreateMap<PreviousTransfer, PreviousTransferModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank));

      //          cfg.CreateMap<PreviousPunishment, PreviousPunishmentModel>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<PreviousMission, PreviousMissionModel>()
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));



      //          cfg.CreateMap<QuickLinkModel, QuickLink>();


      //          //*****************API to Data*********************

      //          cfg.CreateMap<Configuration.ServiceModel.ModuleModel, Configuration.Models.Module>();
      //          cfg.CreateMap<Configuration.ServiceModel.FeatureModel, Configuration.Models.Feature>()
      //          .ForMember(d => d.Module, opt => opt.MapFrom(s => s.Module));
      //          cfg.CreateMap<RankCategoryModel, RankCategory>();
      //          cfg.CreateMap<RankModel, Rank>();
      //          cfg.CreateMap<ColorModel, Color>();
      //          cfg.CreateMap<LeaveTypeModel, LeaveType>();

      //          cfg.CreateMap<EyeVisionModel, EyeVision>();
      //          cfg.CreateMap<BloodGroupModel, BloodGroup>();
      //          cfg.CreateMap<MedicalCategoryModel, MedicalCategory>();
      //          cfg.CreateMap<PhysicalStructureModel, PhysicalStructure>();
      //          cfg.CreateMap<PhysicalConditionModel, PhysicalCondition>()
      //         .ForMember(d => d.Color, opt => opt.MapFrom(s => s.Color))
      //         .ForMember(d => d.Color1, opt => opt.MapFrom(s => s.Color1))
      //         .ForMember(d => d.Color2, opt => opt.MapFrom(s => s.Color2))
      //         .ForMember(d => d.EyeVision, opt => opt.MapFrom(s => s.EyeVision))
      //         .ForMember(d => d.PhysicalStructure, opt => opt.MapFrom(s => s.PhysicalStructure))
      //         .ForMember(d => d.MedicalCategory, opt => opt.MapFrom(s => s.MedicalCategory));

      //          cfg.CreateMap<RankMapModel, RankMap>()
      //        .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank))
      //        .ForMember(d => d.Rank1, opt => opt.MapFrom(s => s.Rank1))
      //        .ForMember(d => d.Rank2, opt => opt.MapFrom(s => s.Rank2));

      //          cfg.CreateMap<ExtracurricularTypeModel, ExtracurricularType>();

      //          cfg.CreateMap<CategoryModel, Category>();
      //          cfg.CreateMap<SubCategoryModel, SubCategory>()
      //           .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category));
      //          cfg.CreateMap<CommissionTypeModel, CommissionType>();
      //          cfg.CreateMap<BranchModel, Branch>();
      //          cfg.CreateMap<SubBranchModel, SubBranch>();
      //          cfg.CreateMap<OfficerStreamModel, OfficerStream>();
      //          cfg.CreateMap<BatchModel, Batch>();
      //          cfg.CreateMap<ExecutionRemarkModel, ExecutionRemark>();
      //          cfg.CreateMap<CountryModel, Country>();
      //          cfg.CreateMap<MaritalTypeModel, MaritalType>();
      //          cfg.CreateMap<SubjectModel, Subject>();
      //          cfg.CreateMap<ReligionModel, Religion>();

      //          cfg.CreateMap<ReligionCastModel, ReligionCast>()
      //              .ForMember(d => d.Religion, opt => opt.MapFrom(s => s.Religion));

      //          cfg.CreateMap<NationalityModel, Nationality>();

      //          cfg.CreateMap<GenderModel, Gender>();
      //          cfg.CreateMap<OfficerTypeModel, OfficerType>();
      //          cfg.CreateMap<EmployeeStatusModel, EmployeeStatus>();

      //          cfg.CreateMap<EmployeeModel, Employee>()
      //         .ForMember(d => d.Batch, opt => opt.MapFrom(s => s.Batch))
      //         .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender))
      //         .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank))
      //         .ForMember(d => d.RankCategory, opt => opt.MapFrom(s => s.RankCategory))
      //         .ForMember(d => d.OfficerType, opt => opt.MapFrom(s => s.OfficerType))
      //         .ForMember(d => d.EmployeeStatus, opt => opt.MapFrom(s => s.EmployeeStatus));

      //          cfg.CreateMap<EmployeeGeneralModel, EmployeeGeneral>()
      //         .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
      //         .ForMember(d => d.SubCategory, opt => opt.MapFrom(s => s.SubCategory))
      //         .ForMember(d => d.Branch, opt => opt.MapFrom(s => s.Branch))
      //         .ForMember(d => d.SubBranch, opt => opt.MapFrom(s => s.SubBranch))
      //         .ForMember(d => d.CommissionType, opt => opt.MapFrom(s => s.CommissionType))
      //         .ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject))
      //          .ForMember(d => d.Nationality, opt => opt.MapFrom(s => s.Nationality))
      //          .ForMember(d => d.MaritalType, opt => opt.MapFrom(s => s.MaritalType))
      //          .ForMember(d => d.Religion, opt => opt.MapFrom(s => s.Religion))
      //          .ForMember(d => d.ReligionCast, opt => opt.MapFrom(s => s.ReligionCast));

      //          cfg.CreateMap<EmployeeOtherModel, EmployeeOther>()
      //.ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<EducationModel, Education>()
      //           .ForMember(d => d.ExamCategory, opt => opt.MapFrom(s => s.ExamCategory))
      //           .ForMember(d => d.Examination, opt => opt.MapFrom(s => s.Examination))
      //           .ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject))
      //           .ForMember(d => d.Result, opt => opt.MapFrom(s => s.Result))
      //            .ForMember(d => d.Board, opt => opt.MapFrom(s => s.Board));


      //          cfg.CreateMap<AddressModel, Address>()
      //         .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //         .ForMember(d => d.Division, opt => opt.MapFrom(s => s.Division))
      //         .ForMember(d => d.District, opt => opt.MapFrom(s => s.District))
      //           .ForMember(d => d.Upazila, opt => opt.MapFrom(s => s.Upazila));

      //          cfg.CreateMap<ExtracurricularModel, Extracurricular>()
      //        .ForMember(d => d.ExtracurricularType, opt => opt.MapFrom(s => s.ExtracurricularType));

      //          cfg.CreateMap<SocialAttributeModel, SocialAttribute>();
      //          cfg.CreateMap<SportModel, Sport>();

      //          cfg.CreateMap<ParentModel, Parent>()
      //         .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //         .ForMember(d => d.Nationality, opt => opt.MapFrom(s => s.Nationality))
      //         .ForMember(d => d.Religion, opt => opt.MapFrom(s => s.Religion))
      //         .ForMember(d => d.ReligionCast, opt => opt.MapFrom(s => s.ReligionCast))
      //         .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation));

      //          cfg.CreateMap<ExamCategoryModel, ExamCategory>();
      //          cfg.CreateMap<ExaminationModel, Examination>();

      //          cfg.CreateMap<InstituteTypeModel, InstituteType>();

      //          cfg.CreateMap<InstituteModel, Institute>()
      //          .ForMember(d => d.Board, opt => opt.MapFrom(s => s.Board));


      //          cfg.CreateMap<BoardModel, Board>();
      //          cfg.CreateMap<MemberRoleModel, MemberRole>();
      //          cfg.CreateMap<ExamSubjectModel, ExamSubject>()
      //         .ForMember(d => d.Examination, opt => opt.MapFrom(s => s.Examination));
      //          cfg.CreateMap<ResultModel, Result>();

      //          cfg.CreateMap<DivisionModel, Division>();
      //          cfg.CreateMap<DistrictModel, District>()
      //          .ForMember(d => d.Division, opt => opt.MapFrom(s => s.Division));
      //          cfg.CreateMap<UpazilaModel, Upazila>()
      //          .ForMember(d => d.District, opt => opt.MapFrom(s => s.District))
      //          .ForMember(d => d.Division, opt => opt.MapFrom(s => s.Division));


      //          cfg.CreateMap<PromotionNominationModel, PromotionNomination>()
      //            .ForMember(d => d.PromotionBoard, opt => opt.MapFrom(s => s.PromotionBoard))
      //            .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //            .ForMember(d => d.ExecutionRemark, opt => opt.MapFrom(s => s.ExecutionRemark));

      //          cfg.CreateMap<TerminationTypeModel, TerminationType>();

      //          cfg.CreateMap<AgeServicePolicyModel, AgeServicePolicy>()
      //              .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
      //              .ForMember(d => d.SubCategory, opt => opt.MapFrom(s => s.SubCategory))
      //              .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank));


      //          cfg.CreateMap<RetiredAgeModel, RetiredAge>()
      //              .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
      //              .ForMember(d => d.SubCategory, opt => opt.MapFrom(s => s.SubCategory))
      //              .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank));


      //          cfg.CreateMap<EmployeeServiceExtensionModel, EmployeeServiceExt>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<CourseCategoryModel, CourseCategory>();

      //          cfg.CreateMap<CourseSubCategoryModel, CourseSubCategory>()
      //              .ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory));

      //          cfg.CreateMap<EmployeeLeaveModel, EmployeeLeave>()
      //          .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //          .ForMember(d => d.LeaveType, opt => opt.MapFrom(s => s.LeaveType));


      //          cfg.CreateMap<EmpRunMissingModel, EmpRunMissing>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<EmpRejoinModel, EmpRejoin>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank));

      //          cfg.CreateMap<ResultTypeModel, ResultType>();

      //          cfg.CreateMap<CourseModel, Course>()
      //              .ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory))
      //              .ForMember(d => d.CourseSubCategory, opt => opt.MapFrom(s => s.CourseSubCategory))
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));

      //          cfg.CreateMap<TrainingInstituteModel, TrainingInstitute>()
      //             .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));

      //          cfg.CreateMap<TrainingPlanModel, TrainingPlan>()
      //              .ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory))
      //              .ForMember(d => d.CourseSubCategory, opt => opt.MapFrom(s => s.CourseSubCategory))
      //              .ForMember(d => d.Course, opt => opt.MapFrom(s => s.Course))
      //              .ForMember(d => d.TrainingInstitute, opt => opt.MapFrom(s => s.TrainingInstitute))
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));

      //          cfg.CreateMap<TrainingResultModel, TrainingResult>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.ResultType, opt => opt.MapFrom(s => s.ResultType))
      //              .ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory))
      //              .ForMember(d => d.CourseSubCategory, opt => opt.MapFrom(s => s.CourseSubCategory))
      //              .ForMember(d => d.TrainingPlan, opt => opt.MapFrom(s => s.TrainingPlan));

      //          cfg.CreateMap<OccupationModel, Occupation>();
      //          cfg.CreateMap<ResultGradeModel, ResultGrade>();

      //          cfg.CreateMap<SiblingModel, Sibling>()
      //              .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<SpouseModel, Spouse>()
      //              .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<PreCommissionRankModel, PreCommissionRank>();

      //          cfg.CreateMap<PreCommissionCourseModel, PreCommissionCourse>();
      //          cfg.CreateMap<PreCommissionCourseDetailModel, PreCommissionCourseDetail>();


      //          cfg.CreateMap<RelationModel, Relation>();
      //          cfg.CreateMap<HeirTypeModel, HeirType>();

      //          cfg.CreateMap<AptCatModel, AptCat>();
      //          cfg.CreateMap<AptNatModel, AptNat>();

      //          cfg.CreateMap<HeirNextOfKinInfoModel, HeirNextOfKinInfo>()
      //              .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender))
      //              .ForMember(d => d.Relation, opt => opt.MapFrom(s => s.Relation))
      //              .ForMember(d => d.HeirType, opt => opt.MapFrom(s => s.HeirType));

      //          cfg.CreateMap<ZoneModel, Zone>();
      //          cfg.CreateMap<ShipCategoryModel, ShipCategory>();

      //          cfg.CreateMap<OfficeModel, Office>()
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //              .ForMember(d => d.Country1, opt => opt.MapFrom(s => s.Country1))
      //              .ForMember(d => d.Zone, opt => opt.MapFrom(s => s.Zone))
      //              .ForMember(d => d.Pattern, opt => opt.MapFrom(s => s.Pattern))
      //              .ForMember(d => d.ShipCategory, opt => opt.MapFrom(s => s.ShipCategory));

      //          cfg.CreateMap<PatternModel, Pattern>();
      //          cfg.CreateMap<LprCalculateInfoModel, LprCalculateInfo>();

      //          cfg.CreateMap<OfficeAppointmentModel, OfficeAppointment>()
      //              .ForMember(d => d.AptCat, opt => opt.MapFrom(s => s.AptCat))
      //              .ForMember(d => d.AptNat, opt => opt.MapFrom(s => s.AptNat))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office));


      //          cfg.CreateMap<MedalModel, Medal>();
      //          cfg.CreateMap<AwardModel, Award>();
      //          cfg.CreateMap<PublicationCategoryModel, PublicationCategory>();

      //          cfg.CreateMap<PromotionBoardModel, PromotionBoard>()
      //  .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank));

      //          cfg.CreateMap<BoardMemberModel, BoardMember>()
      //              .ForMember(d => d.PromotionBoard, opt => opt.MapFrom(s => s.PromotionBoard))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.MemberRole, opt => opt.MapFrom(s => s.MemberRole));


      //          cfg.CreateMap<PublicationModel, Publication>()
      //              .ForMember(d => d.PublicationCategory, opt => opt.MapFrom(s => s.PublicationCategory));



      //          cfg.CreateMap<PunishmentCategoryModel, PunishmentCategory>();

      //          cfg.CreateMap<PunishmentSubCategoryModel, PunishmentSubCategory>()
      //              .ForMember(d => d.PunishmentCategory, opt => opt.MapFrom(s => s.PunishmentCategory));

      //          cfg.CreateMap<CommendationModel, Commendation>();
      //          cfg.CreateMap<PunishmentNatureModel, PunishmentNature>();


      //          cfg.CreateMap<AchievementModel, Achievement>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Employee1, opt => opt.MapFrom(s => s.Employee1))
      //              .ForMember(d => d.Commendation, opt => opt.MapFrom(s => s.Commendation))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office));

      //          cfg.CreateMap<MedalAwardModel, MedalAward>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Award, opt => opt.MapFrom(s => s.Award))
      //              .ForMember(d => d.Medal, opt => opt.MapFrom(s => s.Medal))
      //              .ForMember(d => d.Publication, opt => opt.MapFrom(s => s.Publication))
      //              .ForMember(d => d.PublicationCategory, opt => opt.MapFrom(s => s.PublicationCategory));

      //          cfg.CreateMap<ObservationIntelligentModel, ObservationIntelligent>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Employee1, opt => opt.MapFrom(s => s.Employee1));

      //          cfg.CreateMap<PunishmentAccidentModel, PunishmentAccident>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.PunishmentCategory, opt => opt.MapFrom(s => s.PunishmentCategory))
      //              .ForMember(d => d.PunishmentSubCategory, opt => opt.MapFrom(s => s.PunishmentSubCategory))
      //              .ForMember(d => d.PunishmentNature, opt => opt.MapFrom(s => s.PunishmentNature));

      //          cfg.CreateMap<NominationModel, Nomination>()
      //              .ForMember(d => d.MissionAppointment, opt => opt.MapFrom(s => s.MissionAppointment));

      //          cfg.CreateMap<NominationDetailModel, NominationDetail>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Nomination, opt => opt.MapFrom(s => s.Nomination));


      //          cfg.CreateMap<TransferModel, Transfer>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Nomination, opt => opt.MapFrom(s => s.Nomination))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office))
      //              .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank))
      //              .ForMember(d => d.OfficeAppointment, opt => opt.MapFrom(s => s.OfficeAppointment))
      //              .ForMember(d => d.District, opt => opt.MapFrom(s => s.District));


      //          cfg.CreateMap<ExtraAppointmentModel, ExtraAppointment>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office))
      //              .ForMember(d => d.OfficeAppointment, opt => opt.MapFrom(s => s.OfficeAppointment))
      //              .ForMember(d => d.Transfer, opt => opt.MapFrom(s => s.Transfer));

      //          cfg.CreateMap<RemarkModel, Remark>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<NominationScheduleModel, NominationSchedule>()
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //              .ForMember(d => d.VisitCategory, opt => opt.MapFrom(s => s.VisitCategory))
      //              .ForMember(d => d.VisitSubCategory, opt => opt.MapFrom(s => s.VisitSubCategory));

      //          cfg.CreateMap<VisitCategoryModel, VisitCategory>();

      //          cfg.CreateMap<VisitSubCategoryModel, VisitSubCategory>()
      //              .ForMember(d => d.VisitCategory, opt => opt.MapFrom(s => s.VisitCategory));

      //          cfg.CreateMap<MissionAppointmentModel, MissionAppointment>()
      //              .ForMember(d => d.AptCat, opt => opt.MapFrom(s => s.AptCat))
      //              .ForMember(d => d.AptNat, opt => opt.MapFrom(s => s.AptNat))
      //              .ForMember(d => d.NominationSchedule, opt => opt.MapFrom(s => s.NominationSchedule));


      //          cfg.CreateMap<PreviousExperienceModel, PreviousExperience>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
      //              .ForMember(d => d.PreCommissionRank, opt => opt.MapFrom(s => s.PreCommissionRank));

      //          cfg.CreateMap<EmployeeDollarSignModel, EmployeeDollarSign>()
      //           .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<SecurityClearanceReasonModel, SecurityClearanceReason>();

      //          cfg.CreateMap<EmployeeSecurityClearanceModel, EmployeeSecurityClearance>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.SecurityClearanceReason, opt => opt.MapFrom(s => s.SecurityClearanceReason));


      //          cfg.CreateMap<ServiceExamCategoryModel, ServiceExamCategory>();

      //          cfg.CreateMap<ServiceExamModel, ServiceExam>()
      //              .ForMember(d => d.Branch, opt => opt.MapFrom(s => s.Branch))
      //              .ForMember(d => d.ServiceExamCategory, opt => opt.MapFrom(s => s.ServiceExamCategory));


      //          cfg.CreateMap<EmployeeServiceExamResultModel, EmployeeServiceExamResult>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.ServiceExamCategory, opt => opt.MapFrom(s => s.ServiceExamCategory))
      //              .ForMember(d => d.ServiceExam, opt => opt.MapFrom(s => s.ServiceExam));


      //          cfg.CreateMap<PftTypeModel, PftType>();

      //          cfg.CreateMap<PftResultModel, PftResult>();

      //          cfg.CreateMap<EmployeePftModel, EmployeePft>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.PftType, opt => opt.MapFrom(s => s.PftType))
      //              .ForMember(d => d.PftResult, opt => opt.MapFrom(s => s.PftResult));


      //          cfg.CreateMap<EmployeeChildrenModel, EmployeeChildren>()
      //         .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //         .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.Occupation));

      //          cfg.CreateMap<TraceSettingModel, TraceSetting>();
      //          cfg.CreateMap<OprOccasionModel, OprOccasion>();
      //          cfg.CreateMap<OprGradingModel, OprGrading>();
      //          cfg.CreateMap<SuitabilityModel, Suitability>();
      //          cfg.CreateMap<SpecialAptTypeModel, SpecialAptType>();


      //          cfg.CreateMap<EmployeeOprModel, EmployeeOpr>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.OprOccasion, opt => opt.MapFrom(s => s.OprOccasion))
      //              .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank))
      //              .ForMember(d => d.RecomandationType, opt => opt.MapFrom(s => s.RecomandationType))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office));

      //          cfg.CreateMap<OprAptSuitabilityModel, OprAptSuitability>()
      //              .ForMember(d => d.EmployeeOpr, opt => opt.MapFrom(s => s.EmployeeOpr))
      //              .ForMember(d => d.Suitability, opt => opt.MapFrom(s => s.Suitability))
      //              .ForMember(d => d.SpecialAptType, opt => opt.MapFrom(s => s.SpecialAptType));


      //          cfg.CreateMap<CoursePointModel, CoursePoint>()
      //             .ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory));

      //          cfg.CreateMap<PtDeductPunishmentModel, PtDeductPunishment>()
      //             .ForMember(d => d.PunishmentSubCategory, opt => opt.MapFrom(s => s.PunishmentSubCategory));

      //          cfg.CreateMap<BonusPtMedalModel, BonusPtMedal>()
      //    .ForMember(d => d.Medal, opt => opt.MapFrom(s => s.Medal));

      //          cfg.CreateMap<BonusPtAwardModel, BonusPtAward>()
      //    .ForMember(d => d.Award, opt => opt.MapFrom(s => s.Award));

      //          cfg.CreateMap<BonusPtPublicModel, BonusPtPublic>();

      //          cfg.CreateMap<BonusPtComAppModel, BonusPtComApp>()
      //   .ForMember(d => d.Commendation, opt => opt.MapFrom(s => s.Commendation));


	     //       cfg.CreateMap<SeaServiceModel, SeaService>()
		    //        .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
		    //        .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));


      //          cfg.CreateMap<EmployeeCourseFuturePlanModel, EmployeeCourseFuturePlan>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.Course, opt => opt.MapFrom(s => s.Course))
      //              .ForMember(d => d.CourseCategory, opt => opt.MapFrom(s => s.CourseCategory))
      //              .ForMember(d => d.CourseSubCategory, opt => opt.MapFrom(s => s.CourseSubCategory));

      //          cfg.CreateMap<EmployeeTransferFuturePlanModel, EmployeeTransferFuturePlan>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee))
      //              .ForMember(d => d.AptNat, opt => opt.MapFrom(s => s.AptNat))
      //              .ForMember(d => d.AptCat, opt => opt.MapFrom(s => s.AptCat))
      //              .ForMember(d => d.Pattern, opt => opt.MapFrom(s => s.Pattern))
      //              .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office))
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country));


      //          cfg.CreateMap<RetiredEmployeeModel, RetiredEmployee>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<EmployeeReportModel, EmployeeReport>();

	     //       cfg.CreateMap<CertificateModel, Certificate>();
      //          cfg.CreateMap<TransferProposalModel, TransferProposal>();
      //          cfg.CreateMap<ProposalDetailModel, ProposalDetail>()
      //            .ForMember(d => d.Office, opt => opt.MapFrom(s => s.Office))
      //            .ForMember(d => d.OfficeAppointment, opt => opt.MapFrom(s => s.OfficeAppointment));

      //          cfg.CreateMap<ProposalCandidateModel, ProposalCandidate>()
      //            .ForMember(d => d.ProposalDetail, opt => opt.MapFrom(s => s.ProposalDetail))
      //            .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<UsedStoreProcedureModel, UsedStoreProcedure>()
      //              .ForMember(d => d.UsedReport, opt => opt.MapFrom(s => s.UsedReport));


      //          cfg.CreateMap<UsedReportModel, UsedReport>();

      //          cfg.CreateMap<OfficeShipmentModel, OfficeShipment>();


      //          cfg.CreateMap<StatusChangeModel, StatusChange>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<PhotoModel, Photo>();
      //          cfg.CreateMap<QuickLinkModel, QuickLink>();

      //          cfg.CreateMap<ForeignProjectModel, ForeignProject>()
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<PreviousLeaveModel, PreviousLeave>()
      //              .ForMember(d => d.LeaveType, opt => opt.MapFrom(s => s.LeaveType))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<PreviousTransferModel, PreviousTransfer>()
      //              .ForMember(d => d.Rank, opt => opt.MapFrom(s => s.Rank))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));


      //          cfg.CreateMap<PreviousPunishmentModel, PreviousPunishment>()
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));

      //          cfg.CreateMap<PreviousMissionModel, PreviousMission>()
      //              .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
      //              .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee));
            });
        }
    }
}
