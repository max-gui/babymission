using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;
using System.ComponentModel;
using System.Threading;

namespace xm_mis.Main
{
    public class MailNotifyInfo
    {
        private readonly string to, subject, body;

        public MailNotifyInfo(string to, string subject, string body)
        {
            this.to = to;
            this.subject = subject;
            this.body = body;
        }

        public string To { get { return to; } }
        public string Subject { get { return subject; } }
        public string Body { get { return body; } }
    }

    public class BeckSendMail
    {
        private BeckSendMail()
        {
            this.m_lockObject = new object();

            this.m_mailNotifyInfos = new LinkedList<MailNotifyInfo>();

            this.m_threadEvent = new ManualResetEvent(false);

            this.m_workThread = new Thread(this.ThreadStart);

            this.m_workThread.Start();
        }

        private readonly LinkedList<MailNotifyInfo> m_mailNotifyInfos;

        private readonly Thread m_workThread;

        private readonly ManualResetEvent m_threadEvent;

        private readonly Object m_lockObject;

        private static readonly BeckSendMail staticMM = new BeckSendMail();

        private void AppendNotification(MailNotifyInfo mailNotifyInfo)
        {

            lock (this.m_lockObject)
            {

                this.m_mailNotifyInfos.AddLast(mailNotifyInfo);

                if (this.m_mailNotifyInfos.Count != 0)
                {

                    this.m_threadEvent.Set();

                }
            }
        }

        private void ThreadStart()
        {

            while (true)
            {

                this.m_threadEvent.WaitOne();

                MailNotifyInfo mailNotifyInfo = this.m_mailNotifyInfos.First.Value;

                sendMail toSend = new sendMail();

                toSend.MailSender(mailNotifyInfo);

                lock (this.m_lockObject)
                {

                    this.m_mailNotifyInfos.Remove(mailNotifyInfo);

                    if (this.m_mailNotifyInfos.Count == 0)
                    {
                        this.m_threadEvent.Reset();
                    }
                }
            }
        }

        public static BeckSendMail getMM()
        {
            return staticMM;
        }

        public void NewMail(string to, string subject, string body)
        {
            MailNotifyInfo info = new MailNotifyInfo(to, subject, body);

            AppendNotification(info);
        }
    }

    public class sendMail
    {
        public void MailSender(MailNotifyInfo e)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(e.To);

            msg.From = new MailAddress("mis@xmel.sh.cn", "xm_mis", System.Text.Encoding.UTF8);

            msg.Subject = e.Subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
            msg.Body = e.Body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
            msg.IsBodyHtml = false;//是否是HTML邮件 
            msg.Priority = MailPriority.High;//邮件优先级 

            SmtpClient client = new SmtpClient();
            //client.Credentials = new System.Net.NetworkCredential("playstation.gui", "seiyali_122278");//("playstation-123@live.cn", "seiyali_122278"); 

            //client.Host = "smtp.gmail.com";

            //client.EnableSsl = true;

            client.Credentials = new System.Net.NetworkCredential("mis", "123456");//("playstation-123@live.cn", "seiyali_122278"); 

            client.Host = "serv.xmel.sh.cn";
            
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            object userState = msg;

