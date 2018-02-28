using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Approve : System.Web.UI.Page
{
    blApprove bl = new blApprove();
   
    dlApprove dl = new dlApprove();
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
            if (Session["username"] == null)
            {
                Response.Redirect("../LogOut.aspx");
            }
            else
            {
                bl.Userid = Session["username"].ToString();
                bl.Ofc_mappingid = Session["EmpMapID"].ToString();
                rd = dl.select_admin_info(bl);
                if (rd.table.Rows.Count > 0)
                {

                        bl.State = rd.table.Rows[0]["state_id"].ToString();
                        bind_district_code();
                        bindgrd_ofc();
                        
                        ddl_district.SelectedValue = bl.District;
                       
                       
                    }
                    
                }
            }
        }

    
    public void bind_district_code()
    {
        ddl_district.Items.Clear();
        bl.State = "22";
        rd = dl.district_code(bl);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";
        ddl_district.DataBind();
        ddl_district.Items.Insert(0, new ListItem("Select", "0"));
    }


    public void bindgrd_ofc()
    {
        bl.Userid = Session["username"].ToString();
        rd = dl.select_admin_info(bl);

     
            bl.State = rd.table.Rows[0]["state_id"].ToString();
            bl.District = ddl_district.SelectedValue;

       
            bl.Officelevelcode = "00";
          
        
        rd = dl.Bind_grid(bl);
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
        lbl_count.Text = "Total Offices = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        grd_Office.DataSource = rd.table;
        grd_Office.DataBind();
    }
    protected void grd_Office_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Office.PageIndex = e.NewPageIndex;
        bindgrd_ofc();
    }
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrd_ofc();
    }

    protected void chkAllSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)grd_Office.HeaderRow.FindControl("chkAllSelect");
        foreach (GridViewRow row in grd_Office.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chk_approve");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }

    protected void btnapprove_Click(object sender, EventArgs e)
    {
        int counter = 0;

        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {

            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            HttpBrowserCapabilities browse = Request.Browser;
            if (grd_Office.Rows.Count < 1)
            {
                Utilities.MessageBoxShow("No Record");
                return;
            }

            foreach (GridViewRow row in grd_Office.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRowCheck = (row.Cells[5].FindControl("chk_approve") as CheckBox);
                    if (chkRowCheck.Checked == true)
                    {
                        counter++;
                        bl.Officeid =  grd_Office.DataKeys[row.RowIndex].Values["NewOfficeCode"].ToString();
                        rd = dl.select(bl);

                        bl.District = rd.table.Rows[0]["district"].ToString();
                        bl.Officelvl = rd.table.Rows[0]["ofclvl"].ToString();
                        bl.Office = rd.table.Rows[0]["ofice"].ToString();
                        bl.Base_department = rd.table.Rows[0]["basedept"].ToString();
                        bl.Country = "91";
                        bl.OfficeCategory = rd.table.Rows[0]["OfficeCategory"].ToString();
                        bl.State = "22";
                        bl.OfficeAddress = rd.table.Rows[0]["address"].ToString();
                        bl.Contact = rd.table.Rows[0]["mobile"].ToString();
                        bl.Fax = rd.table.Rows[0]["Fax"].ToString();
                        bl.Email = rd.table.Rows[0]["email"].ToString();
                        bl.OfficeURL = rd.table.Rows[0]["ofc_url"].ToString();
                        bl.Operating_system = Utilities.System_Info(this.Page);
                        bl.Browser = browse.Browser;
                        bl.Useragent = Request.UserAgent.ToString();
                        bl.Ipaddress = ul.GetClientIpAddress(this);
                        bl.Userid= Session["username"].ToString();
                        bl.Status = "Y";
                     
                        rb = dl.Update_status1(bl);

                    }
                }
            }

            if (counter > 0)
            {


                if (rb.status == true)
                {
                    Utilities.MessageBox_UpdatePanel(UpdatePanel1, "approve successfully");
                    bindgrd_ofc();
                }
                else
                {
                    Utilities.MessageBox_UpdatePanel(UpdatePanel1, " Not approve successfully");
                }
            }
            else
            { Utilities.MessageBox_UpdatePanel(UpdatePanel1, " PLease Select Row for approval"); }
        }
    }
    
}