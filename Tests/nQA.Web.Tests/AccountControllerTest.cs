using System.Web.Mvc;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

using NSubstitute;
using NUnit.Framework;

using nQA.Model.Entities;
using nQA.Model.Interfaces;
using nQA.Web.Controllers;
using nQA.Web.Services;

namespace nQA.Web.Tests
{
    [TestFixture]
    public class AccountControllerTest
    {
        private AccountController CreateAccountController(
            IOpenIdMembershipService openIdMembershipService = null,
            IAuthenticationProvider authenticationProvider = null,
            IUserService userService = null,
            IUserProvider userProvider = null
            )
        {
            if (openIdMembershipService == null)
                openIdMembershipService = Substitute.For<IOpenIdMembershipService>();

            if (authenticationProvider == null)
                authenticationProvider = Substitute.For<IAuthenticationProvider>();

            if (userService == null)
                userService = Substitute.For<IUserService>();

            if (userProvider == null)
                userProvider = Substitute.For<IUserProvider>();

            return new AccountController(openIdMembershipService, authenticationProvider, userService, userProvider);
        }

        [Test]
        public void Login_UnauthorizationUserOpenLoginPage_OpenLoginPage()
        {
            var openIdMembershipServiceStub = Substitute.For<IOpenIdMembershipService>();
            openIdMembershipServiceStub.GetResponse().Returns(info => null);

            var controller = CreateAccountController(openIdMembershipService: openIdMembershipServiceStub);

            var actionResult = controller.Login() as ViewResult;

            Assert.IsEmpty(actionResult.ViewName);
        }

        [Test]
        public void Login_Executed_GetResponseFromOpenIdMembershipServiceOnce()
        {
            var idMembershipServiceMock = Substitute.For<IOpenIdMembershipService>();

            var controller = CreateAccountController(openIdMembershipService: idMembershipServiceMock);

            controller.Login();

            idMembershipServiceMock.Received(1).GetResponse();
        }

        [Test]
        public void Login_ResponseFromOpenIdProviderUserIsAuthenticated_UserLogged()
        {
            var authenticationProviderMock = Substitute.For<IAuthenticationProvider>();

            var authenticationResponseStub = Substitute.For<IAuthenticationResponse>();
            authenticationResponseStub.Status.Returns(AuthenticationStatus.Authenticated);

            var openIdMembershipServiceMock = Substitute.For<IOpenIdMembershipService>();
            openIdMembershipServiceMock.GetResponse().Returns(authenticationResponseStub);
            openIdMembershipServiceMock.ResponseIntoUser(authenticationResponseStub)
                                       .Returns(
                                           info => new OpenIdUser(
                                               new ClaimsResponse()
                                                   {
                                                       Email = "Email",
                                                       FullName = "FullName",
                                                       Nickname = "NickName"
                                                   },
                                               "openIdIdentifier"));

            var userServiceMock = Substitute.For<IUserService>();
            userServiceMock.Login("openIdIdentifier", "NickName", "FullName", "Email").Returns(new User { Login = "Login" });


            var userProvider = Substitute.For<IUserProvider>();

            var controller = CreateAccountController(
                openIdMembershipServiceMock, authenticationProviderMock, userServiceMock, userProvider);

            controller.Login();

            // TODO: refactor it - but how?
            openIdMembershipServiceMock.Received(1).ResponseIntoUser(authenticationResponseStub);
            userServiceMock.Received().Login("openIdIdentifier", "NickName", "FullName", "Email");
            authenticationProviderMock.Received(1).RedirectFromLoginPage("Login", true);
        }

        [Test]
        public void Login_CanceledResponseFromOpenIdProvider_ShowErrorMessage()
        {
            var authenticationResponse = Substitute.For<IAuthenticationResponse>();
            authenticationResponse.Status.Returns(AuthenticationStatus.Canceled);

            var openIdMembershipServiceStub = Substitute.For<IOpenIdMembershipService>();
            openIdMembershipServiceStub.GetResponse().Returns(authenticationResponse);
            
            var controller = CreateAccountController(openIdMembershipServiceStub);

            controller.Login();

            Assert.AreEqual("Provider canceled request.", controller.ModelState["error"].Errors[0].ErrorMessage);
        }

        [Test]
        public void Login_NotAuthenicatedAndCanceledResponseFromOpenIdProvider_ShowErrorMessage()
        {
            var authenticationResponse = Substitute.For<IAuthenticationResponse>();
            authenticationResponse.Status.Returns(AuthenticationStatus.Failed);

            var openIdMembershipServiceStub = Substitute.For<IOpenIdMembershipService>();
            openIdMembershipServiceStub.GetResponse().Returns(authenticationResponse);

            var controller = CreateAccountController(openIdMembershipServiceStub);

            controller.Login();

            Assert.AreEqual("Problem occured with retriving data from OpenId provider.", controller.ModelState["error"].Errors[0].ErrorMessage);
        }

        [Test]
        public void LogOff_Executed_UserLoggedOff()
        {
            var authenticationProvider = Substitute.For<IAuthenticationProvider>();

            var controller = CreateAccountController(authenticationProvider: authenticationProvider);

            controller.LogOff();

            authenticationProvider.Received(1).SignOut();
        }

        [Test]
        public void LogOff_Executed_UserProviderIsEmpty()
        {
            var userProvider = Substitute.For<IUserProvider>();

            var controller = CreateAccountController(userProvider: userProvider);

            controller.LogOff();

            Assert.IsNull(userProvider.CurrentUser);
        }

        [Test]
        public void LogOff_Executed_RedirectToHomePage()
        {
            var controller = CreateAccountController();

            var result = controller.LogOff() as RedirectToRouteResult;

            Assert.AreEqual("Home", result.RouteValues["controller"].ToString());
            Assert.AreEqual("Index", result.RouteValues["action"].ToString());
        }
    }
}