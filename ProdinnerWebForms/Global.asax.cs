using System;
using System.Globalization;
using System.Threading;
using Omu.AwesomeWebForms;
using Resources;

namespace ProdinnerWebForms
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Settings.Lookup.ClearButtonText = string.Empty;
            Settings.Lookup.PopupButtonText = string.Empty;
            Settings.MultiLookup.ClearButtonText = string.Empty;
            Settings.MultiLookup.PopupButtonText = string.Empty;

            Settings.GetText = (n, k) =>
            {
                if (k == "CancelText") return Mui.Cancel;
                if (k == "SearchText") return Mui.Search;
                if (k == "MoreText") return Mui.More;
                if (n == "MultiLookup" && k == "Title")
                    return Mui.MultiLookupTitle;
                if (n == "Lookup" && k == "Title")
                    return Mui.LookupTitle;
                return null;
            };
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var c = Request.Cookies.Get("lang");
            if (c != null && c.Value != "auto")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(c.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(c.Value);    
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}