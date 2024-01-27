using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
	public class CurrentStatusService : ICurrentStatusService
	{
		private readonly IBnoisRepository<Employee> employeeRepository;
		private readonly IBnoisRepository<HeirType> heirTypeRepository;
		private readonly IBnoisRepository<Zone> zoneRepository;
        BnoisDbContext db = new BnoisDbContext();
        public CurrentStatusService(IBnoisRepository<Employee> employeeRepository, IBnoisRepository<HeirType> heirTypeRepository, IBnoisRepository<Zone> zoneRepository)
		{
			this.employeeRepository = employeeRepository;
			this.heirTypeRepository = heirTypeRepository;
			this.zoneRepository = zoneRepository;
		}

		public object GetGeneralInformation(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetAnnx1OfficerGeneralReport] '{0}' ", pNo));
			return dataTable.ToJson().FirstOrDefault();
		}

		public List<object> GetCivilAcademicQualification(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetPersonalityBriefByCivilEducationReport] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}
		public List<object> GetSecurityClearance(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetOfficerSecurityClearance] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}


		public List<object> GetCourseAttended(string pNo)
		{
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeePNo;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && pNo == "undefined")
            {
                employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if (RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && pNo != "undefined")
            {
                employeePNo = pNo;
            }
            else
            {
                throw new Exception("Some thing went wrong.");
            }
            int employeeStatus = 0;
            employeeStatus = db.Database.SqlQuery<int>("Select top(1) EmployeeStatusId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
            if (employeeStatus != 1)
            {
                throw new InfinityNotFoundException("Officer is not active.");
            }
            if (employeePNo != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetAnnx6CourseReport] '{0}' ", employeePNo));
            }
            else
            {
                throw new Exception("Missed the P No.");
            }
            return dataTable.ToJson().ToList();


   //         string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
   //         pNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + userId + "'").SingleOrDefault();
   //         DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetAnnx6CourseReport] '{0}' ", pNo));

			//return dataTable.ToJson().ToList();
		}

		public List<object> GetExamTestResult(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetExamTestResult] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}

		public List<object> GetPunishmentDiscipline(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetPunishmentDiscipline] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}

		public List<object> GetCommendationAppreciation(string pNo,int type)
		{
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeePNo;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && pNo == "undefined")
            {
                employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if (RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && pNo != "undefined")
            {
                employeePNo = pNo;
            }
            else
            {
                throw new Exception("Some thing went wrong.");
            }
            int employeeStatus = 0;
            employeeStatus = db.Database.SqlQuery<int>("Select top(1) EmployeeStatusId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
            if (employeeStatus != 1)
            {
                throw new InfinityNotFoundException("Officer is not active.");
            }
            if (employeePNo != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetCommendationAppreciation] '{0}','{1}' ", employeePNo, type));
            }
            else
            {
                throw new Exception("Missed the P No.");
            }
            return dataTable.ToJson().ToList();
		}

		public List<object> GetAward(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetAward] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}

		public List<object> GetMedal(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetMedal] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}

		public List<object> GetPublication(string pNo)
		{
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeePNo;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && pNo == "undefined")
            {
                employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if (RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && pNo != "undefined")
            {
                employeePNo = pNo;
            }
            else
            {
                throw new Exception("Some thing went wrong.");
            }
            int employeeStatus = 0;
            employeeStatus = db.Database.SqlQuery<int>("Select top(1) EmployeeStatusId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
            if (employeeStatus != 1)
            {
                throw new InfinityNotFoundException("Officer is not active.");
            }
            if (employeePNo != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetPublication] '{0}' ", employeePNo));
            }
            else
            {
                throw new Exception("Missed the P No.");
            }
            return dataTable.ToJson().ToList();
		}


        public List<object> GetCleanService(string pNo)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetCSA] '{0}' ", pNo));

            return dataTable.ToJson().ToList();
        }



        public List<object> GetChildren(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetChildren] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}

		public List<object> GetSibling(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetSibling] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}

		public List<object> GetNextOfKin(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetNextOfKin] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}


		private List<object> GetHeir(string pNo, int heirTypeId)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetHeir] '{0}',{1} ", pNo, heirTypeId));

			return dataTable.ToJson().ToList();
		}

		public dynamic GetHeirInfo(string pno)
		{
			var heirTypes = heirTypeRepository.Where(x => x.IsActive).ToList();
            Dictionary<string, dynamic> keyValues = new Dictionary<string, dynamic>();
			foreach (var heirType in heirTypes)
			{
				var vaHeirs = GetHeir(pno, heirType.HeirTypeId);

			    if (vaHeirs.Count >0)
			    {
			        keyValues.Add(heirType.Name, vaHeirs);
                }
				
			}
			return keyValues;
		}

	    public List<object> GetOprGrading(string pNo)
	    {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeePNo;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && pNo == "0")
            {
                employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if (RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && pNo != "undefined")
            {
                employeePNo = pNo;
            }
            else
            {
                throw new Exception("Some thing went wrong.");
            }
            int employeeStatus = 0;
            employeeStatus = db.Database.SqlQuery<int>("Select top(1) EmployeeStatusId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
            if (employeeStatus != 1)
            {
                throw new InfinityNotFoundException("Officer is not active.");
            }
            if (employeePNo != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetOprGrading] '{0}' ", employeePNo));
            }
            else
            {
                throw new Exception("Missed the P No.");
            }
            return dataTable.ToJson().ToList();


         //   if (pNo == null || pNo == "undefined" || pNo == "0")
         //   {
         //       string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
         //       pNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + userId + "'").SingleOrDefault();
         //   }
         //   DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetOprGrading] '{0}' ", pNo));

	        //return dataTable.ToJson().ToList();
	    }

        public List<object> GetOprIndex(string pNo)
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeePNo;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && pNo == "0")
            {
                employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if (RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && pNo != "undefined")
            {
                employeePNo = pNo;
            }
            else
            {
                throw new Exception("Some thing went wrong.");
            }
            int employeeStatus = 0;
            employeeStatus = db.Database.SqlQuery<int>("Select top(1) EmployeeStatusId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
            if (employeeStatus != 1)
            {
                throw new InfinityNotFoundException("Officer is not active.");
            }
            if (employeePNo != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetOprIndex] '{0}' ", employeePNo));
            }
            else
            {
                throw new Exception("Missed the P No.");
            }
            return dataTable.ToJson().ToList();
        }
        public List<object> GetForeignVisit(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetForeignVisit] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }
	    public List<object> GetParentInfo(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetParentsInfo] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }


	    public List<object> GetSpouseInfo(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetSpouseInfo] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }

	    public List<object> GetTransferHistory()
	    {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string pNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + userId + "'").SingleOrDefault();
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [SpGetTransferHistory] '{0}'", pNo));

	        return dataTable.ToJson().ToList();
	    }
        public List<object> GetTransferHistory(string pNo)
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeePNo;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && pNo == "undefined")
            {
                employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if (RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && pNo != "undefined")
            {
                employeePNo = pNo;
            }
            else
            {
                throw new Exception("Some thing went wrong.");
            }
            int employeeStatus = 0;
            employeeStatus = db.Database.SqlQuery<int>("Select top(1) EmployeeStatusId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
            if (employeeStatus != 1)
            {
                throw new InfinityNotFoundException("Officer is not active.");
            }
            if (employeePNo != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [SpGetTransferHistory] '{0}' ", employeePNo));
            }
            else
            {
                throw new Exception("Missed the P No.");
            }
            return dataTable.ToJson().ToList();
            //string pNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + userId + "'").SingleOrDefault();
            //DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [SpGetTransferHistory] '{0}'", pNo));

            //return dataTable.ToJson().ToList();
        }

        public List<object> GetPromotionHistory(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [SpGetPromotionHistory] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }

	    private List<object> GetAdditionalSeaServicesByType(string pNo, int type)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetAdditionalSeaServices] '{0}',{1} ", pNo, type));

	        return dataTable.ToJson().ToList();
	    }

	    public object GetAdditionalSeaServicesGrandTotal(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetAdditionalSeaServicesGrandTotal] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }


	    public dynamic GetAdditionalSeaServices(string pno)
	    {

	        Dictionary<string, dynamic> keyValues = new Dictionary<string, dynamic>();
	        foreach (var type in Enum.GetValues((typeof(ShipType))))
	        {
	            var results = GetAdditionalSeaServicesByType(pno, (int)type);

	            var shipType = String.Empty;
	            if (results.Count > 0)
	            {
	                if ((int)type == (int)ShipType.Small)
	                {
	                    shipType = "Small";

	                }
	                else if ((int)type == (int)ShipType.Medium)
	                {
	                    shipType = "Medium";
	                }
	                else
	                {
	                    shipType = "Large";
	                }


	                keyValues.Add(shipType, results);
	            }

	        }
	        return keyValues;
	    }


	    private List<object> GetSeaServicesByType(string pNo, int type)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetSeaServices] '{0}',{1} ", pNo, type));

	        return dataTable.ToJson().ToList();
	    }

	    public object GetSeaServicesGrandTotal(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format(@"declare @GrandTotalDuration varchar(100) exec [spGetSeaServicesGrandTotal] '{0}',@GrandTotalDuration output select isnull(@GrandTotalDuration,'') as GrandTotalDuration", pNo));
	        return dataTable.ToJson().ToList();
	    }


	    public dynamic GetSeaServices(string pno)
	    {

	        Dictionary<string, dynamic> keyValues = new Dictionary<string, dynamic>();
	        foreach (var type in Enum.GetValues((typeof(ShipType))))
	        {
	            var results = GetSeaServicesByType(pno, (int)type);

	            var shipType = String.Empty;
	            if (results.Count > 0)
	            {
	                if ((int)type == (int)ShipType.Small)
	                {
	                    shipType = "Small";

	                }
	                else if ((int)type == (int)ShipType.Medium)
	                {
	                    shipType = "Medium";
	                }
	                else
	                {
	                    shipType = "Large";
	                }


	                keyValues.Add(shipType, results);
	            }

	        }
	        return keyValues;
	    }


        public List<object> GetInstructionalServices(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetInstructionalServices] '{0}' ", pNo));

            return dataTable.ToJson().ToList();
		}


		private List<object> GetSeaCommandServicesByType(string pNo,int type)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetSeaCommandServices] '{0}',{1} ", pNo,type));

			return dataTable.ToJson().ToList();
		}

	    public object GetSeaCommandServicesGrandTotal(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetSeaCommandServicesGrandTotal] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }


        public dynamic GetSeaCommandServices(string pno)
	    {
	       
	        Dictionary<string, dynamic> keyValues = new Dictionary<string, dynamic>();
	        foreach (var type in Enum.GetValues((typeof(ShipType))))
	        {
	            var results = GetSeaCommandServicesByType(pno, (int)type);

	            var shipType = String.Empty;
	            if (results.Count > 0)
	            {
	                if ((int) type == (int)ShipType.Small)
	                {
	                    shipType = "Small";

	                }else if ((int) type == (int) ShipType.Medium)
	                {
	                    shipType = "Medium";
                    }
	                else
	                {
	                    shipType = "Large";
                    }


	                keyValues.Add(shipType, results);
	            }

	        }
	        return keyValues;
	    }



	    private List<object> GetZoneServicesByType(string pNo, int type)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetZoneServices] '{0}',{1} ", pNo, type));

	        return dataTable.ToJson().ToList();
	    }

	    public object GetZoneServicesGrandTotal(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetZoneServicesGrandTotal] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }


	    public dynamic GetZoneServices(string pno)
	    {
	        var zones = zoneRepository.Where(x => x.IsActive).ToList();
            Dictionary<string, dynamic> keyValues = new Dictionary<string, dynamic>();
	        foreach (var item in zones)
	        {
	            var results = GetZoneServicesByType(pno, item.ZoneId);
	         
	            if (results.Count > 0)
	            {
	              

	                keyValues.Add(item.Name, results);
	            }

	        }
	        return keyValues;
	    }



        public List<object> GetInterOrganizationServices(string pNo)	
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetInterOrganizationServices] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}
		public List<object> GetIntelligenceServices(string pNo)
		{
			DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetIntelligenceServices] '{0}' ", pNo));

			return dataTable.ToJson().ToList();
		}


	    public List<object> GetMissions(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetMissions] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }


	    public List<object> GetForeignProjects(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetForeignProjects] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }


	    public List<object> GetHODServices(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetHODServices] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }

	    public List<object> GetDockyardServices(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDockyardServices] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }



        public List<object> GetNotifications( string userid, string pNo)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetNotification] '{0}','{1}' ", userid, pNo));

            return dataTable.ToJson().ToList();
        }


        public object GetCurrentStatus(string pno)
	    {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeePNo;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && pno == "undefined")
            {
                employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if(RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && pno != "undefined")
            {
                employeePNo = pno;
            }
            else
            {
                throw new Exception("Some thing went wrong.");
            }

            if (employeePNo != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetCurrentStatus] '{0}' ", employeePNo));
            }
            else
            {
                throw new Exception("Missed the P No.");
            }
            return dataTable.ToJson().FirstOrDefault();
	    }
        public object GetCurrentStatus1(string pno)
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string RoleId = ConfigurationResolver.Get().LoggedInUser.RoleIds[0].ToString();
            string employeePNo;
            int employeeStatus = 0;
            DataTable dataTable;
            if (RoleId == "8ba5c634-71bd-4d9c-9bcd-388d2c2dbc4a" && pno == "undefined")
            {
                employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            }
            else if (RoleId == "15abe3a1-2df6-4a3d-9cda-a606a0602b34" && pno != "undefined")
            {
                employeePNo = pno;
            }
            else
            {
                throw new InfinityNotFoundException("Some thing went wrong.");
            }

            employeeStatus = db.Database.SqlQuery<int>("Select top(1) EmployeeStatusId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
            if(employeeStatus != 1)
            {
                throw new InfinityNotFoundException("Officer is not active.");
            }
            if (employeePNo != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetCurrentStatus1] '{0}' ", employeePNo));
            }
            else
            {
                throw new InfinityNotFoundException("Missed the P No.");
            }
            return dataTable.ToJson().FirstOrDefault();
        }
        public object GetCurrentStatus()
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetCurrentStatus] '{0}' ", employeePNo));
            return dataTable.ToJson().FirstOrDefault();
        }

        public object GetISSB(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetISSB] '{0}' ", pNo));
	        return dataTable.ToJson().FirstOrDefault();
	    }



        public object GetLeaveInfo(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetLeaveInfo] '{0}' ", pNo));
	        return dataTable.ToJson().FirstOrDefault();
	    }


        public List<object> GetTemporaryTransferHistory(int transferId)
	    {

            
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [SpGetTemporaryTransferHistory] {0}", transferId));


	        return dataTable.ToJson().ToList();
        }

	    public List<object> GetPersuasion(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetPersuasion] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
        }

	    public List<object> GetRemark(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetRemark] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
	    }

	    public List<object> GetCourseFuturePlan(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetCourseFuturePlan] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
        }

	    public List<object> GetTransferFuturePlan(string pNo)
	    {
	        DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetTransferFuturePlan] '{0}' ", pNo));

	        return dataTable.ToJson().ToList();
        }

        public List<object> GetCostGuardHistory(string pNo)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetCostGuardHistory] '{0}'", pNo));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetUserLogData(DateTime Fromdate, DateTime ToDate)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetUserLogData] '{0}','{1}'", Fromdate,ToDate));

            return dataTable.ToJson().ToList();
        }
    }
}