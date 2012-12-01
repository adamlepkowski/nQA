using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

namespace nQA.Web.Services
{
    public class OpenIdUser
    {
        public string Email { get; private set; }
        public string Nickname { get; private set; }
        public string FullName { get; private set; }
        public string ClaimedIdentifier { get; private set; }

        public OpenIdUser(ClaimsResponse claim, string identifier)
        {
            ProccessData(claim, identifier);
        }

        private void ProccessData(ClaimsResponse claim, string identifier)
        {
            Email = claim.Email;
            FullName = claim.FullName;
            Nickname = claim.Nickname ?? claim.Email;
            ClaimedIdentifier = identifier;
        }
    }
}