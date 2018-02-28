using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        try
        {
            //Response.Cookies["User"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Request.Cookies.Clear();
            }

            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1); // make it expire yesterday
                Response.Cookies.Add(aCookie); // overwrite it
            }
        }
        catch (Exception ex)
        { 
            Response.Redirect("~/default.aspx"); }
        Session["userid"] = null;
        Session["username"] = null;
        Response.Redirect("~/default.aspx");
        

       // Response.Redirect("~/Login.aspx");

    }
}