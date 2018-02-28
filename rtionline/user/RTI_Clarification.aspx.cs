using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Threading;
using System.Globalization;

public partial class user_RTI_Clarification : System.Web.UI.Page
{
    Utilities ul = new Utilities();
    bl_login bl = new bl_login();
    dl_login dl = new dl_login();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    
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

                    
                    //if (decrypt_query_string != null)
                    //{
                    //    //bl.RTIID = Request.QueryString["rtiid"].ToString();
                    //    bl.RTIID = decrypt_query_string;
                    //    rd = dl.detailsfor_meeting(bl);
                    //    string datea = rd.table.Rows[0]["startdate"].ToString();
                    //    startdate = Convert.ToDateTime(datea);
                    //    bl.Office = rd.table.Rows[0]["office"].ToString();

                    //}

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
                        h_RTI_ID.Value = bl.RegistrationID;
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
                                                     
                           // lnk_file.Text = "RTI Document";
                            if (rd.table.Rows[0]["country"].ToString() == "OTHER" || rd.table.Rows[0]["country"] == null)
                            {
                                div_state.Visible = false;
                                Lbl_country.Text = rd.table.Rows[0]["o_country"].ToString();
                            }
                            else
                            {
                                Lbl_country.Text = rd.table.Rows[0]["country"].ToString();
                            }

                        }
                        // Add code for description of clarification
                        rd = dl.Select_rti_action_detail(bl);
                        if (rd.table.Rows.Count > 0)
                        {
                            lbl_resultdesc.Text = rd.table.Rows[0]["action_detail"].ToString();
                        }
                        else { 
                            // No action record found
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    Response.Redirect("../LogOut.aspx");
                }
            }

        }


    } // pageLoad End


    protected void btn_submit_Click(object sender, EventArgs e)
    {

        bl_employee_action bl1 = new bl_employee_action();
        dl_employee_action dl1 = new dl_employee_action();
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            HttpBrowserCapabilities browse = Request.Browser;
            bl.RegistrationID = h_RTI_ID.Value;
            rd = dl.Select_rti_action_detail(bl);
            if (rd.table.Rows.Count > 0)
            {
                 bl1.Rti_id = h_RTI_ID.Value;
                 bl1.Userid = Session["username"].ToString();
                 bl1.Dept_Status = "NREV";
                 bl1.Action_id = dl1.max_action_id(bl1);
                 bl1.Office_mapping_id = rd.table.Rows[0]["officer_mapping_id"].ToString();
                bl1.Ipaddress = ul.GetClientIpAddress(this.Page);
                bl1.Useragent = Request.UserAgent.ToString();
                bl1.Useros = Utilities.System_Info(this.Page);
                bl1.User_browser = browse.Browser;
                bl1.Isnew = "Y";
               // string dtstring = rd.table.Rows[0]["action_date"].ToString();
               // DateTime dt = DateTime.ParseExact(dtstring, "dd/MM/yyyy HH:mm:ss", null);
                string dt1 = DateTime.Now.ToString("yyyy/MM/dd");
                bl1.Action_date = dt1;//dt.ToString("yyyy/MM/dd");
                bl1.Action_discription = rd.table.Rows[0]["action_detail"].ToString() + " :CLARIFICATION: " + txt_RequestApplicationText.Text;
                bl1.Is_file_upload = "N";
                bl1.Rti_status = "PEN";
                bl1.R_office_map_id = bl1.Office_mapping_id;
            }
            else
            {
                // No action record found
            }

            rb = dl1.Insert_Clarification_Action(bl1);

            if (rb.status == true)
            {
                if (Session["language"].ToString() == "en-GB")
                {
                    Utilities.MessageBoxShow_Redirect("Action Submitted Successfully", "../user/UserDashBoard.aspx");
                    
                }
                else
                {
                    Utilities.MessageBoxShow_Redirect("कार्रवाई सफलतापूर्वक सबमिट की गई", "../user/UserDashBoard.aspx");
                   
                }
                
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {
                    Utilities.MessageBoxShow("Action Not Submitted");
                    
                }
                else
                {
                    Utilities.MessageBoxShow("कार्रवाई सबमिट नहीं की गई");
                   
                }
                
            }



        }
       

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





}// Class End