using System.Web.Mvc;
//using NSubstitute;
using NSubstitute;
using NUnit.Framework;
using nQA.Web.Controllers;
using nQA.Web.Services;

namespace nQA.Web.Tests
{
    [TestFixture]
    public class AccountControllerTest
    {
        [Test]
        public void Login_UnauthorizationUserOpenLoginPage_OpenLoginPage()
        {
            var openIdMembershipServiceStub = Substitute.For<IOpenIdMembershipService>();
            openIdMembershipServiceStub.GetResponse().Returns(info => null);

            var controller = new AccountController(openIdMembershipServiceStub);

            var actionResult = controller.Login() as ViewResult;

            Assert.IsEmpty(actionResult.ViewName);
        }

        [Test]
        public void Login_Executed_GetResponseFromOpenIdMembershipServiceOnce()
        {
            var idMembershipServiceMock = Substitute.For<IOpenIdMembershipService>();

            var controller = new AccountController(idMembershipServiceMock);

            controller.Login();

            idMembershipServiceMock.Received(1).GetResponse();
        }
    }
}