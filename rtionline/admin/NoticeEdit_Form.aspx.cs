//ekta kushwah
//nov-2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Transactions;
using System.Text;
using System.Globalization;
using System.Threading;
public partial class usr_NoticeEdit_Form : BasePage //System.Web.UI.Page
{
    string LastFileId = null;
    string fd = null, nb = null;
    //Int32 maxsize = 4489100;
    Int32 maxSizePdf = 10485760;
    Int32 maxSizeJpg = 1048576;
    Utilities util = new Utilities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 

            if (!Page.IsPostBack)
            {
                CalendarExtender1.StartDate = DateTime.Now;
                CalendarExtender2.StartDate = DateTime.Now;
                CalendarExtender3.StartDate = DateTime.Now;
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                int fid = Convert.ToInt32(Request.QueryString["fid"].ToString());
                if (fid != 0)
                {
                    LinkButton2.Visible = true;
                }
                else if (fid ==0)
                {
                    LinkButton2.Visible = false;
                }

                bind_district();
                BaseDepartmentBind();
                OfficeBind();


               // string nid = Request.QueryString["nid"].ToString();
                show();
                BindDropDownList();
                BindTitles();


            }
        }
        catch { }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    protected override void InitializeCulture()
    {
        Culture = "en-GB";
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

        base.InitializeCulture();
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
   
    public void show()
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        dl_rti_notice dl = new dl_rti_notice();
        Notice_Entry ba = new Notice_Entry();
        ba.NoticeID = Request.QueryString["nid"].ToString();
        try
        {
            dt = dl.LoadNotice(ba);

            if (dt.table.Rows.Count > 0)
            {
                ddl_department.SelectedValue = dt.table.Rows[0]["base_dept_id"].ToString();
                ddl_district.SelectedValue = dt.table.Rows[0]["district_id"].ToString();
                OfficeBind();
                ddl_office.SelectedValue = dt.table.Rows[0]["office_id"].ToString();
                txtHeader.Text = dt.table.Rows[0]["header"].ToString();
                //lblpreview.Text = txtHeader.Text;
                //Image1.ImageUrl = dt.table.Rows[0]["image"].ToString();

                if (dt.table.Rows[0]["permanent"].ToString() == "Yes")
                {
                    ddlPermanent.SelectedValue = "Y";
                    //divPublishDate.Visible = true;
                    // txtPublishDate.Text = Convert.ToDateTime(dt.table.Rows[0]["Publish_Date"]).ToString("dd/MM/yyyy HH:mm:ss");
                    txtPublishDate.Text = Convert.ToString(dt.table.Rows[0]["Publish_Date"].ToString());
                    RequiredFieldValidator9.Enabled = false;
                    RequiredFieldValidator9.ValidationGroup = "x";
                    RegularExpressionValidator10.Enabled = false;
                    RegularExpressionValidator10.ValidationGroup = "x";
                    RequiredFieldValidator10.Enabled = false;
                    RequiredFieldValidator10.ValidationGroup = "x";
                    RegularExpressionValidator17.Enabled = false;
                    RegularExpressionValidator17.ValidationGroup = "x";

                }
                else if (dt.table.Rows[0]["permanent"].ToString() == "No")
                {
                    ddlPermanent.SelectedValue = "N";
                    // divPublishDate.Visible = false;
                    // divFrom.Visible = true;
                    // divTo.Visible = true;
                    txtDateFrom.Text = Convert.ToString(dt.table.Rows[0]["Validity_From"].ToString());
                    txtDateTo.Text = Convert.ToString(dt.table.Rows[0]["Validity_To"].ToString());
                    RequiredFieldValidator7.Enabled = false;
                    RequiredFieldValidator7.ValidationGroup = "x";
                    RegularExpressionValidator1.Enabled = false;
                    RegularExpressionValidator1.ValidationGroup = "x";
                }
                if (dt.table.Rows[0]["active"].ToString() == "Active")
                {
                    ddlActive.SelectedValue = "A";
                }
                else if (dt.table.Rows[0]["active"].ToString() == "Inactive")
                {
                    ddlActive.SelectedValue = "D";
                }
                if (dt.table.Rows[0]["hyperlink"].ToString() == "Yes")
                {
                    ddlHyperlink.SelectedValue = "Y";
                    RequiredFieldValidator3.Enabled = false;
                    RequiredFieldValidator3.ValidationGroup = "x";
                    //divType.Visible = true;
                }
                else if (dt.table.Rows[0]["hyperlink"].ToString() == "No")
                {
                    ddlHyperlink.SelectedValue = "N";
                    RequiredFieldValidator3.Enabled = false;
                    RequiredFieldValidator3.ValidationGroup = "x";
                    RequiredFieldValidator6.Enabled = false;
                    RequiredFieldValidator6.ValidationGroup = "x";
                    RequiredFieldValidator4.Enabled = false;
                    RequiredFieldValidator4.ValidationGroup = "x";
                    regUrl.ValidationGroup = "x";
                    regUrl.Enabled = false;
                }
                else {            
                    RequiredFieldValidator3.Enabled = true;
                    RequiredFieldValidator3.ValidationGroup = "a";
                }
                if (dt.table.Rows[0]["file_type"].ToString() == "Internal")
                {
                    ddlType.SelectedValue = "I";
                    //divFileUpload.Visible = true;
                    RequiredFieldValidator6.Enabled = true;
                    RequiredFieldValidator6.ValidationGroup = "a";
                    RequiredFieldValidator4.Enabled = false;
                    RequiredFieldValidator4.ValidationGroup = "x";
                    regUrl.ValidationGroup = "x";
                    regUrl.Enabled = false;
                }
                else if (dt.table.Rows[0]["file_type"].ToString() == "External")
                {
                    ddlType.SelectedValue = "E";
                    //divUrl.Visible = true;
                    txtUrl.Text = dt.table.Rows[0]["url"].ToString();
                    RequiredFieldValidator6.Enabled = false;
                    RequiredFieldValidator6.ValidationGroup = "x";
                        

                    
                }
                if (dt.table.Rows[0]["bold"].ToString() == "Y")
                {
                    RadioButtonListbold.SelectedValue = "Yes";

                }
                else if (dt.table.Rows[0]["bold"].ToString() == "N")
                {
                    RadioButtonListbold.SelectedValue = "No";

                }
                if (dt.table.Rows[0]["blink"].ToString() == "Y")
                {
                    RadioButtonListblink.SelectedValue = "Yes";

                }
                else if (dt.table.Rows[0]["blink"].ToString() == "N")
                {
                    RadioButtonListblink.SelectedValue = "No";

                }
                if (dt.table.Rows[0]["priority"].ToString() == "H")
                {
                    ddlpriority.SelectedValue = "H";

                }
                else if (dt.table.Rows[0]["priority"].ToString() == "M")
                {
                    ddlpriority.SelectedValue = "M";
                }
                else if (dt.table.Rows[0]["priority"].ToString() == "L")
                {
                    ddlpriority.SelectedValue = "L";
                }
                color.Text = dt.table.Rows[0]["color"].ToString();
               // ColorPicker1.SelectedColor = color.Text;
               
            }
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("admin_NewsEdit_Form.aspx_show()", ex);
        }

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
        if (bold == "B")
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

     
    public void clear()
    {
        txtHeader.Text = null;
        ddlPermanent.SelectedIndex = -1;
        txtDateFrom.Text = null;
        txtDateTo.Text = null;
        ddlActive.SelectedIndex = -1;
        ddlHyperlink.SelectedIndex = -1;
        ddlType.SelectedIndex = -1;
        FileUpload1.Attributes.Clear();
        txtUrl.Text = null;
        
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Notice_Entry ne = new Notice_Entry();
        Upload_doc ud = new Upload_doc();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        dl_rti_notice dl = new dl_rti_notice();
      

        ud.Status = "N";

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

            if (Page.IsValid)
            {
                Boolean st = true;
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        ne.District_id = ddl_district.SelectedValue;
                        ne.Base_dept_id = ddl_department.SelectedValue;
                        ne.Office_id = ddl_office.SelectedValue;
                        ne.NoticeID = Request.QueryString["nid"].ToString();
                        ne.Header = txtHeader.Text;
                        ne.Permanent = ddlPermanent.SelectedValue;
                        ne.Active = ddlActive.SelectedValue;
                        ne.Hyperlink = ddlHyperlink.SelectedValue.ToString();
                        ne.Client_ip = util.GetClientIpAddress(this.Page);
                        ne.User_id = Session["username"].ToString();
                        ne.Datetime = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");

                        if (ddlPermanent.SelectedValue == "N")
                        {
                            ne.Publish_date = null;
                            //string datefrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd HH:mm:ss");
                            //string dateto = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd HH:mm:ss");
                            //ne.Datefrom = datefrom.ToString();
                            //ne.Dateto = dateto.ToString();

                            DateTime datefrom = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            ne.Datefrom = datefrom.ToString("yyyy/MM/dd HH:mm:ss");
                            DateTime dateto = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            ne.Dateto = dateto.ToString("yyyy/MM/dd HH:mm:ss");


                        }
                        else if (ddlPermanent.SelectedValue == "Y")
                        {

                            DateTime Publish_date = DateTime.ParseExact(txtPublishDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            ne.Publish_date = Publish_date.ToString("yyyy/MM/dd HH:mm:ss");
                            //ne.Publish_date = Convert.ToDateTime(txtPublishDate.Text.Trim()).ToString("yyyy/MM/dd HH:mm:ss");  
                            ne.Datefrom = null;
                            ne.Dateto = null;
                        }
                        if (ddlHyperlink.SelectedValue == "Y")
                        {
                            ne.Filetype = ddlType.SelectedValue;
                            if (ddlType.SelectedValue == "I")
                            {
                                ne.Url = null;

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
                                           // Utilities.MessageBoxShow("This PDF File Is too Big...Maximum Size allowed is 10 Mb");
                                            st = false;
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
                                           // Utilities.MessageBoxShow("This JPG File Is too Big...Maximum Size allowed is 1 Mb");
                                            st = false;
                                        }
                                    }
                                    if (st)
                                    {
                                        if (FileUpload1.PostedFile.ContentType == "application/x-pdf" || FileUpload1.PostedFile.ContentType == "application/pdf" || FileUpload1.PostedFile.ContentType == "image/jpg" || FileUpload1.PostedFile.ContentType == "image/jpeg")
                                        {
                                            string file_ext = Path.GetExtension(FileUpload1.PostedFile.FileName).ToString();

                                            Stream fs = default(Stream);
                                            fs = FileUpload1.PostedFile.InputStream;
                                            BinaryReader br1 = new BinaryReader(fs);
                                            byte[] pdfbytes = br1.ReadBytes(FileUpload1.PostedFile.ContentLength);
                                            ud.File_Extn = file_ext;
                                            ud.File_Data = pdfbytes;
                                            ud.Filename = FileUpload1.PostedFile.FileName;
                                            ud.Client_ip = util.GetClientIpAddress(this.Page);
                                            ud.Upload_Date = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                                            ud.User_id = Session["username"].ToString();
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
                                            //Utilities.MessageBoxShow("Only PDF and JPG files are allowed");
                                            st = false;
                                        }
                                    }
                                }
                            }
                            else if (ddlType.SelectedValue == "E")
                            {
                                ne.file_id = null;
                                ne.Url = txtUrl.Text.Trim();

                                nb = ",File_Id=@file_id";
                                int fid = Convert.ToInt32(Request.QueryString["fid"].ToString());
                                if (fid!=0)
                                {
                                    ud.file_id = Request.QueryString["fid"].ToString();
                                    rb = dl.update_file_details(ud);
                                }
                            }
                        }
                        else if (ddlHyperlink.SelectedValue == "N")
                        {
                            ud.file_id = null;
                            ud.File_Extn = null;
                            ud.Content_type = null;
                            ud.File_Data = new byte[] { };
                            ud.URL = null;
                           // nb = ",File_Id=@File_id";
                           // nb = ",File_Id=@file_id";
                            int fid = Convert.ToInt32(Request.QueryString["fid"].ToString());
                            if (fid !=0)
                            {
                                ud.file_ID = Convert.ToInt32(Request.QueryString["fid"].ToString());
                                rb = dl.update_file_details(ud);
                            }
                        }

                        if (FileUpload1.HasFile)
                        {
                            if (st)
                            {
                                int fid = Convert.ToInt32(Request.QueryString["fid"].ToString());
                                if (fid==0)
                                {
                                    //if (rb.status == true)
                                    //{
                                    ud.Filename = FileUpload1.PostedFile.FileName;
                                    ud.Client_ip = util.GetClientIpAddress(this.Page);
                                    ud.User_id = Session["username"].ToString();
                                    ud.Upload_Date = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                                        rb = dl.Insert_FileDetails(ud);
                                   // }

                                    if (rb.status == true)
                                    {
                                        ne.file_ID = Convert.ToInt32(dl.Get_Max_ID().table.Rows[0][0].ToString());
                                        //nb = ",File_Id=@file_ID";
                                    }
                                }
                                    
                                else if (fid!=0)
                                {
                                    ud.file_id = Request.QueryString["fid"].ToString();
                                    ud.Status = "Y";
                                    rb = dl.update_file_details(ud);
                                }
                            }
                        }

                        if (st)
                        {
                            if (RadioButtonListbold.SelectedValue == "Yes")
                            {
                                ne.Bold = "Y";
                            }
                            else
                            {
                                ne.Bold = "N";
                            }
                            if (RadioButtonListblink.SelectedValue == "Yes")
                            {
                                ne.Blink = "Y";
                            }
                            else
                            {
                                ne.Blink = "N";
                            }
                            ne.Priority = ddlpriority.SelectedValue;
                            ne.Color = color.Text.Trim();

                            if (ddlimage.SelectedValue == "")
                            {
                                ne.Image = Image1.ImageUrl;
                            }
                            else
                            {
                                //ne.Image = ddlimage.SelectedValue;
                                ne.Image = ddlimage.SelectedItem.Text;
                            }

                            if (ne.Image.StartsWith(".."))
                            {
                                string imgPath = ne.Image;
                                string cut_imgPath = imgPath.Remove(0, 3);
                                ne.Image = cut_imgPath;
                            }

                            rb = dl.update_notice(ne);
                           // rb = dl.update_notice(ne, nb);
                        }

                        if (rb.status == true)
                        {
                            ts.Complete();
                            clear();
                            if (Session["language"].ToString() == "en-GB")
                            {

                                Utilities.MessageBoxShow_Redirect("Notice Updated successfully", "NoticeEdit_List.aspx");
                            }
                            else
                            {

                                Utilities.MessageBoxShow_Redirect("रिकार्ड सफलतापूर्वक सुरक्षित हुआ", "NoticeEdit_List.aspx");
                            }
                           // Utilities.MessageBoxShow_Redirect("Notice Updated successfully", "NoticeEdit_List.aspx");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Session["language"].ToString() == "en-GB")
                    {

                        Utilities.MessageBoxShow("Record could not be updated.");
                    }
                    else
                    {

                        Utilities.MessageBoxShow("रिकार्ड सुरक्षित नहीं हुआ |");
                    }
                   // Utilities.MessageBoxShow("Record could not be updated.");
                    Gen_Error_Rpt.Write_Error("admin_NewsEdit_Form.aspx_btnUpdate_Click", ex);
                }
            }
        }


    }

    protected void Page_Init(object sender, EventArgs e)
    {
        
        //bind_district();
        //BaseDepartmentBind();
        //OfficeBind();

        //show();

    } 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
           
            //if (Session["Role_ID"].ToString() == "01")
            //  this.MasterPageFile = "../MasterPage_Admin.master";
        }
        catch { }
    }
    protected void BindDropDownList()
    {
        //Upload_doc bl = new Upload_doc();
       // Notice_Entry n = new Notice_Entry();
        dl_rti_notice dl = new dl_rti_notice();
       // ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();

       // dt = dl.bindimage();
        try
        {

            dt = dl.bindimage();
            ddlimage.DataSource = dt.table;

            ddlimage.DataTextField = "name";
            ddlimage.DataValueField = "value";

            ddlimage.DataBind();
           // ddlimage.Items.Insert(0, "");
           ddl_district.Items.Insert(0, "Select");

        }
        catch { }
    }
    protected void ddlimage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTitles();
    }
    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string Ecode;
         Notice_Entry ne = new Notice_Entry();
        Upload_doc ud = new Upload_doc();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        dl_rti_notice dl = new dl_rti_notice();

        Ecode = Request.QueryString["fid"].ToString();
        string lol = "notice_download.aspx?fid=" + Ecode;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open(" + "'" + lol + "','_blank');", true);
    }


    public string blinking { get; set; }

    public object sm { get; set; }

    public string col { get; set; }

}