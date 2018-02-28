using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_DeptUserManual : BasePage // System.Web.UI.Page
{
    Notice_Entry bl = new Notice_Entry();
    dl_rti_notice dl = new dl_rti_notice();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();

    protected override void InitializeCulture()
    {
        Culture = "en-GB";
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");

        base.InitializeCulture();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            GridView1.ShowFooter = true;


            GridView1.AllowPaging = true;

            bind_district();
            BaseDepartmentBind();
            OfficeBind();
            //  db.gv(qry, GridView1, Label1);
            bind_GridView();

        }
  
    } // ENd of Page_Load
    protected void btn_search_click(object sender, EventArgs e)
    {
        
        bind_GridView();
    }

    protected void lnk_file_Click(object sender, EventArgs e)
    {
        string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
        Utilities ut = new Utilities();
        string encrypt_file_id = ((LinkButton)sender).CommandArgument.ToString();
        string file_id = ut.Encrypt_AES(encrypt_file_id, key);

        string filedes = ((LinkButton)sender).Text.ToString();
        if (filedes == "No File")
        {

        }
        else
        {
            Response.Redirect("DeptUserManualFileHandler.ashx?fid=" + Server.UrlEncode(file_id));
        }
    } // End of lnk_file_click


    protected void a123_Click(object sender, EventArgs e)
    {
        if (pp.Visible == true)
        {
            pp.Visible = false;
        }
        else
        {
            pp.Visible = true;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.AllowPaging = true;
        GridView1.PageIndex = e.NewPageIndex;

   
     //   bind_GridView();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // string nid = GridView1.DataKeys[e.Row.RowIndex].Values["notice_id"].ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk_button = (e.Row.FindControl("lnk_file") as LinkButton);
            HiddenField h_field = (e.Row.FindControl("h_file_name") as HiddenField);
            string fid = h_field.Value;

            if (fid == "No File")
            {
                lnk_button.Enabled = false;
            }
            else
            {
                lnk_button.Enabled = true;

            }

        }
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

    public void OfficeBind()
    {
        Notice_Entry bl = new Notice_Entry();
        dl_rti_notice dl = new dl_rti_notice();
        bl.Base_dept_id = ddl_department.SelectedItem.Value;
        bl.District_id = ddl_district.SelectedItem.Value;
        ReturnClass.ReturnDataTable dt = dl.Get_Office_For_Notice(bl);
        ddl_office.DataSource = dt.table;
        ddl_office.DataTextField = "OfficeName";
        ddl_office.DataValueField = "NewOfficeCode";
        ddl_office.DataBind();
        ddl_office.Items.Insert(0, new ListItem("--Select Office--", "0"));
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {

        OfficeBind();

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeBind();
    }
    public void bind_GridView()
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
              
        bl.District_id = ddl_district.SelectedValue;
        bl.Base_dept_id = ddl_department.SelectedValue;
        bl.Office_id = ddl_office.SelectedValue;
        bl.IssueYear =  txtYear.Text;
        dt = dl.Get_DeptUserManual_File(bl);
        int row = dt.table.Rows.Count;
        int page;
        if (row % 15 == 0)
        {
            page = row / 15;
        }
        else
        {
            page = row / 15;
            page = page + 1;
        }

        if (Session["language"].ToString() == "en-GB")
        {
            lbl_count.Text = "Total File = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        }
        else
        {
            lbl_count.Text = "कुल फ़ाइल् = " + row.ToString() + "  और  कुल पेज = " + page.ToString() + "";
        }
        // lbl_count.Text = "Total Records = " + row.ToString() + "  and  Total page = " + page.ToString() + "";

        GridView1.DataSource = dt.table;
        GridView1.DataBind();

    }

    //private string getDate(string givendate)
    //{
    //    if (givendate == "") return givendate;
    //    string date1 = givendate.ToString();

    //    string dd = date1.Substring(0, 2);
    //    string mm = date1.Substring(3, 2);
    //    string yyyy = date1.Substring(6, 4);
    //    string date2 = yyyy + "-" + mm + "-" + dd;
    //    return date2;
    //}

}// End of Class