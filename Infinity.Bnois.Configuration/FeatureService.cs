using Infinity.Bnois.Configuration.Data;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.ExceptionHelper;
using LazyCache;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infinity.Bnois.Configuration
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IAppCache _appCache;
        public FeatureService(IFeatureRepository featureRepository)
        {
            this._featureRepository = featureRepository;
            this._appCache = new CachingService();
        }

        public List<Infinity.Bnois.Configuration.ServiceModel.FeatureModel> GetFeatures(int pageSize, int pageNumber, string searchText, out int total)
        {

            IQueryable<Feature> featureQuery = _featureRepository.FilterWithInclude(x => x.FeatureName.Contains(searchText)||String.IsNullOrEmpty(searchText)|| x.Module.Name.Contains(searchText),"Module");
            total = featureQuery.Count();
            featureQuery = featureQuery.OrderByDescending(x => x.FeatureName).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return ObjectConverter<Models.Feature, ServiceModel.FeatureModel>.ConvertList(featureQuery.ToList()).ToList();
        }

        public ServiceModel.FeatureModel GetFeature(int featureId)
        {
            if (featureId == 0)
            {
                return new Infinity.Bnois.Configuration.ServiceModel.FeatureModel() {FeatureTypeId=FeatureType.Feature };
            }

            Models.Feature feature = _featureRepository.FindOne(x => x.FeatureId == featureId);

            if (feature == null)
            {
                throw new InfinityNotFoundException("Feature not found !");
            }
            return ObjectConverter<Models.Feature, Infinity.Bnois.Configuration.ServiceModel.FeatureModel>.Convert(feature);
        }

        public Infinity.Bnois.Configuration.ServiceModel.FeatureModel Save(int featureId, Infinity.Bnois.Configuration.ServiceModel.FeatureModel model)
        {
            Models.Feature feature = new Models.Feature();

            if (featureId > 0)
            {
                feature = _featureRepository.FindOne(x => x.FeatureId == featureId);

                if (feature == null)
                {
                    throw new ExceptionHelper.InfinityNotFoundException("Feature not found !");
                }
                feature.ModuleId = model.ModuleId;
                feature.FeatureName = model.FeatureName;
                feature.ActionNgHref = model.ActionNgHref;
                feature.OrderNo = model.OrderNo;
                feature.IsReport =FeatureType.Report== model.FeatureTypeId;
                feature.FeatureTypeId = model.FeatureTypeId;
                feature.FeatureCode = model.FeatureCode;
                feature.EditedDate = DateTime.Now;
            }
            else
            {
                feature = ObjectConverter<Infinity.Bnois.Configuration.ServiceModel.FeatureModel, Models.Feature>.Convert(model);
                feature.CreatedDate = DateTime.Now;
                feature.IsReport = FeatureType.Report == model.FeatureTypeId;
            }
            feature.IsActive = true;

            _featureRepository.Save(feature);
            model.FeatureId = feature.FeatureId;

            return model;
        }

        public int Delete(int featureId)
        {
            if (featureId <= 0)
            {
                throw new ExceptionHelper.InfinityArgumentMissingException("Invalid Feature!");
            }

            Models.Feature feature = _featureRepository.FindOne(p => p.FeatureId == featureId);

            if (feature == null)
            {
                throw new InfinityNotFoundException("features/Hrm/Feature/Features.html");
            }

            _featureRepository.Delete(feature);
            return 1;
        }

        public byte[] downloadFeatureReport()
        {
            System.Data.DataTable FeatureDatatable = _featureRepository.ExecWithSqlQuery(string.Format("EXEC spGetFeatureReport"));
            var localReport = new LocalReport
            {
                DisplayName = "Features",
                ReportEmbeddedResource = "Infinity.Bnois.Configuration.AppReport.Features.rdlc",
                EnableHyperlinks = true
            };

            var FeatureDataSource = new ReportDataSource("FeaturesDataSet", FeatureDatatable);

            localReport.DataSources.Add(FeatureDataSource);

            // Render the report as PDF.
            Warning[] warnings;
            string[] streams;
            string mimeType, encoding, fileNameExtension;
            var renderedReport = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            return renderedReport;
        }
    }
}