using System.Web;

using nQA.Model.Entities;

namespace nQA.Web.Services
{
    public class UserProvider : IUserProvider
    {
        private const string CurrentUserSessionKey = "CurrentUserSessionKey";

        public User CurrentUser
        {
            get
            {
                return HttpContext.Current.Session[CurrentUserSessionKey] as User;
            }
            set
            {
                HttpContext.Current.Session[CurrentUserSessionKey] = value;
            }
        }
    }
}