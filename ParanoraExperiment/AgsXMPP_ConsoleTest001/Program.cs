using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.disco;
using agsXMPP.sasl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Web;
using System.Web.Hosting;
using agsXMPP.Xml.Dom;


namespace AgsXMPP_ConsoleTest001
{
    class Program
    {

        private static XmppClientConnection xmppCon;
        private static DiscoManager discoManager;
        private static string domain;

        static void Main(string[] args)
        {
            //AppSet();

            //domain = "bobin.openfire.com";
            ////domain = "10.4.99.3";
            //xmppCon = InitXmppClientConnection();
            //discoManager = InitDiscoManager(xmppCon);

            //LoginXmppServer(xmppCon, "bobin", "sh.1234", domain, "wp");

            //Console.ReadKey();
            //ExitXmppServer(xmppCon);
            Test();
            Console.ReadKey();
        }

        public static void Test1()
        {
             
        }

        public static void Test()
        {
            string ip = "61.152.255.";
            string str = "";
            for (int i = 226; i <= 255; i++)
            {
                str += ip+i.ToString() + ",";
            }
            str=str.TrimEnd(',');
            Console.WriteLine(str);
        }

        #region AppSet
        public static void AppSet()
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }
        #endregion

        #region ExitXmppServer
        public static void ExitXmppServer(XmppClientConnection xmppCon)
        {
            xmppCon.Close();
            Thread.Sleep(1000);
            Monitor("ExitXmppServer");
        }

        #endregion

        #region Monitor
        public static void Monitor(string msg)
        {
            string startEndTagStr = "/******************************************************************/";
            string msgFormat = string.Format("{0}\r\n{1}\r\n{2}\r\n", startEndTagStr, msg, startEndTagStr);
            Console.WriteLine(msgFormat);
        }

        #endregion

        #region InitDiscoManager
        public static DiscoManager InitDiscoManager(XmppClientConnection xmppCon)
        {
            DiscoManager discoManager = new DiscoManager(xmppCon);
            return discoManager;
        }
        #endregion

        #region InitXmppClientConnection
        public static XmppClientConnection InitXmppClientConnection()
        {
            XmppClientConnection xmppCon = new XmppClientConnection();

            xmppCon.SocketConnectionType = agsXMPP.net.SocketConnectionType.Direct;

            xmppCon.OnReadXml += new XmlHandler(XmppCon_OnReadXml);
			xmppCon.OnWriteXml		    += new XmlHandler(XmppCon_OnWriteXml);
			
			xmppCon.OnRosterStart	    += new ObjectHandler(XmppCon_OnRosterStart);
			xmppCon.OnRosterEnd		    += new ObjectHandler(XmppCon_OnRosterEnd);
			xmppCon.OnRosterItem	    += new agsXMPP.XmppClientConnection.RosterHandler(XmppCon_OnRosterItem);

			xmppCon.OnAgentStart	    += new ObjectHandler(XmppCon_OnAgentStart);
			xmppCon.OnAgentEnd		    += new ObjectHandler(XmppCon_OnAgentEnd);
			xmppCon.OnAgentItem		    += new agsXMPP.XmppClientConnection.AgentHandler(XmppCon_OnAgentItem);

			xmppCon.OnLogin			    += new ObjectHandler(XmppCon_OnLogin);
			xmppCon.OnClose			    += new ObjectHandler(XmppCon_OnClose);
			xmppCon.OnError			    += new ErrorHandler(XmppCon_OnError);
			xmppCon.OnPresence		    += new PresenceHandler(XmppCon_OnPresence);
			xmppCon.OnMessage		    += new MessageHandler(XmppCon_OnMessage);
			xmppCon.OnIq			    += new IqHandler(XmppCon_OnIq);
			xmppCon.OnAuthError		    += new XmppElementHandler(XmppCon_OnAuthError);
            xmppCon.OnSocketError += new ErrorHandler(XmppCon_OnSocketError);
            xmppCon.OnStreamError += new XmppElementHandler(XmppCon_OnStreamError);

           
            xmppCon.OnReadSocketData    += new agsXMPP.net.BaseSocket.OnSocketDataHandler(ClientSocket_OnReceive);
            xmppCon.OnWriteSocketData   += new agsXMPP.net.BaseSocket.OnSocketDataHandler(ClientSocket_OnSend);

            xmppCon.ClientSocket.OnValidateCertificate += new System.Net.Security.RemoteCertificateValidationCallback(ClientSocket_OnValidateCertificate);

            xmppCon.OnXmppConnectionStateChanged += new XmppConnectionStateHandler(XmppCon_OnXmppConnectionStateChanged);
            xmppCon.OnSaslStart += new SaslEventHandler(XmppCon_OnSaslStart);

            return xmppCon;
        }

        private static void XmppCon_OnXmppConnectionStateChanged(object sender, XmppConnectionState state)
        {
            Monitor("XmppCon_OnXmppConnectionStateChanged");
        }

        private static void XmppCon_OnSaslStart(object sender, SaslEventArgs args)
        {
            Monitor("XmppCon_OnSaslStart");
        }

