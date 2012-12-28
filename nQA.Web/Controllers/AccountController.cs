using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

using nQA.Model.Interfaces;
using nQA.Web.Models;
using nQA.Web.Services;

namespace nQA.Web.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IOpenIdMembershipService openIdMembershipService;
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IUserService userService;
        private readonly IUserProvider userProvider;

        public AccountController(IOpenIdMembershipService openIdMembershipService, IAuthenticationProvider authenticationProvider, IUserService userService, IUserProvider userProvider)
        {
            this.openIdMembershipService = openIdMembershipService;
            this.authenticationProvider = authenticationProvider;
            this.userService = userService;
            this.userProvider = userProvider;
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            IAuthenticationResponse response = this.openIdMembershipService.GetResponse();
            if (response == null)
            {
                return View();
            }

            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    // get information about user from response
                    var openIdUser = this.openIdMembershipService.ResponseIntoUser(response);

                    // create or retrieve user
                    userProvider.CurrentUser = userService.Login(openIdUser.ClaimedIdentifier, openIdUser.Nickname, openIdUser.FullName, openIdUser.Email);

                    // authenticate user
                    authenticationProvider.RedirectFromLoginPage(userProvider.CurrentUser.Login, true);
                    break;
                case AuthenticationStatus.Canceled: //Canceled
                    ModelState.AddModelError("error", "Provider canceled request.");
                    return View();
                default:
                    ModelState.AddModelError("error", "Problem occured with retriving data from OpenId provider.");
                    // TODO: log it
                    return View();
            }

            return new EmptyResult();
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string openid_identifier)
        {
            try
            {
                var request = this.openIdMembershipService.CreateRequest(openid_identifier);
                return request.RedirectingResponse.AsActionResult();
            }
            catch (ProtocolException ex)
            {
                //TODO: LOG
            }
            return View();
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            authenticationProvider.SignOut();
            userProvider.CurrentUser = null;

            return RedirectToAction("Index", "Home");
        }
    }
}
