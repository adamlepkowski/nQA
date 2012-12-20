using Ninject.Modules;

using nQA.Model.Interfaces;
using nQA.Model.Services;
using nQA.Web.Services;

namespace nQA.Web
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IOpenIdMembershipService>().To<OpenIdMembershipService>();
            this.Bind<IAuthenticationProvider>().To<AuthenticationProvider>();
            this.Bind<IUserProvider>().To<UserProvider>();
            this.Bind<IUserService>().To<UserService>();
        }
    }
}