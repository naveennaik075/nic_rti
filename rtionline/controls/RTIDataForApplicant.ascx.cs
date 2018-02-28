using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Configuration;

public partial class controls_RTIDataForApplicant : System.Web.UI.UserControl
{

    bl_RTIDataForApplicant bl = new bl_RTIDataForApplicant();
    dl_RTIDataForApplicant dl = new dl_RTIDataForApplicant();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    Utilities ul = new Utilities();
    //protected override void InitializeCulture()
    //{
    //    Culture = "en-GB";
    //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

    //    base.InitializeCulture();
    //}
    protected override void FrameworkInitialize()
    {
        String selectedLanguage = "en-GB";
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedLanguage);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);

        base.FrameworkInitialize();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender2.StartDate = DateTime.Now;
        comval1.ValueToCompare = DateTime.Now.Date.ToShortDateString();
         comval1.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
        //comp_date.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
        if (!Page.IsPostBack)
        {            
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Session["username"] == null)
            {
                Response.Redirect("../logout.aspx");
            }
            else
            {
             // Any initialization here
   
            }
        }
    }
    
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            
           
            if (Page.IsValid)
            {
                if (btnSubmit.Text == "Submit")
                {
                    #region rti_data_for_applicant
                    bl.RTI_data_id = dl.max_RTI_Data_id();
                    bl.Rti_id = "2017000056";    // Need to get it from Session
                    bl.Action_id = "002"; // Need to get it from Session
                    //bl.RTI_fileID = ""; // Insert file first if  is File Upload is true considerd in file upload section
                    bl.Userid = "lkverma"; // Need to get it from session [rti_prepared_emp]
                    bl.Office_mapping_id = "201700021001"; // Need to get it from either session or database.
                    bl.Result_description = txt_description.Text;
                    bl.IsMeeting = RadioMeeting.SelectedValue;
                    #region meeting
                    if (RadioMeeting.SelectedValue == "Y")
                    {
                        bl.Meeting_date = txt_meeting.Text;
                        if (bl.Meeting_date != "")
                        {
                            bl.Meeting_date = getDate(txt_meeting.Text);
                        }
                        else
                        {
                            bl.Meeting_date = "";
                        }

                    }
                    else
                    {
                        bl.Meeting_date = null;
                    }
                    #endregion meeting
                    bl.IsAdditionalFees = rbl_additional_fees.SelectedValue;
                    #region Additional Fees
                    if (bl.IsAdditionalFees == "Y")
                    {
                        bl.Fees_amount = txt_amount.Text;
                        
                    }
                    else
                    {
                        bl.Fees_amount = null;
                    }
                    #endregion Additional Fees
                    HttpBrowserCapabilities browse = Request.Browser;
                    bl.Ipaddress = ul.GetClientIpAddress(this.Page);
                    bl.Useragent = Request.UserAgent.ToString();
                    bl.Useros = Utilities.System_Info(this.Page);
                    bl.User_browser = browse.Browser;
                    #endregion rti_data_for_applicant
                    bl.Is_file_upload = ddl_fileup.SelectedValue;
                   
                    #region file upload
                    bl.RTI_fileID = "";
                    if (ddl_fileup.SelectedValue == "Y")
                    {
                        if (fu_action.HasFile)
                        {
                            if (txt_file_desc.Text != "")
                            {
                                HttpPostedFile file = fu_action.PostedFile;
                                if (file.ContentLength < 2000000)
                                {
                                    if (file.ContentType == "application/pdf" || file.ContentType == "application/x-pdf"
                                        || file.ContentType == "application/x-unknown")
                                    {
                                        bl.RTI_fileID = dl.max_file_id();
                                        Stream fs = file.InputStream;
                                        BinaryReader br = new BinaryReader(fs);
                                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                        bl.RTI_fileName = file.FileName;
                                        bl.RTI_fileType = file.ContentType;
                                        bl.RTI_fileData = bytes;
                                        bl.FileDescription = txt_file_desc.Text;

                                        rb = dl.Insert_Action_Info(bl);
                                        if (rb.status == true)
                                        {
                                            Utilities.MessageBox_UpdatePanel_Redirect(udp, "Action Submitted Successfully", "../Employee/EmployeeWelcome_Page.aspx");
                                        }
                                        else
                                        {
                                            Utilities.MessageBox_UpdatePanel(udp, "Action Not Submitted");
                                        }
                                    }
                                    else
                                    {
                                        Utilities.MessageBox_UpdatePanel(udp, "Only PDF type File will be accepted");
                                    }

                                }
                                else
                                {
                                    Utilities.MessageBox_UpdatePanel(udp, "Your file size is greater than 2 MB  ");
                                }
                            }
                            else
                            {
                                Utilities.MessageBox_UpdatePanel(udp, "File Description Is Required");
                            }
                        }
                        else
                        {
                            Utilities.MessageBox_UpdatePanel(udp, "PDF File required");
                        }

                    }
                    #endregion
                    else
                    {
                        rb = dl.Insert_Action_Info(bl);
                        if (rb.status == true)
                        {
                            Utilities.MessageBox_UpdatePanel_Redirect(udp, "Data Submitted Successfully", "../Employee/EmployeeWelcome_Page.aspx");
                        }
                        else
                        {
                            Utilities.MessageBox_UpdatePanel(udp, "Data Not Submitted");
                        }
                    }

                    //Response.Write("Success");
                }

            }
        }
    }

    private string getDate(string givendate)
    {
        string date1 = givendate.ToString();

        string dd = date1.Substring(0, 2);
        string mm = date1.Substring(3, 2);
        string yyyy = date1.Substring(6, 4);
        string date2 = yyyy + "-" + mm + "-" + dd;
        return date2;
    }
    protected void ddl_fileup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_fileup.SelectedValue == "Y")
        {
            div_file_upload.Visible = true;
            rfv_fu.Enabled = true;
            rfv_file_desc.Enabled = true;

        }
        else
        {

            div_file_upload.Visible = false;
            rfv_fu.Enabled = false;
            rfv_file_desc.Enabled = false;
        }

    }


    protected void RadioMeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioMeeting.SelectedValue == "Y")
        {
            div2.Visible = true;
            rfv_meeting.Enabled = true;
            comval1.Enabled = true;
            
        }
        else if (RadioMeeting.SelectedValue == "N")
        {
            div2.Visible = false;
            rfv_meeting.Enabled = false;
            comval1.Enabled = false;
           
        }
    }


    protected void Radioadditional_fees_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_additional_fees.SelectedValue == "Y")
        {
            div_additional_fees.Visible = true;
            rfv_txt_amount.Enabled = true;
            rev_txt_amount.Enabled = true;

        }
        else if (rbl_additional_fees.SelectedValue == "N")
        {
            div_additional_fees.Visible = false;
            rfv_txt_amount.Enabled = false;
            rev_txt_amount.Enabled = false;
        }
    }

}