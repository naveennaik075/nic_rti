using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.IO;
using System.Net;
using System.Net.Mail;
public partial class User_Mobile_Varification : BasePage   // System.Web.UI.Page
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
        if (btn_otp.Visible == true)
        {
            if (Session["language"].ToString() == "en-GB")
            {
                btn_send.Text = "Resend";

            }
            else
            {
                btn_send.Text = "फिर से भेजें";

            }


        }
        else
        {
            if (Session["language"].ToString() == "en-GB")
            {
                btn_send.Text = "send";
            }
            else
            {
                btn_send.Text = "भेजें";

            }
        }
        //btn_send.Text = "Send";
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
            
            ////btn_send.Text = "Send";  // moved this line to pre_render event
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
                    div_otp.Visible = true;
                    // Email sending Code Added By Naveen on 09-oct-2017 Start
                    // The below code for sending email is working need to give the from account mail id and password
                    /*
                    string fromMail = "From gmail ID here";
                    string fromPass = "Enter From Account Password Here";
                    using (MailMessage mm = new MailMessage(fromMail, bl.EmailID))
                    {
                        mm.Subject = "Registration OTP";
                        mm.Body = bl.Sms_detail;                        
                        mm.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(fromMail, fromPass);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                     }
                     */
                    // Email sending Code Added By Naveen on 09-oct-2017 end
                   
                    string str_msg = "";
                    if (Session["language"].ToString() == "en-GB")
                    {
                        btn_send.Text = "Resend";
                        str_msg =  "One Time Password Has Been Sended To Your Mobile Please Verify";
                    }
                    else
                    {
                        btn_send.Text = "फिर से भेजें";
                        str_msg = "आपका वन टाइम पासवर्ड आपके मोबाईल न. पर भेज दिया गया है|कृपया जाँच कर लें| ";
                    }

                    Utilities.MessageBox_UpdatePanel(udp, str_msg);

                }
                else
                {
                    if (Session["language"].ToString() == "en-GB")
                    {
                        btn_send.Text = "send";
                    }
                    else
                    {
                        btn_send.Text = "भेजें";
                       
                    }
                   // btn_send.Text = "send";
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

                                    string str_msg = "";
                                    if (Session["language"].ToString() == "en-GB")
                                    {

                                        str_msg = "Your Mobile Varification Is successful. Your Userid is " + bl.UserID + " and Password is That You Have selected";
                                    }
                                    else
                                    {
                                        
                                        str_msg = "आपके मोबाईल की जाँच सफलतापूर्वक हो गई है| आपका यूजर आई. डी. है " + bl.UserID + "और पासवर्ड वह है जो आपने चुन लिया है |" ;
                                    }


                                    Utilities.MessageBox_UpdatePanel_Redirect(udp,str_msg, "../LogOut.aspx");
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
                            string rti_message = "";
                            if (Session["language"].ToString() == "en-GB")
                            {
                                rti_message = "RTI Submission successfull your Registration no. is: " + bl_rti.RTI_Request_id + "  And  Your Security Code is:  " + bl_rti.Securitycode + "   please save  details for further Information.";
                            }
                            else
                            {
                                rti_message = "आपका आर. टी. आई. सफलता पूर्वक सबमिट हो गया, आपका रजिस्ट्रेशन न. है " + bl_rti.RTI_Request_id + " और सुरक्षा कोड है " + bl_rti.Securitycode + "कृपया विवरण को उपयोग के लिए सुरक्षित रखें|";
                            }


                           // string rti_message = "Your Mobile Varification Is successful. Your Rti Has Been Registered with registration no.: " + bl_rti.RTI_Request_id + " AND Your Security Code is " + bl_rti.Securitycode + ". Please Save Both For Details";
                            Utilities.MessageBox_UpdatePanel_Redirect(udp, rti_message, "../user/UserWelcome.aspx");
                        }
                    }
                    else
                    {
                        txt_otp.Text = "";
                        div_otp.Visible = false;
                        if (Session["language"].ToString() == "en-GB")
                        {
                            Utilities.MessageBox_UpdatePanel_Redirect(udp, "Rti Already Registered", "../user/UserWelcome.aspx");
                        }
                        else
                        {
                            Utilities.MessageBox_UpdatePanel_Redirect(udp, "आर. टी. आई. पहले से ही रजिस्टर्ड है", "../user/UserWelcome.aspx");
                        }
                       //  Utilities.MessageBox_UpdatePanel_Redirect(udp, "Rti Already Registered", "../user/UserWelcome.aspx");
                    }
                }
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {
                    Utilities.MessageBox_UpdatePanel(udp, "Your Mobile Varification Is Unsuccessful. You Entered Wrong OTP");
                }
                else
                {
                    Utilities.MessageBox_UpdatePanel(udp, "गलत ओ. टी. पी. के कारण आपका मोबाईल जाँच असफल हो गया है|");
                }
                //Utilities.MessageBox_UpdatePanel(udp, "Your Mobile Varification Is Unsuccessful. You Entered Wrong OTP");
                txt_otp.Text = "";
                div_otp.Visible = false;
            }
        }
    }
}