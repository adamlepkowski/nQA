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
        public void Login_RequestFromTheUser_OpenLoginPage()
        {
            var openIdMembershipService = Substitute.For<IOpenIdMembershipService>();
            openIdMembershipService.GetResponse().Returns(info => null);

            var controller = new AccountController(openIdMembershipService);

            var actionResult = controller.Login() as ViewResult;

            Assert.IsEmpty(actionResult.ViewName);
        }
    }
}