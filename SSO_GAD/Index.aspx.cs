using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SSO_GAD
{
    public partial class Index : System.Web.UI.Page
    {
        
        protected HtmlForm form1;
        protected Image Image1;
        protected Login Login1;
        
        // Methods
        private bool authenticated(string user, string pwd)
        {
            return ((user == "prueba") & (pwd == "Seguridad0101"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (this.authenticated(this.Login1.UserName, this.Login1.Password))
            {
                this.Session["user"] = this.Login1.UserName;
                this.Session["pass"] = this.Login1.Password;
                e.Authenticated = true;
            }
            else
            {
                e.Authenticated = false;
            }

        }
    }
}