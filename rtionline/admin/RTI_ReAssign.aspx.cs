using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Drawing;

public partial class RTI_ReAssign : BasePage //System.Web.UI.Page
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
                    BindOfficer();
                    BindRTIStatus();
                    BindGridView();

                }
            }
        }
    }
    protected void BindGridView()
    {
        dl_RTIReAssign dl_rti = new dl_RTIReAssign();
        bl.State_id = "22"; // ddl_state.SelectedValue;
        bl.District_id = ddl_district.SelectedValue;
        bl.Department = ddl_department.SelectedValue;
        bl.Office_id = ddl_office.SelectedValue;
        bl.Office_mapping_id = ddl_officer.SelectedValue;
        bl.RTI_dept_status = ddl_status.SelectedValue;

        rd = dl_rti.Bind_grid(bl);

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
            lbl_count.Text = "Total RTI = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल आर.टी.आई. = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
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
        
        ddl_department.SelectedValue = "0";

        ddl_office.SelectedValue = "0";


    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
              
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
        bl.StateCode = "22"; // ddl_state.SelectedValue;
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
        bl.State_id = "22"; //ddl_state.SelectedValue;
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
        bl.StateCode = "22"; // ddl_state.SelectedValue;
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

    public void BindOfficer()
    {
        bl_RTI_Request objblData = new bl_RTI_Request();
        dl_RTIReAssign objData = new dl_RTIReAssign();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        //DDL_RTI_Officer.Items.Clear();
        objblData.Base_department_id = ddl_department.SelectedValue;
        objblData.District_id_ofc = ddl_district.SelectedValue;
        objblData.Office_id = ddl_office.SelectedValue;

        rd = objData.BindOfficerDL(objblData);
        ddl_officer.DataSource = rd.table;
        ddl_officer.DataTextField = "Name_en";
        ddl_officer.DataValueField = "office_mapping_id";
        ddl_officer.DataBind();
        // DDL_RTI_Officer.Items.Add(new ListItem(" select officer", "0"));
        ddl_officer.Items.Insert(0, new ListItem("--Select Officer--", "0"));

    }


    public void BindRTIStatus()
    {
        dl_RTIReAssign objData = new dl_RTIReAssign();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();

        rd = objData.BindRTIStatusDL();
        ddl_status.DataSource = rd.table;
        ddl_status.DataTextField = "name";
        ddl_status.DataValueField = "value";
        ddl_status.DataBind();
        ddl_status.Items.Insert(0, new ListItem("--Select Status--", "0"));

    }


    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        bl.State_id = "22";
        bind_district_code();
        bind_department_id();
        bind_office();
        BindOfficer();
        BindGridView();
    }
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {

        bind_department_id();
        bind_office();
        BindOfficer();
        BindGridView();
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {

        bind_office();
        BindOfficer();
        BindGridView();

    }
    protected void ddl_office_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bl.State_id = ddl_state.SelectedValue;
        //bl.District_id = ddl_district.SelectedValue;
        //bl.Department = ddl_department.SelectedValue;
        //bl.Office_id = ddl_office.SelectedValue;
        BindOfficer();
        BindGridView();
    }

    protected void ddl_officer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void ddl_grid_state_SelectedIndexChanged(object sender, EventArgs e)
    {

        bind_grid_district_code();
        bind_grid_office();

    }
    public void bind_grid_district_code()
    {
        DropDownList ddl_district = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_district_grid");
        //DropDownList ddl_state = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_state");
        bl.StateCode = "22";
        rd = dl.district_code(bl);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";

        ddl_district.DataBind();
        ddl_district.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    public void bind_grid_office()
    {
        DropDownList ddl_office = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_office_grid");
        DropDownList ddl_district = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_district_grid");
        DropDownList ddl_department = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_department_grid");
        bl.StateCode = "22";
        bl.DistrictCodeNew = ddl_district.SelectedValue;
        bl.Department = ddl_department.SelectedValue;
        rd = dl.office(bl);
        ddl_office.DataSource = rd.table;
        ddl_office.DataTextField = "OfficeName";
        ddl_office.DataValueField = "NewOfficeCode";

        ddl_office.DataBind();
        ddl_office.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void bind_grid_officer()
    {
        DropDownList ddl_office = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_office_grid");
        DropDownList ddl_district = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_district_grid");
        DropDownList ddl_department = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_department_grid");
        DropDownList ddl_officer = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddl_officer_grid");
        bl_RTI_Request objblData = new bl_RTI_Request();
        dl_RTIReAssign objData = new dl_RTIReAssign();
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        objblData.District_id_ofc = ddl_district.SelectedValue;
        objblData.Base_department_id = ddl_department.SelectedValue;
        objblData.Office_id = ddl_office.SelectedValue;
        rd = objData.BindOfficerDL(objblData);
        ddl_officer.DataSource = rd.table;
        ddl_officer.DataTextField = "Name_en";
        ddl_officer.DataValueField = "office_mapping_id";
        ddl_officer.DataBind();
        ddl_officer.Items.Insert(0, new ListItem("--Select Officer--", "0"));
    
    }
    protected void ddl_office_grid_SelectedIndexChanged(object sender, EventArgs e) 
    {
        bind_grid_officer();
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

        
       // GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
       // string state = (GridView1.Rows[e.RowIndex].FindControl("ddl_state") as DropDownList).SelectedItem.Value;
       //GridView1.DataKeys[e.RowIndex].Values["ofc_map_id"].ToString();
        bl_employee_action bl_emp_action = new bl_employee_action();
        dl_RTIReAssign dl_rti = new dl_RTIReAssign();
        bl_emp_action.Rti_status = "PEN";
        bl_emp_action.Dept_Status = (GridView1.Rows[e.RowIndex].FindControl("ddl_dept_status") as DropDownList).SelectedItem.Value;
        bl_emp_action.R_office_map_id = (GridView1.Rows[e.RowIndex].FindControl("ddl_officer_grid") as DropDownList).SelectedItem.Value; 
        bl_emp_action.Ipaddress = ul.GetClientIpAddress(this.Page);
        HttpBrowserCapabilities browse = Request.Browser;
        bl_emp_action.User_browser =browse.Browser;
        bl_emp_action.Useros = Utilities.System_Info(this.Page);
        bl_emp_action.Useragent = Request.UserAgent.ToString();
        bl_emp_action.Isnew = "Y";
        bl_emp_action.Userid = Session["username"].ToString();
        bl_emp_action.Rti_id = GridView1.DataKeys[e.RowIndex].Values["rti_id"].ToString(); ;
        DateTime today = DateTime.Today;
        bl_emp_action.Action_date = today.ToString("yyyy/MM/dd");

        rb = dl_rti.Update_RTIstatus(bl_emp_action);
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
        
    }


    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //GridViewRow gvRow = e.Row;
        //if (gvRow.RowType == DataControlRowType.Header)
        //{
        //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell cell0 = new TableCell();
        //    cell0.Text = "RTI Information";
        //    cell0.HorizontalAlign = HorizontalAlign.Center;
        //    cell0.ColumnSpan = 5;
        //    TableCell cell1 = new TableCell();
        //    cell1.Text = "Office Information";
        //    cell1.HorizontalAlign = HorizontalAlign.Center;
        //    cell1.ColumnSpan = 5;
        //    gvrow.Cells.Add(cell0);
        //    gvrow.Cells.Add(cell1);
        //    GridView1.Controls[0].Controls.AddAt(0, gvrow);

        //}
       

        if (e.Row.RowType == DataControlRowType.DataRow)
            //if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            //{


            //    DropDownList ddl_state = (DropDownList)e.Row.FindControl("ddl_state");
            //    rd = dl.state_code();
            //    ddl_state.DataSource = rd.table;
            //    ddl_state.DataTextField = "state_name_e";
            //    ddl_state.DataValueField = "state_id";
            //    ddl_state.DataBind();
            //    ddl_state.Items.Insert(0, new ListItem("Select", "0"));
            //    ddl_state.SelectedValue = GridView1.DataKeys[e.Row.RowIndex].Values["state_id"].ToString();
            //}
        if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddl_district = (DropDownList)e.Row.FindControl("ddl_district_grid");
            //DropDownList ddl_state = (DropDownList)e.Row.FindControl("ddl_state");
            bl.StateCode = "22";
            rd = dl.district_code(bl);
            ddl_district.DataSource = rd.table;
            ddl_district.DataTextField = "District_Name";
            ddl_district.DataValueField = "District_ID";

            ddl_district.DataBind();
            ddl_district.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl_district.SelectedValue = GridView1.DataKeys[e.Row.RowIndex].Values["dist_id"].ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex )
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
        if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex )
        {
            DropDownList ddl_office = (DropDownList)e.Row.FindControl("ddl_office_grid");
            DropDownList ddl_district = (DropDownList)e.Row.FindControl("ddl_district_grid");
           // DropDownList ddl_state = (DropDownList)e.Row.FindControl("ddl_state");
            DropDownList ddl_department = (DropDownList)e.Row.FindControl("ddl_department_grid");
            bl.StateCode = "22";
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

        if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddl_office = (DropDownList)e.Row.FindControl("ddl_office_grid");
            DropDownList ddl_district = (DropDownList)e.Row.FindControl("ddl_district_grid");
            DropDownList ddl_department = (DropDownList)e.Row.FindControl("ddl_department_grid");
            DropDownList ddl_officer = (DropDownList)e.Row.FindControl("ddl_officer_grid");
            bl_RTI_Request objblData = new bl_RTI_Request();
            dl_RTIReAssign objData = new dl_RTIReAssign();
            ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
            objblData.District_id_ofc = ddl_district.SelectedValue;
            objblData.Base_department_id = ddl_department.SelectedValue;
            objblData.Office_id = ddl_office.SelectedValue;
            rd = objData.BindOfficerDL(objblData);
            ddl_officer.DataSource = rd.table;
            ddl_officer.DataTextField = "Name_en";
            ddl_officer.DataValueField = "office_mapping_id";
            ddl_officer.DataBind();
            ddl_officer.Items.Insert(0, new ListItem("--Select Officer--", "0"));
            ddl_officer.SelectedValue = GridView1.DataKeys[e.Row.RowIndex].Values["ofc_map_id"].ToString();
         }
        if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddl_status = (DropDownList)e.Row.FindControl("ddl_dept_status");

            dl_RTIReAssign objData = new dl_RTIReAssign();
            ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();

            rd = objData.BindRTIStatusDL();
            ddl_status.DataSource = rd.table;
            ddl_status.DataTextField = "name";
            ddl_status.DataValueField = "value";
            ddl_status.DataBind();
            ddl_status.Items.Insert(0, new ListItem("--Select Status--", "0"));


        }

        //if (Page.IsPostBack)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DropDownList ddl = e.Row.FindControl("ddl_district_grid") as DropDownList;
        //        if (ddl != null)
        //        {
        //            ddl.AutoPostBack = true;
        //            ddl.SelectedIndexChanged += new EventHandler(ddl_grid_district_SelectedIndexChanged);
                    
                  
        //        }
        //    }
        //}
       
        
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        // Start of Grid Header grouping 
        //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //TableHeaderCell cell = new TableHeaderCell();
        //cell.Attributes.CssStyle["text-align"] = "center";
        //cell.Text = "RTI Information";

        //cell.ColumnSpan = 5;
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Attributes.CssStyle["text-align"] = "center";
        //cell.ColumnSpan = 5;
        //cell.Text = "Officer Assignment Information";
        //row.Controls.Add(cell);

        //row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        //GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
        // End of Grid Header Grouping

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

