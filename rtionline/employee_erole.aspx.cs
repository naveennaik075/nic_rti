using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class employee_erole : BasePage // System.Web.UI.Page
{
    bl_login bl = new bl_login();
    dl_login dl = new dl_login();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
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
                if (Session["username"] == null)
                {
                    Response.Redirect("Logout.aspx");
                }
                else
                {
                    string ro = Session["checkroll"].ToString();
                    if (ro == "true")
                    {
                        rfv_role.Enabled = false;
                        bindrole();
                        div_roll.Visible = false;
                        binddesignation();
                        //
                        //Response.Redirect(Session["WelcomePage"].ToString());

                    }
                    else if (ro == "false")
                    {
                        bindrole();
                        div_roll.Visible = true;
                        binddesignation();
                    }
                }
            }
        }
        catch (NullReferenceException)
        {
            if (Session["language"].ToString() == "en-GB")
            {
                Utilities.MessageBox_UpdatePanel_Redirect(upd1, "Your Session Has ended, Please Login Again.", "logOut.aspx");

            }
            else
            {
                Utilities.MessageBox_UpdatePanel_Redirect(upd1, "आपका सत्र समाप्त हो गया है, कृपया फिर से लॉगिन करें", "logOut.aspx");
            }
           // Utilities.MessageBox_UpdatePanel_Redirect(upd1, "Your Session Has ended Please Login Again.", "logOut.aspx");
          //  Response.Redirect("logout.aspx");
        }
    }
    protected void ddl_role_SelectedIndexChanged(object sender, EventArgs e)
    {
        bl.RollID = ddl_role.SelectedValue;
        Session["role"] = ddl_role.SelectedValue;
        binddesignation();
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Page.IsValid)
            {
                Session["role"] = ddl_role.SelectedValue;
                Session["EmpMapID"] = ddl_designatiom.SelectedValue;
                bl.UserID = Session["username"].ToString();
                bl.Active = Session["EmpMapID"].ToString();
                bl.RollID = Session["role"].ToString();
                rd = dl.select_welcomepage(bl);
                Response.Redirect(rd.table.Rows[0]["page"].ToString());
            }
        }
    }
    public void bindrole()
    {
        bl.UserID = Session["username"].ToString();
        rd = dl.select_roll_emp(bl);
        ddl_role.DataSource = rd.table;
        ddl_role.DataTextField = "name";
        ddl_role.DataValueField = "id";
        ddl_role.DataBind();
        if (rd.table.Rows.Count > 1)
        {
            ddl_role.Items.Insert(0, new ListItem("----Select-----", "0"));
        }
        else
        {
            ddl_role.SelectedIndex = 0;
            Session["role"] = rd.table.Rows[0]["id"];
        }
    }
    public void binddesignation()
    {
        bl.UserID = Session["username"].ToString();
        bl.RollID = ddl_role.SelectedValue;
        rd = dl.select_deg_emp(bl);
        ddl_designatiom.DataSource = rd.table;
        ddl_designatiom.DataTextField = "name";
        ddl_designatiom.DataValueField = "ID";
        ddl_designatiom.DataBind();
        if (rd.table.Rows.Count > 1)
        {
            ddl_designatiom.Items.Insert(0, new ListItem("----Select-----", "0"));
        }
        else if (rd.table.Rows.Count == 1)
        {
            Session["EmpMapID"] = rd.table.Rows[0]["ID"].ToString();
            //Response.Redirect(rd.table.Rows[0]["page"].ToString());

        }
        else
        {
            ddl_designatiom.Items.Insert(0, new ListItem("----Select-----", "0"));
        }

    }
    protected void btnlogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("LogOut.aspx");

    }
}