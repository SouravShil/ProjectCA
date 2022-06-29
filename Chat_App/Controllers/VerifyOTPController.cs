using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chap_App.Models;
using System.Net.Mail;

namespace Chap_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyOTPController : ControllerBase
    {
        private readonly ChatAppContext _context;

        public VerifyOTPController(ChatAppContext context)
        {
            _context = context;
        }

        [HttpPost]
        public string Post(Users user)
        {
            Random rnd = new Random();
            Int64 rndNumber = rnd.Next(111111, 999999);
            string otp = rndNumber.ToString();
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.outlook.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("chat.app.47@outlook.com", "chatapp47");
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = "Your OTP for Chat App";
            msg.Body = "\nYour otp for Reset Password: " + otp + "\n\n Please does not share your otp/this mail to anyone.";
            string toaddress = user.Email;
            msg.To.Add(toaddress);
            string fromaddress = "Chat App <chat.app.47@outlook.com>";
            msg.From = new MailAddress(fromaddress);
            try
            {
                smtp.Send(msg);
            }
            catch
            {
                throw;
            }
            return otp;
        }
    }
}
