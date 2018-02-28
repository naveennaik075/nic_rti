using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report : System.Web.UI.Page
{
    bl_Dio bl = new bl_Dio();
    dl_Dio dl = new dl_Dio();
    bl_report bl1 = new bl_report();
    dl_report dl1 = new dl_report();
    bl_RTI_RequestFiles blr = new bl_RTI_RequestFiles();
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
            bind_district_code();
            ddl_district.SelectedValue = bl1.District_id;
            bind_department_id();
            ddl_department.SelectedValue = bl1.Department;
            BindOfficeView();

        }
    }
    public void bind_district_code()
    {
        ddl_district.Items.Clear();
        rd = dl1.district_code1();
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";

        ddl_district.DataBind();
        ddl_district.Items.Insert(0, new ListItem("Select", "0"));

    }
    public void bind_department_id()
    {
        ddl_department.Items.Clear();

        rd = dl1.department_id(bl1);
        ddl_department.DataSource = rd.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";
        ddl_department.DataBind();
        ddl_department.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void BindOfficeView()
    {

        bl1.Base_department = ddl_department.SelectedValue;
        bl1.District = ddl_district.SelectedValue;

        rd = dl1.GetTotalOfficeInGrid1(bl1);

        int row = rd.table.Rows.Count;
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

        lbl_count.Text = "Total Office = " + row.ToString() + "  and  Total page = " + page.ToString() + "";
        GV_Office.DataSource = rd.table;
        GV_Office.DataBind();

    }
    protected void GV_Office_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Office.PageIndex = e.NewPageIndex;
        BindOfficeView();


    }
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_section1.Text = "OFFICE";
        div_ofc.Visible = true;
        div_ofc_req.Visible = false;
        bindoffice();
        //   bind_department_id();


        BindOfficeView();
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_section1.Text = "OFFICE";
        div_ofc.Visible = true;
        div_ofc_req.Visible = false;
        bindoffice();
        BindOfficeView();

    }
    protected void ddl_doffice_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_doffice.SelectedIndex == 1)
        {
            lbl_section1.Text = "Section 1";
            pnl_grid.Visible = false;
            div_ofc.Visible = false;
            div_ofc_req.Visible = true;
            bind_ofc_lvl();
            bind_category();
            pnl_farm.Visible = false;
        }

    }
    void bindoffice()
    {
        ddl_doffice.Items.Clear();
        bl1.Base_department = ddl_department.SelectedValue;
        bl1.District = ddl_district.SelectedValue;
        rd = dl1.GetTotalOfficeInGrid1(bl1);
        ddl_doffice.DataSource = rd.table;
        ddl_doffice.DataValueField = "NewOfficeCode";
        ddl_doffice.DataTextField = "office";
        ddl_doffice.DataBind();
        ddl_doffice.Items.Insert(0, "-----Select Office------");
        ddl_doffice.Items.Insert(1, "----- Add Your Office------");
    }
    public void bind_ofc_lvl()
    {
        ddl_Ofc_Lvl.Items.Clear();
        bl.Base_department = ddl_department.SelectedValue;
        rd = dl.office_level(bl);
        ddl_Ofc_Lvl.DataSource = rd.table;
        ddl_Ofc_Lvl.DataTextField = "Office_level";
        ddl_Ofc_Lvl.DataValueField = "olc";
        ddl_Ofc_Lvl.Items.Add(new ListItem("Select Office Level", "0"));
        ddl_Ofc_Lvl.DataBind();

    }
    public void bind_category()
    {
        ddl_category.Items.Clear();
        rd = dl.category(bl);
        ddl_category.DataSource = rd.table;
        ddl_category.DataTextField = "name";
        ddl_category.DataValueField = "code";
        ddl_category.Items.Add(new ListItem("Select Office Category", "0"));
        ddl_category.DataBind();
    }
    protected void btn_change_Click(object sender, EventArgs e)
    {
        pnl_farm.Visible = false;
        pnl_div.Visible = true;
    }
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        DataTable officefile_dt = new DataTable("dt");
        officefile_dt.Columns.Add("filename", typeof(string));
        officefile_dt.Columns.Add("filetype", typeof(string));
        officefile_dt.Columns.Add("filedata", typeof(Byte[]));
        //if (fu_identity.PostedFile != null || fu_identity.PostedFile.FileName != "")
        if(fu_identity.HasFile)
        {
            if (fu_identity.PostedFile.ContentLength < 209715)
            {
                if (fu_identity.PostedFile.ContentType == "image/jpeg" || fu_identity.PostedFile.ContentType == "image/pipeg")
                {
                    blr.RTI_fileName = fu_identity.PostedFile.FileName;
                    blr.RTI_fileType = fu_identity.PostedFile.ContentType;
                    Stream fs = fu_identity.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    blr.RTI_fileData = bytes;
                    officefile_dt.Rows.Add(new object[] { blr.RTI_fileName, blr.RTI_fileType, blr.RTI_fileData });
                    ViewState["file"] = officefile_dt;
                    pnl_div.Visible = false;
                    pnl_farm.Visible = true;
                    lbl_department.Text = ddl_department.SelectedItem.Text;
                    lbl_district.Text = ddl_district.SelectedItem.Text;
                    Lbl_ofc_lvl.Text = ddl_Ofc_Lvl.SelectedItem.Text;
                    lbl_ofc_cat.Text = ddl_category.SelectedItem.Text;
                    lbl_ofc_name.Text = txt_office.Text;
                    lbl_ofc_address.Text = txt_address.Text;
                    if (txt_contact.Text == "")
                    {
                        lbl_contact.Text = "Not Available";
                    }
                    else
                    {
                        lbl_contact.Text = txt_contact.Text;
                    }
                    if (txt_email.Text == "")
                    {
                        lbl_email_id.Text = "Not Available";
                    }
                    else
                    {
                        lbl_email_id.Text = txt_contact.Text;
                    }
                    if (txt_fax.Text == "")
                    {
                        lbl_fax_number.Text = "Not Available";
                    }
                    else
                    {
                        lbl_fax_number.Text = txt_contact.Text;
                    }
                    if (txt_url.Text == "")
                    {
                        lbl_website.Text = "Not Available";
                    }
                    else
                    {
                        lbl_website.Text = txt_contact.Text;
                    }
                }

                else
                {
                    Utilities.MessageBox_UpdatePanel(udp, "The provided file in not image format, Please provide image of your identity.");

                }
            }
            else
            {
                Utilities.MessageBox_UpdatePanel(udp, "provided Image is Big. Please provide Image Less than 200kb ");
            }

        }

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                bl.State = "22";
                bl.Active = "N";
                bl.Country = "91";
                bl.District = ddl_district.SelectedValue;
                bl.Officelvl = ddl_Ofc_Lvl.SelectedValue;
                bl.Base_department = ddl_department.SelectedValue;
                bl.Category = ddl_category.SelectedValue;
                bl.Office = txt_office.Text;
                bl.Address = txt_address.Text;
                bl.Contact = txt_contact.Text;
                bl.Fax = txt_fax.Text;
                bl.Email = txt_email.Text;
                bl.Url = txt_url.Text;
                bl.Officeid = dl.Max_office_code(bl);
                bl.Userid = "";
                bl.Ipaddress = ul.GetClientIpAddress(this.Page);
                bl.Useragent = Request.UserAgent.ToString();
                bl.Clientos = Utilities.System_Info(this.Page);
                bl.Client_browser = Request.Browser.Browser;
                Session["office_print"] = bl.Officeid;
                DataTable dtt = (DataTable)ViewState["file"];
                blr.RTI_fileName = dtt.Rows[0]["filename"].ToString();
                blr.RTI_fileType = dtt.Rows[0]["filetype"].ToString();
                blr.RTI_fileData = (byte[])dtt.Rows[0]["filedata"];
                bl.Userid = "";
                rb = dl.insert_ofc_new(bl, blr);
                if (rb.status)
                {
                    Utilities.MessageBox_UpdatePanel_Redirect(udp, "Office request Has been Submitted.", "office_print.aspx");
                }
                else
                {
                    Utilities.MessageBox_UpdatePanel(udp, "Office request failed due to some reason plaese try again later. ");
                }

            }
        }

    }
}



