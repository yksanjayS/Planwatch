using SmartMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SmartMonitoring.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult Contacts()
        {
            Contact temp = new Contact();
            return View(temp);
        }

        [HttpPost]
        public ActionResult Contacts(Contact c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    MailAddress from = new MailAddress("info@vibration-service.com");
                    StringBuilder sb = new StringBuilder();
                    msg.IsBodyHtml = true;
                    smtp.Host = "smtp.zoho.com";
                    smtp.Port = 587;
                    msg.To.Add("support@vibration-service.com");
                    msg.From = from;
                    msg.Subject = c.Subject;
                    msg.Body += " <html>";
                    msg.Body += "<body>";
                    msg.Body += "<table>";
                    msg.Body += "<tr>";
                    msg.Body += "<td>First Name : </td><td>" + c.Name + "</td>";
                    msg.Body += "</tr>";
                    msg.Body += "<tr>";
                    msg.Body += "<td>Email ID : </td><td>" + c.Email + "</td>";
                    msg.Body += "</tr>";
                    msg.Body += "<tr>";
                    msg.Body += "<td>Contact No. : </td><td>" + c.Phone + "</td>";
                    msg.Body += "</tr>";
                    msg.Body += "<tr>";
                    msg.Body += "<td>Description : </td><td>" + c.Message + "</td>";
                    msg.Body += "</tr>";
                    msg.Body += "</table>";
                    msg.Body += "</body>";
                    msg.Body += "</html>";
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("info@vibration-service.com", "Brijesh1951?");
                    smtp.Send(msg);
                    msg.Dispose();
                    return RedirectToAction("Home", "Home");
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return View();
        }
    }
}