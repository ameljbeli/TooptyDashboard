using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TooptyDashboard.Models;
namespace TooptyDashboard.Controllers
{
    public class RegistrationAdminController : Controller
    {
        // GET: RegistrationAdmin
        public ActionResult Registration(int id = 0)
        {
            Admin user = new Admin();
            return View(user);
        }
        [HttpPost]
        public ActionResult Registration(Admin user)
        {
            using (TooptyDBEntities db = new TooptyDBEntities())
            {
                if (db.Admins.Any(x => x.Name == user.Name))
                {
                    ViewBag.DuplicateMessage = "User Name Alreay Exists.";
                    return View("Registration", user);
                }
                else if (ModelState.IsValid)
                
                    {
                    
                        db.Admins.Add(user);
                        db.SaveChanges();

                    ////Send Email to User
                    //SendVerificationLinkEmail(user.E_mail, user.ActivationCode.ToString());
                    //message = "Registration successfully done. Account activation link " +
                    //    " has been sent to your email id:" + user.EmailID;
                    //Status = true;

                }
                ModelState.Clear();
                ViewBag.SuccessMessage = "Saved Successfully.";
                return View("Registration", new Admin());
            }
        }

        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("dotnetawesome@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "********"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                " successfully created. Please click on the below link to verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }
}