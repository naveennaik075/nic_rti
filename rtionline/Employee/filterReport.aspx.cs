using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report : BasePage // System.Web.UI.Page
{
   

    bl_report bl = new bl_report();
    dl_report dl = new dl_report();
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
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Session["username"] == null)
            {
                Response.Redirect("~/LogOut.aspx");
            }
            else
            {
                bind_state_code();
                ddl_state.SelectedValue = bl.State_id;
                bind_district_code();
                ddl_district.SelectedValue = bl.District_id;
                bind_department_id();
                ddl_department.SelectedValue = bl.Department;
                bind_office();
                ddl_office.SelectedValue = bl.NewOfficeCode;
                  
                BindDesignation();
                ddl_designation.SelectedValue = bl.Designation_id;
                bind_ddl_district1();
                bind_department_id1();
                bind_ddl_officelevel();
                bind_ddl_category();
                BindGridView();
            } 
        }
    }
    protected void btn_create_Click(object sender, EventArgs e)
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            if (Page.IsValid)
            {

                  
                    bl.Country = "091";
                    bl.State = "22";

                    bl.District = ddl_district1.SelectedValue;
                    bl.Base_department = ddl_department1.SelectedValue;
                    bl.Office_level_id = ddl_Ofc_Lvl.SelectedValue;
                    bl.Category = ddl_category.SelectedValue;
                    bl.Office_id = txt_office.Text;
                bl.Address = txt_address.Text;
                bl.Contact = txt_contact.Text;
                bl.Fax = txt_fax.Text;
                bl.Email_id = txt_email.Text;
                bl.Url = txt_url.Text;
                bl.Officeid = dl.Max_office_code1(bl);
              
             
                rb = dl.insert_office(bl);
              
                if (rb.status == true)
                {
                    txt_address.Text = "";
                    txt_contact.Text = "";
                    txt_email.Text = "";
                    txt_fax.Text = "";
                    txt_office.Text = "";
                    txt_url.Text = "";
                    ddl_department1.SelectedValue = "0";
                    ddl_district1.SelectedValue = "0";
                    ddl_Ofc_Lvl.SelectedValue = "0";
                    ddl_category.SelectedValue = "0";
                    Utilities.MessageBox_UpdatePanel(udp, "OFFICE CREATION SUCCESFUL");
                    Utilities.MessageBox_UpdatePanel_Redirect(udp, "OFFICE CREATION SUCCESFUL","../Employee/filterReport.aspx");
                   
                }
                else
                {
                    Utilities.MessageBox_UpdatePanel(udp, "office Creation Unsuccessful");
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
        bl.Designation_id = ddl_designation.SelectedValue;
        rd = dl.Bind_gridreport(bl);

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
      //  lbl_count.Text = "Total Employee = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GridView1.DataSource = rd.table;
        GridView1.DataBind();

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();


    }

    public void bind_state_code()
    {
        bl.State_id = "22";
        rd = dl.state_code(bl);
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

        rd = dl.office(bl);

        ddl_office.DataSource = rd.table;
        ddl_office.DataTextField = "OfficeName";
        ddl_office.DataValueField = "NewOfficeCode";
        ddl_office.DataBind();
        ddl_office.Items.Insert(0, new ListItem("Select", "0"));

    }

    public void BindDesignation()        
    {
        bl_empMap bl = new bl_empMap();
        dl_empMap dl = new dl_empMap();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        ddl_designation.Items.Clear();
      
        rd = dl.GetDesignation(bl);
        ddl_designation.DataSource = rd.table;
        ddl_designation.DataTextField = "Designation_Name";
        ddl_designation.DataValueField = "Designation_ID";
        ddl_designation.DataBind();

        ddl_designation.Items.Insert(0, new ListItem("--Select Designation--", "0"));

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
        BindGridView();
    }


    protected void ddl_designation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void lnk_addoffice_Click(object sender, EventArgs e)
    {
        div_office.Visible = true;
        udp.Visible = true;
    }
    public void bind_ddl_district1()
    {
        ddl_district1.Items.Clear();
        
        rd = dl.district_code1();
        ddl_district1.DataSource = rd.table;
        ddl_district1.DataTextField = "District_Name";
        ddl_district1.DataValueField = "District_ID";

        ddl_district1.DataBind();
        ddl_district1.Items.Insert(0, new ListItem("Select", "0"));


    }
    public void bind_department_id1()
    {
        ddl_department1.Items.Clear();

        rd = dl.department_id(bl);
        ddl_department1.DataSource = rd.table;
        ddl_department1.DataTextField = "dept_name";
        ddl_department1.DataValueField = "dept_id";
        ddl_department1.DataBind();
        ddl_department1.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void bind_ddl_officelevel()
    {
       
       
        ddl_Ofc_Lvl.Items.Clear();
        
        bl.Base_department = ddl_department1.SelectedValue;
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

    protected void ddl_district1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Ofc_Lvl.SelectedValue = "0";
        ddl_department1.SelectedValue = "0";
        bind_ddl_officelevel();
    }
    protected void ddl_department1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_ddl_officelevel();
    }
    protected void ddl_Ofc_Lvl_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_ddl_category();
    }
    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}