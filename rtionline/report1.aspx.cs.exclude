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

            BindDepartmentView();
            
        }
    }
    protected void BindDepartmentView()
    {

        rd = dl.GetTotalDepartmentInGrid1();

        int row = rd.table.Rows.Count;
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

        lbl_count.Text = "Total Department = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GV_Department.DataSource = rd.table;
        GV_Department.DataBind();

    }
    protected void GV_Department_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Department.PageIndex = e.NewPageIndex;
        BindDepartmentView();

    }
  


}