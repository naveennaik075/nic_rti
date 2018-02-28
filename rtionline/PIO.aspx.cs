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

            BindOfficeView();
            
        }
    }
    protected void BindOfficeView()
    {
       
        rd = dl.GetTotalPIOInGrid(bl);

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

        lbl_count.Text = "Total PIO = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GV_PIO.DataSource = rd.table;
        GV_PIO.DataBind();

    }
    protected void GV_PIO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_PIO.PageIndex = e.NewPageIndex;
        BindOfficeView();


    }
  

  
   
  

}