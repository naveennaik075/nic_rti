using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class EmpOfficeMapAllocated :BasePage // System.Web.UI.Page
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    bl_empMap bl = new bl_empMap();
    dl_empMap dl = new dl_empMap();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    Utilities ul = new Utilities();
    string rollID = null;
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
              //  ddl_department1.SelectedValue = "0";

                rd = dl.select_admin_info(bl);
                if (rd.table.Rows.Count > 0)
                {
                    h_roll_id.Value = bl.Role_id;
                    if (bl.Role_id == "4" || bl.Role_id == "5")
                    {
                        bind_district1();
                        bind_district();
                        ddl_dist_view.Visible = false;
                        rfv_district1.Enabled = false;
                        txt_dist_view.Visible = true;
                        txt_district1.Visible = true;

                        txt_district1.Text = rd.table.Rows[0]["District_Name_En"].ToString();

                        ddl_district1.SelectedValue = rd.table.Rows[0]["district_id"].ToString();
                        ddl_district.SelectedValue = rd.table.Rows[0]["district_id"].ToString();
                        ddl_district.Enabled = false;
                        rfv_district.Enabled = false;
                        if (bl.Role_id == "5")
                        {
                            BaseDepartmentBind1();                                                    
                            BaseDepartmentBind();
                            ddl_department1.SelectedValue = rd.table.Rows[0]["base_department_id"].ToString();
                            ddl_department.SelectedValue = rd.table.Rows[0]["base_department_id"].ToString();
                            ddl_department.Enabled = false;
                            RFV_department.Enabled = false;
                            ddl_department1.Enabled = false;
                            rfv_department1.Enabled = false;
                            OfficeCategoryBind();
                            OfficeCategoryBind1();
                            ddl_office_Category1.SelectedValue = rd.table.Rows[0]["category"].ToString();
                            ddl_office_Category1.Enabled = false;
                            rfv_office_category1.Enabled = false;
                            ddl_office_category.SelectedValue = rd.table.Rows[0]["category"].ToString();
                            ddl_office_category.Enabled = false;
                            ddl_office_category.Enabled = false;
                            OfficeLevelBind();
                            OfficeLevelBind1();
                            ddl_office_level1.SelectedValue = rd.table.Rows[0]["office_level_id"].ToString();
                            ddl_office_level1.Enabled = false;
                            ddl_office_level1.Enabled = false;
                            ddl_office_level.SelectedValue = rd.table.Rows[0]["office_level_id"].ToString();
                            ddl_office_level.Enabled = false;
                            ddl_office_level.Enabled = false;
                            OfficeBind1();
                            OfficeBind();
                            ddl_office_name1.SelectedValue = rd.table.Rows[0]["office_id"].ToString();
                            ddl_office_name1.Enabled = false;
                            ddl_office_name1.Enabled = false;
                            ddl_office.SelectedValue = rd.table.Rows[0]["office_id"].ToString();
                            ddl_office.Enabled = false;
                            ddl_office.Enabled = false;
                            ddl_designation1.Enabled = false;
                        }
                        BaseDepartmentBind1();
                        BaseDepartmentBind();
                    }
                    else
                    {
                        ddl_dist_view.Visible = true;
                        rfv_district1.Enabled = true;
                        txt_dist_view.Visible = false;
                        txt_district1.Visible = false;
                        bind_district();
                        ddl_district.SelectedValue = "0";
                        bind_district1();
                        ddl_district1.SelectedValue = "0";
                        BaseDepartmentBind();
                        ddl_department.SelectedValue = "0";
                        BaseDepartmentBind1();
                        ddl_department1.SelectedValue = "0";
                        OfficeCategoryBind();
                        ddl_office_category.SelectedValue = "0";
                        OfficeCategoryBind1();
                        ddl_office_Category1.SelectedValue = "0";
                        OfficeLevelBind();
                        ddl_office_level.SelectedValue = "0";
                        OfficeLevelBind1();
                        ddl_office_level1.SelectedValue = "0";
                        OfficeBind1();
                        OfficeBind();
                    }
                }
                if (bl.Role_id == "4" || bl.Role_id == "5")  // Added by Naveen
                {
                    OfficeCategoryBind();      // commented by Naveen
                    OfficeCategoryBind1();     // commented by Naveen
                    OfficeLevelBind();         // commented by Naveen
                    OfficeLevelBind1();       // commented by Naveen
                    OfficeBind1();
                    OfficeBind();
                    BindOfficer1();
                    BindDesignation1();
                    BindDesignation();
                    ChargeTypeBind();
                    ddl_Active.SelectedValue = "Y";
                    bindpermission();
                    bindrole();
                    bind_GridView();
                }
                else {                                  // Added by Naveen
                    OfficeBind1();
                    OfficeBind();
                    BindOfficer1();
                    BindDesignation1();
                    BindDesignation();
                    ChargeTypeBind();
                    ddl_Active.SelectedValue = "Y";
                    bindpermission();
                    bindrole();
                    bind_GridView();
                
                }
            }
        }

    }  // End of Page Load




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
        // ddl_district.Items.Add(new ListItem("Choose District", "0"));
        ddl_district1.Items.Insert(0, new ListItem("--Select District--", "0"));

        OfficeCategoryBind1();
        OfficeLevelBind1();
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
        // ddl_district.Items.Add(new ListItem("Choose District", "0"));
        ddl_district.Items.Insert(0, new ListItem("--Select District--", "0"));

    }

    public void BaseDepartmentBind1()
    {
        ddl_department1.Items.Clear();
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        //string dept = DDL_Department.SelectedItem.Value;
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
        //string dept = DDL_Department.SelectedItem.Value;
        ReturnClass.ReturnDataTable dt = dl.Get_BaseDepartment(bl);// ("SELECT dept_id, dept_name FROM basedepartment order by dept_name ");
        ddl_department.DataSource = dt.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";
        ddl_department.DataBind();
        ddl_department.Items.Insert(0, new ListItem("--Select Department--", "0"));
    }

    public void OfficeCategoryBind1()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        ReturnClass.ReturnDataTable dt = dl.Get_OfficeCategory(bl);// ("SELECT OfficeCategoryCode, OfficeCategoryName FROM officeCategory order by OfficeCategoryName ");
        ddl_office_Category1.DataSource = dt.table;
        ddl_office_Category1.DataTextField = "OfficeCategoryName";
        ddl_office_Category1.DataValueField = "OfficeCategoryCode";
        ddl_office_Category1.DataBind();
        ddl_office_Category1.Items.Insert(0, new ListItem("--Select Office Category--", "0"));
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

    public void OfficeLevelBind1()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        bl.Role_id = Session["role"].ToString();
        bl.Base_department_id = ddl_department1.SelectedValue;
       

        ReturnClass.ReturnDataTable dt = dl.Get_OfficeLevel(bl);
        ddl_office_level1.DataSource = dt.table;
        ddl_office_level1.DataTextField = "Office_level";
        ddl_office_level1.DataValueField = "olc";
        ddl_office_level1.DataBind();
        ddl_office_level1.Items.Insert(0, new ListItem("--Select Office Level--", "0"));
    }
    public void OfficeLevelBind()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        bl.Role_id = Session["role"].ToString();
        bl.Base_department_id = ddl_department.SelectedItem.Value;
        if (bl.Role_id == "4")
        {
            bl.Office_level_id = "5";
        }

        ReturnClass.ReturnDataTable dt = dl.Get_OfficeLevel(bl);
        ddl_office_level.DataSource = dt.table;
        ddl_office_level.DataTextField = "Office_level";
        ddl_office_level.DataValueField = "olc";
        ddl_office_level.DataBind();
        ddl_office_level.Items.Insert(0, new ListItem("--Select Office Level--", "0"));
    }

    public void OfficeBind1()
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        bl.Base_department_id = ddl_department1.SelectedValue;
        bl.District_id_ofc = ddl_district1.SelectedValue;
        bl.Office_category = ddl_office_Category1.SelectedValue;
        bl.Office_level_id = ddl_office_level1.SelectedValue;

        ReturnClass.ReturnDataTable dt = dl.Get_Office_Data(bl);
        ddl_office_name1.DataSource = dt.table;
        ddl_office_name1.DataTextField = "OfficeName";
        ddl_office_name1.DataValueField = "NewOfficeCode";
        ddl_office_name1.DataBind();
        ddl_office_name1.Items.Insert(0, new ListItem("--Select Office--", "0"));
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
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();

        bl.Base_department_id = ddl_department1.SelectedValue;
        bl.District_id_ofc = ddl_district1.SelectedValue;
        bl.Office_category = ddl_office_Category1.SelectedValue;
        bl.Office_level_id = ddl_office_level1.SelectedValue;
        bl.Office_id = ddl_office_name1.SelectedValue;

        rd = dl.GetEmployeeDataAllocated(bl);
        ddl_employee1.DataSource = rd.table;
        ddl_employee1.DataTextField = "Name_en";
        ddl_employee1.DataValueField = "emp_id";
        ddl_employee1.DataBind();
        // DDL_RTI_Officer.Items.Add(new ListItem(" select officer", "0"));
        ddl_employee1.Items.Insert(0, new ListItem("--Select Officer--", "0"));

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
    public void BindDesignation1()        // user district
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        ddl_designation1.Items.Clear();
        //bl.State = "22";//DDL_State.SelectedValue; //22 for Chhattisgarh
        rd = dl.GetDesignation(bl);
        ddl_designation1.DataSource = rd.table;
        ddl_designation1.DataTextField = "Designation_Name";
        ddl_designation1.DataValueField = "Designation_ID";
        ddl_designation1.DataBind();
        // ddl_district.Items.Add(new ListItem("Choose District", "0"));
        ddl_designation1.Items.Insert(0, new ListItem("--Select Designation--", "0"));

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
        // ddl_district.Items.Add(new ListItem("Choose District", "0"));
        ddl_designation.Items.Insert(0, new ListItem("--Select Designation--", "0"));

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

    public void bind_dist()
    {

        rd = dl.district_code(bl);

        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "district_nm_e";
        ddl_district.DataValueField = "district_id";
        ddl_district.DataBind();
    }




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






    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Page.IsValid)
            {
               // if (btnSubmit.Text == "Submit")
               // {
                    HttpBrowserCapabilities browse = Request.Browser;
                    //Insert parameter start
                    string empCode = ddl_employee1.SelectedValue; //"201500001";// TBD
                    bl.Office_mapping_id = dl.GetMappingID(empCode);//dl.GetMaxValue();
                    bl.Emp_code = empCode; //txt_code.Text;
                    bl.Office_id = ddl_office.SelectedValue;
                    bl.Designation_id = ddl_designation.SelectedValue;
                    bl.Base_department_id = ddl_department.SelectedValue;
                    bl.Office_level_id = ddl_office_level.SelectedValue;
                    bl.District_id_ofc = ddl_district.SelectedValue;
                    bl.Office_category = ddl_office_category.SelectedValue;
                    bl.User_id = Session["username"].ToString();
                    bl.Charge_type = ddl_charge.SelectedValue;
                    bl.Active = ddl_Active.SelectedValue;
                    bl.Client_ip = ul.GetClientIpAddress(this);
                    bl.Role_id = Ddl_role.SelectedValue;

                    if (bl.Active == "Y")
                    {
                        bl.FromActive = DateTime.Now.ToString("yyyy-MM-dd"); //DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                        bl.ToActive = null;
                    }
                    else
                    {
                        bl.ToActive = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    // Insert parameter End
                    // update parameter start
                    bl.Emp_code1 = empCode;
                    bl.Office_id1 = ddl_office_name1.SelectedValue;
                    bl.Designation_id1 = ddl_designation1.SelectedValue;
                    bl.Base_department_id1 = ddl_department1.SelectedValue;
                    bl.Office_level_id1 = ddl_office_level1.SelectedValue;
                    bl.District_id_ofc1 = ddl_district1.SelectedValue;
                    bl.Office_category1 = ddl_office_Category1.SelectedValue;
                    bl.ToActive1 = DateTime.Now.ToString("yyyy-MM-dd");


                    // update parameter End
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
                    rb = dl.Insert_empOfficeMapAllocated(bl);
                    if (rb.status == true)
                    {
                        if (Session["language"].ToString() == "en-GB")
                        {
                            Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel1, "Employee Mapping Successsful", "EmpOfficeMapAllocated.aspx");
                           
                        }
                        else
                        {
                            Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel1, "कर्मचारी मैपिंग सफल", "EmpOfficeMapAllocated.aspx");
                          
                        }
                        //Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel1, "Employee Mapping Successsful","EmpOfficeMapAllocated.aspx");

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
                //}  //Commented Submit
            }  // if PageIsvalid
        }
    }

    public void clear()
    {
        ddl_department1.SelectedValue = "0";
        ddl_district1.SelectedValue = "0";
        ddl_employee1.SelectedValue = "0";
        ddl_department.SelectedValue = "0";
        ddl_district.SelectedValue = "0";
        ddl_office_category.SelectedValue = "0";
        ddl_office_level.SelectedValue = "0";
        ddl_office.SelectedValue = "0";
        ddl_designation.SelectedValue = "0";
        ddl_Active.SelectedValue = "0";
        ddl_charge.SelectedValue = "0";

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
        bl.Base_department_id = ddl_department1.SelectedValue;
        bl.District_id_ofc = ddl_district1.SelectedValue;
        bl.Emp_code = ddl_employee1.SelectedValue;
        rd = dl.GetSelectedEmpData(bl);
        if (rd.table.Rows.Count > 0)
        {
            OfficeCategoryBind1(); 
            ddl_office_Category1.SelectedValue = rd.table.Rows[0]["office_category"].ToString();
            OfficeLevelBind1();
            ddl_office_level1.SelectedValue = rd.table.Rows[0]["office_level_id"].ToString();
            OfficeBind1();
            ddl_office_name1.SelectedValue = rd.table.Rows[0]["office_id"].ToString();
            BindDesignation();
            ddl_designation1.SelectedValue = rd.table.Rows[0]["designation_id"].ToString();
        }

    }
    protected void ddl_office_Category1_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeLevelBind1();
        OfficeBind1();
    }
    protected void ddl_office_level1_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeBind1();
    }

    protected void ddl_office_name1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_office_category_SelectedIndexChanged(object sender, EventArgs e)
    {

        OfficeLevelBind();
        OfficeBind();
        // BindOfficer();
        bind_GridView();
    }
    protected void ddl_office_level_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeBind();
        //BindOfficer();
        bind_GridView();
    }
    protected void ddl_emp_name_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlBaseDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeLevelBind();
        OfficeBind();
      //  BindOfficer1();

        //bind_Office();

        bind_GridView();

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bind_Office();
        OfficeLevelBind();
        OfficeBind();
        // BindOfficer();
        bind_GridView();

    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindOfficer();
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
        if (row % 4 == 0)
        {
            page = row / 4;
        }
        else
        {
            page = row / 4;
            page = page + 1;
        }
        lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";


        GridView1.DataSource = dt.table;
        GridView1.DataBind();

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

}