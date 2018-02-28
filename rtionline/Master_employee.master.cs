using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
public partial class Master_employee : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["language"] != null)
        {
            if (Session["language"].ToString() == "hi-IN")
            {
                hin.Visible = false;
                eng.Visible = true;


            }
            else if (Session["language"].ToString() == "en-GB")
            {
                eng.Visible = false;
                hin.Visible = true;
                Session["language"] = "en-GB";
            }
        }
        if (!IsPostBack)
        {
            if (CultureInfo.CurrentCulture.Name != null)
            {
                Session["language"] = CultureInfo.CurrentCulture.Name;
            }
        }
        
        try
        {
            if (Session["username"] == null)
            {
                Utilities.MessageBoxShow_Redirect("Your Session is expired......... Please Login Again ..", "../LogOut.aspx");
            }
            else if (Session["role"].ToString() != "3")
            {
                Utilities.MessageBoxShow_Redirect("Your Session is expired......... Please Login Again ..", "../LogOut.aspx");
                //   Response.Redirect("../LogOut.aspx");
            }
            else
            {

            }
        }
        catch (NullReferenceException es)
        {
            Response.Redirect("../LogOut.aspx");
        }

    }

    protected void hin_Click(object sender, EventArgs e)
    {
        hin.Visible = false;
        eng.Visible = true;
        Session["language"] = "hi-IN";


    }
    protected void eng_Click(object sender, EventArgs e)
    {

        eng.Visible = false;
        hin.Visible = true;
        Session["language"] = "en-GB";
    }



}
