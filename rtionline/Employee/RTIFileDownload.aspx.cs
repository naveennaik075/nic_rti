using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
public partial class Employee_RTIFileDownload : System.Web.UI.Page
{
    bl_employee_action bl1 = new bl_employee_action();
    dl_employee_action dl1 = new dl_employee_action();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string key = ConfigurationManager.AppSettings["EncKey"].ToString();
            Utilities ut = new Utilities();
            //bl1.RTI_fileID = ((LinkButton)sender).CommandArgument.ToString();
           // bl1.RTI_fileID = Request.QueryString["rtiid"];
            string sas = Server.UrlDecode(Request.QueryString["rtiid"].ToString());
            sas = sas.Replace(" ", "+");
            string decrypt_query_string = ut.Decrypt_AES(sas, key);
            bl1.RTI_fileID = decrypt_query_string;

            rd = dl1.select_rti_file(bl1);
            if (rd.table.Rows.Count > 0)
            {
                byte[] bt = (byte[])rd.table.Rows[0]["FileData"];
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = rd.table.Rows[0]["FileType"].ToString();
                Response.AddHeader("content-disposition", "attachment;filename="
                    //      + rd.table.Rows[0]["fileName"].ToString());
        + "abc.pdf");
                Response.BinaryWrite(bt);
                Response.Flush();
                Response.End();

            }
        }
        catch (Exception ex)
        { }
    }
}