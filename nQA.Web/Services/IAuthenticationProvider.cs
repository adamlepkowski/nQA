namespace nQA.Web.Services
{
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Redirects an authenticated user back to the originally requested URL or the default URL.
        /// </summary>
        /// <param name="userName">The authenticated user name. </param><param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param><exception cref="T:System.Web.HttpException">The return URL specified in the query string contains a protocol other than HTTP: or HTTPS:.</exception>
        void RedirectFromLoginPage(string userName, bool createPersistentCookie);

        void SignOut();
    }
}