using DotNetOpenAuth.OpenId.RelyingParty;

namespace nQA.Web.Services
{
    public interface IOpenIdMembershipService 
    {
        IAuthenticationRequest CreateRequest(string openidIdentifier);

        IAuthenticationResponse GetResponse();
        OpenIdUser ResponseIntoUser(IAuthenticationResponse response);
    }
}