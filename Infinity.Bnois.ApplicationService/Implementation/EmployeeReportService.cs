using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.Data.Models;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class EmployeeReportService : IEmployeeReportService
    {
        private readonly IEmployeeReportRepository employeeReportRepository;
        public EmployeeReportService(IEmployeeReportRepository employeeReportRepository)
        {
            this.employeeReportRepository = employeeReportRepository;
        }
        public dynamic GetEmployeeReports()
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            DataTable dataTable = employeeReportRepository.ExecWithSqlQuery(String.Format("exec [spGetEmployeeReport] '{0}' ", userId));
            return dataTable.ToJson().ToList();
        }



        public async Task<EmployeeReportModel> GetEmployeeReport(int id)
        {
            if (id <= 0)
            {
                return new EmployeeReportModel();
            }
            EmployeeReport employeeReport = await employeeReportRepository.FindOneAsync(x => x.Id == id);
            if (employeeReport == null)
            {
                throw new InfinityNotFoundException("Employee not found");
            }
            EmployeeReportModel model = ObjectConverter<EmployeeReport, EmployeeReportModel>.Convert(employeeReport);
            return model;
        }


        public async Task<EmployeeReportModel> SaveEmployeeReport(int id, EmployeeReportModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Employee Report data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeeReport employeeReport = ObjectConverter<EmployeeReportModel, EmployeeReport>.Convert(model);
            employeeReport.UserId = userId;
            employeeReport.EmployeeId = model.EmployeeId;
            await employeeReportRepository.SaveAsync(employeeReport);
            model.Id = employeeReport.Id;
            return model;
        }
        public async Task<bool> DeleteEmployeeReport(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeReport employeeReport = await employeeReportRepository.FindOneAsync(x => x.Id == id);
            if (employeeReport == null)
            {
                throw new InfinityNotFoundException("Employee not found");
            }
            else
            {
                return await employeeReportRepository.DeleteAsync(employeeReport);
            }
        }

        public bool DeleteEmployeeReports()
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            List<EmployeeReport> employeeReports = employeeReportRepository.Where(x => x.UserId == userId).ToList();
            if (!employeeReports.Any())
            {
                throw new InfinityNotFoundException("Officer List not found !");
            }
            else
            {
                return employeeReportRepository.RemoveRange(employeeReports) > 0;
            }
        }



        public int SaveGraphicalReport(int fromYear, int toYear, string userId)
        {
            employeeReportRepository.DeleteGraphReport(userId);
            var employees = employeeReportRepository.Where(x => x.UserId == userId).ToList();
            foreach (var employee in employees)
            {
                for (int i = fromYear; i <= toYear; i++)
                {
                    employeeReportRepository.SaveGraphReport(i, employee.EmployeeId, userId);
                }
            }


            return 0;
        }



        public Chart GetLastOPRChart(int lastOprNo, string userId)
        {

            var list = new List<object>();
            for (int i = 1; i <= lastOprNo; i++)
            {
                list.Add(i);
            }
            Chart _chart = new Chart();
            _chart.labels = list;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();

            List<EmployeeReportDataModel> oprResult = employeeReportRepository.GetOprGraphListReport(lastOprNo, userId);
            List<string> employeePnoList = oprResult.Select(x => x.PNo).Distinct().ToList();
            for (int i = 0; i < employeePnoList.Count(); i++)
            {
                _dataSet.Add(new Datasets()
                {
                    label = employeePnoList[i],
                    data = oprResult.Where(x => x.PNo == employeePnoList[i]).OrderBy(x => x.Row_Num).Select(x => x.OprGrade).ToArray(),
                    borderColor = GraphColor.BorderColor[i],
                    borderWidth = "1",
                    backgroundColor = GraphColor.BorderColor[i],
                    fill = false
                });
            }

            _chart.datasets = _dataSet;
            return _chart;
        }



        public Chart GetSeaServiceChart(string userId)
        {

            List<EmployeeReportDataModel> seaService = employeeReportRepository.GetSeaServiceGraph(userId);
            List<string> employeePnoList = seaService.Select(x => x.PNo).ToList();
            var list = new List<object>();
            var colorList = new List<object>();

            for (int i = 0; i < employeePnoList.Count; i++)
            {
                list.Add(employeePnoList[i]);
                colorList.Add(GraphColor.BorderColor[i]);

            }
            Chart _chart = new Chart();
            _chart.labels = list;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();



            _dataSet.Add(new Datasets()
            {
                label = "Sea Service",
                data = seaService.Select(x => x.SeaTDay).ToArray(),
                borderColor = colorList,
                borderWidth = "1",
                backgroundColor = colorList,
                //fill = false
            });



            _chart.datasets = _dataSet;
            return _chart;
        }


        public Chart GetTraceChart(string userId)
        {

            var employees = employeeReportRepository.Where(x => x.UserId == userId).ToList();
            foreach (var employee in employees)
            {
                employeeReportRepository.GetTraceGraph(employee.EmployeeId, userId);
            }


            var list = new List<object>();
            var colorList = new List<object>();

            for (int i = 0; i < employees.Count; i++)
            {
                list.Add(employees[i].EmpPno);
                colorList.Add(GraphColor.BorderColor[i]);

            }
            employees = employeeReportRepository.Where(x => x.UserId == userId).ToList();
            Chart _chart = new Chart();
            _chart.labels = list;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();



            _dataSet.Add(new Datasets()
            {
                label = "Trace Marks",
                data = employees.Select(x => x.Marks).ToArray(),
                borderColor = colorList,
                borderWidth = "1",
                backgroundColor = colorList,
                //fill = false
            });



            _chart.datasets = _dataSet;
            return _chart;
        }



        public Chart GetCourseResultChart(int categoryId, int? subCategoryId, int venue, string userId)
        {
            subCategoryId = subCategoryId ?? -1;
            List<EmployeeReportDataModel> courseResult = employeeReportRepository.GetCourseResultGraph(categoryId, subCategoryId, venue, userId);
            List<string> employeePnoList = courseResult.Select(x => x.PNo).ToList();
            var list = new List<object>();
            var colorList = new List<object>();

            for (int i = 0; i < employeePnoList.Count; i++)
            {
                list.Add(employeePnoList[i]);
                colorList.Add(GraphColor.BorderColor[i]);

            }
            Chart _chart = new Chart();
            _chart.labels = list;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();



            _dataSet.Add(new Datasets()
            {
                label = "Percentage",
                data = courseResult.Select(x => x.Percentage).ToArray(),
                borderColor = colorList,
                borderWidth = "1",
                backgroundColor = colorList,
                //fill = false
            });



            _chart.datasets = _dataSet;
            return _chart;
        }


        public Chart GetSeaCommandServiceChart(string userId)
        {

            List<EmployeeReportDataModel> seaService = employeeReportRepository.GetSeaCommandServiceGraph(userId);
            List<string> employeePnoList = seaService.Select(x => x.PNo).ToList();
            var list = new List<object>();
            var colorList = new List<object>();

            for (int i = 0; i < employeePnoList.Count; i++)
            {
                list.Add(employeePnoList[i]);
                colorList.Add(GraphColor.BorderColor[i]);

            }
            Chart _chart = new Chart();
            _chart.labels = list;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();



            _dataSet.Add(new Datasets()
            {
                label = "Sea Command Service",
                data = seaService.Select(x => x.TDay).ToArray(),
                borderColor = colorList,
                borderWidth = "1",
                backgroundColor = colorList,

            });

            _dataSet.Add(new Datasets()
            {
                label = "Sea Service",
                data = seaService.Select(x => x.SeaTDay).ToArray(),
                borderColor = colorList,
                borderWidth = "1",
                backgroundColor = colorList,

            });

            _chart.datasets = _dataSet;
            return _chart;
        }

        public Chart GetOprYearlyChart(int fromYear, int toYear, string userId)
        {

            var list = new List<object>();
            for (int i = fromYear; i <= toYear; i++)
            {
                list.Add(i);
            }
            Chart _chart = new Chart();
            _chart.labels = list;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();

            List<EmployeeReportDataModel> oprResult = employeeReportRepository.GetGetOprGraphYearlyReport(userId);
            List<string> employeePnoList = oprResult.Select(x => x.PNo).Distinct().ToList();
            for (int i = 0; i < employeePnoList.Count(); i++)
            {
                _dataSet.Add(new Datasets()
                {
                    label = employeePnoList[i],
                    data = oprResult.Where(x => x.PNo == employeePnoList[i]).Select(x => x.OprGrade).ToArray(),
                    borderColor = GraphColor.BorderColor[i],
                    borderWidth = "1",
                    backgroundColor = GraphColor.BorderColor[i],
                    fill = false
                });
            }

            _chart.datasets = _dataSet;
            return _chart;
        }

    }
}
