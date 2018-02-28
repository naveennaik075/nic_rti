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
public partial class actionHistory : BasePage // System.Web.UI.Page
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
                   
                }

           }
        }
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

    protected void btnClose_Click(object sender, EventArgs e)
    { 
    
    
    }

}