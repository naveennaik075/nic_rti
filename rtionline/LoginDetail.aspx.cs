using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginDetail : BasePage // System.Web.UI.Page
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
                Response.Redirect("Logout.aspx");
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
                    txt_reg_year.Text = dt.table.Rows[0]["Registration_Year"].ToString();
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
                    txt_userid.Text = dt.table.Rows[0]["UserID"].ToString();
                    if (dt.table.Rows[0]["IsValid"].ToString() == "n")
                    {
                        //txt_uservalid.Text = "Unvarified User";
                        //btn_userValid.Visible = true;
                    }
                    else
                    {
                        //    txt_uservalid.Text = "varified";
                        //    btn_userValid.Visible = false;
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
    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }

    }
    protected void Btn_Password_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Btn_Password.Text == "Change Password")
            {
                rfv_Password.Enabled = true;
                Rfv_PAss.Enabled = true;
                rfv_repass.Enabled = true;
                comp_repass.Enabled = true;
                Rev_password.Enabled = true;
                lbl_password.Visible = true;
                txt_Password.Visible = true;
                div_Password.Visible = true;
                Btn_Password.Text = "Update Password";
                btn_update.Visible = false;

            }
            else if (Btn_Password.Text == "Update Password")
            {
                if (txt_new_pass.Text == txt_repass.Text)
                {
                    bl.Password = ul.GenerateMd5Hash(txt_Password.Text);
                    bl.UserID = Session["username"].ToString();
                    dt = dl.select_user(bl);
                    if (dt.table.Rows.Count > 0)
                    {
                        bl.RegistrationID = dt.table.Rows[0]["LoginID"].ToString();
                        bl.Password =ul.GenerateMd5Hash(txt_new_pass.Text);
                        bl.UserID = Session["username"].ToString();
                        bl.PasswordChange = "Y";
                        rb = dl.update_password(bl);
                        if (rb.status == true)
                        {
                            
                            comp_repass.Enabled = false;
                            Rev_password.Enabled = false;
                            rfv_Password.Enabled = false;
                            Rfv_PAss.Enabled = false;
                            rfv_repass.Enabled = false;
                            lbl_password.Visible = false;
                            txt_Password.Visible = false;
                            div_Password.Visible = false;
                            Btn_Password.Text = "Change Password";
                            btn_update.Visible = true;
                            Utilities.MessageBoxShow("Password Change Successfully");
                        }
                        else
                        {
                            Utilities.MessageBoxShow("Password Change UnSuccessful");
                        }
                       
                    }
                }
            }
        }
    }
}
//protected void BindData() {


//     bl.UserID =  Session["username"].ToString();

//    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
//       


//}// End of Bind Data


// End of Login Detail