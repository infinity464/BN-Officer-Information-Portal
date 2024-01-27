using System;
using System.Web;
using Infinity.Bnois.Api.Web;
using Infinity.Bnois.Api.Web.Data;
using Infinity.Bnois.Api.Web.Services;
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Data;
using Infinity.Bnois.Data;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;


[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Infinity.Bnois.Api.Web
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'NinjectWebCommon'
    public static class NinjectWebCommon 
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'NinjectWebCommon'
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
         //   kernel.Bind<BnoisDbContext>().ToSelf().InRequestScope();

        
          //  kernel.Bind(typeof(IBnoisRepository<>)).To(typeof(IBnoisRepository<>));

            //kernel.Bind<InfinityIdentityDbContext>().ToSelf().InRequestScope();
     
            kernel.Bind<IModuleService>().To<ModuleService>().InRequestScope();
            kernel.Bind<IModuleRepository>().To<ModuleRepository>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();

            kernel.Bind<IFeatureRepository>().To<FeatureRepository>().InRequestScope();
            kernel.Bind<IFeatureService>().To<FeatureService>().InRequestScope();

            kernel.Bind<IRoleFeatureRepository>().To<RoleFeatureRepository>().InRequestScope();
            kernel.Bind<IRoleFeatureService>().To<RoleFeatureService>().InRequestScope();

            kernel.Bind<IRoleService>().To<RoleService>().InRequestScope();

            kernel.Bind<ILanguageService>().To<LanguageService>().InRequestScope();
            kernel.Bind<ILanguageRepository>().To<LanguageRepository>().InRequestScope();
           kernel.Bind(x => x.FromAssemblyContaining<IRankService>().SelectAllClasses().BindDefaultInterfaces());
            kernel.Bind(x => x.FromAssemblyContaining<IRankRepository>().SelectAllClasses().BindDefaultInterfaces());


        }
    }
}
