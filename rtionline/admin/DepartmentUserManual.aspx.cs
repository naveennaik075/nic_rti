using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Transactions;

public partial class admin_DepartmentUserManual : BasePage // System.Web.UI.Page
{
    public string userID = null;
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("../logout.aspx");
        }
        else
        {
            userID = Session["username"].ToString();
          
            if (!Page.IsPostBack)
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
               
                DistrictBind();
                BaseDepartmentBind();
                OfficeCategoryBind();
                OfficeLevelBind();
                OfficeBind();
                hiddenn.Value = DateTime.Today.Year.ToString(); 
            }

        }

    }

    protected void DDL_Category_SelectedIndexChanged(object sender, EventArgs e)  // Office Category
    {
        OfficeLevelBind();
        OfficeBind();
       
    }
    protected void DDL_BaseDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeLevelBind();
        OfficeBind();
        
    }
    protected void DDL_OfficeLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        OfficeBind();
       
    }
    protected void DDL_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        OfficeLevelBind();
        OfficeBind();
       
    }

    public void DistrictBind()
    {
        dl_RTI_Request objDL = new dl_RTI_Request();
        //string dept = DDL_Department.SelectedItem.Value;
        ReturnClass.ReturnDataTable dt = objDL.Get_RTI_DDL_Data("SELECT District_ID, District_Name FROM districts WHERE StateCode='22' order by District_Name ");
        RTI_DDL_District.DataSource = dt.table;
        RTI_DDL_District.DataTextField = "District_Name";
        RTI_DDL_District.DataValueField = "District_ID";
        RTI_DDL_District.DataBind();
        RTI_DDL_District.Items.Insert(0, new ListItem("--Select District--", "0"));
    }



    public void BaseDepartmentBind()
    {
        dl_RTI_Request objDL = new dl_RTI_Request();
        //string dept = DDL_Department.SelectedItem.Value;
        ReturnClass.ReturnDataTable dt = objDL.Get_RTI_DDL_Data("SELECT dept_id, dept_name FROM basedepartment order by dept_name ");
        DDL_BaseDepartment.DataSource = dt.table;
        DDL_BaseDepartment.DataTextField = "dept_name";
        DDL_BaseDepartment.DataValueField = "dept_id";
        DDL_BaseDepartment.DataBind();
        DDL_BaseDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
    }

    public void OfficeCategoryBind()
    {
        dl_RTI_Request objDL = new dl_RTI_Request();
        //string dept = DDL_Department.SelectedItem.Value;
        ReturnClass.ReturnDataTable dt = objDL.Get_RTI_DDL_Data("SELECT OfficeCategoryCode, OfficeCategoryName FROM officeCategory order by OfficeCategoryName ");
        DDL_OfficeCategory.DataSource = dt.table;
        DDL_OfficeCategory.DataTextField = "OfficeCategoryName";
        DDL_OfficeCategory.DataValueField = "OfficeCategoryCode";
        DDL_OfficeCategory.DataBind();
        DDL_OfficeCategory.Items.Insert(0, new ListItem("--Select Office Category--", "0"));
    }

    public void OfficeLevelBind()
    {
        dl_RTI_Request objDL = new dl_RTI_Request();
        string dept = DDL_BaseDepartment.SelectedItem.Value;
        string strquery = "SELECT OfficeLevelCode, OfficeLevelName FROM officelevel WHERE  " +
                          " officelevel.BaseDeptCode='" + dept + "' order by OfficeLevelName ";
        ReturnClass.ReturnDataTable dt = objDL.Get_RTI_DDL_Data(strquery);
        DDL_OfficeLevel.DataSource = dt.table;
        DDL_OfficeLevel.DataTextField = "OfficeLevelName";
        DDL_OfficeLevel.DataValueField = "OfficeLevelCode";
        DDL_OfficeLevel.DataBind();
        DDL_OfficeLevel.Items.Insert(0, new ListItem("--Select Office Level--", "0"));
    }

    public void OfficeBind()
    {
        dl_RTI_Request objDL = new dl_RTI_Request();
        string dept = DDL_BaseDepartment.SelectedItem.Value;
        string district = RTI_DDL_District.SelectedItem.Value;
        string officeCategory = DDL_OfficeCategory.SelectedItem.Value;
        string officeLevel = DDL_OfficeLevel.SelectedItem.Value;
        string strquery = "SELECT NewOfficeCode, OfficeName FROM office WHERE  " +
                          " office.BaseDeptCode='" + dept + "' AND office.DistrictCodeNew='" + district + "'" +
                          " AND office.OfficeCategory='" + officeCategory + "' AND office.OfficeLevel='" + officeLevel + "' order by OfficeName ";
        ReturnClass.ReturnDataTable dt = objDL.Get_RTI_DDL_Data(strquery);
        DDL_Office.DataSource = dt.table;
        DDL_Office.DataTextField = "OfficeName";
        DDL_Office.DataValueField = "NewOfficeCode";
        DDL_Office.DataBind();
        DDL_Office.Items.Insert(0, new ListItem("--Select Office--", "0"));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        blAdmin objBL = new blAdmin();
        dlAdmin objDL = new dlAdmin();
        Utilities ul = new Utilities();
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            HttpBrowserCapabilities browse = Request.Browser;

            objBL.UserID = Session["username"].ToString();
            objBL.Client_ip = ul.GetClientIpAddress(this.Page);
            objBL.Useragent=  Request.UserAgent.ToString();
            objBL.ClientOS = Utilities.System_Info(this.Page);
            objBL.ClientBrowser= browse.Browser;

            objBL.Base_department_id = DDL_BaseDepartment.SelectedValue;
            objBL.District_id_ofc = RTI_DDL_District.SelectedValue;
            objBL.Office_category = DDL_OfficeCategory.SelectedValue;
            objBL.Office_level_id = DDL_OfficeLevel.SelectedValue;
            objBL.Office_id = DDL_Office.SelectedValue;
            objBL.Year_issue = txtYearOfIssue.Text;
            if (fu_UserManual.HasFile)
            {
                if (txt_file_desc.Text != "")
                {
                    HttpPostedFile file = fu_UserManual.PostedFile;
                    if (file.ContentLength < 20000000)     // 20 MB
                    {
                        if (file.ContentType == "application/pdf" || file.ContentType == "application/x-pdf"
                            || file.ContentType == "application/x-unknown")
                        {
                            
                            Stream fs = file.InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            objBL.FileName = file.FileName;
                            objBL.ContentType = file.ContentType;
                            objBL.FileData = bytes;
                            objBL.FileDescription = txt_file_desc.Text;

                        }
                        else
                        {
                            Utilities.MessageBox_UpdatePanel(update1, "Only PDF type File will be accepted");
                            return;
                        }

                    }
                    else
                    {
                        Utilities.MessageBox_UpdatePanel(update1, "Your file size is greater than 2 MB  ");
                        return;
                    }
                }
                else
                {
                    Utilities.MessageBox_UpdatePanel(update1, "File Description Is Required");
                    return;
                }
            }
            else
            {
                Utilities.MessageBox_UpdatePanel(update1, "PDF File required");
                return;
            }

            ReturnClass.ReturnBool rb = objDL.Insert_DeptUserManual(objBL);
            if (rb.status == true)
            {
                if (Session["language"].ToString() == "en-GB")
                {

                    Utilities.MessageBox_UpdatePanel(update1, "Action Submitted Successfully");
                }
                else
                {

                    Utilities.MessageBox_UpdatePanel(update1, "कार्रवाई सफलतापूर्वक सबमिट की गई");
                }
                //   Utilities.MessageBox_UpdatePanel_Redirect(udp, "Action Submitted Successfully", "../Employee/EmployeeWelcome_Page.aspx");
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {
                    Utilities.MessageBox_UpdatePanel(update1, "Action Not Submitted");

                }
                else
                {
                    Utilities.MessageBox_UpdatePanel(update1, "कार्रवाई सबमिट नहीं की गई");

                }
                // Utilities.MessageBox_UpdatePanel(udp, "Action Not Submitted");
            }
            // }  // end of Submit

        }


    } // End of ButtonSubmit click


}