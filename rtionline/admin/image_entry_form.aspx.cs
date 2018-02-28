using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_image_entry_form : BasePage  //System.Web.UI.Page
{
    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    dl_rti_notice dl = new dl_rti_notice();
    Upload_doc bl = new Upload_doc();
    Utilities util = new Utilities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            try
            {
                if (Session["username"] != null)
                {
                    bl.user_id = Session["username"].ToString();
                }
                else
                {
                    Response.Redirect("~/logout.aspx");
                }
                
                if (Btnsubmit.Text == "submit")
                {
                    Image1.Visible = false;
                }
                else
                {
                    Image1.Visible = true;
                }
            }
            catch
            {
                Response.Redirect("~/logout.aspx");
            }
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }

    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

                bl.user_id = Session["username"].ToString();
                bl.district_id = ""; //Session["Dist_Code"].ToString();
                

               // Int32 maxLimit = 100000;// for 100 kb
                Int32 maxLimit = 5000;


                string content_type;
                if (FileUpload7.HasFile)
                { 
                  //  string filename=  FileUpload7.FileName;
                    double size = FileUpload7.PostedFile.ContentLength;
                    string contenttype = FileUpload7.PostedFile.ContentType;

                    if (FileUpload7.PostedFile.ContentLength > maxLimit)
                    {
                        Utilities.MessageBoxShow("File Is too Big...Maximum Size allowed is 5 KB");
                    }
                    else if (contenttype == "image/gif" || contenttype == "image/png" || contenttype == "image/jpg" || contenttype == "image/jpeg" || contenttype == "image/pjpeg" || contenttype == "image/x-png")
                    {
                        
                        content_type = FileUpload7.PostedFile.ContentType.ToString();
                       // string filename = System.IO.Path.GetFileName(FileUpload7.PostedFile.FileName);

                        //Get Filename from fileupload control
                        string filename = Path.GetFileName(FileUpload7.PostedFile.FileName);


                        //Save images into Images folder
                        FileUpload7.SaveAs(Server.MapPath("../images/" + filename));
                        bl.filename = "../images/" + filename;
                       // bl.filename = "images/" + filename;

                       
                        rb = dl.Insert_notice_image(bl);
                        if (rb.status == true)
                        {
                            Utilities.MessageBoxShow("Record Saved successfully");
                        }
                        else
                        {
                            Utilities.MessageBoxShow("Please Try again");
                        }
                    }
                    else
                    {
                        Utilities.MessageBoxShow("सिर्फ़ .jpg,.png,.gif,.jpeg दस्तावेज की अनुमति है, कृप्या पुनः प्रयास करे");
                    }
                }
                else
                {
                    Utilities.MessageBoxShow("कृप्या पुनः प्रयास करे");
                     
                }
            }
            else
            {
                Utilities.MessageBoxShow(" पेज रिफ्रेश करना वर्जित है ");
            }
        }
    }
}