using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TV.Replays.IDAL;
using TV.Replays.XmlDAL;

namespace TV.Replays.WebApi.Models
{
    public class NinjectDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel Kernel;
        public NinjectDependencyResolver()
        {
            Kernel = new StandardKernel();

            Kernel.Bind<IPlayerDal>().To<PlayerDal>();
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInterface || serviceType.IsAbstract)
            {
                return null;
            }
            else
            {
                return Kernel.GetService(serviceType);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        { }
    }
}