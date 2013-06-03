using System;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace ProdinnerWebForms
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public string La;
        protected void Page_Load(object sender, EventArgs e)
        {
            La = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (!IsPostBack) {
                Lang.Items.Add(new ListItem("auto","auto"));
                Lang.Items.Add(new ListItem("English","en"));
                Lang.Items.Add(new ListItem("Français","fr"));
                Lang.Items.Add(new ListItem("Español","es"));
                Lang.Items.Add(new ListItem("Româna","ro"));

                var c = Request.Cookies.Get("lang");
                if (c != null)
                {
                    Lang.SelectedValue = c.Value;
                }
            }
        }

        protected void LangChange(object sender, EventArgs e)
        {
            var c = new HttpCookie("lang", Lang.SelectedValue);
            c.Expires.AddYears(1);
            Response.Cookies.Add(c);
            Response.Redirect(Request.Url.AbsolutePath);
        }
    }
}
