using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
public partial class theme_master : System.Web.UI.MasterPage
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

            if (Session["username"].ToString() == null)
            {
                Response.Redirect("../LogOut.aspx");
            }
            else
            {
                if (Session["role"].ToString() == "1")
                {
                    li_add.Visible = true;
                    li_action.Visible = true;
                    li_roll.Visible = true;
                }
                else if(Session["role"].ToString() == "4")
                {
                    li_add.Visible = true; 
                    li_action.Visible = false;
                    li_roll.Visible = false;
                }
                else
                {
                    li_add.Visible = false;
                }
            }

        }catch(NullReferenceException ex)
        {
            Utilities.MessageBoxShow_Redirect("your Session Has been expired Please Login Again","../LogOut.aspx");
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


}//End Of Class

