using Ninject.Modules;
using nQA.Model.Interfaces;
using nQA.Model.Repositories;
using nQA.Model.Services;

namespace nQA.Model
{
    public class ModelNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DatabaseContext>().ToSelf();//TODO: Instance per request
            Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
            Bind<IUserService>().To<UserService>();
        }
    }
}