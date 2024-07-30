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
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace contact.Controllers
{
    public class NameNotProvidedException : Exception
    {
        public NameNotProvidedException() : base("Name field cannot be left blank.") { }
        public NameNotProvidedException(string message) : base(message) { }
        public NameNotProvidedException(string message, Exception inner) : base(message, inner) { }
    }
    public class EmailNotProvidedException : Exception
    {
        public EmailNotProvidedException() : base("Email field cannot be left blank.") { }
        public EmailNotProvidedException(string message) : base(message) { }
        public EmailNotProvidedException(string message, Exception inner) : base(message, inner) { }
    }

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
                if (name == null) { throw new NameNotProvidedException(); }
                if (email == null) { throw new EmailNotProvidedException(); }
                if (message == null) {}
                
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

            catch (NameNotProvidedException ex)
            {
                ViewBag.Message = ex.Message;
            }

            catch (EmailNotProvidedException ex)
            {
                ViewBag.Message = ex.Message;
            }

            catch (Exception ex)
            {
                ViewBag.Message = "Error: " +  ex.Message;
            }
            

            return View();
        }
    }
}
