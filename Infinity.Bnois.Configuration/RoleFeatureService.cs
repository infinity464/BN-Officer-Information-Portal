
using Infinity.Bnois.Configuration.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Infinity.Bnois.Configuration.ServiceModel;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.Configuration
{
    public class RoleFeatureService : IRoleFeatureService
    {
        private readonly IRoleFeatureRepository roleFeatureRepository;
        private readonly IFeatureRepository featureRepository;

        private ConfigurationDbContext context = new ConfigurationDbContext();
        public RoleFeatureService()
        {
            this.roleFeatureRepository = new RoleFeatureRepository(context);
            this.featureRepository = new FeatureRepository(context);
        }

        public List<RoleFeatureModel> GetRoleFeatures(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                throw new ExceptionHelper.InfinityArgumentMissingException("Invalid Role !");
            }
            IQueryable<Infinity.Bnois.Configuration.Models.Feature> features = featureRepository.FilterWithInclude(x => x.IsActive, "Module");
            // List<int> roleFeatures = roleFeatureRepository.Where(x => x.RoleId == roleId).Select(x => x.FeatureKey).ToList();

            var roleFeatures = roleFeatureRepository.Where(x => x.RoleId == roleId);

            List<RoleFeatureModel> roleFeatureList = features.Select(x => new RoleFeatureModel { FeatureKey = x.FeatureId, FeatureName = x.FeatureName, ModuleName = x.Module.Name, IsAssigned = roleFeatures.Any(y => x.FeatureId == y.FeatureKey)
                ,Add=  roleFeatures.Any(y => x.FeatureId == y.FeatureKey&&y.Add),
                Update = roleFeatures.Any(y => x.FeatureId == y.FeatureKey && y.Update),
                Delete = roleFeatures.Any(y => x.FeatureId == y.FeatureKey && y.Delete),
                Report = roleFeatures.Any(y => x.FeatureId == y.FeatureKey && y.Report),
                FeatureTypeId =x.FeatureTypeId, IsReport = x.IsReport }).ToList();

            return roleFeatureList;
        }

        public List<RoleFeatureModel> GetRoleFeatures1(string roleId)
        {

            if (string.IsNullOrEmpty(roleId))
            {
                throw new ExceptionHelper.InfinityArgumentMissingException("Invalid Role !");
            }
            List<Infinity.Bnois.Configuration.Models.Feature> features = featureRepository.FilterWithInclude(x => x.IsActive, "Module").ToList();
            List<int> roleFeatures = roleFeatureRepository.Where(x => x.RoleId == roleId).Select(x => x.FeatureKey).ToList();
            List<RoleFeatureModel> modeules = features.GroupBy(x => x.Module.Name).Select(x =>
            {
                return new RoleFeatureModel { RoleId = roleId, ModuleName = x.First().Module.Name };
            }).ToList();
            modeules = modeules.Select(y =>
            {
                y.Nodes = features.Where(x => x.Module.Name == y.ModuleName).Select(x => new RoleFeatureModel { FeatureKey = x.FeatureId, FeatureName = x.FeatureName, ModuleName = x.Module.Name, IsAssigned = roleFeatures.Any(z => x.FeatureId == z) }).ToList();
                return y;
            }).ToList();

            return modeules;
        }

        public bool SaveRoleFeatures(string roleId, Infinity.Bnois.Configuration.ServiceModel.RoleFeatureModel[] roleFeatures)
        {
            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new ExceptionHelper.InfinityArgumentMissingException("Invalid Role !");
            }

            List<Infinity.Bnois.Configuration.Models.RoleFeature> roleFeatureList = roleFeatures.Where(x => x.IsAssigned).Select(y => new Infinity.Bnois.Configuration.Models.RoleFeature() { RoleId = roleId, FeatureKey = y.FeatureKey }).ToList();

            if (DeleteRoleFeatures(roleId))
            {
                roleFeatureRepository.AddRange(roleFeatureList);
                roleFeatureRepository.SaveChanges();
            }
            return false;
        }

        private bool DeleteRoleFeatures(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new ExceptionHelper.InfinityArgumentMissingException("Invalid Role !");
            }
            List<Infinity.Bnois.Configuration.Models.RoleFeature> roleFeatureList = roleFeatureRepository.Where(x => x.RoleId == roleId).ToList();
            if (roleFeatureList.Any())
            {

                return roleFeatureRepository.RemoveRange(roleFeatureList) > 0;
            }

            return true;
        }

        public int[] GetUserFeature(string[] roleIds)
        {
            int[] features = roleFeatureRepository.Where(x => roleIds.Contains(x.RoleId)).Select(y => y.FeatureKey).ToArray();
            return featureRepository.Where(x => features.Contains(x.FeatureId)).Select(y => y.FeatureCode).ToArray();
        }

        public bool AssignRoleFeatures(string roleId, RoleFeatureModel rfeature)
        {

            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new ExceptionHelper.InfinityArgumentMissingException("Invalid Role !");
            }
            if (rfeature.IsAssigned)
            {

                RoleFeature roleFeature= roleFeatureRepository.FindOne(x => x.RoleId == roleId && x.FeatureKey == rfeature.FeatureKey)??new RoleFeature();
                roleFeature.RoleId = roleId;
                roleFeature.FeatureKey = rfeature.FeatureKey;
                roleFeature.Add = rfeature.Add; roleFeature.Update = rfeature.Update;
                roleFeature.Delete = rfeature.Delete;
                roleFeature.Report = rfeature.Report;
                roleFeatureRepository.Save(roleFeature);
            }
            else
            {
                roleFeatureRepository.Delete(x => x.RoleId == roleId && x.FeatureKey == rfeature.FeatureKey);
            }
            return roleFeatureRepository.SaveChanges() > 0;


        }

        public  RoleFeature GetPermitedRoleFeatures(int featureCode)
        {
            string[] roleIds = ConfigurationResolver.Get().LoggedInUser.RoleIds;
            Feature feature = featureRepository.FindOne(x => x.FeatureCode == featureCode);
            RoleFeature roleFeature = roleFeatureRepository.FindOne(x => roleIds.Contains(x.RoleId) && x.FeatureKey == feature.FeatureId);
            return roleFeature;
        }
    }
}
