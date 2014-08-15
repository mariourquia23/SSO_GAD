﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.DirectoryServices;
using log4net;
using log4net.Config;
using System.Reflection;



namespace SSO_GAD
{
    public partial class welcome : System.Web.UI.Page
    {


        public static string Pass;
        public static string Username;
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Methods
        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected internal static CookieContainer GetCookieContainer(HttpCookieCollection collection, string host)
        {
            CookieContainer container = new CookieContainer();
            foreach (string str in collection)
            {
                HttpCookie cookie = collection[str];
                Cookie cookie2 = new Cookie(cookie.Name, cookie.Value, cookie.Path);
                if (!string.IsNullOrEmpty(cookie.Domain))
                {
                    cookie2.Domain = cookie.Domain;
                }
                else
                {
                    cookie2.Domain = "." + host;
                }
                cookie2.Expires = cookie.Expires;
                container.Add(cookie2);
            }
            return container;
        }

        private void metodoPost(string url)
        {
            Uri uri = new Uri(url);
            ASCIIEncoding encoding = new ASCIIEncoding();
            string s = "username=" + Username + "&password=" + Pass;
            byte[] bytes = encoding.GetBytes(s);
            CookieContainer container = new CookieContainer();
            this.TextBox1.Text = this.TextBox1.Text + "\nCreando HttpRequest...";
            this.log.Debug("Creando HttpRequest...");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = s.Length;
            request.CookieContainer = container;
            this.TextBox1.Text = this.TextBox1.Text + "ok";
            this.TextBox1.Text = this.TextBox1.Text + "\nEnviando Parametros...";
            this.log.Debug("Enviando Parametros...");
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            this.TextBox1.Text = this.TextBox1.Text + "ok";
            this.TextBox1.Text = this.TextBox1.Text + "\nCreando HttpRequest...";
            this.log.Debug("Creando HttpRequest...");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            this.TextBox1.Text = this.TextBox1.Text + "ok";
            string cookie = "";
            if (response.Cookies.Count > 0)
            {
                foreach (Cookie c in response.Cookies)
                {
                    cookie = c.Value;
                    this.transferCookieToApplication(c, uri.Host);
                }
            }
            this.TextBox1.Text = this.TextBox1.Text + "\nRecibiendo respuesta  del Sitio \n comienzo...";
            this.log.Debug("Recibiendo respuesta  del Sitio \n comienzo...");
            this.TextBox1.Text = this.TextBox1.Text + reader.ReadToEnd();
            this.TextBox1.Text = this.TextBox1.Text + "\n  fin.";
            this.TextBox1.Text = this.TextBox1.Text + "\nEstableciendo URL...";
            this.log.Debug("Estableciendo URL...");
            String str2 = String.Format("{0};jsessionid={1}", Properties.Settings.Default.WebClient_URL_SSO,cookie);
            this.log.Debug("URL>>"+ str2);
            this.TextBox1.Text = this.TextBox1.Text + "ok\n";
            this.HyperLink1.NavigateUrl = str2;
            this.TextBox1.Text = this.TextBox1.Text + "\nCerrando conexiones...";
            this.log.Debug("Cerrando conexiones...");
            reader.Close();
            response.Close();
            this.TextBox1.Text = this.TextBox1.Text + "ok";
            this.TextBox1.Text = this.TextBox1.Text + "\n\nFavor utilice el URL para ir al GOANY. ";
            Response.Redirect(str2,false);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            if (!base.IsPostBack)
            {
                this.log.Debug("Recibiendo Datos");
                Username = this.Session["user"].ToString().Trim();
                this.log.Debug(String.Format("Usuario {0}",Username));
                this.log.Debug("Recuperando Contraseña");
                Pass = RetrievePassFromLDAP(Username);
                this.log.Debug("Contraseña recuperada ");
                this.Label1.Text = "Bienvenido " + Username;

            }
            

                string url = Properties.Settings.Default.WebClient_URL;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.AcceptAllCertifications);                             
                this.metodoPost(url);
            }
            catch (Exception exception)
            {
                this.TextBox1.Text = "\t\t*****ERROR****\n  " + exception.ToString();
                this.log.Error(exception.ToString());
            }
        }
        private String RetrievePassFromLDAP(String user)
        {
            this.log.Debug("Leyendo Parametros");
            String path = String.Format("LDAP://{0}:{1}/{2}",
                Properties.Settings.Default.LDAP_Host,
                Properties.Settings.Default.LDAP_Port,
                Properties.Settings.Default.LDAP_DNBase);
            

            DirectoryEntry nRoot = new DirectoryEntry(path);
            nRoot.AuthenticationType = AuthenticationTypes.Anonymous;
            //nRoot.Username = "cn=Manager,dc=maxcrc,dc=com";
            //nRoot.Password = "secret";      
      
            DirectorySearcher nDS = new DirectorySearcher(nRoot);
            nDS.SearchScope = SearchScope.Subtree;
            nDS.Filter = String.Format("uid={0}",Username);
            nDS.PropertiesToLoad.Add(Properties.Settings.Default.LDAP_AttributePass);
            SearchResult sr = nDS.FindOne();
            String resp = sr.Properties[Properties.Settings.Default.LDAP_AttributePass][0].ToString();                       
            return resp;

        }
       

        protected internal void transferCookieToApplication(Cookie c, string host)
        {
            HttpCookie cookie = new HttpCookie(c.Name)
            {
                Value = c.Value,
                Expires = DateTime.Now.AddDays(1.0),
                Domain = "." + host,
                Path = "/"
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        




        
    }
}