using System.Collections.Generic;
using System.Net.Mail;

namespace Minint.BL.Helpers
{
    public class EmailSenderHelper
    {

        public bool SendEmail(string from,
                              List<string> to,
                              string subject,
                              string body,
                              bool isBodyHtml = true,
                              List<string> cc = null,
                              List<string> cco = null)
        {
            try
            {
                var myMailMessage = new MailMessage
                {
                    From = new MailAddress(from)
                };

                foreach (var email in to)
                {
                    if (!string.IsNullOrEmpty(email))
                        myMailMessage.To.Add(email);
                }

                if (cc != null)
                {
                    foreach (var email in cc)
                    {
                        if (!string.IsNullOrEmpty(email))
                            myMailMessage.CC.Add(email);
                    }
                }

                if (cco != null)
                {
                    foreach (var email in cco)
                    {
                        if (!string.IsNullOrEmpty(email))
                            myMailMessage.Bcc.Add(email);
                    }
                }

                myMailMessage.Subject = subject;
                myMailMessage.IsBodyHtml = isBodyHtml;
                myMailMessage.Body = body;

                var SMTPClient = new SmtpClient();

                SMTPClient.Send(myMailMessage);

                return true;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
    }
}
