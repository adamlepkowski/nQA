using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace nQA.Web.Services
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(modules);
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType, new IParameter[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType, new IParameter[0]);
        }
    }
}