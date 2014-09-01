using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using log4net;
using System.Reflection;

namespace SSO_GAD
{
    public partial class Index : System.Web.UI.Page
    {
        
        protected HtmlForm form1;
        protected Image Image1;
        protected Login Login1;
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        // Methods
        private bool authenticated(string user, string pwd)
        
        {
            bool result=false;
            try
            {
                
                //Declaracion de objeto
                //-----------Banrep
                
                //co.gov.banrep.osb.peticionWSAutenticarUsuarioRSA peticion = new co.gov.banrep.osb.peticionWSAutenticarUsuarioRSA();
                //co.gov.banrep.osb.respuestaWSAutenticarUsuarioRSA respuesta = new co.gov.banrep.osb.respuestaWSAutenticarUsuarioRSA();
                //co.gov.banrep.osb.AdministradorRSAWS rsa= new co.gov.banrep.osb.AdministradorRSAWS();
                
                //this.log.Debug("Comenzando autenticacion con RSA");
                //peticion.usuario = user.TrimEnd().TrimStart();
                //peticion.passCode = pwd.TrimEnd().TrimStart();
                //respuesta = rsa.autenticarRSA(peticion);
                //result = respuesta.resultado;
                
                
                //------------GLT

                GLTWS1.EmulacionRSA rsaglt = new GLTWS1.EmulacionRSA();
                result = rsaglt.autenticarRSA(Int32.Parse(pwd.TrimEnd().TrimStart()), user.TrimEnd().TrimStart());
               
                
                
                
                this.log.Debug("Fin autenticacion RSA = " + result.ToString());
                
            }
            catch (FormatException ) {
                errorLabel("Formato Token Invalido");
                
            }
            catch (Exception e)
            {                
                //errorLabel( e.ToString());
                this.log.Error(e.ToString());
                
            }
            return result;
        }
        private void errorLabel(String mensaje)
        {
            Login1.FailureText = mensaje;
            this.log.Debug(mensaje);
       
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string script = "$(window).load(function () { $('[id*=btnSubmit]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            } 
             
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (this.authenticated(this.Login1.UserName, this.Login1.Password))
            {
                this.Session["user"] = this.Login1.UserName;
                
                this.Session["pass"] = this.Login1.Password;
                e.Authenticated = true;
                this.log.Debug("Usuario autenticado");
                
                

            }
            else
            {
                e.Authenticated = false;
                this.log.Debug("autenticacion Fallida");
            }

        }
    }
}