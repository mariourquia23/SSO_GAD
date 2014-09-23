using log4net;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace SSO_GAD
{
    public partial class WebFormPost : System.Web.UI.Page
    {
        protected String resultado;
        protected Uri finalURL { get; set; }
        protected bool succes = false;
        public static string Pass;
        public static string Username;
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected String redirectHtML;

        protected void Page_Load(object sender, EventArgs e)
        {          
            
            if (!base.IsPostBack)
            {
               try
                {
                    this.log.Debug("Recibiendo Datos");
                    Username = Request.Params["username"]!=null?Request.Params["username"].Trim():"";
                    this.log.Debug(String.Format("Usuario \"{0}\"", Username));
                    Pass = Request.Params["password"]!=null?Request.Params["password"].Trim():"";
                    this.log.Debug(String.Format("tamaño de contraseña {0}", Pass.Length));
                    if (Username.Length > 1 && Pass.Length >1) 
                    {
                        String url = null;
                        if (Properties.Settings.Default.useRequestUrl.Length > 0)
                        {
                            Uri UrlConfig = new Uri(Properties.Settings.Default.WebClient_URL);
                            url = String.Format("{0}://{1}{2}",
                                Request.Url.Scheme,
                                Request.Url.Authority,
                                UrlConfig.AbsolutePath
                                );
                            this.log.Debug(String.Format("Utilizar Request URL = true \nURL creada de Request {0} ", url));
                        }
                        else
                        {
                            url = Properties.Settings.Default.WebClient_URL;
                        }

                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.AcceptAllCertifications);
                        string resultado=this.metodoPost(url);
                        this.log.Debug("Resultado de HTTP post--" + resultado);
                        redireccionar(resultado);
                    }
                    else 
                    {
                        this.titulo.Visible = true;
                        this.TextBox1.Text+="\n\n \t\t\tNo se recibieron Credenciales";                        
                        this.log.Debug("No se Han recibido credenciales");
                    }
                }
                catch (Exception ex)
                {
                    this.titulo.Visible = true;
                    this.log.Debug(ex.ToString());
                    this.TextBox1.Text += ex.ToString();
                }
                
            }
        }
        private String metodoPost(string url)
        {
            String cookieName = "JSESSIONID";
            HttpCookie Jsessionid = new HttpCookie(cookieName);
            Jsessionid = Request.Cookies[cookieName];
            if (Jsessionid != null)
                this.log.Debug(String.Format("El valor de la cookie {0} en el navegador es:{1}", Jsessionid.Name, Jsessionid.Value));
            else
                this.log.Debug(String.Format("Cookie {0} NO ENCONTRADA", cookieName));
            if (Request.Cookies[cookieName] != null)
            {
                HttpCookie myCookie = new HttpCookie(cookieName);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            Uri uri = new Uri(url);
            ASCIIEncoding encoding = new ASCIIEncoding();
            string content = "username=" + Username + "&password=" + Pass;
            byte[] bytes = encoding.GetBytes(content);
            CookieContainer container = new CookieContainer();
            
            this.log.Debug("Creando HttpRequest...");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = content.Length;
            request.CookieContainer = container;
            this.log.Debug("Enviando Parametros...");
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            
            this.log.Debug("Creando HttpRequest...");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
      
            resultado = response.Headers["X-GDX-Reply"].ToString();
            if (!resultado.Contains("200 Welcome,"))
                return resultado;            
            string cookie = "";
            if (response.Cookies.Count > 0)
            {
                foreach (Cookie c in response.Cookies)
                {
                    cookie = c.Value;
                    this.transferCookieToApplication(c, uri.Host);
                }
            }
            
            this.log.Debug("Recibiendo respuesta  del Sitio \n comienzo...");
            
            this.log.Debug("Estableciendo URL...");

            String str2 = null;
            if (Properties.Settings.Default.useRequestUrl.Length > 0)
            {
                Uri UrlConfig = new Uri(Properties.Settings.Default.WebClient_URL_SSO);
                
                str2 = String.Format("{0}://{1}{2};jsessionid={3}",
                    Request.Url.Scheme,
                    Request.Url.Authority,
                    UrlConfig.AbsolutePath,
                    cookie);
                this.log.Debug(String.Format("Utilizar Request URL = true \nURL creada de Request {0} ", str2));

            }
            else
            {
                str2 = String.Format("{0};jsessionid={1}", Properties.Settings.Default.WebClient_URL_SSO, cookie);
            }
            this.log.Debug("URL>>" + str2);
          
            this.finalURL = new Uri(str2);
            
            this.log.Debug("Cerrando conexiones...");
            reader.Close();
            response.Close();
           
            
            Jsessionid = Request.Cookies[cookieName];
            if (Jsessionid != null)
                this.log.Debug(String.Format("El valor de la cookie {0} en el navegador es:{1}", Jsessionid.Name, Jsessionid.Value));
            else
                this.log.Debug(String.Format("Cookie {0} NO ENCONTRADA", cookieName));
            succes = true;
            return resultado;






        }
        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
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
        protected void redireccionar(String resultado)
        {
            if (succes)
            {
                redirectHtML = String.Format("\n<meta http-equiv=\"refresh\" content=\"0; url={0}\" />", finalURL.ToString());
                //head.InnerHtml = String.Format("\n<meta http-equiv=\"refresh\" content=\"0; url={0}\" />", finalURL.ToString());
                panel.InnerHtml = "<asp:Image ID=\"Image1\" ImageUrl=\"~/img/logo.png\" /><br /><br /><div class=\"loading\" align=\"center\">Cargando...<br /><br /><img src=\"img/loader.gif\" /></div>";
            }
            else
            {
                redirectHtML = "";
                this.titulo.Visible = true;
                this.TextBox1.Text += "\n\n\t\t\t Respuesta del Servidor GTA\n\n";
                this.TextBox1.Text += resultado;
            }
        }

        protected internal void transferCookieToApplication(Cookie c, string host)
        {
            HttpCookie cookie = new HttpCookie(c.Name)
            {
                Name = "JSESSIONID",
                Value = c.Value,
                //Domain="."+host,
                //Domain=Request.Url.Host,
                Expires = DateTime.Now.AddDays(1.0),
                Path = "/",
                Secure = true,

            };

            HttpContext.Current.Response.Cookies.Add(cookie);
            this.log.Debug(String.Format("Cookie {0} con valor {1} establecida", cookie.Name, cookie.Value));
        }
        


   
    }
}