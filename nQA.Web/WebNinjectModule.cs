using Ninject.Modules;
using nQA.Web.Services;

namespace nQA.Web
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IOpenIdMembershipService>().To<OpenIdMembershipService>();
        }
    }
}