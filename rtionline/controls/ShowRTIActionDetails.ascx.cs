using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_ShowRTIActionDetails :  System.Web.UI.UserControl
{
    bl_rti_emp bl = new bl_rti_emp();
    dl_rti_emp dl = new dl_rti_emp();
    ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["username"] != null)
            {
                    
                   
                    bl.User_id =  Session["username"].ToString();
                    string rollid = null;
                    rt = dl.GetRoll_ID(bl);
                    if (rt.table.Rows.Count > 0)
                    {
                        rollid = rt.table.Rows[0]["RollID"].ToString();
                        h_Roll_ID.Value = rollid;
                        if (rollid == "2")  // User  ROll ID
                        {
                            div_security.Visible = true;
                            txtScurity.Visible = true;
                            rfv_security.Enabled = true;

                        }
                        else {
                            div_security.Visible = false;
                            txtScurity.Visible = false;
                            rfv_security.Enabled = false;
                        }
                    }


            }
            else
            {
                Response.Redirect("../logout.aspx");
            }

           
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void BindGridView()
    {
        ReturnClass.ReturnDataTable rtt = new ReturnClass.ReturnDataTable();
        bl.Rti_id = txt_rti_id.Text;
        bl.Roll_id = h_Roll_ID.Value;
        if (h_Roll_ID.Value == "2") {
            bl.Security_code = txtScurity.Text;
        }
        rtt = dl.Get_Action_Detail(bl);

        int row = rtt.table.Rows.Count;
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

        lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = rtt.table;
        GridView1.DataBind();

    }

    protected void GV_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Label cell = e.Row.FindControl("lbl_Isnew") as Label;
            //string isnew = cell.Text;

            string isupload = DataBinder.Eval(e.Row.DataItem, "file_ID").ToString();
            if (isupload == "No File")
            {
                e.Row.Cells[11].Enabled = false;
                e.Row.Cells[11].Text = "No File";
            }

        }
    } // End of OnrowDatabound

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (btn_submit.Text == "Submit")
        {
            BindGridView();

        }


    }
}