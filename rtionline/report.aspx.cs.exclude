using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report : System.Web.UI.Page
{
  
    bl_report bl = new bl_report();
    dl_report dl = new dl_report();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    Utilities ul = new Utilities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDesignation();
            ddl_designation.SelectedValue = bl.Designation_id;
                BindGridView();
            
        }
    }
    protected void BindGridView()
    {
        bl.Designation_id = ddl_designation.SelectedValue;

        rd = dl.Bind_gridreport(bl);

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

        lbl_count.Text = "Total Employee = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = rd.table;
        GridView1.DataBind();

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();


    }
    public void BindDesignation()
    {
       
      
        ddl_designation.Items.Clear();

        rd = dl.GetDesignation(bl);
        ddl_designation.DataSource = rd.table;
        ddl_designation.DataTextField = "Designation_Name";
        ddl_designation.DataValueField = "Designation_ID";
        ddl_designation.DataBind();

        ddl_designation.Items.Insert(0, new ListItem("--Select Designation--", "0"));

    }

   

    protected void ddl_designation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView();
    }
}