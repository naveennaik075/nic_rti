using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_UserWelcome : BasePage  // System.Web.UI.Page
{
    Utilities ul = new Utilities();
    bl_login bl = new bl_login();
    dl_login dl = new dl_login();
    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
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
               // griddatabind();
            }
        }
        griddatabind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        griddatabind();

    }
   public  void griddatabind()
    {
        bl.UserID = Session["username"].ToString();
        dt = dl.Select_Rti_By_User(bl);

        int row = dt.table.Rows.Count;
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
            lbl_count.Text = "Total RTI = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else {
            lbl_count.Text = "कुल आर. टी. आई. = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
        GridView1.DataSource = dt.table;
        GridView1.DataBind();

        //GridView1.DataSource = dt.table;
        //GridView1.DataBind();
    }

   protected void lnk_id_Click(object sender, EventArgs e)
   {
       string encrypt_b = ((LinkButton)sender).CommandArgument.ToString();
       string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
       Utilities ut = new Utilities();
       string reqid = ut.Encrypt_AES(encrypt_b, key);

           Response.Redirect("Detail_Rti.aspx?rtiid="+reqid+"");

   }
}