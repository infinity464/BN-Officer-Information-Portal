using Infinity.Bnois.Configuration.Data;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Reflection;

namespace Infinity.Bnois.Configuration
{
    public static class ConfigurationResolver
    {
        public static IKernel kernel = new StandardKernel();

        static ConfigurationResolver()
        {
            var caller = Assembly.GetEntryAssembly();
            if (caller == null)
            {


                kernel.Bind(x => x.FromAssembliesMatching("Infinity.Bnois.Api.dll").SelectAllClasses().InheritedFrom<IConfigurationFactory>().BindSelection((type, baseType) => new[] { typeof(IConfigurationFactory) }));
                kernel.Bind(x => x.FromAssembliesMatching("Infinity.Bnois.Api.Web.dll").SelectAllClasses().InheritedFrom<IConfigurationFactory>().BindSelection((type, baseType) => new[] { typeof(IConfigurationFactory) }));
            }

        }

        public static CompanyConfiguration Get()
        {
            try
            {
                var cfg = kernel.Get<IConfigurationFactory>();
                return cfg.Get();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
