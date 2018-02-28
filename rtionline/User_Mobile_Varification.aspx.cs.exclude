using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

public partial class User_Mobile_Varification : System.Web.UI.Page
{
    Utilities ul = new Utilities();
    bl_RTI_Registration bl = new bl_RTI_Registration();
    dl_RTI_Registration dl = new dl_RTI_Registration();
    bl_RTI_Request bl_rti = new bl_RTI_Request();
    dl_RTI_Request dl_rti = new dl_RTI_Request();
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
            try
            {
                if (Session["username"].ToString() != null)
                {
                    bl.UserID = Session["username"].ToString();
                    dt = dl.CheckUserID(bl);
                    if (dt.table.Rows.Count == 0)
                    {
                        bl.RegistrationID = Session["REGID"].ToString();
                        dt = dl.Select_User_detail(bl);
                        if (dt.table.Rows.Count > 0)
                        {
                            bl.MobileNo = dt.table.Rows[0]["MobileNo"].ToString();
                            bl.UserID = dt.table.Rows[0]["UserID"].ToString();
                            Session["VERIFICATION_TYPE"] = "User";
                        }
                    }
                    else if (dt.table.Rows.Count > 0)
                    {
                        if (Session["VERIFICATION_TYPE"].ToString() == "Rti")  // For Rti
                        {
                            bl.RegistrationID = dt.table.Rows[0]["LoginID"].ToString();
                            Session["REGID"] = bl.RegistrationID;
                            bl_rti.RTI_Request_id = Session["RTI_ID"].ToString();
                            dt = dl_rti.GetMobileNumForVerification(bl_rti);
                            if (dt.table.Rows.Count > 0)
                            {
                                bl.MobileNo = dt.table.Rows[0]["Mobile_No"].ToString();
                                bl.UserID = dt.table.Rows[0]["User_ID"].ToString();
                            }
                        }
                        else
                        {
                            bl.RegistrationID = dt.table.Rows[0]["LoginID"].ToString();
                            Session["REGID"] = bl.RegistrationID;
                            dt = dl.Select_User_detail(bl);
                            if (dt.table.Rows.Count > 0)
                            {
                                bl.MobileNo = dt.table.Rows[0]["MobileNo"].ToString();
                                bl.UserID = dt.table.Rows[0]["UserID"].ToString();
                            }
                        } // VERIFICATION_TYPE ==User 
                    }
                }
                else
                {
                    Response.Redirect("../LogOut.aspx");
                }
            }
            catch (NullReferenceException ex)
            {
                Response.Redirect("../LogOut.aspx");
            }
            txt_mobile.Text = bl.MobileNo;
            btn_send.Text = "Send";
        }
    }

    protected void btn_send_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            txt_otp.Text = "";
            bl.RegistrationID = Session["REGID"].ToString();
            dt = dl.Select_User_detail(bl);
            if (dt.table.Rows.Count > 0)
            {
                bl.OTP = dl.GenOTPString(8);
                bl.EmailID = dt.table.Rows[0]["EmailID"].ToString();
                bl.UserID = dt.table.Rows[0]["UserID"].ToString();
                bl.MobileNo = txt_mobile.Text;
                bl.Type = Session["VERIFICATION_TYPE"].ToString();
                bl.UserIP = ul.GetClientIpAddress(this.Page);
                bl.Sms_detail = "your OTP for Userid " + bl.UserID + " is " + bl.OTP + ". Do Not disclose Your OTP With AnyOne. it is Valid For 10 minutes.";
                bl.RegistrationID = dt.table.Rows[0]["RegistrationID"].ToString();
                bl.EmailID = dt.table.Rows[0]["EmailID"].ToString();
                Session.Add("OTP", bl.OTP);
                rb = dl.InsertOtp(bl);
                if (rb.status == true)
                {
                    btn_send.Text = "Resend";
                    div_otp.Visible = true;
                    Utilities.MessageBox_UpdatePanel(udp, "One Time Password Has Been Sended To Your Mobile Please Varify");
                }
                else
                {
                    btn_send.Text = "send";
                }
            }
            else
            {
                Response.Redirect("../LogOut.aspx");
            }
        }
    }
    protected void btn_otp_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            string OTP = Session["OTP"].ToString();
            if (txt_otp.Text == OTP)
            {
                HttpBrowserCapabilities browse = Request.Browser;
                if (Session["VERIFICATION_TYPE"].ToString() == "User")
                {
                    bl.RegistrationID = Session["REGID"].ToString();
                    dt = dl.Select_User_detail(bl);
                    if (dt.table.Rows.Count > 0)
                    {
                        bl.UserID = dt.table.Rows[0]["UserID"].ToString();
                        bl.RegistrationID = dt.table.Rows[0]["RegistrationID"].ToString();
                        bl.Password = dt.table.Rows[0]["Password"].ToString();
                        bl.RollID = "2";
                        bl.PasswordChange = "N";
                        bl.Active = "Y";
                        bl.IsValid = "Y";
                        bl.UserIP = ul.GetClientIpAddress(this.Page);
                        bl.UserAgent = Request.UserAgent.ToString();
                        bl.UserOS = Utilities.System_Info(this.Page);
                        bl.User_browser = browse.Browser;
                        using (TransactionScope ts = new TransactionScope())
                        {
                            rb = dl.Update_registration(bl);
                            if (rb.status == true)
                            {
                                rb = dl.Insert_Login(bl);
                                if (rb.status == true)
                                {
                                    ts.Complete();
                                    Utilities.MessageBox_UpdatePanel_Redirect(udp, "Your Mobile Varification Is successful. Your Userid is " + bl.UserID + " and Password is That You Have selected", "../LogOut.aspx");
                                }
                            }
                        }
                    }
                }
                else if (Session["VERIFICATION_TYPE"].ToString() == "Rti")
                {
                    bl_rti.RTI_Request_id = Session["RTI_ID"].ToString();
                    dt = dl_rti.GetRtiStatus_detail(bl_rti);
                    if (dt.table.Rows.Count > 0)
                    {
                        bl_rti.IsValid = dt.table.Rows[0]["isvalid"].ToString();
                    }
                    if (bl_rti.IsValid == "N" || bl_rti.IsValid == "")
                    {
                        bl_rti.RTI_Request_id = Session["RTI_ID"].ToString();
                        bl_rti.IsValid = "Y";
                        rb = dl_rti.update_Rti_status(bl_rti);
                        if (rb.status == true)
                        {
                            bl_rti.RTI_Request_id = Session["RTI_ID"].ToString();
                            dt = dl_rti.GetRtiStatus_detail(bl_rti);
                            if (dt.table.Rows.Count > 0)
                            {
                                bl_rti.Securitycode = dt.table.Rows[0]["seqcode"].ToString();
                                bl_rti.IsValid = dt.table.Rows[0]["isvalid"].ToString();
                            }
                            string rti_message = "Your Mobile Varification Is successful. Your Rti Has Been Registered with registration no.: " + bl_rti.RTI_Request_id + " AND Your Security Code is " + bl_rti.Securitycode + ". Please Save Both For Details";
                            Utilities.MessageBox_UpdatePanel_Redirect(udp, rti_message, "../user/UserWelcome.aspx");
                        }
                    }
                    else
                    {
                        txt_otp.Text = "";
                        div_otp.Visible = false;
                        Utilities.MessageBox_UpdatePanel_Redirect(udp, "Rti Already Registered", "../user/UserWelcome.aspx");
                    }
                }
            }
            else
            {
                Utilities.MessageBox_UpdatePanel(udp, "Your Mobile Varification Is Unsuccessful. You Entered Wrong OTP");
                txt_otp.Text = "";
                div_otp.Visible = false;
            }
        }
    }
}