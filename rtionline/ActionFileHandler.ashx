<%@ WebHandler Language="C#" Class="ActionFileHandler" %>

using System;
using System.Web;

public class ActionFileHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        dl_rti_emp objDL = new dl_rti_emp();
        ReturnClass.ReturnDataTable dt = objDL.GetDataForHandler(context.Request.QueryString["fileid"]);

        for (int i = 0; i < dt.table.Rows.Count; i++)
        {
            //context.Response.ContentType = "image/jpg";
            context.Response.ContentType = dt.table.Rows[i]["ContentType"].ToString();
            byte[] data = (byte[])(dt.table.Rows[i]["fileData"]);
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