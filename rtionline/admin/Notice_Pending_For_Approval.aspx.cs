//ekta kushwah
//nov-2-16
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Resources;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Runtime.InteropServices;
public partial class admin_Pending_For_Approval : BasePage // System.Web.UI.Page
{
    Utilities util = new Utilities();
    dl_rti_notice dl = new dl_rti_notice();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Label lblheader = (Label)Master.FindControl("lbl_header");
            //lblheader.Text = "Edit Notice Board";

            if (!Page.IsPostBack)
            {
                bind_grid();
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            }
        }
        catch { }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    public void bind_grid()
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
      
        Notice_Entry ne = new Notice_Entry();

        try
        {
            ne.pending = "P";
            //ne.district_id = Session["Dist_Code"].ToString();
            dt = dl.pending_for_approval(ne);
            //if (dt.table.Rows.Count > 0)
            //{
            //    GridView1.DataSource = dt.table;
            //    GridView1.DataBind();
            //}
            //else
            //{
            //    btnapprove.Visible = false;
            //           }

            if (dt.table.Rows.Count > 0)
            {
                GridView1.DataSource = dt.table;
                GridView1.DataBind();
                double c = Convert.ToInt32(dt.table.Rows.Count.ToString());
                double cal = Math.Ceiling(c / 30);
                if (cal == 0)
                {
                    cal = 1;

                }

                lblTotalBoilers.Text = "Total Records : " + dt.table.Rows.Count.ToString() + " (Page : 1 to " + cal + ")";

            }
            else
            {
                btnapprove.Visible = false;
                GridView1.DataSource = dt.table;
                GridView1.DataBind();
                lblTotalBoilers.Text = "Total Records : " + dt.table.Rows.Count.ToString();
            }

        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("admin_NewsEdit_List_bind_grid()", ex);
            Utilities.MessageBoxShow(ex.Message);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind_grid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bind_grid();
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            // if (Session["Role_ID"].ToString() == "01")
            // this.MasterPageFile = "../MasterPage_Admin.master";
        }
        catch { }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        //dl_dmf_designation dl = new dl_dmf_designation();
        Notice_Entry ne = new Notice_Entry();
        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            ne.file_id = GridView1.DataKeys[gvrow.RowIndex].Values["file_id"].ToString();
            ne.noticeID = GridView1.DataKeys[gvrow.RowIndex].Values["Notice_Id"].ToString();
           // ne.district_id = Session["Dist_Code"].ToString();
            rb = dl.delete_notice_and_file(ne);

            if (rb.status == true)
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Notice Deleted Successfully");
                }
                else
                {

                    Utilities.MessageBoxShow("सूचना सफलतापूर्वक हटाई गई");
                }
              //  Utilities.MessageBoxShow("Notice Deleted Successfully");
                bind_grid();
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Failed to Deleted");
                }
                else
                {

                    Utilities.MessageBoxShow("सूचना हटाई नहीं जा सकी");
                }
              //  Utilities.MessageBoxShow("Failed to Deleted");
            }
        }

        catch (Exception ex)
        {

        }

    }

    protected void chkAllSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkAllSelect");
        foreach (GridViewRow row in GridView1.Rows)
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
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
       // dl_dmf_designation dl = new dl_dmf_designation();
        Notice_Entry ne = new Notice_Entry();


        try
        {

            if (GridView1.Rows.Count < 1)
            {
                Utilities.MessageBoxShow("No Record");
                return;
            }

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRowCheck = (row.Cells[11].FindControl("chk_approve") as CheckBox);
                    if (chkRowCheck.Checked == true)
                    {

                        ne.file_id = GridView1.DataKeys[row.RowIndex].Values["file_id"].ToString();
                        ne.noticeID = GridView1.DataKeys[row.RowIndex].Values["Notice_Id"].ToString();
                        ne.Status = "A";
                       // ne.district_id = Session["Dist_Code"].ToString();
                        rb = dl.approve_notice(ne);
                    }
                }
            }

            if (rb.status == true)
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Approve Successfully");
                }
                else
                {

                    Utilities.MessageBoxShow("सफलतापूर्वक स्वीकृत");
                }
               // Utilities.MessageBoxShow("approve successfully");
                bind_grid();
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Approve Failed");
                }
                else
                {

                    Utilities.MessageBoxShow("अस्वीकृत");
                }
              //  Utilities.MessageBoxShow(" Not approve successfully");
            }

        }
        catch (Exception ex)
        {

        }

    }
    protected void exp_excel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=pending_for_approval.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridView1.AllowPaging = false;
            this.bind_grid();

            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hypl = (HyperLink)e.Row.Cells[1].Controls[0];
            //string chkhyperlink = GridView1.DataKeys[e.Row.RowIndex].Values["hyperlink"].ToString();
            //string fileType = GridView1.DataKeys[e.Row.RowIndex].Values["file_type"].ToString();
            string fid = GridView1.DataKeys[e.Row.RowIndex].Values["file_id"].ToString();
            string url = e.Row.Cells[9].Text;
            string chkhyperlink = e.Row.Cells[7].Text;
            string fileType = e.Row.Cells[8].Text;
            if (chkhyperlink.ToString() == "Yes")
            {
                if (fileType.ToString() == "Internal")
                {
                    hypl.NavigateUrl = "../admin/notice_download.aspx?fid=" + fid;
                    hypl.Target = "_blank";
                }
                else if (fileType.ToString() == "External")
                {
                    hypl.NavigateUrl = url;
                    hypl.Target = "_blank";
                }
            }
            else
            {
                hypl.Enabled = false;
                hypl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            }
        }
    }
}