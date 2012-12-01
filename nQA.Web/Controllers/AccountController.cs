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

namespace nQA.Web.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            var openid = new OpenIdRelyingParty();
            var response = openid.GetResponse();
            if (response == null)
            {
                Identifier id;
                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    try
                    {
                        var request = openid.CreateRequest(Request.Form["openid_identifier"]);
                        var fetchRequest = new FetchRequest();
                        fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                        fetchRequest.Attributes.AddOptional(WellKnownAttributes.Name.First);
                        fetchRequest.Attributes.AddOptional(WellKnownAttributes.Name.Last);
                        request.AddExtension(fetchRequest);

                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        //TODO: Log
                        ViewBag.Message = ex.Message;
                        return View();
                    }
                }
                
                return View();
            }

            switch (response.Status)
            {

                case AuthenticationStatus.Authenticated:
                    //TODO: Add logic responsible for logging or registration process
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
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
