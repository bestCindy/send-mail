using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml.Linq;
using System.Net.Mail;

namespace TestSendMail.Services
{
    public class SendMail
    {
        string mail_receivers, mail_ccs, mail_mobiles, mail_folder, mail_sign, mail_connectionname, mail_securitytype, mail_host,mail_account, mail_accountpass;
        SmtpClient smtp = new SmtpClient();
        int mail_port;
        public void SentMail()
        {
            string configpathmail = "D:/TestSendMail/TestSendMail/Config.xml";
            var configMail = XElement.Load(configpathmail);  /*加载XElement文件  XElement表示一个XML元素*/
            var connmail = configMail.Element("SMTP").Element("Groups");
            var mainmailpro = configMail.Element("SMTP");
            MailMessage mailMessage = new MailMessage(); /*MailMessage 类的实例用于构造可使用 SmtpClient 类传输到 SMTP 服务器以便传递的电子邮件。*/
            try
            {
                int count = 0;
                foreach (XElement xe in connmail.Elements())
                {
                    if (xe.Attribute("Name").Value.Equals("TestUsers"))
                    {
                        mail_receivers = xe.Element("MailTo").Value.Trim();
                        mail_ccs = xe.Element("CCTo").Value.Trim();
                        mail_mobiles = xe.Element("MSGTo").Value.Trim();
                        count++;
                    }
                }/*获得邮件的基本配置信息*/

                mail_folder = mainmailpro.Element("AttachmentsFolder").Value.Trim();
                mail_sign = mainmailpro.Element("Signature").Value.Trim();
                mail_connectionname = mainmailpro.Element("Connection").Value.Trim();
                mail_securitytype = mainmailpro.Element("SecurityType").Value.Trim();
                mail_host = mainmailpro.Element("SmtpHost").Value.Trim();
                mail_port = int.Parse(mainmailpro.Element("SmtpPort").Value.Trim());
                mail_account = mainmailpro.Element("EmailAddress").Value.Trim();
                //mail_accountpass = DecryptString(mainmailpro.Element("EmailPass").Value.Trim());
                mail_accountpass = mainmailpro.Element("EmailPass").Value.Trim();
                //if (count > 1)  WriteMsg("配置文件中配置了多次邮件组" + _sr.SMTP_GROUP);

            }
            catch (Exception ex)
            {
                //WriteMsg(ex.Message);
            }
            smtp = GetMailClient();
            //收件人
            mailMessage.To.Add(mail_receivers);
            if (!mail_ccs.Equals(""))
            {
                mailMessage.CC.Add(mail_ccs);
            }

            mailMessage.IsBodyHtml = false; /*邮件正文是否以 html 格式。*/
            mailMessage.From = new MailAddress(mail_account, mail_sign); /*设置来自电子邮件地址。*/
            mailMessage.Body = "测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试";
            mailMessage.Subject = "测试邮件"; /*获取或设置此电子邮件的主题行*/
            //mailMessage.Attachments.Add(attach);
            smtp.Send(mailMessage);/*发送邮件*/
        }
        /// <summary>
        /// 根据xml文件的配置，实例化出来邮件的SMTP配置
        /// </summary>
        /// <returns></returns>
        public SmtpClient GetMailClient()
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = mail_host;
            smtpClient.Port = mail_port;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; /*network表示要使用的远程SMTP服务器*/
            smtpClient.Credentials = new NetworkCredential(mail_account, mail_accountpass); /*获取或设置用来对发件人进行身份验证的凭据*/
            smtpClient.Timeout = 60000;
            if (mail_securitytype.ToUpper() == "SSL")
            {
                smtpClient.EnableSsl = true;
            }
            else
            {
                smtpClient.EnableSsl = false;
            }
            return smtpClient;
        }
    }
}