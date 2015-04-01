using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Activation;
using Ninject.Syntax;
using Ninject.Web.Common;
using TrelloModel.Factories;
using TrelloModel.Interfaces;
using TrelloWebAPI;
using Ninject.Parameters;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace TrelloWebAPI
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        /// 
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            //ninject to resolve repository dependency
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectWebApiResolver(kernel); 

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IBoardRepositoryFactory>().To<BoardRepositoryFactory>();
            kernel.Bind<IListRepositoryFactory>().To<ListRepositoryFactory>();
            kernel.Bind<ICardRepositoryFactory>().To<CardRepositoryFactory>();
        }        
    }

    public class NinjectWebApiScope : IDependencyScope
    {
        private IResolutionRoot _resolver;

        public NinjectWebApiScope(IResolutionRoot resolver)
        {
            _resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (_resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed.");

            return _resolver.Resolve(CreateRequest(serviceType)).SingleOrDefault();
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (_resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed.");

            return _resolver.Resolve(CreateRequest(serviceType));
        }

        public void Dispose()
        {
            var disposable = _resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            _resolver = null;
        }

        private IRequest CreateRequest(Type serviceType)
        {
            return _resolver.CreateRequest(serviceType, null, new Parameter[0], true, true);
        }
    }

    // This class is the resolver, but it is also the global scope. So we derive from NinjectScope.
    public class NinjectWebApiResolver : NinjectWebApiScope, IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectWebApiResolver(IKernel kernel) : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectWebApiScope(_kernel.BeginBlock());
        }
    }
}