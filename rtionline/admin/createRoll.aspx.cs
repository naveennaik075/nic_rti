using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dio_create_roll : BasePage //System.Web.UI.Page
{
    dl_Dio dl = new dl_Dio();
    bl_Dio bl = new bl_Dio();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
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
                //bl.Userid = Session["username"].ToString();
                //rd = dl.select_admin_info(bl);
                //if (rd.table.Rows.Count > 0)
                //{
                //   // bl.Role = rd.table.Rows[0]["RollID"].ToString();
                    bl.Role = Session["role"].ToString();
                    if (bl.Role == "1")
                    {
                        bindgrd_Role();
                    }
                    else
                    {
                        if (Session["language"].ToString() == "en-GB")
                        {

                            Utilities.MessageBoxShow_Redirect("You Are Not Authorised For Create New Roll.", "../admin/LoginAdmin.aspx");
                        }
                        else
                        {

                            Utilities.MessageBoxShow_Redirect("आप नई भूमिका जोड़ने के लिए अधिकृत नहीं हैं|", "../admin/LoginAdmin.aspx");
                        }
                       // Utilities.MessageBoxShow_Redirect("You Are Not Authorised For Create Roll  ", "../admin/LoginAdmin.aspx");
                    }
                //}
            }
        } // पोस्टबेक
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            bl.RollName = txt_role_name.Text;
            bl.Welcomepage = txt_de_page.Text;
            bl.Active = ddl_active.SelectedValue;
            bl.Role = dl.role();
            rb = dl.insertroll(bl);
            if (rb.status == true)
            {
                txt_de_page.Text = "";
                txt_role_name.Text = "";
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Roll Inserted Successfully");
                }
                else
                {

                    Utilities.MessageBoxShow(" भूमिका सफलतापूर्वक दर्ज की गई");
                }
               // Utilities.MessageBoxShow("Roll Enter Successfully");
                bindgrd_Role();
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Roll Not Inserted");
                }
                else
                {

                    Utilities.MessageBoxShow(" भूमिका दर्ज नहीं की गई");
                }
              //  Utilities.MessageBoxShow("Roll Not Inserted");
            }

        }
    }
    protected void grd_Office_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Office.PageIndex = e.NewPageIndex;
        bindgrd_Role();
    }
    public void bindgrd_Role()
    {
        rd = dl.select_role();
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
        if (Session["language"].ToString() == "en-GB")
        {
            lbl_count.Text = "Total Role = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल भूमिका = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
        //lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";        
             grd_Office.DataSource = rd.table;
        grd_Office.DataBind();
    }
}