using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Threading;
using System.Globalization;

public partial class user_Detail_Rti : BasePage // System.Web.UI.Page
{
    Utilities ul = new Utilities();
    bl_login bl = new bl_login();
    dl_login dl = new dl_login();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    DateTime startdate;
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
        if (Session["username"] == null)
        {
            Response.Redirect("../LogOut.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                try
                {
                    string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
                    Utilities ut = new Utilities();
                    string sas = Server.UrlDecode(Request.QueryString["rtiid"].ToString());
                    sas = sas.Replace(" ", "+");
                    string decrypt_query_string = ut.Decrypt_AES(sas, key);

                    // if (Request.QueryString["rtiid"] != null)
                    if (decrypt_query_string != null)
                    {
                        //bl.RTIID = Request.QueryString["rtiid"].ToString();
                        bl.RTIID = decrypt_query_string;
                        rd = dl.detailsfor_meeting(bl);
                        string datea = rd.table.Rows[0]["startdate"].ToString();
                        startdate = Convert.ToDateTime(datea);
                        bl.Office = rd.table.Rows[0]["office"].ToString();

                    }

                    if (Session["username"] == null)
                    {
                        Response.Redirect("../LogOut.aspx");
                    }
                    // else if (Request.QueryString["rtiid"].ToString() == null)
                    else if (decrypt_query_string == null)
                    {
                        Response.Redirect("../LogOut.aspx");
                    }
                    else
                    {
                        //bl.RegistrationID = Request.QueryString["rtiid"].ToString();
                        bl.RegistrationID = decrypt_query_string;
                        rd = dl.Select_unique_rti_detail(bl);
                        if (rd.table.Rows.Count > 0)
                        {
                            lbl_applicant_name.Text = rd.table.Rows[0]["applicant"].ToString();
                            Lbl_gender.Text = rd.table.Rows[0]["gender"].ToString();
                            lbl_mobile.Text = rd.table.Rows[0]["mobile"].ToString();
                            lbl_email.Text = rd.table.Rows[0]["email"].ToString();
                            lbl_subject.Text = rd.table.Rows[0]["subject"].ToString();
                            lbl_detail.Text = rd.table.Rows[0]["detail"].ToString();
                            Lbl_address.Text = rd.table.Rows[0]["address"].ToString();
                            lbl_district.Text = rd.table.Rows[0]["district"].ToString();
                            lbl_state.Text = rd.table.Rows[0]["statename"].ToString();
                            lbl_pincode.Text = rd.table.Rows[0]["pincode"].ToString();
                            lbl_status.Text = rd.table.Rows[0]["status"].ToString();
                            lbl_resultdesc.Text = rd.table.Rows[0]["result"].ToString();
                            if (lbl_status.Text == "Completed")
                            {
                                if (rd.table.Rows[0]["result"].ToString() == null || rd.table.Rows[0]["result"].ToString() == "")
                                {
                                    lbl_resultdesc.Text = "No Answer provided";
                                }
                                pnl_result.Visible = true;
                                div_pending.Visible = false;
                            }
                                // Added for Rejected display rejection data on 26-oct-2017
                            else if (lbl_status.Text == "Rejected")
                            {
                                div_pending.Visible = false;
                                pnl_result.Visible = false;
                                div_rejected.Visible = true;
                                ReturnClass.ReturnDataTable rd1 = dl.Select_rti_action_detail(bl);
                                 if (rd1.table.Rows.Count > 0)
                                 {
                                     lbl_rejected_reason.Text = rd1.table.Rows[0]["RejectReason"].ToString();
                                     lbl_Rejected_Description.Text = rd1.table.Rows[0]["action_detail"].ToString();
                                 }
                                 else { 
                                 
                                 }
                            
                            }
                                // End rejected part 26-oct-2017

                            else
                            {

                                pnl_result.Visible = false;
                            }
                            if (rd.table.Rows[0]["meeting_req"].ToString() == "Y")
                            {
                                if (rd.table.Rows[0]["meetingdate"] == null || rd.table.Rows[0]["meetingdate"].ToString() == "")
                                {
                                    caltest.StartDate = Convert.ToDateTime(rd.table.Rows[0]["strdate"]);
                                    caltest.EndDate = Convert.ToDateTime(rd.table.Rows[0]["strdate"]).AddDays(15);
                                    if (caltest.EndDate < DateTime.Now)
                                    {
                                        div_meet.Visible = false;
                                        meet.Visible = true;
                                        lbl_date.Text = "Meeting is expired";
                                    }
                                    else
                                    {
                                        div_meet.Visible = true;
                                        meet.Visible = false;
                                    }
                                }
                                else
                                {
                                    div_meet.Visible = false;
                                    meet.Visible = true;
                                    lbl_date.Text = rd.table.Rows[0]["meetingdate"].ToString();
                                }
                            }
                            else if (rd.table.Rows[0]["meeting_req"].ToString() == "N" || rd.table.Rows[0]["meeting_req"] == null || rd.table.Rows[0]["meeting_req"].ToString() == "")
                            {
                                div_meet.Visible = false;
                                meet.Visible = false;
                                lbl_date.Text = rd.table.Rows[0]["meetingdate"].ToString();
                            }

                            if (rd.table.Rows[0]["fees_req"].ToString() == "N")
                            {
                                div_fees.Visible = false;
                            }
                            else if (rd.table.Rows[0]["fees_req"].ToString() == "Y")
                            {
                                div_fees.Visible = true;
                                lbl_fees_ammount.Text = (rd.table.Rows[0]["fees"].ToString());
                            }
                            else
                            {
                                div_fees.Visible = false;
                            }
                            if (rd.table.Rows[0]["upld"].ToString() == "Y" && rd.table.Rows[0]["fees_req"].ToString() == "N")
                            {
                                Action_file.Visible = true;
                                lbl_act_file_des.Text = rd.table.Rows[0]["act_fl_desc"].ToString();
                                if (Session["language"].ToString() == "en-GB")
                                {
                                    lnk_fil_action.Text = "Result File";
                                }
                                else {
                                    lnk_fil_action.Text = "परिणाम फ़ाइल्";
                                }
                                //lbl_act_file_des.Text=
                            }
                            else
                            {
                                Action_file.Visible = false;

                            }
                            //lbl_file_dtl.Text = rd.table.Rows[0]["filedesc"].ToString();
                            lnk_file.Text = "RTI Document";
                            if (rd.table.Rows[0]["country"].ToString() == "OTHER" || rd.table.Rows[0]["country"] == null)
                            {
                                div_state.Visible = false;
                                Lbl_country.Text = rd.table.Rows[0]["o_country"].ToString();
                            }
                            else
                            {
                                Lbl_country.Text = rd.table.Rows[0]["country"].ToString();
                            }
                            if (rd.table.Rows[0]["filename"] == null || rd.table.Rows[0]["filename"].ToString() == "")
                            {
                                divfile.Visible = false;
                            }
                            else
                            {
                                grd_rti_dtl_Databind();
                                rd = dl.select_rti_files_detail(bl);
                                if (rd.table.Rows.Count > 0)
                                {
                                    lbl_file_dtl.Text = rd.table.Rows[0]["FileDescription"].ToString();
                                }
                                else
                                {
                                    divfile.Visible = false;
                                }
                            }
                        }
                       // grd_rti_dtl_Databind();   // it is not used
                    }
                }
                catch (NullReferenceException ex)
                {
                    Response.Redirect("../LogOut.aspx");
                }
            }

        }
    }
    public void grd_rti_dtl_Databind()
    {
        string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
        Utilities ut = new Utilities();
        string sas = Server.UrlDecode(Request.QueryString["rtiid"].ToString());
        sas = sas.Replace(" ", "+");
        string decrypt_query_string = ut.Decrypt_AES(sas, key);

        // bl.RTIID = Request.QueryString["rtiid"].ToString();
        bl.RTIID = decrypt_query_string;
        rd = dl.Select_Rti_Action_detail(bl);



        int row = rd.table.Rows.Count;
        int page;
        if (row % 20 == 0)
        {
            page = row / 20;
        }
        else
        {
            page = row / 20;
            page = page + 1;
        }

        //lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        //grd_rti_dtl.DataSource = rd.table;
        //grd_rti_dtl.DataBind();


    }
    protected void grd_rti_dtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //grd_rti_dtl.PageIndex = e.NewPageIndex;
        //grd_rti_dtl_Databind();
    }
    protected void lnk_file_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
            Utilities ut = new Utilities();
            string sas = Server.UrlDecode(Request.QueryString["rtiid"].ToString());
            sas = sas.Replace(" ", "+");
            string decrypt_query_string = ut.Decrypt_AES(sas, key);

            //bl.RegistrationID = Request.QueryString["rtiid"].ToString();
            bl.RegistrationID = decrypt_query_string;
            rd = dl.select_rti_files_detail(bl);
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
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Page.IsValid)
            {

                string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
                Utilities ut = new Utilities();
                string sas = Server.UrlDecode(Request.QueryString["rtiid"].ToString());
                sas = sas.Replace(" ", "+");
                string decrypt_query_string = ut.Decrypt_AES(sas, key);
                //bl.RTIID = Request.QueryString["rtiid"];
                bl.RTIID = decrypt_query_string;
                rd = dl.detailsfor_meeting(bl);
                if (rd.table.Rows.Count > 0)
                {
                    string str = txt_meetdate.Text;
                    //DateTime dtl = Convert.ToDateTime(txt_meetdate.Text).ToString("yyyy/MM/dd");
                    DateTime date_renewal = DateTime.ParseExact(txt_meetdate.Text.Trim(), "dd/MM/yyyy", null);
                    bl.Meetingdate = date_renewal.ToString("yyyy/MM/dd");
                    bl.UserID = Session["username"].ToString();
                    bl.RTIID = rd.table.Rows[0]["rti"].ToString();
                    bl.Office = rd.table.Rows[0]["office"].ToString();
                    bl.UserIP = ul.GetClientIpAddress(this.Page);
                    bl.UserAgent = Request.UserAgent.ToString();
                    bl.UserOS = Utilities.System_Info(this.Page);
                    //HttpBrowserCapabilities brs = Request.Browser;
                    //bl.User_browser = brs.Browser;
                    bl.User_browser = Request.Browser.Browser;
                    rb = dl.insertmeetdate(bl);
                    if (rb.status == true)
                    {
                        Utilities.MessageBoxShow( "meeting date has scheduled");
                        //Utilities.MessageBox_UpdatePanel(udp, "meeting date has scheduled");
                        div_meet.Visible = false;
                        rfv_meet_date.Enabled = false;
                    }
                    else
                    {
                        Utilities.MessageBoxShow( "meeting date not schedule. Please Try again After Some time");
                    }
                }
            }
        }
    }
    protected void lnk_fees_Click(object sender, EventArgs e)
    {

    }

    protected void caltest_DayRender(object sender, DayRenderEventArgs e)
    {
        //    if (e.Day.Date < DateTime.Now)
        //    {
        //        e.Day.IsSelectable = false;
        //    }
        //    else if (e.Day.Date >= DateTime.Now)
        //    {
        //        bl.Meetingdate = e.Day.Date.ToString();
        //        rd = dl.get_count_slot(bl);
        //        if (rd.table.Rows.Count > 9)
        //        {
        //            e.Day.IsSelectable = false;
        //        }
        //    }
        //    if (e.Day.Date > startdate.AddDays(30))
        //    {
        //        e.Day.IsSelectable = false;
        //    }
        //    if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        e.Day.IsSelectable = false;
        //    }
        //    if (e.Day.IsSelectable == true)
        //    {
        //        e.Cell.ForeColor = Color.Gray;
        //    }
        //    else
        //    {
        //        e.Cell.ForeColor = Color.Red;
        //    }
    }

    protected void caltest_SelectionChanged(object sender, EventArgs e)
    {
        //txt_meetdate.Text = caltest.SelectedDate.ToString("dd/MM/yyyy");
        //caltest.Visible = false;
    }

    protected void img_cal_Click(object sender, ImageClickEventArgs e)
    {
        if (caltest.Visible == false)
        {
            caltest.Visible = true;
        }
        else { caltest.Visible = false; }

    }

    protected void lnk_fil_action_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
                Utilities ut = new Utilities();
                string sas = Server.UrlDecode(Request.QueryString["rtiid"].ToString());
                sas = sas.Replace(" ", "+");
                string decrypt_query_string = ut.Decrypt_AES(sas, key);

                //bl.RegistrationID = Request.QueryString["rtiid"].ToString();
                bl.RegistrationID = decrypt_query_string;
                rd = dl.select_result_file(bl);
                if (rd.table.Rows.Count > 0)
                {
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        string lol1 = rd.table.Rows[0]["FileType"].ToString();
                        Response.ContentType = rd.table.Rows[0]["FileType"].ToString();
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        byte[] b = ((byte[])rd.table.Rows[0]["FileData"]);
                        Response.BinaryWrite(b);
                        Response.Flush();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                                                
                        //Response.Clear();
                        //byte[] bt = (byte[])rd.table.Rows[0]["FileData"];
                        //Response.Buffer = true;
                        //Response.Charset = "";
                        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //Response.ContentType = rd.table.Rows[0]["FileType"].ToString();
                        //Response.AddHeader("content-disposition", "attachment;filename=Document.pdf");
                        //// + rd.table.Rows[0]["fileName"].ToString());
                        //Response.BinaryWrite(bt);
                        //Response.Flush();
                        ////Response.End();
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();

                    }

                }
            }
        }
        catch (Exception ex)
        { }
    }
}