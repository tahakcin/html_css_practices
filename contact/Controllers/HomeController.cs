using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using System.Configuration;
using static StartUp.StartUp;
using Microsoft.Extensions.Options;



namespace contact.Controllers
{
    public class HomeController : Controller
    {

        readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Contact(string name, string email,string phone, string subject, string message)
        {
            var myEmail = configuration["MailInfo:MyEmail"];
            var myPassword = configuration["MailInfo:Password"];
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                
                mail.From = new MailAddress(email);
                mail.To.Add(myEmail);

                mail.Subject = "Contact Us Form: " + subject;
                mail.Body = $"Name: {name}\nEmail: {email}\nPhone: {phone}\nSubject: {subject}\nMessage: {message}";

                
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(myEmail, myPassword);
                smtpClient.EnableSsl = true; 

                smtpClient.Send(mail);
                ViewBag.Message = "Your message has been sent successfully!";

            }

            catch (Exception ex)
            {
                ViewBag.Message = "Error: " +  ex.Message;
            }

            return View();
        }
    }
}