            try
            {
                client.SendAsync(msg, userState);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Exception caught in CreateTestMessage3(): {0}",
                //      ex.ToString());
                ex.ToString();
            }
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = e.UserState.ToString();

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            else if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }
    }


    //internal class MailEventArgs : EventArgs
    //{
    //    private readonly string to, subject, body;

    //    public MailEventArgs(string to, string subject, string body)
    //    {
    //        this.to = to;
    //        this.subject = subject;
    //        this.body = body;
    //    }

    //    public string To { get { return to; } }
    //    public string Subject { get { return subject; } }
    //    public string Body { get { return body; } }
    //}

    //public class MailManager
    //{
    //    private static readonly MailManager staticMM = new MailManager();

    //    private MailManager()
    //    { }

    //    public static MailManager getMM()
    //    {
    //        return staticMM;
    //    }

    //    internal event EventHandler<MailEventArgs> newMail;

    //    internal virtual void OnNewMail(MailEventArgs e)
    //    {
    //        EventHandler<MailEventArgs> temp = Interlocked.CompareExchange(ref newMail, null, null);

    //        if (temp != null)
    //        {
    //            temp(this, e);
    //        }
    //    }

    //    public void SimulateNewMail(string to, string subject, string body)
    //    {
    //        MailEventArgs e = new MailEventArgs(to, subject, body);

    //        OnNewMail(e);
    //    }
    //}

    //public static class Class1
    //{
    //    public static void connectMM(MailManager mm)
    //    {
    //        mm.newMail += MailSender;
    //    }

    //    private static void MailSender(object sender, MailEventArgs e)
    //    {
    //        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
    //        msg.To.Add(e.To);

    //        msg.From = new MailAddress("vampiler@163.com", "caonima", System.Text.Encoding.UTF8);

    //        msg.Subject = e.Subject;
    //        msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
    //        msg.Body = e.Body;
    //        msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
    //        msg.IsBodyHtml = false;//是否是HTML邮件 
    //        msg.Priority = MailPriority.High;//邮件优先级 

    //        SmtpClient client = new SmtpClient();
    //        client.Credentials = new System.Net.NetworkCredential("playstation.gui", "seiyali_122278");//("playstation-123@live.cn", "seiyali_122278"); 

    //        client.Host = "smtp.gmail.com";//"http://mail.services.live.com/DeltaSync_v2.0.0/sync.aspx"; 

    //        client.EnableSsl = true;
    //        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

    //        object userState = msg;

    //        try
    //        {
    //            client.SendAsync(msg, userState);
    //        }
    //        catch (Exception ex)
    //        {
    //            //Console.WriteLine("Exception caught in CreateTestMessage3(): {0}",
    //            //      ex.ToString());
    //            ex.ToString();
    //        }
    //    }

    //    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    //    {
    //        // Get the unique identifier for this asynchronous operation.
    //        String token = e.UserState.ToString();

    //        if (e.Cancelled)
    //        {
    //            Console.WriteLine("[{0}] Send canceled.", token);
    //        }
    //        else if (e.Error != null)
    //        {
    //            Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
    //        }
    //        else
    //        {
    //            Console.WriteLine("Message sent.");
    //        }
    //    }

    //    public static void SendMailUseZj()
    //    {
    //        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
    //        msg.To.Add("vampiler@163.com");
    //        //msg.To.Add("b@b.com"); 
    //        /* 
    //        * msg.To.Add("b@b.com"); 
    //        * msg.To.Add("b@b.com"); 
    //        * msg.To.Add("b@b.com");可以发送给多人 
    //        */
    //        //msg.CC.Add("c@c.com"); 
    //        /* 
    //        * msg.CC.Add("c@c.com"); 
    //        * msg.CC.Add("c@c.com");可以抄送给多人 
    //        */
    //        msg.From = new MailAddress("vampiler@163.com", "caonima", System.Text.Encoding.UTF8);
    //        /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
    //        msg.Subject = "这是测试邮件";//邮件标题 
    //        msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
    //        msg.Body = "邮件内容";//邮件内容 
    //        msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
    //        msg.IsBodyHtml = false;//是否是HTML邮件 
    //        msg.Priority = MailPriority.High;//邮件优先级 

    //        SmtpClient client = new SmtpClient();
    //        client.Credentials = new System.Net.NetworkCredential("playstation.gui", "seiyali_122278");//("playstation-123@live.cn", "seiyali_122278"); 
    //        //在zj.com注册的邮箱和密码 
    //        client.Host = "smtp.gmail.com";//"http://mail.services.live.com/DeltaSync_v2.0.0/sync.aspx"; 
    //        //client.UseDefaultCredentials = false;

    //        client.EnableSsl = true;
    //        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

    //        object userState = msg;
    //        //try 
    //        //{ 
    //        //client.SendAsync(msg, userState); 
    //        ////简单一点儿可以client.Send(msg); 
    //        //MessageBox.Show("发送成功"); 
    //        //} 
    //        //catch (System.Net.Mail.SmtpException ex) 
    //        //{ 
    //        //MessageBox.Show(ex.Message, "发送邮件出错"); 
    //        //} 
    //        try
    //        {
    //            client.SendAsync(msg, userState);
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("Exception caught in CreateTestMessage3(): {0}",
    //                  ex.ToString());
    //        }
    //        //client.SendAsync(msg, userState);

    //        //client.SendAsyncCancel();
    //    }
    //}
}