        private static bool ClientSocket_OnValidateCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            Monitor("ClientSocket_OnValidateCertificate");
            return true;
        }

        private static void XmppCon_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            //Monitor("XmppCon_OnAuthError");
            Monitor(string.Format("XmppCon_OnAuthError:\r\n{0}\r\n", e.ToString()));
        }

        private static void ClientSocket_OnSend(object sender, byte[] data, int count)
        {
            Monitor("ClientSocket_OnSend");
        }

        private static void ClientSocket_OnReceive(object sender, byte[] data, int count)
        {
            Monitor("ClientSocket_OnReceive");
        }

        private static void XmppCon_OnStreamError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            Monitor("XmppCon_OnStreamError");
        }

        private static void XmppCon_OnSocketError(object sender, Exception ex)
        {
            Monitor(string.Format("XmppCon_OnSocketError:\r\n{0}\r\n", ex.Message));
        }

        private static void XmppCon_OnIq(object sender, IQ iq)
        {
            Monitor("XmppCon_OnIq");
        }

        private static void XmppCon_OnMessage(object sender, Message msg)
        {
            Monitor("XmppCon_OnMessage");
        }

        private static void XmppCon_OnPresence(object sender, Presence pres)
        {
            Monitor("XmppCon_OnPresence");
        }

        private static void XmppCon_OnError(object sender, Exception ex)
        {
            Monitor(string.Format("XmppCon_OnError:\r\n{0}\r\n",ex.Message));
        }

        private static void XmppCon_OnClose(object sender)
        {
            Monitor("XmppCon_OnClose");
        }

        private static void XmppCon_OnLogin(object sender)
        {
            Monitor("XmppCon_OnLogin");
            //SendTextMessageTo(xmppCon, "admin", domain, @"hi!");
        }

        private static void XmppCon_OnAgentItem(object sender, agsXMPP.protocol.iq.agent.Agent agent)
        {
            Monitor("XmppCon_OnAgentItem");
        }

        private static void XmppCon_OnAgentEnd(object sender)
        {
            Monitor("XmppCon_OnAgentEnd");
        }

        private static void XmppCon_OnAgentStart(object sender)
        {
            Monitor("XmppCon_OnAgentStart");
        }


        public static void XmppCon_OnReadXml(object sender, string xml)
        {
            Console.Title = "XmppCon_OnReadXml";
            Console.ForegroundColor = ConsoleColor.Red;
            Monitor(string.Format("XmppCon_OnReadXml\r\nRECV:{0}", xml));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void XmppCon_OnWriteXml(object sender, string xml)
        {
            Console.Title = "XmppCon_OnWriteXml";
            Console.ForegroundColor = ConsoleColor.Red;
            Monitor(string.Format("XmppCon_OnWriteXml\r\nSEND:{0}", xml));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void XmppCon_OnRosterStart(object sender)
        {
            Monitor("XmppCon_OnRosterStart");
        }
        public static void XmppCon_OnRosterEnd(object sender)
        {
            Monitor("XmppCon_OnRosterEnd");
        }
        private static void XmppCon_OnRosterItem(object sender, agsXMPP.protocol.iq.roster.RosterItem item)
        {
            Monitor("XmppCon_OnRosterItem");
        }

        #endregion

        #region LoginXmppServer
        public static void LoginXmppServer(XmppClientConnection xmppCon,
            string userName, 
            string passWord,
            string domain,
            string resource,                      
            bool registerAccount=false,
            int port=5222,
            int priority = 10,
            bool useSSL=false,
            bool autoResolveConnectServer=true,
            bool useCompression=false
            )
        {
            string jidAddress = string.Format(@"{0}@{1}/{2}", userName, domain,resource);
            Monitor(jidAddress);
            Jid jid = new Jid(jidAddress);

            xmppCon.Server = jid.Server;
			xmppCon.Username = jid.User;
			xmppCon.Password = passWord;
            xmppCon.Resource = resource;
            xmppCon.Priority = priority;
            xmppCon.Port = port;
            xmppCon.UseSSL = useSSL;
            xmppCon.AutoResolveConnectServer = autoResolveConnectServer;
            xmppCon.UseCompression = useCompression;
            xmppCon.RegisterAccount = registerAccount;

            xmppCon.EnableCapabilities = true;
            xmppCon.ClientVersion = "1.0";
            xmppCon.Capabilities.Node = "";

            xmppCon.DiscoInfo.AddIdentity(new DiscoIdentity("pc", "MiniClient", "client"));
            xmppCon.DiscoInfo.AddFeature(new DiscoFeature(agsXMPP.Uri.DISCO_INFO));
            xmppCon.DiscoInfo.AddFeature(new DiscoFeature(agsXMPP.Uri.DISCO_ITEMS));
            xmppCon.DiscoInfo.AddFeature(new DiscoFeature(agsXMPP.Uri.MUC));

            xmppCon.Open();


        }
        #endregion

        #region SendTextMessageTo
        public static void SendTextMessageTo(XmppClientConnection xmppCon, string name,string domain,string msgText)
        {
            Message msg = new agsXMPP.protocol.client.Message();

            msg.Type = MessageType.chat;
            msg.To = new Jid(string.Format("{0}@{1}",name,domain));
            msg.Body = msgText;
            xmppCon.Send(msg);
        }

        #endregion

        #region SendMessageTo
        public static void SendMessageTo(XmppClientConnection xmppCon, string name, string domain, Element ele)
        {
            Message msg = new agsXMPP.protocol.client.Message();

            msg.Type = MessageType.chat;
            msg.To = new Jid(string.Format("{0}@{1}", name, domain));
            msg.AddChild(ele);
            xmppCon.Send(msg);
        }

        #endregion
    }
}
