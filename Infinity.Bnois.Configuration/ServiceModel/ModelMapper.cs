
using Infinity.Bnois.Data;

namespace Infinity.Bnois.Configuration.ServiceModel
{
    
    public partial class ModelMapper
    {
        public static void SetUp()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                //*****************Data to API*********************
           

                cfg.CreateMap<Configuration.Models.Module, Configuration.ServiceModel.ModuleModel>();
                cfg.CreateMap<Configuration.Models.Feature, Configuration.ServiceModel.FeatureModel>()
                .ForMember(d => d.Module, opt => opt.MapFrom(s => s.Module));
         
                //****************API to Data*********************
                cfg.CreateMap<Configuration.ServiceModel.ModuleModel, Configuration.Models.Module>();
                cfg.CreateMap<Configuration.ServiceModel.FeatureModel, Configuration.Models.Feature>()
                .ForMember(d => d.Module, opt => opt.MapFrom(s => s.Module));


            });
        }
    }
}
