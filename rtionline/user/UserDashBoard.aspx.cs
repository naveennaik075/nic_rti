using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class UserDashBoard : BasePage // System.Web.UI.Page
{
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    Utilities ul = new Utilities();
    bl_login bl = new bl_login();
    dl_login dl = new dl_login();
    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("../LogOut.aspx");
                }
                else
                {
                    bl.UserID = Session["username"].ToString();
                    dt = dl.Select_user_detail(bl);

                    if (ul.GetClientIpAddress(this.Page).ToString() == "::1")
                    {
                        lbl_ip.Text = "127.0.0.1";
                    }
                    else
                        lbl_ip.Text = ul.GetClientIpAddress(this.Page).ToString();
                    if (dt.table.Rows.Count > 0)
                    {
                        lbl_username.Text = dt.table.Rows[0]["Name_en"].ToString();

                    }
                }//End of Else session != null

              
            } // End of IsPostBack
            bl.UserID = Session["username"].ToString();
            dt = dl.Select_Rti_Count(bl);
            if (dt.table.Rows.Count > 0)
            {

                lbl_TotalRTI_count.Text = dt.table.Rows[0]["count1"].ToString();
                lbl_CompletedRTI_count.Text = dt.table.Rows[1]["count1"].ToString();
                lbl_UnderProcessRTI_count.Text = dt.table.Rows[2]["count1"].ToString();
                lbl_RejectedRTI_count.Text = dt.table.Rows[3]["count1"].ToString();
                lbl_RTIForClarification_count.Text = dt.table.Rows[4]["count1"].ToString();   
            }
                                
           
        }
        catch (NullReferenceException ex)
        {

        }
    }// End of Page Load


    protected void BindTotalRTIGridView()
    {
        bl.UserID = Session["username"].ToString();
        bl.Status = null;
        dt = dl.Select_Rti_By_User1(bl);
        int row = dt.table.Rows.Count;
        int page;
        if (row % 10 == 0)
        {
            page = row / 10;
        }
        else
        {
            page = row / 10;
            page = page + 1;
        }
        if (Session["language"].ToString() == "en-GB")
        {
            lbl_count.Text = "Total RTI = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल कर्मचारी = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
        //lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = dt.table;
        GridView1.DataBind();

    }
    protected void BindCompletedRTIView()
    {

        bl.UserID = Session["username"].ToString();
        bl.Status = "CLT";
        dt = dl.Select_Rti_By_User1(bl);
        int row = dt.table.Rows.Count;
        int page;
        if (row % 10 == 0)
        {
            page = row / 10;
        }
        else
        {
            page = row / 10;
            page = page + 1;
        }
        if (Session["language"].ToString() == "en-GB")
        {
            lbl_count.Text = "Total completed RTI = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल मैप्ड कर्मचारी = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
        //lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = dt.table;
        GridView1.DataBind();

    }
    protected void BindUnderProcessRTIView()
    {

        bl.UserID = Session["username"].ToString();
        bl.Status = "PEN";
        dt = dl.Select_Rti_By_User1(bl);

        int row = dt.table.Rows.Count;
        int page;
        if (row % 10 == 0)
        {
            page = row / 10;
        }
        else
        {
            page = row / 10;
            page = page + 1;
        }
        if (Session["language"].ToString() == "en-GB")
        {
            lbl_count.Text = "Total Pending RTI = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल विभाग = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
        //lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = dt.table;
        GridView1.DataBind();

    }
    protected void BindRejectedRTIView()
    {
        bl.UserID = Session["username"].ToString();
        bl.Status = "REJD";
        dt = dl.Select_Rti_By_User1(bl);

        int row = dt.table.Rows.Count;
        int page;
        if (row % 10 == 0)
        {
            page = row / 10;
        }
        else
        {
            page = row / 10;
            page = page + 1;
        }
        if (Session["language"].ToString() == "en-GB")
        {
            lbl_count.Text = "Total Rejected RTI = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल कार्यालय = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
       // lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = dt.table;
        GridView1.DataBind();

    }

    protected void BindRTIForClarificationView()
    {
        bl.UserID = Session["username"].ToString();
        bl.Status = "CLR";
        dt = dl.Select_Rti_By_User1(bl);

        int row = dt.table.Rows.Count;
        int page;
        if (row % 10 == 0)
        {
            page = row / 10;
        }
        else
        {
            page = row / 10;
            page = page + 1;
        }
        if (Session["language"].ToString() == "en-GB")
        {
            lbl_count.Text = "Total RTI for Clarification = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल कार्यालय = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
        // lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = dt.table;
        GridView1.DataBind();

    }
    protected void ShowTotalRTI_Click(object sender, EventArgs e)
    {
        h_gridtype.Value = "1";
        GridView1.Visible = true;
        lbl_count.Visible = true;
        BindTotalRTIGridView();
    }
    protected void ShowCompletedRTI_Click(object sender, EventArgs e)
    {
        h_gridtype.Value = "2";
        GridView1.Visible = true;
        lbl_count.Visible = true;
        BindCompletedRTIView();

    }

    protected void ShowUnderProcessRTI_Click(object sender, EventArgs e)
    {
        h_gridtype.Value = "3";
        GridView1.Visible = true;
        lbl_count.Visible = true;
        BindUnderProcessRTIView();
    }

    protected void ShowRejectedRTI_Click(object sender, EventArgs e)
    {
        h_gridtype.Value = "4";
        GridView1.Visible = true;   
        lbl_count.Visible = true;

        BindRejectedRTIView();

    }
    protected void ShowRTIForClarification_Click(object sender, EventArgs e)
    {
        h_gridtype.Value = "5";
        GridView1.Visible = true;
        lbl_count.Visible = true;
        BindRTIForClarificationView();

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        switch (h_gridtype.Value) {
            case "1" :
                      BindTotalRTIGridView();
                      break;
            case "2" :
                      BindCompletedRTIView();
                      break;
            case "3" :
                      BindUnderProcessRTIView();
                      break;
            case "4" :
                      BindRejectedRTIView();
                      break;
            case "5" :
                      BindRTIForClarificationView();
                      break;

        }
       
    }

    protected void lnk_id_Click(object sender, EventArgs e)
    {
        string encrypt_b = ((LinkButton)sender).CommandArgument.ToString();
        string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
        Utilities ut = new Utilities();
        string reqid = ut.Encrypt_AES(encrypt_b, key);
        if (h_gridtype.Value == "5")
        {
            Response.Redirect("RTI_Clarification.aspx?rtiid=" + reqid + "");
        }
        else
        {
            Response.Redirect("Detail_Rti.aspx?rtiid=" + reqid + "");
        }
    }

} // End of class

