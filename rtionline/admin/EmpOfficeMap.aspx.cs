using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class EmpOfficeMap : BasePage // System.Web.UI.Page
{
    dl_Dio dl1 = new dl_Dio();
    bl_Dio bl1 = new bl_Dio();
    bl_empMap bl = new bl_empMap();
    dl_empMap dl = new dl_empMap();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    Utilities ul = new Utilities();
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CheckRefresh"] = Server.UrlDecode(DateTime.Now.ToString());
            if (Session["username"] == null)
            {
                Response.Redirect("~/logout.aspx");
            }
            else
            {
                
                string userID = Session["username"].ToString();
                                bl.User_id = userID;
                bl.Role_id = Session["role"].ToString();
                bl.Emp_code1 = Session["EmpMapID"].ToString();
                string rollid = null;
                rd = dl.select_admin_info(bl);
                if (rd.table.Rows.Count > 0)
                {
                    rollid = Session["role"].ToString();
                    h_roll_id.Value = rollid;
                    if (rollid == "4" || rollid == "5")  // DIO ROll ID
                    {
                        ddl_dist_view.Visible = false;
                        rfv_district1.Enabled = false;
                        txt_dist_view.Visible = true;
                        txt_district1.Visible = true;
                        ddl_district1.SelectedValue = rd.table.Rows[0]["district_id"].ToString();
                        txt_district1.Text = rd.table.Rows[0]["District_Name_En"].ToString();
                        bind_district();
                        ddl_district.SelectedValue = rd.table.Rows[0]["district_id"].ToString();
                        ddl_district.Enabled = false;
                        rfv_district.Enabled = false;
                        if (rollid == "5")
                        {
                            BaseDepartmentBind1();
                            ddl_department1.SelectedValue = rd.table.Rows[0]["base_department_id"].ToString();
                            ddl_department1.Enabled = false;
                            rfv_department1.Enabled = false;
                            BaseDepartmentBind();
                            ddl_department.SelectedValue = rd.table.Rows[0]["base_department_id"].ToString();
                            ddl_department.Enabled = false;
                            RFV_department.Enabled = false;

                        }

                    }

                    else
                    {
                        ddl_dist_view.Visible = true;
                        rfv_district1.Enabled = true;
                        txt_dist_view.Visible = false;
                        txt_district1.Visible = false;
                    }
                    bind_district();
                    bind_district1();
                    BaseDepartmentBind();
                    BaseDepartmentBind1();
                    BindDesignation();
                    ChargeTypeBind();
                    OfficeCategoryBind();
                    BindOfficer1();
                    OfficeLevelBind();
                    OfficeBind();
                    bindpermission();
                    bindrole();
                    bind_GridView();
                }



            }

        }
    }  // End of Page Load
    public void ChargeTypeBind()
    {

        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        bl.Charge_type = "ChargeType";
        ReturnClass.ReturnDataTable dt = dl.GetCharge(bl);
        ddl_charge.DataSource = dt.table;
        ddl_charge.DataTextField = "DisplayName_en";
        ddl_charge.DataValueField = "DDL_Name_Value";
        ddl_charge.DataBind();
        ddl_charge.Items.Insert(0, new ListItem("--Select Charge--", "0"));


    }
    public void bind_district1()        // user district
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        ddl_district1.Items.Clear();
        bl.State = "22";//DDL_State.SelectedValue; //22 for Chhattisgarh
        rd = dl.BindDistrict(bl);
        ddl_district1.DataSource = rd.table;
        ddl_district1.DataTextField = "District_Name";
        ddl_district1.DataValueField = "District_ID";
        ddl_district1.DataBind();

        ddl_district1.Items.Insert(0, new ListItem("--Select District--", "0"));

    }
    public void bind_district()        // user district
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        ddl_district.Items.Clear();
        bl.State = "22";//DDL_State.SelectedValue; //22 for Chhattisgarh
        rd = dl.BindDistrict(bl);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";
        ddl_district.DataBind();

        ddl_district.Items.Insert(0, new ListItem("--Select District--", "0"));

    }
    public void BaseDepartmentBind1()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();

        ReturnClass.ReturnDataTable dt = dl.Get_BaseDepartment(bl);// ("SELECT dept_id, dept_name FROM basedepartment order by dept_name ");
        ddl_department1.DataSource = dt.table;
        ddl_department1.DataTextField = "dept_name";
        ddl_department1.DataValueField = "dept_id";
        ddl_department1.DataBind();
        ddl_department1.Items.Insert(0, new ListItem("--Select Department--", "0"));
    }
    public void BaseDepartmentBind()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();

        ReturnClass.ReturnDataTable dt = dl.Get_BaseDepartment(bl);// ("SELECT dept_id, dept_name FROM basedepartment order by dept_name ");
        ddl_department.DataSource = dt.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";
        ddl_department.DataBind();
        ddl_department.Items.Insert(0, new ListItem("--Select Department--", "0"));
    }
    public void OfficeCategoryBind()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();

        ReturnClass.ReturnDataTable dt = dl.Get_OfficeCategory(bl);// ("SELECT OfficeCategoryCode, OfficeCategoryName FROM officeCategory order by OfficeCategoryName ");
        ddl_office_category.DataSource = dt.table;
        ddl_office_category.DataTextField = "OfficeCategoryName";
        ddl_office_category.DataValueField = "OfficeCategoryCode";
        ddl_office_category.DataBind();
        ddl_office_category.Items.Insert(0, new ListItem("--Select Office Category--", "0"));
    }
    public void OfficeLevelBind()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        bl.Base_department_id = ddl_department.SelectedItem.Value;
        bl.Role_id = h_roll_id.Value;
        ReturnClass.ReturnDataTable dt = dl.Get_OfficeLevel(bl);
        ddl_office_level.DataSource = dt.table;
        ddl_office_level.DataTextField = "Office_level";
        ddl_office_level.DataValueField = "olc";
        ddl_office_level.DataBind();
        ddl_office_level.Items.Insert(0, new ListItem("--Select Office Level--", "0"));
    }
    public void OfficeBind()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        bl.Base_department_id = ddl_department.SelectedItem.Value;
        bl.District_id_ofc = ddl_district.SelectedItem.Value;
        bl.Office_category = ddl_office_category.SelectedItem.Value;
        bl.Office_level_id = ddl_office_level.SelectedItem.Value;

        ReturnClass.ReturnDataTable dt = dl.Get_Office_Data(bl);
        ddl_office.DataSource = dt.table;
        ddl_office.DataTextField = "OfficeName";
        ddl_office.DataValueField = "NewOfficeCode";
        ddl_office.DataBind();
        ddl_office.Items.Insert(0, new ListItem("--Select Office--", "0"));
    }


    public void BindOfficer1()
    {
        ddl_employee1.Items.Clear();
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        bl.Base_department_id = ddl_department1.SelectedValue;
        bl.District_id_ofc = ddl_district1.SelectedValue;
        rd = dl.GetEmployeeData1(bl);
        ddl_employee1.DataSource = rd.table;
        ddl_employee1.DataBind();
        ddl_employee1.DataValueField = "id";
        ddl_employee1.DataTextField = "label";
        ddl_employee1.DataBind();
        ddl_employee1.Items.Insert(0, new ListItem("--Select Officer--", "0"));




    }
    public void BindDesignation()        // user district
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        ddl_designation.Items.Clear();
        //bl.State = "22";//DDL_State.SelectedValue; //22 for Chhattisgarh
        rd = dl.GetDesignation(bl);
        ddl_designation.DataSource = rd.table;
        ddl_designation.DataTextField = "Designation_Name";
        ddl_designation.DataValueField = "Designation_ID";
        ddl_designation.DataBind();

        ddl_designation.Items.Insert(0, new ListItem("--Select Designation--", "0"));

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            HttpBrowserCapabilities browse = Request.Browser;
            if (Page.IsValid)
            {
                //if (btnSubmit.Text == "Submit")
               // {
                    string empCode = ddl_employee1.SelectedValue; //"201500001";// TBD
                    bl.Office_mapping_id = dl.GetMappingID(empCode);//dl.GetMaxValue();
                    bl.Emp_code = empCode;
                    bl.Office_id = ddl_office.SelectedValue;
                    bl.Designation_id = ddl_designation.SelectedValue;
                    bl.Base_department_id = ddl_department.SelectedValue;
                    bl.Office_level_id = ddl_office_level.SelectedValue;
                    bl.District_id_ofc = ddl_district.SelectedValue;
                    bl.Office_category = ddl_office_category.SelectedValue;
                    bl.User_id = Session["username"].ToString();
                    bl.Charge_type = ddl_charge.SelectedValue;
                    bl.Active = ddl_Active.SelectedValue;
                    bl.Role_id = Ddl_role.SelectedValue;
                    if (bl.Active == "Y")
                    {
                        bl.FromActive = DateTime.Now.ToString("yyyy-MM-dd"); //DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                        bl.ToActive = null;
                    }
                    else
                    {
                        bl.FromActive = DateTime.Now.ToString("yyyy-MM-dd");
                        bl.ToActive = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    bl.ClientOS = Utilities.System_Info(this.Page);
                    bl.ClientBrowser = browse.Browser;
                    bl.Useragent = Request.UserAgent.ToString();
                    bl.Client_ip = ul.GetClientIpAddress(this);   // GetIPAddress();
                    foreach (ListItem sin in ddlcb_permission.Items)
                    {
                        if (sin.Selected)
                        {
                            bl.Permission.Add(sin.Value);
                        }
                    }
                    rb = dl.Insert_empOfficeMap(bl);
                    if (rb.status == true)
                    {
                        if (Session["language"].ToString() == "en-GB")
                        {

                            Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Employee Mapping Successsful");
                        }
                        else
                        {

                            Utilities.MessageBox_UpdatePanel(UpdatePanel1, "कर्मचारी मैपिंग सफल");
                        }
                       // Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Employee Mapping Successsful");

                        clear();
                        bind_GridView();

                    }
                    else
                    {
                        if (Session["language"].ToString() == "en-GB")
                        {

                            Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Employee Mapping Failed");
                        }
                        else
                        {

                            Utilities.MessageBox_UpdatePanel(UpdatePanel1, "कर्मचारी मैपिंग असफल");
                        }
                       // Utilities.MessageBox_UpdatePanel(UpdatePanel1, "Employee Mapping Failed");

                    }
               // }  // Submit
            }  // if PageIsvalid
        }
    }
    public void clear()
    {
        ddl_department1.SelectedValue = "0";
        ddl_district1.SelectedValue = "0";
        ddl_department.SelectedValue = "0";
        ddl_district.SelectedValue = "0";
        ddl_office_category.SelectedValue = "0";
        ddl_office_level.SelectedValue = "0";
        ddl_office.SelectedValue = "0";
        ddl_designation.SelectedValue = "0";
        ddl_Active.SelectedValue = "0";
        ddl_charge.SelectedValue = "0";
        ddl_employee1.SelectedValue = "0";


    }
    protected void ddl_department1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOfficer1();
    }
    protected void ddlDistrict1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOfficer1();

    }
    protected void ddl_employee1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_office_category_SelectedIndexChanged(object sender, EventArgs e)
    {

        OfficeLevelBind();
        OfficeBind();

        bind_GridView();
    }
    protected void ddl_office_level_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeBind();

        bind_GridView();
    }
    protected void ddl_emp_name_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlBaseDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeLevelBind();
        OfficeBind();


        bind_GridView();

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        OfficeLevelBind();
        OfficeBind();

        bind_GridView();

    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {

        bind_GridView();


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind_GridView();

    }
    public void bind_GridView()
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        bl.Base_department_id = ddl_department.SelectedValue;
        bl.District_id_ofc = ddl_district.SelectedValue;
        bl.Office_category = ddl_office_category.SelectedValue;
        bl.Office_level_id = ddl_office_level.SelectedValue;
        bl.Office_id = ddl_office.SelectedValue;
        bl.Designation_id = ddl_designation.SelectedValue;
        bl.Role_id = Ddl_role.SelectedValue;
        dt = dl.showEmpMap(bl);
        int row = dt.table.Rows.Count;
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
        lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";

        GridView1.DataSource = dt.table;
        GridView1.DataBind();

    }
    public void bind_dist()
    {

        rd = dl.district_code(bl);

        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "district_nm_e";
        ddl_district.DataValueField = "district_id";
        ddl_district.DataBind();
    }
    public void bind_department()
    {

        rd = dl.department(bl);

        ddl_department.DataSource = rd.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";
        ddl_department.DataBind();
    }
    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
    public void bind_Office()
    {

        rd = dl.office(bl);

        ddl_office.DataSource = rd.table;
        ddl_office.DataTextField = "OfficeName";
        ddl_office.DataValueField = "NewOfficeCode";
        ddl_office.DataBind();
    }
    public void bindpermission()
    {
        ddlcb_permission.Items.Clear();
        rd = dl.permissions_all();
        ddlcb_permission.DataSource = rd.table;
        ddlcb_permission.DataTextField = "name";
        ddlcb_permission.DataValueField = "value";
        ddlcb_permission.DataBind();
    }

    public void bindrole()
    {
        Ddl_role.Items.Clear();
        bl.Role_id = Session["role"].ToString();
        rd = dl.get_role(bl);


        Ddl_role.DataSource = rd.table;
        Ddl_role.DataTextField = "RoleName";
        Ddl_role.DataValueField = "Role_id";
        Ddl_role.DataBind();
        Ddl_role.Items.Insert(0, new ListItem("--Select Role--", "0"));
    }

    protected void Ddl_role_SelectedIndexChanged(object sender, EventArgs e)
    {
        string massage = "";
        bl.Role_id = Ddl_role.SelectedValue;
        //   bl.District_id_ofc = ddl_district.SelectedValue;
        // bl.Office_id = ddl_office.SelectedValue;
        if (ddl_district.SelectedValue != "0" && ddl_office.SelectedValue != "0" && ddl_department.SelectedValue
            != "0")
        {
            if (Ddl_role.SelectedValue == "4")
            {
                bl.District_id_ofc = ddl_district.SelectedValue;
                bl.Office_id = "";

            }
            else if (Ddl_role.SelectedValue == "5" || Ddl_role.SelectedValue == "6")
            {
                bl.District_id_ofc = ddl_district.SelectedValue;
                bl.Office_id = ddl_office.SelectedValue;

            }
            rd = dl.check_roll(bl);
            if (rd.table.Rows.Count > 0)
            {
                if (Ddl_role.SelectedValue == "4")
                {
                    massage = @"District  admin for this District  mr.  " + rd.table.Rows[0]["name_en"].ToString() + " is  Already Posted "
                //"  From Office   "+ rd.table.Rows[0]["Officename"].ToString()+ "   under District  "+ rd.table.Rows[0]["District_name_en"].ToString()+"  Is Already "
                + "";
                }
                else if (Ddl_role.SelectedValue == "5" || Ddl_role.SelectedValue == "6")
                {
                    massage = @"Nodal  admin for this Office  mr.  " + rd.table.Rows[0]["name_en"].ToString() + " is  Already Posted "
                //"  From Office   "+ rd.table.Rows[0]["Officename"].ToString()+ "   under District  "+ rd.table.Rows[0]["District_name_en"].ToString()+"  Is Already "
                + "";

                }
                Utilities.MessageBox_UpdatePanel(UpdatePanel2, massage);
            }
        }
        else
        {
            Utilities.MessageBox_UpdatePanel(UpdatePanel2, "please select all fields from above");
        }
    }
}