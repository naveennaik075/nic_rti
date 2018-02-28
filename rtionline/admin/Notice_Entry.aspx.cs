//ekta kushwah
//nov-2016
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using System.Globalization;
using System.Threading;
public partial class admin_notice_entry : BasePage  // System.Web.UI.Page
{
    string LastFileId = null;
    Int32 maxsize = 4489100;
    Int32 maxSizePdf = 10485760;

    //Int32 maxSizePdf = 4194304;
    Int32 maxSizeJpg = 1048576;
    Utilities util = new Utilities();
    SqlTransaction sqlt;
    dl_rti_notice dl = new dl_rti_notice();
    Notice_Entry n = new Notice_Entry();
    Upload_doc bl = new Upload_doc();
    //string conn = ConfigurationManager.ConnectionStrings["Boiler_Constr"].ConnectionString.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        //   literal.Text = "<span id='myId'></span>";
        try
        {
            //  Label lblheader = (Label)Master.FindControl("lbl_header");
            //lblheader.Text = "Notice Entry";
            

            if (!Page.IsPostBack)
            {
                CalendarExtender1.StartDate = DateTime.Now;
                CalendarExtender2.StartDate = DateTime.Now;
                CalendarExtender3.StartDate = DateTime.Now;
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                //BindDropDownList();
                //BindTitles();


                bind_district();
                BaseDepartmentBind();
                OfficeBind();
             //   show();
            }
            BindDropDownList();
            BindTitles();
        }
        catch { }
    }


    public void bind_district()        // user district
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        ddl_district.Items.Clear();
        bl.State = "22";//DDL_State.SelectedValue; //22 for Chhattisgarh
        rd = dl.BindDistrict(bl);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";
        ddl_district.DataBind();

        ddl_district.Items.Insert(0, new ListItem("--Select District--", "0"));

    }
    public void BaseDepartmentBind()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();

        ReturnClass.ReturnDataTable dt = dl.Get_BaseDepartment(bl);// ("SELECT dept_id, dept_name FROM basedepartment order by dept_name ");
        ddl_department.DataSource = dt.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";
        ddl_department.DataBind();
        ddl_department.Items.Insert(0, new ListItem("--Select Department--", "0"));
    }

    public void OfficeBind()
    {
        Notice_Entry bl = new Notice_Entry();
        dl_rti_notice dl = new dl_rti_notice();
        bl.Base_dept_id = ddl_department.SelectedItem.Value;
        bl.District_id = ddl_district.SelectedItem.Value;
        ReturnClass.ReturnDataTable dt = dl.Get_Office_For_Notice(bl);
        ddl_office.DataSource = dt.table;
        ddl_office.DataTextField = "OfficeName";
        ddl_office.DataValueField = "NewOfficeCode";
        ddl_office.DataBind();
        ddl_office.Items.Insert(0, new ListItem("--Select Office--", "0"));
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {

        OfficeBind();

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeBind();
    }

    protected override void InitializeCulture()
    {
       
        //if (Session["language"].ToString() == "en-GB")
        //{

            Culture = "en-GB";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
        //}
        //else
        //{
        //    Culture = "hi-IN";
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("hi-IN");
        //}
        base.InitializeCulture();
    }
    //public void show()
    //{
        
    //    if (ViewState["Permanent"].ToString() == "Y")
    //    {
    //        ddlPermanent.SelectedValue = "Y";
    //    }
    //    else if (ViewState["Permanent"].ToString() == "N")
    //    {
    //        ddlPermanent.SelectedValue = "N";
    //    }
    //    if ( ViewState["Hyperlink"].ToString() == "Y")
    //    {
    //        ddlHyperlink.SelectedValue = "Y";
    //    }
    //    else if ( ViewState["Hyperlink"].ToString() == "N")
    //    {
    //        ddlHyperlink.SelectedValue = "N";
    //    } 
    //}
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //if (Session["Role_ID"].ToString() == "01")
            //   this.MasterPageFile = "../Master_user.master";
        }
        catch { }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    public void preview()
    {
        BindDropDownList();
        BindTitles();
        Image1.ImageUrl = HiddenField1.Value;
        lblpreview.Text = HiddenField2.Value;
        string colorcode = HiddenField3.Value;
        lblpreview.ForeColor = System.Drawing.ColorTranslator.FromHtml(colorcode);
        string bold = HiddenFieldbold.Value;
        if (bold=="B" )
        {
            lblpreview.Font.Bold = true;
        }
        else
        {
            lblpreview.Font.Bold = false;
        }
        string blink = HiddenFieldblink.Value;
        if (blink == "BL")
        {
            blinkimage.Attributes.Add("class", "blinking");
        }
        else
        {
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Upload_doc bl = new Upload_doc();
        Notice_Entry n = new Notice_Entry();

        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();

        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (ddlPermanent.SelectedValue == "Y")
            {
                RequiredFieldValidator10.Enabled = false;
                RequiredFieldValidator10.ValidationGroup = "x";
                RequiredFieldValidator9.Enabled = false;
                RequiredFieldValidator9.ValidationGroup = "x";
                RegularExpressionValidator10.Enabled = false;
                RegularExpressionValidator10.ValidationGroup = "x";
                RegularExpressionValidator17.Enabled = false;
                RegularExpressionValidator17.ValidationGroup = "x";

                RequiredFieldValidator7.Enabled = true;
                RequiredFieldValidator7.ValidationGroup = "a";
                RegularExpressionValidator1.Enabled = true;
                RegularExpressionValidator1.ValidationGroup = "a";


            }
            else if (ddlPermanent.SelectedValue == "N")
            {
                RequiredFieldValidator10.Enabled = true;
                RequiredFieldValidator9.Enabled = true;
                RequiredFieldValidator10.ValidationGroup = "a";
                RequiredFieldValidator9.ValidationGroup = "a";
                RegularExpressionValidator10.Enabled = true;
                RegularExpressionValidator10.ValidationGroup = "a";
                RegularExpressionValidator17.Enabled = true;
                RegularExpressionValidator17.ValidationGroup = "a";

                RequiredFieldValidator7.Enabled = false;
                RequiredFieldValidator7.ValidationGroup = "x";
                RegularExpressionValidator1.Enabled = false;
                RegularExpressionValidator1.ValidationGroup = "x";

            }
            if (ddlHyperlink.SelectedValue == "Y")
            {
                RequiredFieldValidator3.Enabled = true;
                RequiredFieldValidator3.ValidationGroup = "a";


            }
            else if (ddlHyperlink.SelectedValue == "N")
            {
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator3.ValidationGroup = "x";
                RequiredFieldValidator6.Enabled = false;
                RequiredFieldValidator6.ValidationGroup = "x";
                RequiredFieldValidator4.Enabled = false;
                RequiredFieldValidator4.ValidationGroup = "x";
                regUrl.ValidationGroup = "x";
                regUrl.Enabled = false;

            }
            if (ddlType.SelectedValue == "E")
            {
                RequiredFieldValidator6.Enabled = false;
                RequiredFieldValidator6.ValidationGroup = "x";
                RequiredFieldValidator4.Enabled = true;
                RequiredFieldValidator4.ValidationGroup = "a";
                regUrl.ValidationGroup = "a";
                regUrl.Enabled = true;
            }
            else if (ddlType.SelectedValue == "I")
            {
                RequiredFieldValidator6.Enabled = true;
                RequiredFieldValidator6.ValidationGroup = "a";
                RequiredFieldValidator4.Enabled = false;
                RequiredFieldValidator4.ValidationGroup = "x";
                regUrl.ValidationGroup = "x";
                regUrl.Enabled = false;

            }
            if (HiddenField1.Value == "0" || HiddenField1.Value == "Select")
            {
                RFV_Image_Icon.ValidationGroup = "a";
                RFV_Image_Icon.Enabled = true;
            }
            else {
                RFV_Image_Icon.ValidationGroup = "x";
                RFV_Image_Icon.Enabled = false;
            }

            if (Page.IsValid)
            {
                Boolean st = true;
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        n.District_id = ddl_district.SelectedValue;// Session["Dist_Code"].ToString();
                        n.Base_dept_id = ddl_department.SelectedValue;
                        n.Office_id = ddl_office.SelectedValue;

                        n.Header = txtHeader.Text.Trim();
                        n.Permanent = ddlPermanent.SelectedValue;
                        n.Active = ddlActive.SelectedValue;
                        n.Hyperlink = ddlHyperlink.SelectedValue.ToString();
                        n.Client_ip = util.GetClientIpAddress(this.Page);
                        n.User_id = Session["username"].ToString();
                      //  n.Datetime = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                        DateTime today = DateTime.Today;
                        n.Datetime = today.ToString("yyyy/MM/dd");
                        if (ddlPermanent.SelectedValue == "N")
                        {
                            n.Publish_date = null;
                            //string datefrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd HH:mm:ss");
                            //string dateto = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd HH:mm:ss");
                            //n.Datefrom = datefrom.ToString();
                            //n.Dateto = dateto.ToString();

                            DateTime datefrom = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            n.Datefrom = datefrom.ToString("yyyy/MM/dd HH:mm:ss");
                            DateTime dateto = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            n.Dateto = dateto.ToString("yyyy/MM/dd HH:mm:ss");

                        }
                        else if (ddlPermanent.SelectedValue == "Y")
                        {

                            DateTime Publish_date = DateTime.ParseExact(txtPublishDate.Text, "dd/MM/yyyy",  CultureInfo.InvariantCulture);
                            n.Publish_date = Publish_date.ToString("yyyy/MM/dd HH:mm:ss");

                            //n.Publish_date = Convert.ToDateTime(txtPublishDate.Text.Trim()).ToString("yyyy/MM/dd HH:mm:ss");
                            n.Datefrom = null;
                            n.Dateto = null;
                        }

                        if (ddlHyperlink.SelectedValue == "Y")
                        {
                            n.Filetype = ddlType.SelectedValue;
                            if (ddlType.SelectedValue == "I")
                            {
                                if (FileUpload1.HasFile)
                                {
                                    if (FileUpload1.PostedFile.ContentType == "application/x-pdf" || FileUpload1.PostedFile.ContentType == "application/pdf")
                                    {
                                        if (FileUpload1.PostedFile.ContentLength > maxSizePdf)
                                        {
                                            if (Session["language"].ToString() == "en-GB")
                                            {

                                                Utilities.MessageBoxShow("This PDF File Is too Big...Maximum Size allowed is 10 Mb");
                                            }
                                            else
                                            {

                                                Utilities.MessageBoxShow("यह PDF बहुत बड़ा है...अधिकतम साइज़ 10 Mb की अनुमति है");
                                            }
                                          //  Utilities.MessageBoxShow("This PDF File Is too Big...Maximum Size allowed is 10 Mb");
                                            st = false;
                                            preview();
                                             
                                        }
                                    }
                                    else if (FileUpload1.PostedFile.ContentType == "image/jpg" || FileUpload1.PostedFile.ContentType == "image/jpeg")
                                    {
                                        if (FileUpload1.PostedFile.ContentLength > maxSizeJpg)
                                        {
                                            if (Session["language"].ToString() == "en-GB")
                                            {

                                                Utilities.MessageBoxShow("This JPG File Is too Big...Maximum Size allowed is 1 Mb");
                                            }
                                            else
                                            {

                                                Utilities.MessageBoxShow("यह JPG बहुत बड़ा है...अधिकतम साइज़ 1 Mb की अनुमति है");
                                            }
                                            //Utilities.MessageBoxShow("This JPG File Is too Big...Maximum Size allowed is 1 Mb");
                                            st = false;
                                            preview();
                                        }
                                    }
                                    if (st)
                                    {
                                        if (FileUpload1.PostedFile.ContentType == "application/x-pdf" || FileUpload1.PostedFile.ContentType == "application/pdf" || FileUpload1.PostedFile.ContentType == "image/jpg" || FileUpload1.PostedFile.ContentType == "image/jpeg")
                                        {
                                            //string content_type = FileUpload1.PostedFile.ContentType.ToString();
                                            string file_ext = Path.GetExtension(FileUpload1.PostedFile.FileName).ToString();

                                            Stream fs = default(Stream);
                                            fs = FileUpload1.PostedFile.InputStream;
                                            BinaryReader br1 = new BinaryReader(fs);
                                            byte[] pdfbytes = br1.ReadBytes(FileUpload1.PostedFile.ContentLength);
                                            bl.File_Extn = file_ext;
                                            bl.File_Data = pdfbytes;
                                            bl.URL = null;
                                            bl.Filename = FileUpload1.PostedFile.FileName;
                                        }
                                        else
                                        {
                                            if (Session["language"].ToString() == "en-GB")
                                            {

                                                Utilities.MessageBoxShow("Only PDF and JPG files are allowed");
                                            }
                                            else
                                            {

                                                Utilities.MessageBoxShow(" केवल PDF और JPG फाईल की अनुमति है");
                                            }
                                           // Utilities.MessageBoxShow("Only PDF and JPG files are allowed");
                                            st = false;
                                            preview();
                                        }
                                    }
                                }
                            }
                            else if (ddlType.SelectedValue == "E")
                            {
                                n.File_id = null;
                                n.url = txtUrl.Text.Trim();
                            }
                        }
                        else if (ddlHyperlink.SelectedValue == "N")
                        {
                            bl.File_id = null;
                            bl.File_Extn = null;
                            bl.Content_type = null;
                            bl.File_Data = new byte[] { };
                            bl.URL = null;
                            bl.Filename = null;
                        }
                        if (FileUpload1.HasFiles)
                        {
                            if (st)
                            {
                                bl.Filename = FileUpload1.PostedFile.FileName;
                                bl.Client_ip = util.GetClientIpAddress(this.Page);
                                bl.User_id = Session["username"].ToString();
                                bl.Upload_Date = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");


                                rb = dl.Insert_FileDetails(bl); // inserting file details..

                                if (rb.status == true)
                                {
                                    n.file_ID = Convert.ToInt32(dl.Get_Max_ID().table.Rows[0][0].ToString());

                                }
                            }
                        }

                        if (st)
                        {
                            //new
                            if (RadioButtonListbold.SelectedValue == "Yes")
                            {
                                n.Bold = "Y";
                            }
                            else
                            {
                                n.Bold = "N";
                            }
                            if (RadioButtonListblink.SelectedValue == "Yes")
                            {
                                n.Blink = "Y";
                            }
                            else
                            {
                                n.Blink = "N";
                            }
                            n.Priority = ddlpriority.SelectedValue;
                            n.Color = color.Text.Trim();
                           // n.Image = ddlimage.SelectedItem.Text;
                            string imgPath= HiddenField1.Value.ToString();
                            string cut_imgPath = imgPath.Remove(0, 3);
                            n.Image = cut_imgPath; //HiddenField1.Value;
                            n.status = "P";

                            rb = dl.Insert_Notice(n); // inserting notice details
                        }

                        if (rb.status == true)
                        {
                            ts.Complete();
                            if (Session["language"].ToString() == "en-GB")
                            {

                                Utilities.MessageBoxShow_Redirect("Record Saved Successfully", "../admin/notice_entry.aspx");
                            }
                            else
                            {

                                Utilities.MessageBoxShow_Redirect("रिकार्ड सफलतापूर्वक सुरक्षित हुआ", "../admin/notice_entry.aspx");
                            }
                           // Utilities.MessageBoxShow_Redirect("Record Save Successfully", "../admin/notice_entry.aspx");
                            clear();

                            // BindDropDownList();
                            // BindTitles();
                        }
                    }
                }
                catch (Exception ex)
                {
                    rb.status = false;
                    if (Session["language"].ToString() == "en-GB")
                    {

                        Utilities.MessageBoxShow("Record could not be saved.");
                    }
                    else
                    {

                        Utilities.MessageBoxShow("रिकार्ड सुरक्षित नहीं हुआ |");
                    }
                  //  Utilities.MessageBoxShow("Record could not be saved.");
                    Gen_Error_Rpt.Write_Error("Notice_Entry.aspx_btnSubmit_Click", ex);
                }
            }
        }
    }

    public void clear()
    {
        txtHeader.Text = null;
        // ddlPermanent.SelectedIndex = -1;
        ddlPermanent.SelectedValue = "N";
        txtPublishDate.Text = null;
        txtDateFrom.Text = null;
        txtDateTo.Text = null;
        ddlHyperlink.SelectedIndex = 0;
        ddlType.SelectedIndex = 0;
        ddlpriority.SelectedIndex = 0;
        FileUpload1.Attributes.Clear();
        txtUrl.Text = null;

        RadioButtonListbold.SelectedValue = "";
        RadioButtonListblink.SelectedValue = "";
        ddlpriority.SelectedValue = "select";
        color.Text = "";
        ddlimage.SelectedValue = "0";
    }
    //protected void BindTitles()
    //{
    //    if (ddlimage != null)
    //    {
    //        foreach (ListItem li in ddlimage.Items)
    //        {
    //            li.Attributes["title"] = li.Value; // setting text value of item as tooltip

    //        }
    //    }
    //}
    //protected void BindDropDownList()
    //{
    //    Upload_doc bl = new Upload_doc();
    //    Notice_Entry n = new Notice_Entry();
    //    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    //    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();

    //    dt = dl.bindimage();
    //    try
    //    {
    //        dt = dl.bindimage();
    //        ddlimage.DataSource = dt.table;

    //        ddlimage.DataTextField = "value";
    //        ddlimage.DataValueField = "name";

    //        ddlimage.DataBind();
    //        ddlimage.Items.Insert(0, "");
    //        //ddlimage.Items.Insert(0, "Select");

    //    }
    //    catch { }
    //}

    protected void BindTitles()
    {
        if (ddlimage != null)
        {
            foreach (ListItem li in ddlimage.Items)
            {
                //li.Attributes["title"] = li.Value; // setting text value of item as tooltip
                if (li.Text == "Select")
                { }
                else
                    li.Attributes["title"] = "../images/" + li.Text;

            }
        }
    }
    protected void BindDropDownList()
    {
        Upload_doc bl = new Upload_doc();
        Notice_Entry n = new Notice_Entry();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();

        dt = dl.bindimage();
        try
        {
            dt = dl.bindimage();
            ddlimage.DataSource = dt.table;

            ddlimage.DataTextField = "name";
            ddlimage.DataValueField = "value";

            ddlimage.DataBind();
            ddlimage.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch { }
    }
    protected void ddlimage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTitles();
       // Image1.Attributes.Add("class", "image1size");
        //Image1.Style.Add("width", "30px");
    }
    //protected void Btnpreview_Click(object sender, EventArgs e)
    //{
    //    Boolean st = true;
    //    ReturnClass.ReturnDataTable dtt = new ReturnClass.ReturnDataTable();
    //    dl_dmf_designation dl = new dl_dmf_designation();
    //    Notice_Entry ne = new Notice_Entry();
    //    Upload_doc bl = new Upload_doc();
    //    try
    //    {
    //        ne.bold = RadioButtonListbold.SelectedValue;
    //        ne.blink = RadioButtonListblink.SelectedValue;
    //        string blinking = "blinking";
    //        string qr = "<div class='col-sm-1 blinking'>" + "<img src=" + ddlimage.SelectedValue + "></div>";
    //        string qr1 = "<div class='col-sm-1><img src=" + ddlimage.SelectedValue + "></div>";

    //        StringBuilder lo = new StringBuilder();
    //        if (ne.bold == "Yes" || color.Text != "")
    //        {
    //            if (ddlimage.SelectedValue != "")
    //            {
    //                if (ne.blink == "Yes")
    //                {
    //                    //lo.AppendLine(qr + "<b>" + "<font color=" + color.Text + ">" + txtHeader.Text + "</font>" + "</b></div>");
    //                    lo.AppendLine(qr + "<div class='col-sm-11'><font color=" + color.Text + ">" + txtHeader.Text + "</font></div>");
    //                }
    //                else
    //                {

    //                    lo.AppendLine(qr1 + "<div class='col-sm-11'><b>" + "<font color=" + color.Text + ">" + txtHeader.Text + "</font>" + "</b></div>");

    //                }
    //            }
    //            else
    //            {
    //                lo.AppendLine("<div class='col-sm-11'><b>" + "<font color=" + color.Text + ">" + txtHeader.Text + "</font>" + "</b></div>");
    //            }
    //        }
    //        else if (ddlimage.SelectedValue != "")
    //        {
    //            if (ne.blink == "Yes")
    //            {
    //                lo.AppendLine(qr + "<div class='col-sm-11'><font color=" + color.Text + ">" + txtHeader.Text + "</font></div>");
    //            }
    //            else
    //            {
    //                lo.AppendLine(qr1 + "<div class='col-sm-11'><font color=" + color.Text + ">" + txtHeader.Text + "</font></div>");
    //            }

    //        }
    //        else
    //        {
    //            lo.AppendLine(txtHeader.Text);
    //        }


    //        literal.Text = lo.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        Gen_Error_Rpt.Write_Error("admin_NewsEdit_List_bind_grid()", ex);
    //        Utilities.MessageBoxShow(ex.Message);
    //    }
    //    //-----------------------for temprary save data----------------------------------
    //      ViewState["Hyperlink"] = ddlHyperlink.SelectedValue;
    //      ViewState["Permanent"] = ddlPermanent.SelectedValue;
    //      if (txtPublishDate.Text != "")
    //      {
    //          ViewState["Publish_Date"] = Convert.ToDateTime(txtPublishDate.Text.Trim()).ToString("yyyy/MM/dd HH:mm:ss");
    //      }
    //      if (txtDateFrom.Text != "")
    //      {
    //          ViewState["Validity_From"] = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd HH:mm:ss");

    //      }
    //      if (txtDateTo.Text != "")
    //      {
    //          ViewState["Validity_To"] = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd HH:mm:ss");

    //      }
    //      if (ddlType.SelectedValue != "")
    //      {
    //          ViewState["file_type"] = ddlType.SelectedValue;
    //      }
    //      if (txtUrl.Text != "")
    //      {
    //          ViewState["Url"] = txtUrl.Text.Trim();
    //      }
    //      show();
    //}

    public string blinking { get; set; }

    public object sm { get; set; }

    public string col { get; set; }

}