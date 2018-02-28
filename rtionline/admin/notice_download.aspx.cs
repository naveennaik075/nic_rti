using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_notice_download : System.Web.UI.Page
{
    Notice_Entry n = new Notice_Entry();

    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
    dl_rti_notice dl = new dl_rti_notice();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //n.file_id = Session["fid"].ToString();
            n.file_id=Request.QueryString["fid"].ToString();
            
        }
        catch
        {
            Response.Redirect("log_out.aspx");

        }
        dt = dl.select_file_details(n);

        if (dt.table.Rows.Count > 0)
        {
            string ext = "";
            if (dt.table.Rows[0]["mime_type"].ToString() == "application/pdf" || dt.table.Rows[0]["mime_type"].ToString() == "application/x-pdf" || dt.table.Rows[0]["mime_type"].ToString() == "application/x-unknown")
            {
                ext = ".pdf";
            }
            else if (dt.table.Rows[0]["mime_type"].ToString() == "image/png" || dt.table.Rows[0]["mime_type"].ToString() == "image/gif" || dt.table.Rows[0]["mime_type"].ToString() == "image/jpg" || dt.table.Rows[0]["mime_type"].ToString() == "image/jpeg")
            {
                ext = ".jpeg";
            }
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = dt.table.Rows[0]["mime_type"].ToString();
            
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            byte[] b = ((byte[])dt.table.Rows[0]["file_data"]);
            Response.BinaryWrite(b);
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();


        }
    }
}