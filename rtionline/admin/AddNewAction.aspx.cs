using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addNewAction : BasePage // System.Web.UI.Page
{
    dl_Action dl = new dl_Action();
    bl_Action bl = new bl_Action();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       // if (!Page.IsPostBack)
       // {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Session["username"] == null)
            {
                Response.Redirect("../LogOut.aspx");
            }
            else
            {
                bl.Userid = Session["username"].ToString();
                rd = dl.select_admin_info(bl);
                if (rd.table.Rows.Count > 0)
                {
                    bl.Role = rd.table.Rows[0]["RollID"].ToString();
                    if (bl.Role == "1")
                    {
                        bindgrd_Action();
                    }
                    else
                    {
                        if (Session["language"].ToString() == "en-GB")
                        {

                            Utilities.MessageBoxShow_Redirect("You Are Not Authorised For Adding New Action.", "../admin/loginAdmin.aspx");
                        }
                        else
                        {

                            Utilities.MessageBoxShow_Redirect("आप नई कार्रवाई जोड़ने के लिए अधिकृत नहीं हैं|", "../admin/loginAdmin.aspx");
                        }
                       // Utilities.MessageBoxShow_Redirect("You Are Not Authorised For Add New Action", "../admin/loginAdmin.aspx");
                    }
                }

            }
        //} PostBack
    }
    public string ddl_id()
    {
        ReturnClass.ReturnDataTable dtt = new ReturnClass.ReturnDataTable();
        int i;
        try
        {


            dtt = dl.Max_id();
            if (dtt.table.Rows.Count > 0)
            {
                bl.Ddl_id = dtt.table.Rows[0]["id"].ToString();
                if (bl.Ddl_id == "0")
                {
                    i = Convert.ToInt32(bl.Ddl_id) + 1;
                    bl.Ddl_id = i.ToString();
                }
                else
                {
                    i = Convert.ToInt32(bl.Ddl_id) + 1;
                    bl.Ddl_id = i.ToString();

                }
            }
            else
            {
                i = Convert.ToInt32(bl.Ddl_id) + 1;
                bl.Ddl_id = i.ToString();
            }

        }
        catch
        {
            bl.Ddl_id = "1";
        }
        return bl.Ddl_id;
    }

  
    protected void grd_Action_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Action.PageIndex = e.NewPageIndex;
        bindgrd_Action();
    }
    public void bindgrd_Action()
    {
        rd = dl.select_action();
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
            lbl_count.Text = "Total Action = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल कार्यवाही = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
        //lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        grd_Action.DataSource = rd.table;
        grd_Action.DataBind();
    }

    protected void btn_save_Click1(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            //Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            bl.Ddl_id = Convert.ToString(ddl_id());

            bl.Actionname = txt_action_name.Text;
            bl.DDL_Name_Value = txtaction_id.Text;
            bl.Remark = txtremark.Text;
            bl.DisplayOrder = txtdisplay_order.Text;
            bl.Category = "permission";

            rb = dl.insertAction(bl);
            if (rb.status == true)
            {

                txt_action_name.Text = "";
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Action Inserted Successfully");
                }
                else
                {

                    Utilities.MessageBoxShow(" कार्य सफलतापूर्वक दर्ज की गई");
                }
               // Utilities.MessageBoxShow("Action Enter Successfully");
                bindgrd_Action();
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Action Not Inserted");
                }
                else
                {

                    Utilities.MessageBoxShow(" कार्य दर्ज नहीं की गई");
                }
                //Utilities.MessageBoxShow("Action Not Inserted");
            }

        }

    }
    protected void lnkdelete_Click(object sender, EventArgs e)
    {
        ReturnClass.ReturnDataTable rb = new ReturnClass.ReturnDataTable();
       
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;


            bl.DDL_List_ID =grd_Action.DataKeys[gvrow.RowIndex].Values["DDL_List_ID"].ToString();
            rb = dl.delete_action(bl);
            if (rb.status == true)
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Delete successfully");
                }
                else
                {

                    Utilities.MessageBoxShow("सफलतापूर्वक हटाए गए");
                }
               // Utilities.MessageBoxShow("Delete successfully");
                txtaction_id.Text = "";
                txt_action_name.Text = "";
                txtremark.Text = "";
                txtdisplay_order.Text = " ";
                bindgrd_Action();
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBoxShow("Failed to Delete ");
                }
                else
                {

                    Utilities.MessageBoxShow("हटाने में विफल ");
                }
              //  Utilities.MessageBoxShow("Failed to Delete ");
            }
        
    }
   
}
