using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nodalofficer : System.Web.UI.Page
{
    dl_Dio dl = new dl_Dio();
    bl_Dio bl = new bl_Dio();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
           if (Session["username"] == null)
        {
            Response.Redirect("../LogOut.aspx");
        }

           if (!Page.IsPostBack)
           {
               bl.Userid = Session["username"].ToString();
              rd= dl.select_admin_info(bl);
              lbl_name.Text = rd.table.Rows[0]["Name_en"].ToString().ToUpper();
           }
    }
}