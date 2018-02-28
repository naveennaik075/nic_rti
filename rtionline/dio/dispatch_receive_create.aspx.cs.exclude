using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dio_dispatch_receive_create : System.Web.UI.Page
{
    Utilities ul = new Utilities();
    dl_Dio dl = new dl_Dio();
    bl_Dio bl = new bl_Dio();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    protected override void InitializeCulture()
    {
        Culture = "en-GB";
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

        base.InitializeCulture();
    }
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
                comval_txt_date_join.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
                bl.Userid = Session["username"].ToString();
                rd = dl.select_admin_info(bl);
                if (rd.table.Rows.Count > 0)
                {
                    bl.Role = rd.table.Rows[0]["RollID"].ToString();
                    //   bl.Role = "5";
                    if (bl.Role == "1")
                    {

                        bl.State = rd.table.Rows[0]["state_id"].ToString();
                        bind_ddl_district();
                        bindgrd_ofc();
                        bind_department();
                        bind_ddl_officelevel();
                        bind_ddl_category();
                    }
                    else if (bl.Role == "4")
                    {
                        bl.District = rd.table.Rows[0]["district_id"].ToString();
                        bl.State = rd.table.Rows[0]["state_id"].ToString();
                        bind_ddl_district();
                        rfv_district.Enabled = false;

                        district.Visible = true;
                        ddl_district.Enabled = false;
                        ddl_district.SelectedValue = bl.District;

                        bind_department();
                        bind_ddl_officelevel();
                        bind_ddl_category();
                        bindgrd_ofc();
                    }
                    else if (bl.Role == "5")
                    {
                        bl.State = rd.table.Rows[0]["state_id"].ToString();
                        bl.District = rd.table.Rows[0]["district_id"].ToString();
                        bl.Base_department = rd.table.Rows[0]["base_department_id"].ToString();
                        bl.Officeid = rd.table.Rows[0]["NewOfficeCode"].ToString();

                        #region bind district
                        rd = dl.district(bl);
                        ddl_district.DataSource = rd.table;
                        ddl_district.DataTextField = "District_Name";
                        ddl_district.DataValueField = "District_ID";
                        ddl_district.Items.Add(new ListItem(" Choose District", "0"));
                        ddl_district.DataBind();
                        ddl_district.SelectedValue = bl.District;
                        ddl_district.Enabled = false;
                        #endregion
                        #region bind department
                        rd = dl.base_department(bl);
                        ddl_department.DataSource = rd.table;
                        ddl_department.DataTextField = "dept_name";
                        ddl_department.DataValueField = "dept_id";
                        ddl_department.Items.Add(new ListItem("Choose Department", "0"));
                        ddl_department.DataBind();
                        ddl_department.SelectedValue = bl.Base_department;
                        ddl_department.Enabled = false;
                        #endregion
                        #region bind Ofclvl
                        ddl_Ofc_Lvl.Items.Clear();
                        rd = dl.select_officedtl(bl);
                        ddl_Ofc_Lvl.DataSource = rd.table;
                        ddl_Ofc_Lvl.DataTextField = "ol_name";
                        ddl_Ofc_Lvl.DataValueField = "code";
                        ddl_Ofc_Lvl.DataBind();
                        ddl_Ofc_Lvl.SelectedValue = bl.Officelvl;
                        ddl_Ofc_Lvl.Enabled = false;
                        #endregion
                        #region bind office category
                        ddl_category.DataSource = rd.table;
                        ddl_category.DataTextField = "ofc_cat";
                        ddl_category.DataValueField = "ofc_cat_code";
                        ddl_category.DataBind();
                        ddl_category.Enabled = false;
                        #endregion
                        #region bind office

                        ddl_office.Items.Clear();
                        ddl_office.DataSource = rd.table;
                        ddl_office.DataTextField = "nameofc";
                        ddl_office.DataValueField = "OfficeCode";
                        ddl_office.DataBind();
                        ddl_office.SelectedValue = bl.Officeid;
                        ddl_office.Enabled = false;
                        #endregion
                        bindgrd_ofc();
                    }
                    else
                    {
                        Utilities.MessageBox_UpdatePanel_Redirect(udp, "You Are Not Authorised For Creating Receiving And Depture Officer", "../dio/DioWelcome.aspx");
                    }
                }
            }
        }
    }
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Ofc_Lvl.SelectedValue = "0";
        ddl_department.SelectedValue = "0";
        bindgrd_ofc();
        bindoffice();
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {

        bind_ddl_officelevel();
        ddl_Ofc_Lvl.SelectedValue = "0";
        bindgrd_ofc();
        bindoffice();
    }
    protected void ddl_Ofc_Lvl_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrd_ofc();
        bindoffice();
    }
    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrd_ofc();
        //rd = dl.Bind_grid(bl);
        bindoffice();
    }
    protected void ddl_office_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_employee();
    }
    protected void grd_Office_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Office.PageIndex = e.NewPageIndex;
        bindgrd_ofc();
    }
    protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_charge();
    }
    protected void btn_create_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Page.IsValid)
            {
             
                HttpBrowserCapabilities browse = Request.Browser;
                bl.Employee = ddl_employee.SelectedValue;
                bl.Officeid = ddl_office.SelectedValue;
                rd = dl.select_employeemapping_rec_dis(bl);
                if (rd.table.Rows.Count > 0)
                {
                    bl.Officeid = ddl_office.SelectedValue;
                    bl.Ofc_mappingid = rd.table.Rows[0]["mappingid"].ToString();
                    bl.Status = ddl_active.SelectedValue;
                    bl.Type = ddl_charge.SelectedValue;
                    bl.Fromdate = txt_from_date.Text;
                    bl.Todate = txt_to_date.Text;
                    bl.Ipaddress = ul.GetClientIpAddress(this.Page);
                    bl.Clientos = Utilities.System_Info(this.Page);
                    bl.Client_browser = browse.Browser;
                    bl.Useragent = Request.UserAgent;
                    bl.Userid = Session["username"].ToString();
                    rb = dl.insert_rec_dep(bl);
                    if (rb.status == true)
                    {
                        txt_from_date.Text = "";
                        txt_to_date.Text = "";
                        Utilities.MessageBox_UpdatePanel(udp,"Officer Created");
                    }
                    else
                    {
                        Utilities.MessageBox_UpdatePanel(udp, "Officer Creation Unsucccessful");
                    }
                }
                else
                {
                    Utilities.MessageBoxShow("The Employee Does not exist in our database that you have selected");
                }
                #region commented
                //rd = dl.select_admin_info(bl);
                //        if (rd.table.Rows[0]["RollID"].ToString() == "1")
                //        {
                //            bl.State = rd.table.Rows[0]["state_id"].ToString();
                //            bl.District = ddl_district.SelectedValue;
                //            bl.Base_department = ddl_department.SelectedValue;
                //            bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
                //        }
                //        else if (rd.table.Rows[0]["RollID"].ToString() == "4")
                //        {
                //            bl.Officelevelcode = "00";
                //            bl.Role = rd.table.Rows[0]["RollID"].ToString();
                //            bl.State = rd.table.Rows[0]["state_id"].ToString();
                //            bl.District = rd.table.Rows[0]["district_id"].ToString();
                //            bl.Base_department = ddl_department.SelectedValue;
                //            bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
                //        }
                //        bl.Country = "091";
                //        //bl.Office = txt_office.Text;
                //        //bl.Address = txt_address.Text;
                //        //bl.Contact = txt_contact.Text;
                //        //bl.Fax = txt_fax.Text;
                //        //bl.Email = txt_email.Text;
                //        //bl.Url = txt_url.Text;
                //        bl.Officeid = dl.Max_office_code(bl);
                //        bl.Category = ddl_category.SelectedValue;
                //        rb = dl.insert_office(bl);
                //        bindgrd_ofc();
                //        if (rb.status == true)
                //        {

                //            Utilities.MessageBox_UpdatePanel(udp, "OFFICE CREATION SUCCESFUL");
                //        }
                //        else
                //        {
                //            Utilities.MessageBox_UpdatePanel(udp, "office Creation Unsuccessful");
                //        }

                //    }
                //}
                #endregion
            }
        }
    }
    public void bindoffice()
    {
        bindgrd_ofc();
        ddl_office.Items.Clear();
        ddl_office.DataSource = rd.table;
        ddl_office.DataTextField = "ofice";
        ddl_office.DataValueField = "NewOfficeCode";
        ddl_office.Items.Add(new ListItem("Choose Office", "0"));
        ddl_office.DataBind();

    }
    public void bind_employee()
    {
        bl.Officeid = ddl_office.SelectedValue;
        rd = dl.select_employee_rec_dis(bl);
        ddl_employee.Items.Clear();
        ddl_employee.DataSource = rd.table;
        ddl_employee.DataTextField = "name";
        ddl_employee.DataValueField = "empcode";
        ddl_employee.Items.Add(new ListItem("Choose Officer", "0"));
        ddl_employee.DataBind();
    }
    public void bind_charge()
    {
        rd = dl.select_recieve_chargetype();
        ddl_charge.Items.Clear();
        ddl_charge.DataSource = rd.table;
        ddl_charge.DataTextField = "name";
        ddl_charge.DataValueField = "short";
        ddl_charge.Items.Add(new ListItem("Choose Charge", "0"));
        ddl_charge.DataBind();
    }
    public void bind_ddl_district()
    {
        ddl_district.Items.Clear();
        bl.Userid = Session["username"].ToString();
        rd = dl.select_admin_info(bl);

        if (rd.table.Rows[0]["RollID"].ToString() == "1")
        {
            bl.State = rd.table.Rows[0]["state_id"].ToString();
            bl.District = ddl_district.SelectedValue;
            bl.Base_department = ddl_department.SelectedValue;
            bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
        }
        else if (rd.table.Rows[0]["RollID"].ToString() == "4")
        {
            bl.Officelevelcode = "00";
            bl.Role = rd.table.Rows[0]["RollID"].ToString();
            bl.State = rd.table.Rows[0]["state_id"].ToString();
            bl.District = rd.table.Rows[0]["district_id"].ToString();
            bl.Base_department = ddl_department.SelectedValue;
            bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
        }
        rd = dl.district(bl);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";
        ddl_district.Items.Add(new ListItem(" Choose District", "0"));
        ddl_district.DataBind();
        bind_ddl_officelevel();


    }
    public void bind_department()
    {
        ddl_department.Items.Clear();
        bl.Userid = Session["username"].ToString();
        rd = dl.select_admin_info(bl);

        if (rd.table.Rows[0]["RollID"].ToString() == "1")
        {
            bl.State = rd.table.Rows[0]["state_id"].ToString();
            bl.District = ddl_district.SelectedValue;
            bl.Base_department = ddl_department.SelectedValue;
            bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
        }
        else if (rd.table.Rows[0]["RollID"].ToString() == "4")
        {
            bl.Officelevelcode = "00";
            bl.Role = rd.table.Rows[0]["RollID"].ToString();
            bl.State = rd.table.Rows[0]["state_id"].ToString();
            bl.District = rd.table.Rows[0]["district_id"].ToString();
            bl.Base_department = ddl_department.SelectedValue;
            bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
        }
        rd = dl.base_department(bl);
        ddl_department.DataSource = rd.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";
        ddl_department.Items.Add(new ListItem("Choose Department", "0"));
        ddl_department.DataBind();

    }
    public void bind_ddl_officelevel()
    {
        bl.Userid = Session["username"].ToString();
        rd = dl.select_admin_info(bl);

        if (rd.table.Rows[0]["RollID"].ToString() == "1")
        {
            bl.State = rd.table.Rows[0]["state_id"].ToString();
            bl.District = ddl_district.SelectedValue;
            bl.Base_department = ddl_department.SelectedValue;
            bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
        }
        else if (rd.table.Rows[0]["RollID"].ToString() == "4")
        {
            bl.Officelevelcode = "00";
            bl.Role = rd.table.Rows[0]["RollID"].ToString();
            bl.State = rd.table.Rows[0]["state_id"].ToString();
            bl.District = rd.table.Rows[0]["district_id"].ToString();
            bl.Base_department = ddl_department.SelectedValue;
            bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
        }
        ddl_Ofc_Lvl.Items.Clear();
        rd = dl.office_level(bl);
        ddl_Ofc_Lvl.DataSource = rd.table;
        ddl_Ofc_Lvl.DataTextField = "Office_level";
        ddl_Ofc_Lvl.DataValueField = "olc";
        ddl_Ofc_Lvl.Items.Add(new ListItem(" Choose Office Level", "0"));
        ddl_Ofc_Lvl.DataBind();

    }
    public void bind_ddl_category()
    {
        ddl_category.Items.Clear();
        rd = dl.category(bl);
        ddl_category.DataSource = rd.table;
        ddl_category.DataTextField = "name";
        ddl_category.DataValueField = "code";
        ddl_category.Items.Add(new ListItem("Choose Category", "0"));
        ddl_category.DataBind();
    }
    public void bindgrd_ofc()
    {
        bl.Userid = Session["username"].ToString();
        rd = dl.select_admin_info(bl);

        if (rd.table.Rows[0]["RollID"].ToString() == "1")
        {
            bl.State = rd.table.Rows[0]["state_id"].ToString();
            bl.District = ddl_district.SelectedValue;

        }
        else if (rd.table.Rows[0]["RollID"].ToString() == "4")
        {
            bl.Officelevelcode = "00";
            bl.Role = rd.table.Rows[0]["RollID"].ToString();
            bl.State = rd.table.Rows[0]["state_id"].ToString();
            bl.District = rd.table.Rows[0]["district_id"].ToString();
        }
        bl.Base_department = ddl_department.SelectedValue;
        bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
        bl.Category = ddl_category.SelectedValue;
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
        lbl_count.Text = "Total Offices = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        grd_Office.DataSource = rd.table;
        grd_Office.DataBind();
    }
}