//if (fu_identity.PostedFile != null || fu_identity.PostedFile.FileName != "")
//{
//    if (fu_identity.PostedFile.ContentLength < 209715)
//    {
//        if (fu_identity.PostedFile.ContentType == "image/jpeg" || fu_identity.PostedFile.ContentType == "image/pipeg")
//        {
//            blr.RTI_fileName = fu_identity.PostedFile.FileName;
//            blr.RTI_fileType = fu_identity.PostedFile.ContentType;
//            Stream fs = fu_identity.PostedFile.InputStream;
//            BinaryReader br = new BinaryReader(fs);
//            byte[] bytes = br.ReadBytes((Int32)fs.Length);
//            blr.RTI_fileData = bytes;
//            bl.Userid = "";
//            rb = dl.insert_ofc_new(bl, blr);
//            if (rb.status)
//            {
//                Utilities.MessageBox_UpdatePanel_Redirect(udp, "Office request Has been Submitted.", "office_print.aspx");
//            }
//            else
//            {
//                Utilities.MessageBox_UpdatePanel(udp, "Office request failed due to some reason plaese try again later. ");
//            }
//        }
//        else
//        {
//            Utilities.MessageBox_UpdatePanel(udp, "The provided file in not image format, Please provide image of your identity.");

//        }
//    }
//    else
//    {
//        Utilities.MessageBox_UpdatePanel(udp, "provided Image is Big. Please provide Image Less than 200kb ");
//    }
//}
// blr.RTI_fileID = fu_identity.PostedFile.FileName;


