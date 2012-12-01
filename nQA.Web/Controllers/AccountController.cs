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

using nQA.Web.Models;
using nQA.Web.Services;

namespace nQA.Web.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IOpenIdMembershipService _openIdMembershipService;

        public AccountController(IOpenIdMembershipService openIdMembershipService)
        {
            _openIdMembershipService = openIdMembershipService;
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            IAuthenticationResponse response = _openIdMembershipService.GetResponse();
            if (response == null)
            {
                return View();
            }

            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    //TODO: Add logic responsible for logging or registration process
                    var openIdUser = _openIdMembershipService.ResponseIntoUser(response);
                    //FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return View();

                case AuthenticationStatus.Canceled:
                    ModelState.AddModelError("", "Canceled at provider");
                    return View();
                case AuthenticationStatus.Failed:
                    ModelState.AddModelError("", "Problem occured" + response.Exception.Message);
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
                var request = _openIdMembershipService.CreateRequest(openid_identifier);
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
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
