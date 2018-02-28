using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_Change_Password :BasePage // System.Web.UI.Page
{
    bl_login bl = new bl_login();
    dl_login dl = new dl_login();
    Utilities ul = new Utilities();
    db_maria_connection db = new db_maria_connection();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
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
            }
        }

    }
    //protected void btn_pass_Click(object sender, EventArgs e)
    //{
    //    HttpBrowserCapabilities browse = Request.Browser;
    //    if (txt_new_pass1.Text == txt_repass.Text && txt_new_pass1.Text!= "")
    //    {
    //        bl.NewPass = ul.GenerateMd5Hash(txt_new_pass1.Text);
    //        bl.Password = ul.GenerateMd5Hash(txt_Password.Text);
    //        bl.UserID = Session["username"].ToString();
    //        dt = dl.select_password_log(bl);
    //        if (dt.table.Rows.Count == 0)
    //        {
    //            dt = dl.select_user(bl);
    //            if (dt.table.Rows.Count > 0)
    //            {
    //                bl.RegistrationID = dt.table.Rows[0]["LoginId"].ToString();
    //                bl.Password = ul.GenerateMd5Hash(txt_Password.Text);
    //                bl.NewPass = ul.GenerateMd5Hash(txt_new_pass1.Text);
    //                bl.UserIP = ul.GetClientIpAddress(this.Page);
    //                bl.UserAgent = Request.UserAgent.ToString();
    //                bl.UserOS = Utilities.System_Info(this.Page);
    //                bl.User_browser = browse.Browser;
    //                bl.PasswordChange = "Y";
    //                rb = dl.update_password(bl);
    //                if (rb.status == true)
    //                {
    //                    Utilities.MessageBoxShow_Redirect("Your Password Update Successful Please Login Again", "../LogOut.aspx");
    //                }
    //                else
    //                {
    //                    Utilities.MessageBoxShow("Password Update Unsuccessful");
    //                }
    //            }
    //            else
    //            {

    //            }
    //        }


    //        else
    //        {
    //            Utilities.MessageBoxShow("Your New Password Looks Like Your Old Password Please Create Unique Password");
    //        }
           
    //    }
    //    else
    //    {
    //        Utilities.MessageBoxShow("Your New Password Does Not Match With The Repeated New Password");
    //    }
    //}
}