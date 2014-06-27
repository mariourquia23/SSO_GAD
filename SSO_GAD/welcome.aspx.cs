using System;
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




namespace SSO_GAD
{
    public partial class welcome : System.Web.UI.Page
    {

        // Fields
        //protected HtmlForm form1;
        //protected HyperLink HyperLink1;
        //protected Image Image1;
        //protected Label Label1;
        public static string Pass;
        //protected TextBox TextBox1;
        public static string Username;

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

        private void metodo1(string url)
        {
            Uri uri = new Uri(url);
            ASCIIEncoding encoding = new ASCIIEncoding();
            string s = "username=" + Username + "&password=" + Pass;
            byte[] bytes = encoding.GetBytes(s);
            CookieContainer container = new CookieContainer();
            this.TextBox1.Text = this.TextBox1.Text + "\nCreando HttpRequest...";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = s.Length;
            request.CookieContainer = container;
            this.TextBox1.Text = this.TextBox1.Text + "ok";
            this.TextBox1.Text = this.TextBox1.Text + "\nEnviando Parametros...";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            this.TextBox1.Text = this.TextBox1.Text + "ok";
            this.TextBox1.Text = this.TextBox1.Text + "\nCreando HttpRequest...";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            this.TextBox1.Text = this.TextBox1.Text + "ok";
            string str2 = "";
            if (response.Cookies.Count > 0)
            {
                foreach (Cookie cookie in response.Cookies)
                {
                    str2 = cookie.Value;
                    this.transferCookieToApplication(cookie, uri.Host);
                }
            }
            this.TextBox1.Text = this.TextBox1.Text + "\nRecibiendo respuesta  del Sitio \n comienzo...";
            this.TextBox1.Text = this.TextBox1.Text + reader.ReadToEnd();
            this.TextBox1.Text = this.TextBox1.Text + "\n  fin.";
            this.TextBox1.Text = this.TextBox1.Text + "\nEstableciendo URL...";
            str2 = "https://s361717226.onlinehome.us:8080/webclient/WebClient.jsf;jsessionid=" + str2;
            this.TextBox1.Text = this.TextBox1.Text + "ok\n";
            this.HyperLink1.NavigateUrl = str2;
            this.TextBox1.Text = this.TextBox1.Text + "\nCerrando conexiones...";
            reader.Close();
            response.Close();
            this.TextBox1.Text = this.TextBox1.Text + "ok";
            this.TextBox1.Text = this.TextBox1.Text + "\n\nFavor utilice el URL para ir al GOANY. ";
            Response.Redirect(str2);
        }

        private void metodo2(string url)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string s = "username=" + Username + "&password=" + Pass;
            byte[] bytes = encoding.GetBytes(s);
            this.Label1.Text = "/" + s + "/";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = s.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            requestStream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(requestStream);
            this.TextBox1.Text = reader.ReadToEnd();
            reader.Close();
            requestStream.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Username = this.Session["user"].ToString().Trim();
                //Pass = this.Session["pass"].ToString().Trim();
                Pass = Properties.Settings.Default.GADPass;
                this.Label1.Text = "Bienvenido " + Username;
            }
            try
            {
                string url = "https://s361717226.onlinehome.us:8080/login";
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.AcceptAllCertifications);
                this.metodo1(url);
            }
            catch (Exception exception)
            {
                this.TextBox1.Text = "\t\t*****ERROR****\n  " + exception.ToString();
            }
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