using Infinity.Bnois.Configuration.Data;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.Configuration.ServiceModel;
using Infinity.Bnois.ExceptionHelper;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity.Bnois.Configuration
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        private readonly IRoleFeatureRepository _roleFeaturesRepository;
        private readonly IFeatureRepository _featuresRepository;
        public ModuleService(IModuleRepository moduleRepository, IRoleFeatureRepository roleFeaturesRepository, IFeatureRepository featuresRepository)
        {
            this._moduleRepository = moduleRepository;
            this._roleFeaturesRepository = roleFeaturesRepository;
            this._featuresRepository = featuresRepository;
        }
        public List<ModuleModel> GetModules(int pageSize, int pageNumber, string searchText, out int total)
        {
            List<Models.Module> modules = _moduleRepository.GetAllModules(pageSize, pageNumber, searchText, out total);
            return ObjectConverter<Models.Module, ModuleModel>.ConvertList(modules).ToList();
        }
        public ModuleModel GetModule(int moduleId)
        {
            if (moduleId == 0)
            {
                return new ModuleModel();
            }
            Models.Module module = _moduleRepository.FindOne(x => x.ModuleId == moduleId);

            if (module == null)
            {
                throw new InfinityNotFoundException("Module not found !");
            }
            return ObjectConverter<Models.Module, ModuleModel>.Convert(module);
        }

        public ModuleModel Save(int moduleId, ModuleModel model)
        {
            Models.Module module = new Models.Module();

            if (moduleId > 0)
            {
                module = _moduleRepository.FindOne(x => x.ModuleId == moduleId);

                if (module == null)
                {
                    throw new InfinityNotFoundException("Module not found !");
                }

                module.Name = model.Name;
                module.OrderNo = model.OrderNo;
                module.IsReport = false;
                module.EditedDate = DateTime.Now;
            }
            else
            {
                module = ObjectConverter<ModuleModel, Models.Module>.Convert(model);
                module.CreatedDate = DateTime.Now;
            }
            module.IsActive = true;
           _moduleRepository.Save(module);
            model.ModuleId = module.ModuleId;
            return model;
        }

        public int Delete(int moduleId)
        {
            if (moduleId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Module!");
            }

            Models.Module module = _moduleRepository.FindOne(p => p.ModuleId == moduleId);

            if (module == null)
            {
                throw new InfinityNotFoundException("modules/Hrm/Module/Modules.html");
            }

            _moduleRepository.Delete(module);
            return 1;
        }

        public List<SelectModel> GetModules()
        {
            List<Models.Module> modules = _moduleRepository.AsQueryable().ToList();
            return modules.Select(x => new SelectModel { Text = x.Name, Value = x.ModuleId }).ToList();
        }

        public List<ModuleModel> GetModuleFeatures(FeatureType featureTypeId)
        {
            if (!ConfigurationResolver.Get().LoggedInUser.RoleIds.Any() && !ConfigurationResolver.Get().LoggedInUser.UserRoles.Any())
            {
                throw new InfinityNotFoundException("Invalid User");
            }
            else
            {
                string[] roleIds = ConfigurationResolver.Get().LoggedInUser.RoleIds;
                int[] featureIds = _roleFeaturesRepository.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.FeatureKey).Distinct().ToArray();
                List<Feature> feautres = _featuresRepository.FilterWithInclude(x => x.IsActive&&x.FeatureTypeId== featureTypeId && x.FeatureCode != 1091 && featureIds.Contains(x.FeatureId), "Module").ToList();
                List<Module> modeules = feautres.GroupBy(x => x.Module.Name).Select(x => x.First().Module).ToList();
                if (!featureIds.Any())
                {
                    throw new InfinityNotFoundException("Features Not Assaigned");
                }

                modeules = modeules.OrderBy(x=>x.OrderNo).Select(x =>
                 {
                     x.Features = feautres.Where(y => y.ModuleId == x.ModuleId).OrderBy(o=>o.FeatureName).ToList();
                     return x;
                 }).ToList();

                return ObjectConverter<Models.Module, ModuleModel>.ConvertList(modeules).ToList();

            }

        }


        public async Task<List<SelectModel>> GetCurrentStatusMenu()
        {
            if (!ConfigurationResolver.Get().LoggedInUser.RoleIds.Any() && !ConfigurationResolver.Get().LoggedInUser.UserRoles.Any())
            {
                throw new InfinityNotFoundException("Invalid User");
            }
            else
            {
                string[] roleIds = ConfigurationResolver.Get().LoggedInUser.RoleIds;
                int[] featureIds = _roleFeaturesRepository.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.FeatureKey).Distinct().ToArray();

                ICollection<Feature> feautres = await _featuresRepository.FilterAsync(x => x.IsActive && x.FeatureTypeId == FeatureType.CurrentStatus && featureIds.Contains(x.FeatureId));
                List<SelectModel> selectModels = feautres.OrderBy(x=>x.OrderNo).Select(x => new SelectModel
                {
                    Text = x.FeatureName,
                    Value = x.ActionNgHref
                }).ToList();
                return selectModels;

            }
          
        }


        public List<Node> GetModuleReports(FeatureType feature)
        {
            if (!ConfigurationResolver.Get().LoggedInUser.RoleIds.Any() && !ConfigurationResolver.Get().LoggedInUser.UserRoles.Any())
            {
                throw new InfinityNotFoundException("Invalid User");
            }
            else
            {
                string[] roleIds = ConfigurationResolver.Get().LoggedInUser.RoleIds;
                int[] featureIds = _roleFeaturesRepository.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.FeatureKey).Distinct().ToArray();
                List<Feature> feautres = _featuresRepository.FilterWithInclude(x => x.IsActive && x.FeatureTypeId == feature && featureIds.Contains(x.FeatureId), "Module").ToList();
                List<Node> modeules = feautres.GroupBy(x => x.Module.Name).Select(x => x.First().Module).Select(x => new Node() { Id = x.ModuleId.ToString(), Text = x.Name }).ToList();
                if (!featureIds.Any())
                {
                    throw new InfinityNotFoundException("Features Not Assaigned");
                }

                modeules = modeules.Select(x =>
                {
                    int id =Convert.ToInt32(x.Id);
                    x.Nodes = feautres.Where(y => y.ModuleId == id).Select(y => new Node() { Id = y.ActionNgHref, Text = y.FeatureName,IsChaild=y.IsReport }).ToList();
                    return x;
                }).ToList();

                return ObjectConverter<Node, Node>.ConvertList(modeules).ToList();

            }

        }
        public byte[] downloadModuleReport()
        {
            System.Data.DataTable ModuleDatatable = _moduleRepository.ExecWithSqlQuery(string.Format("EXEC spGetModuleReport"));
            var localReport = new LocalReport
            {
                DisplayName = "Modules",
                ReportEmbeddedResource = "Infinity.Bnois.Configuration.AppReport.Modules.rdlc",
                EnableHyperlinks = true
            };

            var ModuleDataSource = new ReportDataSource("ModuleDataSet", ModuleDatatable);

            localReport.DataSources.Add(ModuleDataSource);

            // Render the report as PDF.
            Warning[] warnings;
            string[] streams;
            string mimeType, encoding, fileNameExtension;
            var renderedReport = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            return renderedReport;

        }
    }
}