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
using System.Transactions;
public partial class action : BasePage // System.Web.UI.Page
{
    bl_employee_action bl1 = new bl_employee_action();
    dl_employee_action dl1 = new dl_employee_action();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    Utilities ul = new Utilities();
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
    protected void Page_Load(object sender, EventArgs e)
    {
        comval1.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
        comp_date.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
        calex_from.EndDate = DateTime.Now;
        if (Session["username"] == null)
        {
            Response.Redirect("../logout.aspx");
        }
        else
        {
            bl_rti_emp bl = new bl_rti_emp();
            dl_rti_emp dl = new dl_rti_emp();
            ReturnClass.ReturnDataTable rt2 = new ReturnClass.ReturnDataTable();
            bl.User_id = Session["username"].ToString();
            bl.Office_map_id = Session["EmpMapID"].ToString();
            rt2 = dl.Get_EmpDesName(bl);
            if (rt2.table.Rows.Count > 0)
            {
                lbl_UserName.Text = rt2.table.Rows[0]["Name_en"].ToString().ToUpper();
            }
            if (!Page.IsPostBack)
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                bind_grd_Action();
                CalendarExtender2.StartDate = DateTime.Now;

                string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
                Utilities ut = new Utilities();
                string sas = Server.UrlDecode(Request.QueryString["rtiid"].ToString());
                sas = sas.Replace(" ", "+");
                string decrypt_query_string = ut.Decrypt_AES(sas, key);
                bl1.Rti_id = decrypt_query_string;
                sas = Server.UrlDecode(Request.QueryString["empid"].ToString());
                sas = sas.Replace(" ", "+");
                decrypt_query_string = ut.Decrypt_AES(sas, key);
                bl1.Office_mapping_id = Session["EmpMapID"].ToString();
                //bl1.Rti_id = Request.QueryString["rtiid"];
                //bl1.Office_mapping_id = Request.QueryString["emp_ofc_map_id"];

                rd = dl1.applicantDetail(bl1);
                if (rd.table.Rows.Count > 0)
                {
                    txt_applicationNo.Text = rd.table.Rows[0]["rti_id"].ToString();
                    txt_mobileNo.Text = rd.table.Rows[0]["Mobile_No"].ToString();
                    txt_applicantName.Text = rd.table.Rows[0]["Applicant_Name_en"].ToString();
                    txt_applicantAddress.Text = rd.table.Rows[0]["Address"].ToString();
                    txt_subject.Text = rd.table.Rows[0]["rti_Subject"].ToString();
                    txt_date.Text = rd.table.Rows[0]["rti_DateTime"].ToString();
                    txt_application_status.Text = rd.table.Rows[0]["DisplayName_en"].ToString();
                    //txt_dept.Text = rd.table.Rows[0]["dept_name"].ToString();
                    //txt_ofc.Text = rd.table.Rows[0]["OfficeName"].ToString();
                    string t = rd.table.Rows[0]["degid"].ToString();
                    if (t == "1" || t == "2")
                    {
                        div_meeting.Visible = true;
                    }
                    else
                    {
                        div_meeting.Visible = false;
                    }
                }

                bind_Status();
                div_forward.Visible = false;
                bind_district_code();
                bind_department_id();
                ddl_status.SelectedValue = "0";
                // If there is an rti Data then show it
                rd = dl1.GetRTIDetails(bl1);
                if (rd.table.Rows.Count > 0)
                {
                    h_IsRtiData.Value = "Y";
                    //ddl_status.SelectedValue = "DIS";
                    div_dispose.Visible = true;

                    rfv_txt_amount.Enabled = true;
                    txt_result.Text = rd.table.Rows[0]["result_description"].ToString();
                    string filename = rd.table.Rows[0]["FileName"].ToString();
                    if (filename != null && filename != "")
                    {
                        ddl_rti_file.SelectedValue = "Y";
                        div_file_rti.Visible = true;
                        lnk_file_rti.Visible = true;
                        txt_desc_rti.Text = rd.table.Rows[0]["FileDescription"].ToString(); 
                    }
                    else
                    {
                        lnk_file_rti.Visible = false;
                    }
                    string ismeeting = rd.table.Rows[0]["IsMeetingFix"].ToString();
                    if (ismeeting == "Y")
                    {
                        div_meeting.Visible = true;
                        RadioMeeting.SelectedValue = "Y";
                        div_meet_date.Visible = true;
                        txt_meeting.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        div_meeting.Visible = true;
                        RadioMeeting.SelectedValue = "N";
                    }
                    string isadditionalfees = rd.table.Rows[0]["IsAdditionalFees"].ToString();
                    if (isadditionalfees == "Y")
                    {
                        div_additional_fees.Visible = true;
                        rbl_additional_fees_rti.SelectedValue = "Y";
                        txt_amount.Text = rd.table.Rows[0]["FeesAmount"].ToString();
                    }
                    else
                    {

                        rbl_additional_fees_rti.SelectedValue = "N";
                    }


                } // End of if count

            }
        }
    }
    //public void bind_inside_ofc()
    //{
    //    bl1.Office_mapping_id = Request.QueryString["emp_ofc_map_id"];
    //    rd = dl1.bind_withinoffice(bl1);
    //    ddl_with_ofc.Items.Clear();
    //    ddl_with_ofc.DataSource = rd.table;
    //    ddl_with_ofc.DataTextField = "name";
    //    ddl_with_ofc.DataValueField = "mapid";
    //    ddl_with_ofc.Items.Add(new ListItem("select", "0"));
    //    ddl_with_ofc.DataBind();
    //}
    public void bind_Status()
    {
        bl1.Office_mapping_id = Session["EmpMapID"].ToString();
        rd = dl1.get_employee_permissions(bl1);

        ddl_status.Items.Clear();
        ddl_status.DataSource = rd.table;
        ddl_status.DataTextField = "name";
        ddl_status.DataValueField = "value";
        ddl_status.Items.Add(new ListItem("--------Select-------", "0"));
        ddl_status.DataBind();
    }
    protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_status.SelectedValue == "FOR")
        {
            div_forward.Visible = true;
            div_dispose.Visible = false;
            div_reject.Visible = false;
            // div_review.Visible = false;
            forward_auth.Visible = false;
            //div_radio_forward.Visible = true;  //Forward only outside office
            //RadioArea.SelectedValue = "WO";  //Forward only outside office
            //inside_ofc.Visible = false; //Forward only outside office
            out_office.Visible = true;
            // bind_inside_ofc();             //Forward only outside office
            rfv_ddl_forward_auth.Enabled = false;
            //forward
            // rfv_RadioArea.Enabled = false;  //Forward only outside office
            // rfv_with_ofc.Enabled = false;//Forward only outside office
            rfv_dept.Enabled = false;
            rfv_district.Enabled = false;
            rfv_officeLevel.Enabled = false;
            rfv_ofc_cat.Enabled = false;
            rfv_office.Enabled = false;
            rfv_ddl_designation.Enabled = false;
            rfv_ddl_officer.Enabled = false;
            // reject
            rfv_ddl_reject.Enabled = false;
            //rfv_txt_reject.Enabled = false;
            //review
            // rfv_ddl_review.Enabled = false;
            // rfv_txt_review.Enabled = false;
            //dispose
            rfv_txt_result.Enabled = false;
            rfv_rti_file.Enabled = false;
            rfv_txt_desc_rti.Enabled = false;
            rfv_fileuploadrti.Enabled = false;
            rfv_meeting.Enabled = false;
            rfv_rbl_additionalfee.Enabled = false;
            rfv_radiomeeting.Enabled = false;
            rfv_txt_amount.Enabled = false;
        }
        //else if (ddl_status.SelectedValue == "APR")
        //{
        //    div_dispose.Visible = false;
        //    div_forward.Visible = false;
        //    div_meeting.Visible = false;
        //   // div_review.Visible = false;
        //    div_reject.Visible = false;
        //    forward_auth.Visible = false;
        //    rfv_ddl_forward_auth.Enabled = false;
        //    //forward
        //    rfv_RadioArea.Enabled = false;
        //    rfv_with_ofc.Enabled = false;
        //    rfv_dept.Enabled = false;
        //    rfv_district.Enabled = false;
        //    rfv_officeLevel.Enabled = false;
        //    rfv_ofc_cat.Enabled = false;
        //    rfv_office.Enabled = false;
        //    rfv_ddl_designation.Enabled = false;
        //    rfv_ddl_officer.Enabled = false;
        //    // reject
        //    rfv_ddl_reject.Enabled = false;
        //    //rfv_txt_reject.Enabled = false;
        //    //review
        //   // rfv_ddl_review.Enabled = false;
        //    // rfv_txt_review.Enabled = false;
        //    //dispose
        //    rfv_txt_result.Enabled = false;
        //    rfv_rti_file.Enabled = false;
        //    rfv_txt_desc_rti.Enabled = false;
        //    rfv_fileuploadrti.Enabled = false;
        //    rfv_meeting.Enabled = false;
        //    rfv_rbl_additionalfee.Enabled = false;
        //    rfv_radiomeeting.Enabled = false;
        //    rfv_txt_amount.Enabled = false;
        //}
        else if (ddl_status.SelectedValue == "NAPR" ||  ddl_status.SelectedValue == "NREV")
        {
            div_dispose.Visible = false;
            div_forward.Visible = false;
            div_meeting.Visible = true;
            // div_review.Visible = false;
            div_reject.Visible = false;
            forward_auth.Visible = true;
            rfv_ddl_forward_auth.Enabled = true;
            //forward
            // rfv_RadioArea.Enabled = false;   //Forward only outside office
            //rfv_with_ofc.Enabled = false;//Forward only outside office
            rfv_dept.Enabled = false;
            rfv_district.Enabled = false;
            rfv_officeLevel.Enabled = false;
            rfv_ofc_cat.Enabled = false;
            rfv_office.Enabled = false;
            rfv_ddl_designation.Enabled = false;
            rfv_ddl_officer.Enabled = false;
            // reject
            rfv_ddl_reject.Enabled = false;
            //rfv_txt_reject.Enabled = false;
            //review
            // rfv_ddl_review.Enabled = false;
            //rfv_txt_review.Enabled = false;
            //dispose
            rfv_txt_result.Enabled = false;
            rfv_rti_file.Enabled = false;
            rfv_txt_desc_rti.Enabled = false;
            rfv_fileuploadrti.Enabled = false;
            rfv_meeting.Enabled = false;
            rfv_rbl_additionalfee.Enabled = false;
            rfv_radiomeeting.Enabled = false;
            rfv_txt_amount.Enabled = false;
        }
        else if (ddl_status.SelectedValue == "NDIS") {
            div_forward.Visible = false;
            div_dispose.Visible = true;
            div_reject.Visible = false;
            // div_review.Visible = false;
            div_meeting.Visible = true;         // meeting
            forward_auth.Visible = true;
            rfv_ddl_forward_auth.Enabled = false;
            //forward
            // rfv_RadioArea.Enabled = false;//Forward only outside office
            // rfv_with_ofc.Enabled = false;  //Forward only outside office
            rfv_dept.Enabled = false;
            rfv_district.Enabled = false;
            rfv_officeLevel.Enabled = false;
            rfv_ofc_cat.Enabled = false;
            rfv_office.Enabled = false;
            rfv_ddl_designation.Enabled = false;
            rfv_ddl_officer.Enabled = false;
            // reject
            rfv_ddl_reject.Enabled = false;
            // rfv_txt_reject.Enabled = false;
            //review
            // rfv_ddl_review.Enabled = false;
            // rfv_txt_review.Enabled = false;
            //dispose
            rfv_txt_result.Enabled = true;
            rfv_rti_file.Enabled = true;
            rfv_txt_desc_rti.Enabled = true;
            rfv_fileuploadrti.Enabled = true;
            rfv_meeting.Enabled = true;
            rfv_rbl_additionalfee.Enabled = true;
            rfv_radiomeeting.Enabled = false;
            rfv_txt_amount.Enabled = true;
        
        }
        
        else if (ddl_status.SelectedValue == "REJ")         //Rejected
        {
            // Sendto ddl
            forward_auth.Visible = false;             // Rejected No send
            rfv_ddl_forward_auth.Enabled = false;  // Rejected no send
            //forward attributes
            div_forward.Visible = false;
            rfv_dept.Enabled = false;
            rfv_district.Enabled = false;
            rfv_officeLevel.Enabled = false;
            rfv_ofc_cat.Enabled = false;
            rfv_office.Enabled = false;
            rfv_ddl_designation.Enabled = false;
            rfv_ddl_officer.Enabled = false;

            //Reject Reason
            div_reject.Visible = true;
            rfv_ddl_reject.Enabled = true;

            div_dispose.Visible = false;
            rfv_txt_result.Enabled = false;
            rfv_rti_file.Enabled = false;
            rfv_fileuploadrti.Enabled = false;
            rfv_txt_desc_rti.Enabled = false;
            rfv_radiomeeting.Enabled = false;
            rfv_meeting.Enabled = false;
            comval1.Enabled = false;
            rfv_rbl_additionalfee.Enabled = false;
            rfv_txt_amount.Enabled = false;
            rev_txt_amount.Enabled = false;

            ddl_reject_Bind();
        }
        else if (ddl_status.SelectedValue == "NREJ")
        {
            div_forward.Visible = false;
            div_dispose.Visible = false;
            div_reject.Visible = true;
            // div_review.Visible = false;
            forward_auth.Visible = true;
            rfv_ddl_forward_auth.Enabled = true;
            //forward
            //rfv_RadioArea.Enabled = false; //Forward only outside office
            // rfv_with_ofc.Enabled = false;  //Forward only outside office
            rfv_dept.Enabled = false;
            rfv_district.Enabled = false;
            rfv_officeLevel.Enabled = false;
            rfv_ofc_cat.Enabled = false;
            rfv_office.Enabled = false;
            rfv_ddl_designation.Enabled = false;
            rfv_ddl_officer.Enabled = false;
            // reject
            rfv_ddl_reject.Enabled = true;
            //rfv_txt_reject.Enabled = true;
            //review
            // rfv_ddl_review.Enabled = false;
            //rfv_txt_review.Enabled = false;
            //dispose
            rfv_txt_result.Enabled = false;
            rfv_rti_file.Enabled = false;
            rfv_txt_desc_rti.Enabled = false;
            rfv_fileuploadrti.Enabled = false;
            rfv_meeting.Enabled = false;
            rfv_rbl_additionalfee.Enabled = false;
            rfv_radiomeeting.Enabled = false;
            rfv_txt_amount.Enabled = false;
            ddl_reject_Bind();
        }
        else if (ddl_status.SelectedValue == "DIS")
        {
            div_forward.Visible = false;
            div_dispose.Visible = true;
            div_reject.Visible = false;
            // div_review.Visible = false;
            div_meeting.Visible = true;         // meeting
            forward_auth.Visible = false;
            rfv_ddl_forward_auth.Enabled = false;
            //forward
            // rfv_RadioArea.Enabled = false;//Forward only outside office
            // rfv_with_ofc.Enabled = false;  //Forward only outside office
            rfv_dept.Enabled = false;
            rfv_district.Enabled = false;
            rfv_officeLevel.Enabled = false;
            rfv_ofc_cat.Enabled = false;
            rfv_office.Enabled = false;
            rfv_ddl_designation.Enabled = false;
            rfv_ddl_officer.Enabled = false;
            // reject
            rfv_ddl_reject.Enabled = false;
            // rfv_txt_reject.Enabled = false;
            //review
            // rfv_ddl_review.Enabled = false;
            // rfv_txt_review.Enabled = false;
            //dispose
            rfv_txt_result.Enabled = true;
            rfv_rti_file.Enabled = true;
            rfv_txt_desc_rti.Enabled = true;
            rfv_fileuploadrti.Enabled = true;
            rfv_meeting.Enabled = true;
            rfv_rbl_additionalfee.Enabled = true;
            rfv_radiomeeting.Enabled = false;
            rfv_txt_amount.Enabled = true;
        }
        //else if (ddl_status.SelectedValue == "NDIS")
        //{
        //    div_dispose.Visible = true;
        //    div_forward.Visible = false;
        //    div_reject.Visible = false;
        //   // div_review.Visible = false;
        //    forward_auth.Visible = true;
        //    div_meeting.Visible = true;
        //    rfv_ddl_forward_auth.Enabled = true;
        //    //forward
        //    rfv_RadioArea.Enabled = false;
        //    rfv_with_ofc.Enabled = false;
        //    rfv_dept.Enabled = false;
        //    rfv_district.Enabled = false;
        //    rfv_officeLevel.Enabled = false;
        //    rfv_ofc_cat.Enabled = false;
        //    rfv_office.Enabled = false;
        //    rfv_ddl_designation.Enabled = false;
        //    rfv_ddl_officer.Enabled = false;
        //    // reject
        //    rfv_ddl_reject.Enabled = false;
        //    // rfv_txt_reject.Enabled = false;
        //    //review
        //   // rfv_ddl_review.Enabled = false;
        //    // rfv_txt_review.Enabled = false;
        //    //dispose
        //    rfv_txt_result.Enabled = true;
        //    rfv_rti_file.Enabled = true;
        //    rfv_txt_desc_rti.Enabled = true;
        //    rfv_fileuploadrti.Enabled = true;
        //    rfv_meeting.Enabled = true;
        //    rfv_rbl_additionalfee.Enabled = true;
        //    rfv_radiomeeting.Enabled = true;
        //    rfv_txt_amount.Enabled = true;
        //}
        else
        {
            div_dispose.Visible = false;
            div_meeting.Visible = false;
            div_forward.Visible = false;
            div_reject.Visible = false;
            //  div_review.Visible = false;
            forward_auth.Visible = false;
        }
        bind_permission_auth();
        if (h_IsRtiData.Value == "Y")
        {

            div_dispose.Visible = true;

        }
        
    }
    public void bind_district_code()
    {
        ddl_district.Items.Clear();
        bl1.State = "22";
        rd = dl1.district_code(bl1);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";
        ddl_district.DataBind();
        ddl_district.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void bind_office()
    {
        bl1.District_id_ofc = ddl_district.SelectedValue;
        bl1.Base_department_id = ddl_department.SelectedValue;
        bl1.Office_category = ddl_officeCategory.SelectedValue;
        bl1.Office_level_id = ddl_officeLevel.SelectedValue;
        rd = dl1.office(bl1);
        ddl_office.DataSource = rd.table;
        ddl_office.DataTextField = "ofice";
        ddl_office.DataValueField = "NewOfficeCode";
        ddl_office.DataBind();
        ddl_office.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void bind_category()
    {
        rd = dl1.officecategory();
        ddl_officeCategory.DataSource = rd.table;
        ddl_officeCategory.DataTextField = "OfficeCategoryName";
        ddl_officeCategory.DataValueField = "OfficeCategoryCode";
        ddl_officeCategory.DataBind();
        ddl_officeCategory.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void bind_designation()
    {
        rd = dl1.designation();
        ddl_designation.DataSource = rd.table;
        ddl_designation.DataTextField = "Designation_Name";
        ddl_designation.DataValueField = "Designation_ID";
        ddl_designation.DataBind();
        ddl_designation.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void bind_employee()
    {
        bl1.District_id_ofc = ddl_district.SelectedValue;
        bl1.Base_department_id = ddl_department.SelectedValue;
        bl1.Office_id = ddl_office.SelectedValue;
        bl1.Designation = ddl_designation.SelectedValue;
        bl1.Office_category = ddl_officeCategory.SelectedValue;

        string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
        Utilities ut = new Utilities();
       // string sas = Server.UrlDecode(Request.QueryString["emp_ofc_map_id"].ToString());
        string sas = Server.UrlDecode(Request.QueryString["empid"].ToString());
        sas = sas.Replace(" ", "+");
        string decrypt_query_string = ut.Decrypt_AES(sas, key);
        bl1.Office_mapping_id = decrypt_query_string;

       // bl1.Office_mapping_id = Request.QueryString["emp_ofc_map_id"];
        rd = dl1.employee(bl1);
        ddl_officer.DataSource = rd.table;
        ddl_officer.DataTextField = "name";
        ddl_officer.DataValueField = "map_id";
        ddl_officer.DataBind();
        ddl_officer.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void bind_department_id()
    {
        ddl_department.Items.Clear();
        rd = dl1.department_id();
        ddl_department.DataSource = rd.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";
        ddl_department.DataBind();
        ddl_department.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void bind_ddl_officelevel()
    {
        ddl_officeLevel.Items.Clear();
        bl1.District_id_ofc = ddl_district.SelectedValue;
        bl1.Base_department_id = ddl_department.SelectedValue;
        rd = dl1.officelevel(bl1);
        ddl_officeLevel.DataSource = rd.table;
        ddl_officeLevel.DataTextField = "Office_level";
        ddl_officeLevel.DataValueField = "olc";
        ddl_officeLevel.Items.Add(new ListItem(" Select", "0"));
        ddl_officeLevel.DataBind();
    }
    public void bind_grd_Action()
    {
        string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
       // string key = "mk";
        Utilities ut = new Utilities();
        string sas = Server.UrlDecode(Request.QueryString["rtiid"].ToString());
        sas = sas.Replace(" ", "+");
        string decrypt_query_string = ut.Decrypt_AES(sas, key);
        bl1.Rti_id = decrypt_query_string;
       // bl1.Rti_id = Request.QueryString["rtiid"];
        rd = dl1.bind_action_grid(bl1);
        grd_Action.DataSource = rd.table;
        grd_Action.DataBind();
        if (rd.table.Rows.Count > 0)
        {
            lbl_status.Text = rd.table.Rows[0]["status"].ToString();
        }
    }
    public void bind_permission_auth()
    {
        bl1.Office_mapping_id = Session["EmpMapID"].ToString(); ;
        // ddl_forward_auth.Items.Clear();
        if (ddl_status.SelectedValue == "NREJ")
        {
            bl1.Permission = "REJ";
        }
        else if (ddl_status.SelectedValue == "NDIS")
        {
            bl1.Permission = "DIS";
        }
        else
        {

            bl1.Permission = ddl_status.SelectedValue;
        }
        // rd = dl1.Permission_auth(bl1);
        rd = dl1.bind_withinoffice(bl1);
        ddl_forward_auth.DataSource = rd.table;
        ddl_forward_auth.DataTextField = "name";
        ddl_forward_auth.DataValueField = "mapid";
        ddl_forward_auth.DataBind();
        ddl_forward_auth.Items.Insert(0, new ListItem("-------Select--------", "0"));
        //ddl_forward_auth.DataBind();
    }
    //protected void RadioArea_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (RadioArea.SelectedValue == "WO")
    //    {
    //        rfv_with_ofc.Enabled = true;
    //        ddl_officeLevel.SelectedValue = "0";
    //        rfv_officeLevel.Enabled = false;
    //        ddl_officeCategory.SelectedValue = "0";
    //        rfv_ofc_cat.Enabled = false;
    //        ddl_office.SelectedValue = "0";
    //        rfv_office.Enabled = false;
    //        ddl_district.SelectedValue = "0";
    //        rfv_district.Enabled = false;
    //        ddl_department.SelectedValue = "0";
    //        rfv_dept.Enabled = false;
    //        ddl_designation.SelectedValue = "0";
    //        ddl_officer.SelectedValue = "0";
    //        ddl_with_ofc.SelectedValue = "0";
    //        inside_ofc.Visible = true;
    //        out_office.Visible = false;
    //        bind_inside_ofc();
    //    }
    //    else if (RadioArea.SelectedValue == "OSO")
    //    {
    //        if (ddl_status.SelectedValue == "FOR")
    //        {
    //            bind_permission_auth();
    //            // forward_auth.Visible = true;
    //            // rfv_ddl_forward_auth.Enabled = true;
    //        }
    //        else
    //        {
    //            forward_auth.Visible = false;
    //            rfv_ddl_forward_auth.Enabled = false;
    //        }
    //        ddl_officer.SelectedValue = "0";
    //        ddl_designation.SelectedValue = "0";
    //        ddl_department.SelectedValue = "0";
    //        ddl_district.SelectedValue = "0";
    //        ddl_office.SelectedValue = "0";
    //        ddl_officeCategory.SelectedValue = "0";
    //        ddl_officeLevel.SelectedValue = "0";
    //        ddl_with_ofc.SelectedValue = "0";
    //        rfv_ofc_cat.Enabled = true;
    //        rfv_officeLevel.Enabled = true;
    //        rfv_office.Enabled = true;
    //        rfv_dept.Enabled = true;
    //        rfv_district.Enabled = true;
    //        rfv_with_ofc.Enabled = false;
    //        rfv_with_ofc.Enabled = false;
    //        out_office.Visible = true;
    //        inside_ofc.Visible = false;
    //    }

    //}
    protected void RadioMeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioMeeting.SelectedValue == "Y")
        {
            div_meet_date.Visible = true;
        }
        else if (RadioMeeting.SelectedValue == "N")
        {
            div_meet_date.Visible = false;
        }
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_ddl_officelevel();
        bind_office();
        bind_employee();
        //bind_category();
    }
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_ddl_officelevel();
        bind_category();
    }
    protected void ddl_officeLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_office();
    }
    protected void ddl_officeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_office();
        bind_designation();
        bind_employee();
    }
    protected void ddl_office_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_designation();
        bind_employee();
    }
    protected void ddl_designation_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_employee();
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
    //ddl_reject_SelectedIndexChanged
    protected void ddl_reject_Bind()
    {
        rd = dl1.GetRejectReason();
        ddl_reject.DataSource = rd.table;
        ddl_reject.DataTextField = "Name";
        ddl_reject.DataValueField = "Value";
        ddl_reject.DataBind();
        ddl_reject.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            HttpBrowserCapabilities browse = Request.Browser;
           //if (btnSubmit.Text == "Submit")
           // {


                #region status
                string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
                Utilities ut = new Utilities();
                string sas = Server.UrlDecode(Request.QueryString["rtiid"].ToString());
                sas = sas.Replace(" ", "+");
                string decrypt_query_string = ut.Decrypt_AES(sas, key);
                bl1.Rti_id = decrypt_query_string;
                //bl1.Rti_id = Request.QueryString["rtiid"];
                bl1.Userid = Session["username"].ToString();
                bl1.Dept_Status = ddl_status.SelectedValue;
                //bl1.Rti_status = lbl_status.Text;
                bl1.Action_id = dl1.max_action_id(bl1);
                bl1.Office_mapping_id = Session["EmpMapID"].ToString();
                bl1.Ipaddress = ul.GetClientIpAddress(this.Page);
                bl1.Useragent = Request.UserAgent.ToString();
                bl1.Useros = Utilities.System_Info(this.Page);
                bl1.User_browser = browse.Browser;
                bl1.Isnew = "Y";
                bl1.Action_date = getDate(Txt_Action.Text);
                bl1.Approve_auth = ddl_forward_auth.SelectedValue;
                #endregion  status update
                bl1.Action_discription = txt_description.Text;
                bl1.S_officemapping_id = Session["EmpMapID"].ToString();
                bl1.Action_date = getDate(Txt_Action.Text);
                bl1.Is_file_upload = ddl_fileup.SelectedValue;
                #region Action file upload
                bl1.RTI_fileID = null;
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
                                    bl1.RTI_fileID = dl1.max_file_id();
                                    Stream fs = file.InputStream;
                                    BinaryReader br = new BinaryReader(fs);
                                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    bl1.RTI_fileName = file.FileName;
                                    bl1.RTI_fileType = file.ContentType;
                                    bl1.RTI_fileData = bytes;
                                    bl1.FileDescription = txt_file_desc.Text;

                                }
                                else
                                {
                                    Utilities.MessageBox_UpdatePanel(udp, "Only PDF type File will be accepted");
                                    return;
                                }

                            }
                            else
                            {
                                Utilities.MessageBox_UpdatePanel(udp, "Your file size is greater than 2 MB  ");
                                return;
                            }
                        }
                        else
                        {
                            Utilities.MessageBox_UpdatePanel(udp, "File Description Is Required");
                            return;
                        }
                    }
                    else
                    {
                        Utilities.MessageBox_UpdatePanel(udp, "PDF File required");
                        return;
                    }

                }
                #endregion

                bl1.Rti_status = "PEN";
                switch (bl1.Dept_Status)
                {
                    case "NDIS":                // For Disposal 
                    case "NAPR":        // For Approval
                    //case "REV":           // Reviewed   
                    case "NREV":         //For Review
                        bl1.R_office_map_id = ddl_forward_auth.SelectedValue;
                        // bl1.Review = ddl_review.SelectedValue;
                        break;
                    case "NREJ":           //For Rejection
                        bl1.R_office_map_id = ddl_forward_auth.SelectedValue;
                        bl1.Reject = ddl_reject.SelectedValue;
                        break;
                    case "FOR":           // Forward
                        //if (RadioArea.SelectedValue == "OSO") //Forward only outside office
                        //{
                        bl1.R_office_map_id = ddl_officer.SelectedValue;
                        //}
                        //else if (RadioArea.SelectedValue == "WO") //Forward only outside office
                        //{
                        //    bl1.R_office_map_id = ddl_with_ofc.SelectedValue; //Forward only outside office
                        //}
                        break;
                    case "REJ":           // Rejected
                        bl1.R_office_map_id = bl1.Office_mapping_id; //ddl_forward_auth.SelectedValue;
                        bl1.Reject = ddl_reject.SelectedValue;
                        bl1.Rti_status = "REJD";      // added on 23-oct-2017 for rejected rti main status
                        break;
                    case "DIS":           // Dispose
                        bl1.R_office_map_id = bl1.S_officemapping_id; //ddl_forward_auth.SelectedValue;
                        
                        break;
                        //Added on 23-oct-2017 for status clarification
                    case "NCLR" :
                        bl1.R_office_map_id = bl1.S_officemapping_id;  // Keep it for self
                        bl1.Rti_status = "CLR";                  // Main status = clarification
                        break;
                        //End for status clarification
                   
                }// End of Switch
                bl1.IsMeeting = RadioMeeting.SelectedValue;
                if (bl1.IsMeeting != "Y" && bl1.Dept_Status == "DIS" && bl1.IsAdditionalFees !="Y")
                {
                    bl1.Rti_status = "CLT";
                    bl1.Isnew = "N";                       // If status is CLT then New = 'N'
                    bl1.R_office_map_id = bl1.S_officemapping_id;
                }
                
                bl1.RTI_data_id = dl1.max_RTI_Data_id();

                bl1.Rti_Discription = txt_result.Text;
                if (RadioMeeting.SelectedValue == "Y")
                {
                    bl1.Meeting_date = txt_meeting.Text;
                    if (bl1.Meeting_date != "")
                    {
                        bl1.Meeting_date = getDate(txt_meeting.Text);
                    }
                    else
                    {
                        bl1.Meeting_date = "";
                    }

                }
                else
                {
                    bl1.Meeting_date = null;
                }
                bl1.IsAdditionalFees = rbl_additional_fees_rti.SelectedValue;
                if (rbl_additional_fees_rti.SelectedValue == "Y")
                {
                    bl1.Additional_fees = txt_amount.Text;
                }
                else
                {
                    bl1.Additional_fees = null;
                }

                        
                #region RTI file upload
                bl1.Is_rti_file_upload = "N";
                if (ddl_rti_file.SelectedValue == "Y")
                {

                    if (FileUploadRTI.HasFile)
                    {
                        if (txt_desc_rti.Text != "")
                        {
                            HttpPostedFile file = FileUploadRTI.PostedFile;
                            if (file.ContentLength < 2000000)
                            {
                                if (file.ContentType == "application/pdf" || file.ContentType == "application/x-pdf"
                                    || file.ContentType == "application/x-unknown")
                                {
                                    // bl1.RTI_fileID = dl1.max_file_id();
                                    Stream fs = file.InputStream;
                                    BinaryReader br = new BinaryReader(fs);
                                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    bl1.Data_fileName = file.FileName;
                                    bl1.Data_fileType = file.ContentType;
                                    bl1.Data_fileData = bytes;
                                    bl1.Data_FileDescription = txt_desc_rti.Text;
                                    bl1.Is_rti_file_upload = "Y";
                                }
                                else
                                {
                                    Utilities.MessageBox_UpdatePanel(udp, "Only PDF type File will be accepted");
                                    return;
                                }

                            }
                            else
                            {
                                Utilities.MessageBox_UpdatePanel(udp, "Your file size is greater than 2 MB  ");
                                return;
                            }
                        }
                        else
                        {
                            Utilities.MessageBox_UpdatePanel(udp, "File Description Is Required");
                            return;
                        }
                    }
                    else
                    {
                        Utilities.MessageBox_UpdatePanel(udp, "PDF File required");
                        return;
                    }

                }
                #endregion


                // If There is RTI data then Update 
                if (h_IsRtiData.Value == "Y")
                {
                    bl1.Is_RTI_Data = "Y";
                }
                else {
                    bl1.Is_RTI_Data = "N";
                }

                rb = dl1.Insert_Action_Info(bl1);
                if (rb.status == true)
                {
                    if (Session["language"].ToString() == "en-GB")
                    {

                        Utilities.MessageBox_UpdatePanel_Redirect(udp, "Action Submitted Successfully", "../Employee/EmployeeWelcome_Page.aspx");
                    }
                    else
                    {

                        Utilities.MessageBox_UpdatePanel_Redirect(udp, "कार्रवाई सफलतापूर्वक सबमिट की गई", "../Employee/EmployeeWelcome_Page.aspx");
                    }
                 //   Utilities.MessageBox_UpdatePanel_Redirect(udp, "Action Submitted Successfully", "../Employee/EmployeeWelcome_Page.aspx");
                }
                else
                {
                    if (Session["language"].ToString() == "en-GB")
                    {
                        Utilities.MessageBox_UpdatePanel(udp, "Action Not Submitted");
                        
                    }
                    else
                    {
                        Utilities.MessageBox_UpdatePanel(udp, "कार्रवाई सबमिट नहीं की गई");
                       
                    }
                   // Utilities.MessageBox_UpdatePanel(udp, "Action Not Submitted");
                }
           // }  // end of Submit
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
    protected void lnk_file_Click(object sender, EventArgs e)
    {
        bl1.RTI_fileID = ((LinkButton)sender).CommandArgument.ToString();
        int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
        bl1.Action_id = grd_Action.DataKeys[rowIndex].Values["action_id"].ToString();
        bl1.Rti_id = grd_Action.DataKeys[rowIndex].Values["rti_id"].ToString();
        rd = dl1.select_Action_file(bl1);
        if (rd.table.Rows.Count > 0)
        {
            byte[] bt = (byte[])rd.table.Rows[0]["fileData"];
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = rd.table.Rows[0]["fileType"].ToString();
            Response.AddHeader("content-disposition", "attachment;filename="
                // + rd.table.Rows[0]["fileName"].ToString());
    + "Document.pdf");
            Response.BinaryWrite(bt);
            Response.Flush();
            Response.End();
        }
    }
    protected void lnk_file_rti_Click(object sender, EventArgs e)
    {
        string key = ConfigurationManager.AppSettings["EncKey"].ToString();
        Utilities ut = new Utilities();
        bl1.RTI_fileID = Request.QueryString["rtiid"];
       // string decrypt_query_string = ut.Decrypt_AES(bl1.RTI_fileID, key);
        string enc_queryStr = ut.Encrypt_AES(bl1.RTI_fileID, key);   
        string link = "./RTIFileDownload.aspx?rtiid=";
        //Response.Redirect( link + bl1.RTI_fileID);
        Response.Redirect( link + enc_queryStr);
        //try
        //{
        //    //bl1.RTI_fileID = ((LinkButton)sender).CommandArgument.ToString();
        //    bl1.RTI_fileID = Request.QueryString["rtiid"];
        //    rd = dl1.select_rti_file(bl1);
        //    if (rd.table.Rows.Count > 0)
        //    {
        //        byte[] bt = (byte[])rd.table.Rows[0]["FileData"];
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.ContentType = rd.table.Rows[0]["FileType"].ToString();
        //        Response.AddHeader("content-disposition", "attachment;filename="
        //            //      + rd.table.Rows[0]["fileName"].ToString());
        //+ "abc.pdf");
        //        Response.BinaryWrite(bt);
        //        Response.Flush();
        //        Response.End();

        //    }
        //}
        //catch (Exception ex)
        //{ }

    }
    protected void grd_Action_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.Row.FindControl("lnk_file");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string str = DataBinder.Eval(e.Row.DataItem, "fileid").ToString();
            if (str == "" || str == null)
            {
                e.Row.Cells[8].Visible = false;
            }
        }
    }
    protected void grd_Action_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Action.PageIndex = e.NewPageIndex;
        bind_grd_Action();
    }
    protected void ddl_rti_file_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_rti_file.SelectedValue == "Y")
        {
            div_file_rti.Visible = true;
            rfv_fileuploadrti.Enabled = true;
            rfv_txt_desc_rti.Enabled = true;
        }
        else
        {
            div_file_rti.Visible = false;
            rfv_fileuploadrti.Enabled = false;
            rfv_txt_desc_rti.Enabled = false;
        }
    }
    protected void rbl_additional_fees_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (rbl_additional_fees_rti.SelectedValue == "Y")
        {
            div_additional_fees.Visible = true;
            rfv_txt_amount.Enabled = true;
            rev_txt_amount.Enabled = true;
        }
        else
        {
            div_additional_fees.Visible = false;
            rfv_txt_amount.Enabled = false;
            rev_txt_amount.Enabled = false;
        }
    }
}