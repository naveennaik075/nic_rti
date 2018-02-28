using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class Employee : BasePage //System.Web.UI.Page
{

    bl_emp bl = new bl_emp();
    dl_emp dl = new dl_emp();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    Utilities ul = new Utilities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Session["username"] == null)
            {
                Response.Redirect("~/LogOut.aspx");
            }
            else
            {
                bl.Userid = Session["username"].ToString();
                bl.Emp_code = Session["EmpMapID"].ToString();
                bl.Role_id = Session["role"].ToString();
                rd = dl.select_admin_info(bl);

                if (rd.table.Rows.Count > 0)
                {
                    bl.Role_id = Session["role"].ToString();
                    //Session["role"] = bl.Role_id.ToString();

                    bl.State_id = rd.table.Rows[0]["state_id"].ToString();
                    bl.District_id = rd.table.Rows[0]["district_id"].ToString();
                    bl.Base_department_id = rd.table.Rows[0]["base_department_id"].ToString();
                    bl.NewOfficeCode = rd.table.Rows[0]["office_id"].ToString();
                    bind_state_code();
                    ddl_state.SelectedValue = bl.State_id;
                    bind_district_code();
                    ddl_district.SelectedValue = bl.District_id;
                    bind_department_id();
                    ddl_department.SelectedValue = bl.Base_department_id;
                    bind_office();
                    ddl_office.SelectedValue = bl.NewOfficeCode;


                    if (bl.Role_id == "1")
                    {

                        lbl_office.Visible = true;
                        ddl_office.Visible = true;

                    }
                    else if (bl.Role_id == "5")
                    {

                        lbl_office.Visible = true;
                        ddl_office.Visible = true;
                        ddl_district.Enabled = false;
                        ddl_district.SelectedValue = bl.District_id;
                        ddl_district.Enabled = false;
                        ddl_department.Enabled = false;
                        ddl_department.SelectedValue = bl.Base_department_id;
                        ddl_department.Enabled = false;
                        ddl_office.Enabled = false;
                        ddl_office.SelectedValue = bl.NewOfficeCode;
                        ddl_office.Enabled = false;

                    }
                    else if (bl.Role_id == "4")
                    {
                        lbl_office.Visible = true;
                        ddl_office.Visible = true;
                        ddl_district.Enabled = false;
                        ddl_district.SelectedValue = bl.District_id;
                        ddl_district.Enabled = false;

                    }


                    BindGridView();

                }
            }
        }
    }
    protected void BindGridView()
    {
        bl.State_id = ddl_state.SelectedValue;
        bl.District_id = ddl_district.SelectedValue;
        bl.Department = ddl_department.SelectedValue;
        bl.Office_id = ddl_office.SelectedValue;

        rd = dl.Bind_grid(bl);

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
            lbl_count.Text = "Total Employee = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल कर्मचारी = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
        //lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = rd.table;
        GridView1.DataBind();

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();


    }
    public void clear()
    {
        txt_name.Text = "";
        ddl_Active.SelectedValue = "0";
        txt_mobile.Text = "";
        txt_email.Text = "";
        ddl_department.SelectedValue = "0";

        ddl_office.SelectedValue = "0";


    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {

       // if (btn_submit.Text == "Submit")
       // {
            bl.Emp_id = dl.GetEmpCode();
            bl.User_id = Session["username"].ToString();
            bl.Name_en = txt_name.Text;
            bl.Name_hi = "";
            bl.Emailid = txt_email.Text;
            bl.Mobileno = txt_mobile.Text;
            bl.District_id = ddl_district.SelectedValue;

            bl.State_id = ddl_state.SelectedValue;
            bl.Department = ddl_department.SelectedValue;
            bl.Office_id = ddl_office.SelectedValue;
            bl.NewOfficeCode = ddl_office.SelectedValue;
            bl.Emp_date_time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            bl.Client_ip = ul.GetClientIpAddress(this.Page);


            bl.Active = ddl_Active.SelectedValue;
            rb = dl.Insert_emp(bl);

            if (rb.status == true)
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Registration Successsful");
                }
                else
                {

                    Utilities.MessageBox_UpdatePanel(UpdatePanel1, "पंजीकरण सफल हुआ");
                }
               // Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Registration Successsful");

                BindGridView();
                clear();

            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Registration UnSuccesssful");
                }
                else
                {

                    Utilities.MessageBox_UpdatePanel(UpdatePanel1, "पंजीकरण असफल हुआ");
                }
                //Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Registration Unsuccessful");

            }
       // }

    }



    public void bind_state_code()
    {
        rd = dl.state_code();
        ddl_state.DataSource = rd.table;
        ddl_state.DataTextField = "state_name_e";
        ddl_state.DataValueField = "state_id";
        ddl_state.DataBind();
        ddl_state.Items.Insert(0, new ListItem("Select", "0"));


    }
    public void bind_district_code()
    {
        ddl_district.Items.Clear();
        bl.StateCode = ddl_state.SelectedValue;
        rd = dl.district_code(bl);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";

        ddl_district.DataBind();
        ddl_district.Items.Insert(0, new ListItem("Select", "0"));

    }
    public void bind_department_id()
    {
        ddl_department.Items.Clear();
        bl.State_id = ddl_state.SelectedValue;
        bl.District_id = ddl_district.SelectedValue;
        rd = dl.department_id(bl);
        ddl_department.DataSource = rd.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";
        ddl_department.DataBind();
        ddl_department.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void bind_office()
    {
        bl.StateCode = ddl_state.SelectedValue;
        bl.DistrictCodeNew = ddl_district.SelectedValue;
        bl.Department = ddl_department.SelectedValue;
        bl.Role_id = Session["role"].ToString();
        bl.Emp_code = Session["EmpMapID"].ToString();
        if (bl.Role_id == "1")
        {
            rd = dl.office(bl);
        }
        else
        {
            rd = dl.office1(bl);
        }
        ddl_office.DataSource = rd.table;
        ddl_office.DataTextField = "OfficeName";
        ddl_office.DataValueField = "NewOfficeCode";
        ddl_office.DataBind();
        ddl_office.Items.Insert(0, new ListItem("Select", "0"));

    }



    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        bl.State_id = ddl_state.SelectedValue;
        bind_district_code();
        bind_department_id();
        bind_office();

        BindGridView();
    }
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {

        bind_department_id();
        bind_office();

        BindGridView();
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {

        bind_office();

        BindGridView();

    }
    protected void ddl_office_SelectedIndexChanged(object sender, EventArgs e)
    {
        bl.State_id = ddl_state.SelectedValue;
        bl.District_id = ddl_district.SelectedValue;
        bl.Department = ddl_department.SelectedValue;
        bl.Office_id = ddl_office.SelectedValue;

        BindGridView();
    }

    protected void ddl_grid_state_SelectedIndexChanged(object sender, EventArgs e)
    {

        bind_grid_district_code();
        bind_grid_office();

    }
    public void bind_grid_district_code()
    {
        DropDownList ddl_district = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_district");
        DropDownList ddl_state = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_state");
        bl.StateCode = ddl_state.SelectedValue;
        rd = dl.district_code(bl);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";

        ddl_district.DataBind();
        ddl_district.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    public void bind_grid_office()
    {
        DropDownList ddl_office = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_office");
        DropDownList ddl_district = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_district");
        DropDownList ddl_state = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_state");
        DropDownList ddl_department = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_department_grid");
        bl.StateCode = ddl_state.SelectedValue;
        bl.DistrictCodeNew = ddl_district.SelectedValue;
        bl.Department = ddl_department.SelectedValue;
        rd = dl.office(bl);
        ddl_office.DataSource = rd.table;
        ddl_office.DataTextField = "OfficeName";
        ddl_office.DataValueField = "NewOfficeCode";

        ddl_office.DataBind();
        ddl_office.Items.Insert(0, new ListItem("--Select--", "0"));
    }



    protected void ddl_grid_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_grid_department_id();
        bind_grid_office();

    }
    public void bind_grid_department_id()
    {
        DropDownList ddl_department = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_department_grid");

        rd = dl.department_id(bl);
        ddl_department.DataSource = rd.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";

        ddl_department.DataBind();
        ddl_department.Items.Insert(0, new ListItem("--Select--", "0"));
    }



    protected void ddl_grid_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_grid_office();

    }


    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        clear();
    }



    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        btn_update.Visible = true;
        btn_submit.Visible = false;
        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];

        TextBox txt_name = (TextBox)row.FindControl("txt_Emp_Name") as TextBox;
        bl.Name_en = txt_name.Text;

        TextBox txt_mobile = (TextBox)row.FindControl("txt_mob_num") as TextBox;
        bl.Mobile_no = txt_mobile.Text;

        TextBox txt_email = (TextBox)row.FindControl("txt_email") as TextBox;
        bl.Email_id = txt_email.Text;

        string state = (GridView1.Rows[e.RowIndex].FindControl("ddl_state") as DropDownList).SelectedItem.Value;

        string district = (GridView1.Rows[e.RowIndex].FindControl("ddl_district") as DropDownList).SelectedItem.Value;
        string department = (GridView1.Rows[e.RowIndex].FindControl("ddl_department_grid") as DropDownList).SelectedItem.Value;
        string office = (GridView1.Rows[e.RowIndex].FindControl("ddl_office") as DropDownList).SelectedItem.Value;



        bl.User_id = Session["username"].ToString();
        bl.Name_hi = "";

        bl.District_id = district;
        bl.State_id = state;
        bl.Department = department;
        bl.NewOfficeCode = office;

        bl.Emp_id = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();

        bl.Active = "Y";
        rb = dl.update(bl);
        if (rb.status == true)
        {
            if (Session["language"].ToString() == "en-GB")
            {

                Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Update Successsfully");
            }
            else
            {

                Utilities.MessageBox_UpdatePanel(UpdatePanel1, "अपडेट सफल हुआ");
            }
           // Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Update Successsfully");
        }
        else
        {
            if (Session["language"].ToString() == "en-GB")
            {

                Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Update UnSuccesssfully");
            }
            else
            {

                Utilities.MessageBox_UpdatePanel(UpdatePanel1, "अपडेट असफल हुआ");
            }
            //Utilities.MessageBox_UpdatePanel(UpdatePanel1, "update Unsuccessful");

        }
        GridView1.EditIndex = -1;
        BindGridView();
        bind_office();
        bind_district_code();
        bind_department_id();
        btn_update.Visible = false;
        btn_submit.Visible = true;
    }


    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            {


                DropDownList ddl_state = (DropDownList)e.Row.FindControl("ddl_state");
                rd = dl.state_code();
                ddl_state.DataSource = rd.table;
                ddl_state.DataTextField = "state_name_e";
                ddl_state.DataValueField = "state_id";
                ddl_state.DataBind();
                ddl_state.Items.Insert(0, new ListItem("Select", "0"));
                ddl_state.SelectedValue = GridView1.DataKeys[e.Row.RowIndex].Values["state_id"].ToString();
            }
        if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddl_district = (DropDownList)e.Row.FindControl("ddl_district");
            DropDownList ddl_state = (DropDownList)e.Row.FindControl("ddl_state");
            bl.StateCode = ddl_state.SelectedValue;
            rd = dl.district_code(bl);
            ddl_district.DataSource = rd.table;
            ddl_district.DataTextField = "District_Name";
            ddl_district.DataValueField = "District_ID";

            ddl_district.DataBind();
            ddl_district.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl_district.SelectedValue = GridView1.DataKeys[e.Row.RowIndex].Values["dist_id"].ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddl_department = (DropDownList)e.Row.FindControl("ddl_department_grid");

            rd = dl.department_id(bl);
            ddl_department.DataSource = rd.table;
            ddl_department.DataTextField = "dept_name";
            ddl_department.DataValueField = "dept_id";

            ddl_department.DataBind();
            ddl_department.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl_department.SelectedValue = GridView1.DataKeys[e.Row.RowIndex].Values["dept_id"].ToString();

        }
        if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddl_office = (DropDownList)e.Row.FindControl("ddl_office");
            DropDownList ddl_district = (DropDownList)e.Row.FindControl("ddl_district");
            DropDownList ddl_state = (DropDownList)e.Row.FindControl("ddl_state");
            DropDownList ddl_department = (DropDownList)e.Row.FindControl("ddl_department_grid");
            bl.StateCode = ddl_state.SelectedValue;
            bl.DistrictCodeNew = ddl_district.SelectedValue;
            bl.Department = ddl_department.SelectedValue;
            rd = dl.office(bl);
            ddl_office.DataSource = rd.table;
            ddl_office.DataTextField = "OfficeName";
            ddl_office.DataValueField = "NewOfficeCode";

            ddl_office.DataBind();
            ddl_office.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl_office.SelectedValue = GridView1.DataKeys[e.Row.RowIndex].Values["ofc_id"].ToString();
        }



    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;



        BindGridView();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindGridView();
    }

}

