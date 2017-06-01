using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kabadi.Model;
using System.Configuration;
using System.IO;
using BizBrolly.Util;

using System.Net.Mail;
using System.Net;

using log4net;

namespace Kabadi.Model
{
    public class Mailer
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool SendContactMail(string To, string Name, string RequesterMail, string subject, string from, string contact, string message, string CC, string BCC, bool enableSsl, string filePath)
        {
            try
            {
                ContactEmailDetails contactdetail = new ContactEmailDetails();
                
               // Emailer em = new Emailer();
                StreamReader reader = System.IO.File.OpenText(filePath);
                string body = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                body = body.Replace("$NameOfReceiver", contactdetail.ContactNameOfReceiver);
                body = body.Replace("$message", contactdetail.ContactTemplateMessage);
                body = body.Replace("$Content", contactdetail.ContactTemplateContent);
                body = body.Replace("$Name", Name);
                body = body.Replace("$CurrentDate", DateTime.Now.ToShortDateString());
                body = body.Replace("$EMAIL", RequesterMail);
                body = body.Replace("$MOBILE", contact);
                body = body.Replace("$CMESSAGE", message);
               // em.SendMail(To, subject, body, from, CC, BCC, enableSsl);
                SendMail(To, subject, body, from, CC, BCC, enableSsl);
                return true;
            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
                return false;
            }
        }
        public bool SendMail(string To, String subject, string body, string emailfrom, string CC, String BCC, bool enableSsl)
        {
            MailMessage msg = new MailMessage();
            try
            {
                if (To != null & To != "")
                    msg.To.Add(To);
                if (subject != null & subject != "")
                    msg.Subject = subject;
                if (body != null & body != "")
                    msg.Body = body;
                if (BCC != null & BCC != "")
                    msg.Bcc.Add(BCC);

                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.From = new MailAddress(emailfrom, "Query");
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.EnableSsl = enableSsl;
                client.UseDefaultCredentials = true;

                var fromAddress = new MailAddress("brajeshkrgaya@gmail.com");
                var fromPassword = "pass9852";
                client.Host = "smtp.gmail.com";

                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                string strEx = ex.Message;
                return false;
            }
            finally
            {
                msg.Dispose();
            }
        }
    }

}