using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reg_istration : BasePage // System.Web.UI.Page
{
    bl_RTI_Registration bl = new bl_RTI_Registration();
    dl_RTI_Registration dl = new dl_RTI_Registration();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
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
            bind_state();
            //bind_district();
            bind_gender();
            bind_usertype();
        }
        if(ddl_gender.SelectedValue == "0")
        {
            bind_gender();
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
                   // if (btn_submit.Text == "Submit")
                   // {
                        bl.UserID = txt_Userid.Text;
                        rd = dl.CheckUserID(bl);

                        
                        if (rd.table.Rows.Count == 0)
                        {
                            HttpBrowserCapabilities browse = Request.Browser;
                            bl.RegistrationID = dl.Get_unique_Registration_code();
                            bl.Password = ul.GenerateMd5Hash(txt_password.Text);
                            bl.NameHindi = "";
                            bl.NameEnglish = txt_first_name.Text;
                            bl.Gender = ddl_gender.SelectedValue;
                            bl.MobileNo = txt_mobile.Text;
                            bl.EmailID = txt_email.Text;
                            bl.Address = txt_Address.Text;
                            if (ddl_country.SelectedValue == "091")
                            {
                                bl.Country_name = txtState_Other.Text;
                                bl.Country = ddl_country.SelectedValue;
                                bl.Statecode = ddl_state.SelectedValue;
                                bl.DistrictCode = ddl_district.SelectedValue;
                                bl.DistrictName = txt_Districtname.Text;
                            }
                            else
                            {
                                bl.Country_name = txtState_Other.Text;
                                bl.Statecode = "";
                                bl.Country = ddl_country.SelectedValue;
                                bl.DistrictCode = "";
                            }
                            bl.PinCode = txt_pincode.Text;
                            bl.IsValid = "N";
                            bl.UserType = ddl_usertype.SelectedValue;
                            bl.Registration_Year = DateTime.Now.Year.ToString();
                            bl.UserIP = ul.GetClientIpAddress(this.Page);
                            bl.UserAgent = Request.UserAgent.ToString();
                            bl.UserOS = Utilities.System_Info(this.Page);




                            bl.User_browser = browse.Browser;
                            rb = dl.Insert_user_Registration(bl);
                            if (rb.status == true)
                            {
                                Session["username"] = bl.UserID;
                                Session["REGID"] = bl.RegistrationID;
                                //  Response.Redirect("User_Mobile_Varification.aspx?mobile=" + bl.MobileNo + "&RegistrationID=" + bl.RegistrationID + "");
                                Utilities.MessageBox_UpdatePanel_Redirect(udp_farm, "You Are Registered In Our Server You Need To Varify Your Mobile To activate Yor Login ID, We Are Redirecting To Our Varification Page", "user/User_Mobile_Varification.aspx");
                            }
                            else
                            {
                                Utilities.MessageBoxShow("registration Unsuccessful");
                            }
                        }
                        else
                        {
                            Utilities.MessageBoxShow("User ID Not Available Please Choose Another UserID");
                        }
                   // }
                }
                else
                {

                    Utilities.MessageBox_UpdatePanel(udp_farm, "Invalid Captcha");
                }
            }
        }
    }
    protected void txt_Userid_TextChanged(object sender, EventArgs e)
    {
        bl.UserID = txt_Userid.Text;
        rd = dl.CheckUserID(bl);
        if (rd.table.Rows.Count == 0)
        {
            //Utilities.MessageBoxShow("User Id Is Available");
        }
        else
        {
            Utilities.MessageBoxShow("User Id Is Already Exist");

        }
    }
    protected void imgbtn_refresh_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddl_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_country.SelectedValue == "091")
        {
            rfv_state.Enabled = false;
            rfv_ddl_state.Enabled = true;
            rfv_district.Enabled = true;
            rev_pincode.Enabled = true;
            lbl_state.Text = "State";
            lbl_district.Visible = true;
            ddl_district.Visible = true;
            ddl_state.Visible = true;
            txt.Visible = false;
            ddl.Visible = true;
            bind_state();
            bind_district();
            txt_pincode.MaxLength = 6;

        }
        else if (ddl_country.SelectedValue != "091" && ddl_country.SelectedValue != "0")
        {
            txt_Districtname.Text = "";
            txt_Districtname.Visible = false;
            rfv_dis_nam.Enabled = false;
            txt.Visible = true;
            lbl_state.Text = "Country Name";
            rfv_state.Enabled = true;
            rfv_ddl_state.Enabled = false;
            rfv_district.Enabled = false;
            rev_pincode.Enabled = false;
            lbl_district.Visible = false;
            ddl_district.Visible = false;
            ddl_state.Visible = false;
            ddl.Visible = false;
            txtState_Other.Text = "";
            bind_state();
            bind_district();
            txt_pincode.MaxLength = 10;
        }
        else
        {
            rfv_state.Enabled = false;
            rfv_ddl_state.Enabled = true;
            rfv_district.Enabled = true;
            rev_pincode.Enabled = true;
            lbl_state.Text = "State";
            lbl_district.Visible = true;
            ddl_district.Visible = true;
            ddl_state.Visible = true;
            txt.Visible = false;
            ddl.Visible = true;
            bind_state();
            bind_district();
            txt_pincode.MaxLength = 6;

        }
    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_state.SelectedValue == "00" || ddl_state.SelectedValue == "99")
        {
            txt_state_name.Visible = true;
            rfv_state_name.Enabled = true;
            txt_state_name.Text = "";
            ddl_district.SelectedValue = "0";
            txt_Districtname.Visible = false;
            rfv_dis_nam.Enabled = false;
        }
        else
        {
            txt_state_name.Visible = false;
            rfv_state_name.Enabled = false;
            txt_state_name.Text = "";
            bind_district();
            ddl_district.SelectedValue = "0";
            txt_Districtname.Visible = false;
            rfv_dis_nam.Enabled = false;
        }
    }
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_district.SelectedValue == "00" || ddl_district.SelectedValue == "999")
        {
            txt_Districtname.Visible = true;
            rfv_dis_nam.Enabled = true;
            txt_Districtname.Text = "";
        }
        else
        {
            txt_Districtname.Text = "";
            txt_Districtname.Visible = false;
            rfv_dis_nam.Enabled = false;
        }
    }
    public void bind_state()
    {
        ddl_state.Items.Clear();
        bl.Country = ddl_country.SelectedValue;
        rd = dl.BindState(bl);
        ddl_state.DataSource = rd.table;
        ddl_state.DataTextField = "state_name_e";
        ddl_state.DataValueField = "state_id";
        ddl_state.Items.Add(new ListItem(" Choose State", "0"));
        ddl_state.Items.Add(new ListItem(" OTHER", "00"));
        ddl_state.DataBind();
        if (ddl_country.SelectedValue == "091")
        {
            ddl_state.SelectedValue = "22";
            bind_district();
        }
    }
    public void bind_district()
    {
        ddl_district.Items.Clear();
        bl.Statecode = ddl_state.SelectedValue;
        rd = dl.BindDistrict(bl);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name_En";
        ddl_district.DataValueField = "district_id";
        ddl_district.Items.Add(new ListItem("Choose District", "0"));
        ddl_district.Items.Add(new ListItem("OTHER", "00"));
        ddl_district.DataBind();
        if (ddl_state.SelectedValue == "22")
        {
            ddl_district.SelectedValue = "11";
        }
    }
    public void bind_usertype()
    {
        rd = dl.BindUser();
        ddl_usertype.DataSource = rd.table;
        ddl_usertype.DataTextField = "DisplayName_en";
        ddl_usertype.DataValueField = "DDL_Name_Value";
        ddl_usertype.Items.Add(new ListItem("Choose User Type", "0"));
        ddl_usertype.DataBind();

    }
    public void bind_gender()
    {
        rd = dl.BindGender();
        ddl_gender.DataSource = rd.table;
        if (Session["language"].ToString() == "en-GB")
        {
            ddl_gender.DataTextField = "DisplayName_en";
        }
        else {
            ddl_gender.DataTextField = "DisplayName_hi";
        }
        ddl_gender.DataValueField = "DDL_Name_Value";
        ddl_gender.DataBind();

        if (Session["language"].ToString() == "en-GB")
        {           
            ddl_gender.Items.Insert(0, new ListItem("Choose Gender", "0"));
        }
        else
        {
            ddl_gender.Items.Insert(0, new ListItem("लिंग चुनिए", "0"));
        }
       
        
    }
   
}// End of Class
