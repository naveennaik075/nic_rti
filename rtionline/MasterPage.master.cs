using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class MasterPage : System.Web.UI.MasterPage
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
            else //if (Session["language"].ToString() == "en-GB")
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
        //if (!this.IsPostBack)
        //{
        //    if (ddlLanguages.Items.FindByValue(CultureInfo.CurrentCulture.Name) != null)
        //    {
        //        ddlLanguages.Items.FindByValue(CultureInfo.CurrentCulture.Name).Selected = true;
        //    }
        //}




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



    //protected void ddlLanguages_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlLanguages.SelectedValue == "en-GB")
    //    {
    //        Session["language"] = "en-GB";
    //    }
    //    else
    //    {
    //        Session["language"] = "hi-IN";
    //    }
    //}
} // End of Class
