using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginOfficerDetail : BasePage //System.Web.UI.Page
{
    bl_rti_emp bl = new bl_rti_emp();
    dl_rti_emp dl = new dl_rti_emp();
    ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                if (Session["username"] != null)
                {
                    bl.User_id = Session["username"].ToString();
                    bl.Office_map_id = Session["EmpMapID"].ToString();
                    bind_Count();
                    BindGridView();
                }
                else
                {
                    Response.Redirect("../Logout.aspx");
                }

                rt = dl.Get_EmpDesName(bl);
                if (rt.table.Rows.Count > 0)
                {
                    lbl_UserName.Text = rt.table.Rows[0]["Name_en"].ToString().ToUpper();
                }

            }
            BindGridView();
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("../LogOut.aspx");

        }
    }
    public void bind_Count()
    {
        bl.Office_map_id = Session["EmpMapID"].ToString();
        //bl.RequestStatus = "REG";  // It will give the Total RTI
        //rt = dl.GetRequestCount(bl);
        //
        //bl.RequestStatus = "PEN";
        //rt = dl.GetRequestCount(bl);
        //
        bl.RequestStatus = "CLT";
        rt = dl.GetRequestCount(bl);
        lbl_Request_Registered.Text = rt.table.Rows[0]["total"].ToString();        
        lbl_Request_DisposedOf.Text = rt.table.Rows[0]["complete"].ToString();
        lbl_Request_Pending.Text = rt.table.Rows[0]["pending"].ToString();

    }
    protected void BindGridView()
    {
        bl.Office_map_id = Session["EmpMapID"].ToString();
        rt = dl.Get_RTI_Data(bl);
        int row = rt.table.Rows.Count;
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
            lbl_count.Text = "कुल आर. टी. आई. = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
       // lbl_count.Text = "Total Rti = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = rt.table;
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void lnkrti_id_history_Click(object sender, EventArgs e)
    {
        string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
        // string key = "mk";
        Utilities ut = new Utilities();
        Utilities utt = new Utilities();
        string encrypt_rti_id = ((LinkButton)sender).CommandArgument.ToString();
        bl.Rti_id = encrypt_rti_id;
        ReturnClass.ReturnBool rb = dl.Set_IsNew(bl);
        string encrypt_mapid = Session["EmpMapID"].ToString();
        //encrypt_mapid = "2017000000";  // For testing only
        bl.Rti_id = ut.Encrypt_AES(encrypt_rti_id, key);
        bl.Office_map_id = utt.Encrypt_AES(encrypt_mapid, key);
        //bl.Rti_id = ((LinkButton)sender).CommandArgument.ToString();
        //bl.Office_map_id = Session["EmpMapID"].ToString();


        Response.Redirect("actionHistory.aspx?rtiid=" + bl.Rti_id + "&empid=" + Server.UrlEncode(bl.Office_map_id));
    }


    protected void lnkrti_id_Click(object sender, EventArgs e)
    {
        string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
       // string key = "mk";
        Utilities ut = new Utilities();
        Utilities utt = new Utilities();
        string encrypt_rti_id = ((LinkButton)sender).CommandArgument.ToString();
        bl.Rti_id = encrypt_rti_id;
        ReturnClass.ReturnBool rb = dl.Set_IsNew(bl);
        string encrypt_mapid = Session["EmpMapID"].ToString();
        //encrypt_mapid = "2017000000";  // For testing only
        bl.Rti_id = ut.Encrypt_AES(encrypt_rti_id, key);
        bl.Office_map_id = utt.Encrypt_AES(encrypt_mapid, key);
        //bl.Rti_id = ((LinkButton)sender).CommandArgument.ToString();
        //bl.Office_map_id = Session["EmpMapID"].ToString();
       

        Response.Redirect("action.aspx?rtiid=" + bl.Rti_id + "&empid=" + Server.UrlEncode(bl.Office_map_id));
    }
    protected void GV_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField cell = e.Row.FindControl("lbl_Isnew") as HiddenField;
            string isnew = cell.Value;
            if (isnew == "Y")
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
                for (int i = 0; i <= 10; i++)
                {
                    e.Row.Cells[i].CssClass = "gridCellBoldRed";
                }
            }
            string completed = DataBinder.Eval(e.Row.DataItem, "rti_status").ToString();
            if (completed == "Completed")
            {
                e.Row.Cells[10].Enabled = false;
                e.Row.Cells[10].Text = "Done";
            }
        }
    } // End of OnrowDatabound

}