using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WEBHOME_Forget_password : BasePage // System.Web.UI.Page
{
    bl_RTI_Registration bl1 = new bl_RTI_Registration();
    dl_RTI_Registration dl1 = new dl_RTI_Registration();
    bl_login bl = new bl_login();
    dl_login dl = new dl_login();
    Utilities ul = new Utilities();
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
        }
    }
    protected void lnkbtn_otp_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Page.IsValid)
            {
                txt_otp.Text = "";
                bl.UserID = txt_username.Text;
                dt = dl.Select_user_detail(bl);
                if (dt.table.Rows.Count > 0)
                {
                    Session["user"] = bl.UserID;
                    bl1.RegistrationID = dt.table.Rows[0]["RegistrationID"].ToString();
                    dt = dl1.Select_User_detail(bl1);
                    if (dt.table.Rows.Count > 0)
                    {
                        bl1.OTP = dl1.GenOTPString(8);
                        bl1.EmailID = dt.table.Rows[0]["EmailID"].ToString();
                        bl1.UserID = dt.table.Rows[0]["UserID"].ToString();
                        bl1.MobileNo = dt.table.Rows[0]["MobileNo"].ToString();
                        bl1.Type = "Password Forget";
                        bl1.UserIP = ul.GetClientIpAddress(this.Page);
                        bl1.Sms_detail = "your OTP for Userid " + bl.UserID + " is " + bl1.OTP + ". Do Not disclose Your OTP With AnyOne. it is Valid For 10 minutes.";
                        bl1.RegistrationID = dt.table.Rows[0]["RegistrationID"].ToString();
                        bl1.EmailID = dt.table.Rows[0]["EmailID"].ToString();
                        Session.Add("OTP", bl1.OTP);
                        rb = dl1.InsertOtp(bl1);
                        if (rb.status == true)
                        {
                            div1.Visible = true;
                            div2.Visible = false;
                        }
                    }
                    else
                    {
                        if (Session["language"].ToString() == "en-GB")
                        {
                            Utilities.MessageBoxShow_Redirect("You Are Not Registered User ", "Logout.aspx");

                        }
                        else
                        {
                            Utilities.MessageBoxShow_Redirect("आप पंजीकृत उपयोगकर्ता नहीं हैं ", "Logout.aspx");
                        }

                       // Utilities.MessageBoxShow_Redirect("You Are Not Registered User ", "../Logout.aspx");
                    }
                }
                else
                {
                    if (Session["language"].ToString() == "en-GB")
                    {
                        Utilities.MessageBoxShow_Redirect("User Id Not Recognised Please Create New ID ", "logout.aspx");

                    }
                    else
                    {
                        Utilities.MessageBoxShow_Redirect("यूजर आईडी पहचान नहीं है कृपया नया आईडी बनाएं", "logout.aspx");
                    }
                   // Utilities.MessageBoxShow_Redirect("user id Not Recognised Please Create new ID ", "logout.aspx");
                }
            }
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Page.IsValid)
            {
                Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
                if (Captcha1.UserValidated)
                {
                    string OTP = Session["OTP"].ToString();
                    if (txt_otp.Text == OTP)
                    {
                        HttpBrowserCapabilities browse = Request.Browser;
                        if (txt_new_pass.Text == txt_repass.Text)
                        {
                            bl.UserID = Session["user"].ToString();
                            dt = dl.select_password_by_username(bl);
                            Session["pass"] = dt.table.Rows[0]["Password"].ToString();
                            bl.NewPass = ul.GenerateMd5Hash(txt_new_pass.Text);
                            dt = dl.select_password_log(bl);
                            if (dt.table.Rows.Count == 0)
                            {
                                bl.UserID = Session["user"].ToString();
                                bl.RegistrationID = bl.UserID;          //Added by Naveen on 09-sep-2017
                                bl.Password = Session["pass"].ToString();
                                bl.NewPass = ul.GenerateMd5Hash(txt_new_pass.Text);
                                bl.UserIP = ul.GetClientIpAddress(this.Page);
                                bl.UserAgent = Request.UserAgent.ToString();
                                bl.UserOS = Utilities.System_Info(this.Page);
                                bl.User_browser = browse.Browser;
                                bl.PasswordChange = "Y";
                                rb = dl.update_password(bl);
                                if (rb.status == true)
                                {
                                    if (Session["language"].ToString() == "en-GB")
                                    {
                                        Utilities.MessageBoxShow_Redirect("Your Password Updated Successfully, Please Login Again", "LogOut.aspx");

                                    }
                                    else
                                    {
                                        Utilities.MessageBoxShow_Redirect("आपका पासवर्ड सफलतापूर्वक अपडेट किया गया, कृपया फिर से लॉगिन करें", "LogOut.aspx");
                                    }
                                    // Utilities.MessageBoxShow_Redirect("Your Password Update Successful Please Login Again", "../LogOut.aspx");
                                }
                                else
                                {
                                    if (Session["language"].ToString() == "en-GB")
                                    {
                                        Utilities.MessageBoxShow("Password Update Unsuccessful");

                                    }
                                    else
                                    {
                                        Utilities.MessageBoxShow("पासवर्ड अपडेट असफल");
                                    }
                                    // Utilities.MessageBoxShow("Password Update Unsuccessful");
                                }
                            }
                            else
                            {
                                if (Session["language"].ToString() == "en-GB")
                                {
                                    Utilities.MessageBoxShow("Your New Password Looks Like Your Old Password, Please Create Unique Password");

                                }
                                else
                                {
                                    Utilities.MessageBoxShow("आपका नया पासवर्ड आपके पुराने पासवर्ड की तरह दिखता है, कृपया अद्वितीय पासवर्ड बनाएं");
                                }
                                // Utilities.MessageBoxShow("Your New Password Looks Like Your Old Password Please Create Unique Password");
                            }
                        }
                        else
                        {
                            if (Session["language"].ToString() == "en-GB")
                            {
                                Utilities.MessageBoxShow("Your New Password Does Not Match With Your Repeated Password, Please Check");

                            }
                            else
                            {
                                Utilities.MessageBoxShow("आपका नया पासवर्ड आपके दोहराए गए पासवर्ड से मेल नहीं खाता, कृपया चेक करें");
                            }
                            //Utilities.MessageBoxShow("Your New Password Does Not Match With Your Repeated Password Please Check");
                        }
                    }
                    else
                    {
                        if (Session["language"].ToString() == "en-GB")
                        {
                            Utilities.MessageBoxShow("You have Entered Wrong OTP, Please Enter Right OTP");

                        }
                        else
                        {
                            Utilities.MessageBoxShow("आपने गलत OTP दर्ज किया है, कृपया सही OTP दर्ज करें");
                        }
                        // Utilities.MessageBoxShow("You Entered Wrong OTP Please Enter Right OTP");
                    }
                }
                else {
                    if (Session["language"].ToString() == "en-GB")
                    {
                        Utilities.MessageBoxShow("Incorrect Security Code");

                    }
                    else
                    {
                        Utilities.MessageBoxShow("गलत सुरक्षा कोड");
                    }
                  
                }
            }
        }
    }
}