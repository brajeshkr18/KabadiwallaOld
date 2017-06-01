using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kabadi.Model;
using T.Business;
using System.Configuration;
using System.Globalization;

namespace TelaCall.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (Session["UserInfo"] == null)
                    return RedirectToAction("Logout");
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult RequestRaddi()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        
        public ActionResult SignIn()
        {
            return View();
        }
        
        public ActionResult Logout()
        {
            KillUser();
            return RedirectToAction("Index", "Home");
        }
        private void KillUser()
        {
            FormsAuthentication.SetAuthCookie("username", false);
            FormsAuthentication.SignOut();
            Session.Abandon();
        }


        #region Postback Methods
        [HttpPost]
        public ActionResult SignIn(clsSignin objSignin)
        {
            try
            {
                if ((new HomeBLL()).VerifyLogin(ref objSignin))
                {
                    FormsAuthentication.SetAuthCookie(objSignin.Username, false);
                    Response.Cookies[objSignin.Username].Expires = DateTime.Now.AddSeconds(10);
                    Session["UserInfo"] = objSignin;
                    return Json(new { Status = "Success" });
                }
                return Json(new { Status = "Failure", Message = "Username/Password does not match" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Failure", Message = "A network related issue occered. Please try again to login" });
            }

        }
       
        [HttpPost]
        public ActionResult ContactUs(clsContact contactProperties)
        {

            if (contactProperties != null && !string.IsNullOrEmpty(contactProperties.Email) && !string.IsNullOrEmpty(contactProperties.Message))
            {
                Mailer Emailer = new Mailer();
                Emailer.SendContactMail(ConfigurationManager.AppSettings["EmailTo"].ToString(), contactProperties.Name, contactProperties.Email, ConfigurationManager.AppSettings["Subject"].ToString(), ConfigurationManager.AppSettings["EmailFrom"].ToString(), contactProperties.phone, contactProperties.Message, ConfigurationManager.AppSettings["CC"].ToString(), ConfigurationManager.AppSettings["BCC"].ToString(), true, Server.MapPath(ConfigurationManager.AppSettings["EmailTemplatePath"].ToString() + "Template.html"));
                return Json(new { Status = "Success", Message = "Congratulation ! We got your contact request. We will soon get in touch with you." });
            }
            return Json(new { Status = "Failure", Message = "oops Something went wrong!" });
        }
        [HttpPost]
        public ActionResult KabadRequest(clsKabadRequest objKabadRequest)
        {
            //objspeciality.UserId = 101;
            var rEQUEST_Result = (new HomeBLL()).SaveKabadRequest(objKabadRequest);
            //if()
            if (objKabadRequest != null && !string.IsNullOrEmpty(objKabadRequest.Email) && !string.IsNullOrEmpty(objKabadRequest.Message))
            {
                Mailer Emailer = new Mailer();
                Emailer.SendContactMail(ConfigurationManager.AppSettings["EmailTo"].ToString(), objKabadRequest.Name, objKabadRequest.Email,
                    ConfigurationManager.AppSettings["Subject"].ToString(), ConfigurationManager.AppSettings["EmailFrom"].ToString(),
                    objKabadRequest.phone, objKabadRequest.Message,objKabadRequest.Email , ConfigurationManager.AppSettings["BCC"].ToString(),
                    true, Server.MapPath(ConfigurationManager.AppSettings["EmailTemplatePath"].ToString() + "Template.html"));
                return Json(new { Status = "Success", Message = "Congratulation ! We got your contact request. We will soon get in touch with you." });
            }
            return Json(new { Status = "Failure", Message = "oops Something went wrong!" });
        }
        #endregion
       
    }
}
