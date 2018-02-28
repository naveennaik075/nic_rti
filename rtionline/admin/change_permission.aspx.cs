using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dio_Default : System.Web.UI.Page
{
    bl_empMap bl = new bl_empMap();
    dl_empMap dl = new dl_empMap();
    bl_emp bl1 = new bl_emp();
    dl_emp dl1 = new dl_emp();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    Utilities UL = new Utilities();
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
                Response.Redirect("~/Logout.aspx");
            }
            else
            {
                BaseDepartmentBind1();
                bind_district1();
                bl.User_id = Session["username"].ToString();
                rd = dl.select_admin_info(bl);
                if (rd.table.Rows.Count > 0)
                    bl1.Role = rd.table.Rows[0]["RollID"].ToString();
                bl1.State_id = rd.table.Rows[0]["state_id"].ToString();
                bl1.District_id = rd.table.Rows[0]["district_id"].ToString();
                bl1.Base_department_id = rd.table.Rows[0]["base_department_id"].ToString();
                //   bl1.NewOfficeCode = rd.table.Rows[0]["NewOfficeCode"].ToString();
                h_roll_id.Value = bl1.Role;
                if (bl1.Role == "1")
                {
                    h_roll_id.Value = bl1.Role;
                }
                else if (bl1.Role == "4")
                {
                    ddl_district1.Enabled = false;
                    h_district_id.Value = bl1.District_id;
                    ddl_district1.SelectedValue = bl1.District_id;
                }
                else if (bl1.Role == "5")
                {
                    ddl_department1.Enabled = false;
                    ddl_district1.Enabled = false;
                    h_district_id.Value = bl1.District_id;
                    ddl_district1.SelectedValue = bl1.District_id;
                    h_department_code.Value = bl1.Base_department_id;
                    ddl_department1.SelectedValue = bl1.Base_department_id;
                    h_offfice_code.Value = bl1.NewOfficeCode;
                }
                else
                {
                    Utilities.MessageBox_UpdatePanel_Redirect(udp, "You Are Not Authorised For Take Action On Permission", "../logout.aspx");
                }
                //bind_employee();
            }

        }
    }

    protected void ddl_employee1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bl1.Office_mapping_id = ddl_employee1.SelectedValue;
        rd = dl1.permission_employee(bl1);
        if (rd.table.Rows.Count > 0)
        {
            div_permission.Visible = true;
            //approve , pm.review as review, pm.dispose as dispose, pm.forward as forward , pm.reject as reject
            rdb_approve.SelectedValue = rd.table.Rows[0]["approve"].ToString();
            rdb_review.SelectedValue = rd.table.Rows[0]["review"].ToString();
            rdb_dispose.SelectedValue = rd.table.Rows[0]["dispose"].ToString();
            rdb_forward.SelectedValue = rd.table.Rows[0]["forward"].ToString();
            rdb_reject.SelectedValue = rd.table.Rows[0]["reject"].ToString();
        }
        //  bind_employee();

    }

    protected void ddl_department1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_employee();
    }

    protected void ddl_district1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_employee();
    }
    public void bind_employee()
    {
        ddl_employee1.Items.Clear();
        bl1.Role = h_roll_id.Value;
        if (h_roll_id.Value == "1")
        {
            bl1.District = ddl_district1.SelectedValue;
            bl1.Base_department = ddl_department1.SelectedValue;
                    }
        else if (h_roll_id.Value == "4")
        {
            bl1.District = h_district_id.Value;
            bl1.Base_department = ddl_department1.SelectedValue;
            bl1.Officelevelcode = "0";
        }
        else if (h_roll_id.Value == "5")
        {
            bl1.District = h_district_id.Value;
            bl1.Base_department = h_department_code.Value;
            bl1.Officelevelcode = "0";
        }
        rd = dl1.employee_permission(bl1);
        ddl_employee1.DataSource = rd.table;
        ddl_employee1.DataTextField = "name";
        ddl_employee1.DataValueField = "emp_map_id";
        ddl_employee1.DataBind();
        ddl_employee1.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void bind_district1()        // user district
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        ddl_district1.Items.Clear();
        bl.State = "22";//DDL_State.SelectedValue; //22 for Chhattisgarh
        rd = dl.BindDistrict(bl);
        ddl_district1.DataSource = rd.table;
        ddl_district1.DataTextField = "District_Name";
        ddl_district1.DataValueField = "District_ID";
        ddl_district1.DataBind();
        // ddl_district.Items.Add(new ListItem("Choose District", "0"));
        ddl_district1.Items.Insert(0, new ListItem("--Select District--", "0"));

    }
    public void BaseDepartmentBind1()
    {        //string dept = DDL_Department.SelectedItem.Value;
        ReturnClass.ReturnDataTable dt = dl.Get_BaseDepartment(bl);// ("SELECT dept_id, dept_name FROM basedepartment order by dept_name ");
        ddl_department1.DataSource = dt.table;
        ddl_department1.DataTextField = "dept_name";
        ddl_department1.DataValueField = "dept_id";
        ddl_department1.DataBind();
        ddl_department1.Items.Insert(0, new ListItem("--Select Department--", "0"));
    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Page.IsValid)
            {
                bl1.Office_mapping_id = ddl_employee1.SelectedValue;
                bl1.Approve = rdb_approve.SelectedValue;
                bl1.Reject = rdb_reject.SelectedValue;
                bl1.Dispose = rdb_dispose.SelectedValue;
                bl1.Forward = rdb_dispose.SelectedValue;
                bl1.Review = rdb_review.SelectedValue;
                bl1.Client_ip = UL.GetClientIpAddress(this.Page);
                bl1.ClientOS = Utilities.System_Info(this.Page);
                HttpBrowserCapabilities browse = Request.Browser;
                //  bl.ClientBrowser = browse.Browser;
                bl1.ClientBrowser = Request.Browser.Browser;
                bl1.Useragent = Request.UserAgent.ToString();
                bl1.User_id = Session["username"].ToString();
                rb = dl1.Update_permission(bl1);
                if (rb.status == true)
                {
                    Utilities.MessageBox_UpdatePanel(udp, "Permissions of employee Updated Succesfully");
                    div_permission.Visible = false;
                    ddl_department1.SelectedValue = "0";
                    ddl_district1.SelectedValue = "0";
                    ddl_employee1.Items.Clear();
                    ddl_employee1.Items.Insert(0, "-----Select-----");
                }
                else
                {
                    Utilities.MessageBox_UpdatePanel(udp, "Permission For Employee Updation Fail");
                }
            }
        }
    }
}