using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_login_entry : BasePage //System.Web.UI.Page
{
    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    Utilities util = new Utilities();
    bl_login ur = new bl_login();
    dl_login db = new dl_login();
    bl_user_login bl1 = new bl_user_login();
    dl_user_login dl1 = new dl_user_login();
    bl_empMap bl = new bl_empMap();
    dl_empMap dl = new dl_empMap();
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    protected override void InitializeCulture()
    {
        Culture = "en-GB";
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
        base.InitializeCulture();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("../LogOut.aspx");
        }
        if (!Page.IsPostBack)
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            string userID = Session["username"].ToString();
            bl.User_id = userID;
            bl.Emp_code1 = Session["EmpMapID"].ToString();
            string rollid = null;
            bl.Role_id = Session["role"].ToString();
            rollid = bl.Role_id;
            dt = dl.select_admin_info(bl);
            if (dt.table.Rows.Count > 0)
            {
                h_roll_id.Value = Session["role"].ToString();
                if (rollid == "1")  // DIO ROll ID
                {

                    bind_district();
                    BaseDepartmentBind();
                    OfficeBind();
                    Bind_Employee();
                }
                else if (rollid == "4")
                {
                    bl.State = dt.table.Rows[0]["state_id"].ToString();
                    string dist_id = dt.table.Rows[0]["district_id"].ToString();
                    bind_district();
                    DDL_District.SelectedValue = dist_id;
                    DDL_District.Enabled = false;
                    BaseDepartmentBind();
                    OfficeBind();
                    Bind_Employee();

                }
                else if (  rollid == "5") // Nodel admin
                {
                    string  dic, ofc, bd= "";
                    bl.State = dt.table.Rows[0]["state_id"].ToString();
                    bl.Base_department_id = dt.table.Rows[0]["base_department_id"].ToString();
                    dic= dt.table.Rows[0]["district_id"].ToString();
                    bd= dt.table.Rows[0]["base_department_id"].ToString();
                    ofc= dt.table.Rows[0]["office_id"].ToString();
                    bl1.Office_id= dt.table.Rows[0]["office_id"].ToString();
                    bind_district();
                    DDL_District.SelectedValue = dic;
                    DDL_District.Enabled = false;
                    BaseDepartmentBind();

                    DDL_Department.SelectedValue = bd;
                    DDL_Department.Enabled = false;
                    OfficeBind();                  
                    DDL_Office.SelectedValue = ofc;
                    DDL_Office.Enabled = false;
                    Bind_Employee();

                }
            }



            grid_bind();
            txt_user_name.Text = "";
        }

    }

    public void bind_district()          // user district
    {
        DDL_District.Items.Clear();
        bl1.State = bl.State;//DDL_State.SelectedValue; //22 for Chhattisgarh
        dt = dl1.Get_District(bl1);
        DDL_District.DataSource = dt.table;
        DDL_District.DataTextField = "District_Name";
        DDL_District.DataValueField = "District_ID";
        DDL_District.DataBind();

        DDL_District.Items.Insert(0, new ListItem("--Select District--", "0"));

    }
    public void BaseDepartmentBind()
    {
        bl1.District_id = DDL_District.SelectedItem.Value;
        dt = dl1.Get_BaseDepartment(bl1);
        DDL_Department.DataSource = dt.table;
        DDL_Department.DataTextField = "dept_name";
        DDL_Department.DataValueField = "dept_id";
        DDL_Department.DataBind();
        DDL_Department.Items.Insert(0, new ListItem("--Select Department--", "0"));
    }
    public void OfficeBind()
    {

        bl1.District_id = DDL_District.SelectedItem.Value;
        bl1.Department_id = DDL_Department.SelectedItem.Value;
        bl1.Role = h_roll_id.Value;
        
        dt = dl1.Get_Office(bl1);
        DDL_Office.DataSource = dt.table;
        DDL_Office.DataTextField = "OfficeName";
        DDL_Office.DataValueField = "NewOfficeCode";
        DDL_Office.DataBind();
        DDL_Office.Items.Insert(0, new ListItem("--Select Office--", "0"));
    }
    public void clear()
    {
        DDL_District.SelectedValue = "0";
        DDL_Department.SelectedValue = "0";
        DDL_Office.SelectedValue = "0";
        ddl_employee.SelectedValue = "0";
        txt_user_name.Text = "";
        txt_password.Text = "";
        txt_confirm_pass.Text = "";
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            bl_RTI_Registration bl = new bl_RTI_Registration();
            dl_RTI_Registration dl = new dl_RTI_Registration();
            ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
            HttpBrowserCapabilities browse = Request.Browser;
            bl.UserID = txt_user_name.Text;
            rd = dl.CheckUserID(bl);
            if (rd.table.Rows.Count == 0)
            {
                bl.UserID = txt_user_name.Text;
                bl.Password = util.GenerateMd5Hash(txt_password.Text.Trim());
                bl.LoginID = ddl_employee.SelectedValue;
                bl.RegistrationID = bl.LoginID;
                bl.PasswordChange = ddl_change_pass.SelectedValue;
                bl.Active = ddl_active.SelectedValue;
                bl.UserIP = util.GetClientIpAddress(this.Page);

                bl.UserAgent = Request.UserAgent.ToString();
                bl.UserOS = Utilities.System_Info(this.Page);
                bl.User_browser = browse.Browser;
                rb = dl.Insert_Login(bl);
                if (rb.status == true)
                {
                    ddl_employee.SelectedValue = "0";
                    txt_user_name.Text = "";

                    if (Session["language"].ToString() == "en-GB")
                    {

                        Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel1, "Record Saved Successfully", "user_login_entry.aspx");
                    }
                    else
                    {

                        Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel1, "रिकॉर्ड सफलतापूर्वक सुरक्षित रखा गया", "user_login_entry.aspx");
                    }

                    //Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel1, "Record Saved Successfully", "user_login_entry.aspx");
                    grid_bind();
                    clear();
                }
                else
                {
                    if (Session["language"].ToString() == "en-GB")
                    {

                        Utilities.MessageBox_UpdatePanel(UpdatePanel1, "User Id For this Employee Already exist ");
                    }
                    else
                    {

                        Utilities.MessageBox_UpdatePanel(UpdatePanel1, "इस कर्मचारी के लिए उपयोगकर्ता आईडी पहले से मौजूद है ");
                    }
                   // Utilities.MessageBox_UpdatePanel(UpdatePanel1, "User Id For this Employee Already exist ");
                }
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBox_UpdatePanel(UpdatePanel1, "User ID Not Available Please Choose Another UserID ");
                }
                else
                {

                    Utilities.MessageBox_UpdatePanel(UpdatePanel1, "उपयोगकर्ता आईडी उपलब्ध नहीं है कृपया अन्य यूजरआईडी चुनें ");
                }
               // Utilities.MessageBox_UpdatePanel(UpdatePanel1, "User ID Not Available Please Choose Another UserID ");
            }
        }
    }

    public void Bind_Employee()
    {
        bl1.District_id = DDL_District.SelectedItem.Value;
        bl1.Department_id = DDL_Department.SelectedItem.Value;
        bl1.Office_id = DDL_Office.SelectedItem.Value;
        dt = dl1.Get_Employee(bl1);



        ddl_employee.DataSource = dt.table;

        ddl_employee.DataValueField = "emp_id";
        ddl_employee.DataTextField = "Name_en";
        ddl_employee.DataBind();
        ddl_employee.Items.Insert(0, new ListItem("--- Select employee ---", "0"));



    }
    public void grid_bind()
    {

        dt = db.get_login_data();
        if (dt.table.Rows.Count > 0)
        {
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
                lbl_count.Text = "Total User = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
            }
            else
            {
                lbl_count.Text = "कुल उपयोगकर्ता = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
            }
            //lbl_count.Text = "Total Rows = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
            GridView1.DataSource = dt.table;
            GridView1.DataBind();




        }

    }


    protected void DDL_District_SelectedIndexChanged(object sender, EventArgs e)
    {

        BaseDepartmentBind();
        OfficeBind();
        Bind_Employee();

    }
    protected void DDL_Department_SelectedIndexChanged(object sender, EventArgs e)
    {

        OfficeBind();
        Bind_Employee();

    }
    protected void DDL_Office_SelectedIndexChanged(object sender, EventArgs e)
    {

        Bind_Employee();

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid_bind();
    }
}