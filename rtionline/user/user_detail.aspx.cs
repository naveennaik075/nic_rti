using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_user_detail : BasePage // System.Web.UI.Page
{
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable dt, dt1 = new ReturnClass.ReturnDataTable();
    bl_login bl = new bl_login();
    dl_login dl = new dl_login();
    Utilities ul = new Utilities();

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            if (Session["username"] == null)
            {
                Response.Redirect("../LogOut.aspx");
            }
            else
            {
                bl.UserID = Session["username"].ToString();
                dt = dl.Select_user_detail(bl);
                if (dt.table.Rows.Count > 0)
                {
                    txt_first_name.Text = dt.table.Rows[0]["Name_en"].ToString();
                    txt_mobile.Text = dt.table.Rows[0]["MobileNo"].ToString();
                    txt_email.Text = dt.table.Rows[0]["EmailID"].ToString();
                    txt_Address.Text = dt.table.Rows[0]["Address"].ToString();
                    txt_pin_code.Text = dt.table.Rows[0]["PinCode"].ToString();
                    string gend = dt.table.Rows[0]["Gender"].ToString();
                    if (gend == "M")
                    {
                        txt_gender.Text = "Male";
                    }
                    else if (gend == "F")
                    {
                        txt_gender.Text = "Female";
                    }
                    else
                    {
                        txt_gender.Text = "Transgender";
                    }
                    dt1 = dl.Select_user_LoginTime(bl);
                    if (dt1.table.Rows.Count > 1)
                    {
                        if (dt1.table.Rows[1]["logintime"].ToString() != "")
                        {
                            txt_last_login.Text = dt1.table.Rows[1]["logintime"].ToString();
                        }
                        else
                        {
                            txt_last_login.Text = "You Are Login First Time";
                        }
                    }
                    else
                    {
                        txt_last_login.Text = "You Are Login First Time";
                    }
                }
            }
        }
    }
}
