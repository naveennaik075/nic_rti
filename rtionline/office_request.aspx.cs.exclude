using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class unregistered_office_request : System.Web.UI.Page
{
    bl_Dio bl = new bl_Dio();
    dl_Dio dl = new dl_Dio();
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
            bind_district();
            bind_department();
        }
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_ofc_lvl();
        bind_category();

    }
    protected void ddl_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_ofc_lvl();
        bind_category();

    }

    protected void ddl_Ofc_Lvl_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void bind_district()
    {
        ddl_district.Items.Clear();
        bl.State = "22";
        rd = dl.district(bl);
        ddl_district.DataSource = rd.table;
        ddl_district.DataTextField = "District_Name";
        ddl_district.DataValueField = "District_ID";
        ddl_district.Items.Add(new ListItem("Select District", "0"));
        ddl_district.DataBind();
    }
    public void bind_department()
    {
        ddl_department.Items.Clear();
        rd = dl.base_department(bl);
        ddl_department.DataSource = rd.table;
        ddl_department.DataTextField = "dept_name";
        ddl_department.DataValueField = "dept_id";
        ddl_department.Items.Add(new ListItem("Select Department", "0"));
        ddl_department.DataBind();
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
                bl.Ipaddress = ul.GetClientIpAddress(this.Page);
                bl.Useragent = Request.UserAgent.ToString();
                bl.Clientos = Utilities.System_Info(this.Page);
                bl.Client_browser = Request.Browser.Browser;
                if (fu_identity.PostedFile != null || fu_identity.PostedFile.FileName != "")
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
                            bl.Userid = "";
                            rb = dl.insert_ofc_new(bl, blr);
                            if (rb.status)
                            {
                                Utilities.MessageBox_UpdatePanel(udp, "Office request Has been Submitted. ");
                            }
                            else
                            {
                                Utilities.MessageBox_UpdatePanel(udp, "Office request failed due to some reason plaese try again later. ");
                            }
                        }
                    }
                    else
                    {
                        Utilities.MessageBox_UpdatePanel(udp, "provided Image is Big. Please provide Image Less than 200kb ");
                    }
                }
                // blr.RTI_fileID = fu_identity.PostedFile.FileName;

            }
        }

    }
}