using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace nQA.Web.Services
{
    /// <summary>
    /// Based on very good article about OpenId and MVC 4: http://www.strathweb.com/2012/08/adding-openid-authentication-to-your-asp-net-mvc-4-application/
    /// </summary>
    public class OpenIdMembershipService : IOpenIdMembershipService
    {
        private readonly OpenIdRelyingParty _openId;
        public OpenIdMembershipService()
        {
            _openId = new OpenIdRelyingParty();
        }

        public IAuthenticationRequest CreateRequest(string openidIdentifier)
        {
            var request = _openId.CreateRequest(openidIdentifier);
            var fetchRequest = new FetchRequest();
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
            fetchRequest.Attributes.AddOptional(WellKnownAttributes.Name.First);
            fetchRequest.Attributes.AddOptional(WellKnownAttributes.Name.Last);
            request.AddExtension(fetchRequest);

            return request;
        }

        public IAuthenticationResponse GetResponse()
        {
            return _openId.GetResponse();
        }

        public OpenIdUser ResponseIntoUser(IAuthenticationResponse response)
        {
            var claimResponseUntrusted = response.GetUntrustedExtension<ClaimsResponse>();
            var claimResponse = response.GetExtension<ClaimsResponse>();

            if (claimResponse != null)
            {
                return new OpenIdUser(claimResponse, response.ClaimedIdentifier);
            }
            if (claimResponseUntrusted != null)
            {
                return new OpenIdUser(claimResponseUntrusted, response.ClaimedIdentifier);
            }

            return null;
        }
    }
}