using System.Web.Security;

namespace nQA.Web.Services
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public void RedirectFromLoginPage(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.RedirectFromLoginPage(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}