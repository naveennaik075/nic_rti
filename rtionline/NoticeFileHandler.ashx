<%@ WebHandler Language="C#" Class="NoticeFileHandler" %>

using System;
using System.Web;

public class NoticeFileHandler : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        dl_rti_notice objDL = new dl_rti_notice();
        string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
        Utilities ut = new Utilities();
        string sas = context.Server.UrlDecode(context.Request.QueryString["fid"]);
        sas = sas.Replace(" ", "+");
        string decrypt_query_string = ut.Decrypt_AES(sas, key);
        string fid_str = decrypt_query_string;
        
        ReturnClass.ReturnDataTable dt = objDL.GetDataForHandler(fid_str);

        for (int i = 0; i < dt.table.Rows.Count; i++)
        {
            //context.Response.ContentType = "image/jpg";
            context.Response.ContentType = dt.table.Rows[i]["mime_type"].ToString();
            byte[] data = (byte[])(dt.table.Rows[i]["file_data"]);
            context.Response.BinaryWrite(data);
            //Similarly for QuantityInIssueUnit_uom.
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}