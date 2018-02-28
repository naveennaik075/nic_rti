using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_feedback : BasePage //System.Web.UI.Page
{
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
                rfv_txt_sub.Text = "";
                txtCaptcha.Text = "";

            }
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Page.IsValid)
            {
                Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
                if (Captcha1.UserValidated)
                {
                    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
                    bl_feedback bl = new bl_feedback();
                    dl_feedback dl = new dl_feedback();
                    bl.Feedback = txt_feedback.Text;
                    bl.Userid = Session["username"].ToString();
                    bl.Subject = ddl_subject.SelectedValue;
                    bl.Specify = txt_sub.Text;
                    rb = dl.insert_feedback(bl);
                    if (rb.status == true)
                    {
                        Utilities.MessageBox_UpdatePanel_Redirect(udp,"We Recieve Your FeedBack This Is Valuable For Us, We Will Response You Soon Thank You.", "../user/user_detail.aspx");
                    }
                    else
                    {
                        Utilities.MessageBox_UpdatePanel(udp,"Your Feed Back Is Not Registered");
                    }
                }
                else
                    Utilities.MessageBox_UpdatePanel(udp, "Please Enter Right Security code");
            }
        }
    }
    protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_subject.SelectedItem.Text == "Other")
        {
            div_sub.Visible = true;
            rfv_txt_sub.Enabled = true;
        }
        else
        {
            div_sub.Visible = false;
            rfv_txt_sub.Enabled = false;
        }
    }
}