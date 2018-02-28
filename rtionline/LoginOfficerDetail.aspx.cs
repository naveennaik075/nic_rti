using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginOfficerDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
                BindData();
            
        }  

    }

    protected void BindData() {
               
              string userName= Session["username"].ToString();
              dl_LoginOfficerDetail objData = new dl_LoginOfficerDetail();
              ReturnClass.ReturnDataTable dt = objData.GetLoginOfficerDetail(userName);
              //ReturnClass.ReturnDataTable dt = objData.GetLoginOfficerDetail(userName);
              //lbl_UserName.Text = dt.table.Rows[0]["Name"].ToString();
              //lbl_UserType.Text = dt.table.Rows[0]["UserType"].ToString();
              //lbl_LoginTime.Text = dt.table.Rows[0]["LoginTimeStamp"].ToString();
              //ReturnClass.ReturnBool status = objData.UpdateTimeStamp(userName);
              //lbl_RequestApealStatus.Text = DateTime.Now.ToString("dd/MM/yyyy");
              //dt= objData.GetRequestCount(userName, "registered");
              //lbl_Request_Registered.Text = dt.table.Rows[0]["RequestCount"].ToString();
              //dt = objData.GetRequestCount(userName, "pending");
              //lbl_Request_Pending.Text = dt.table.Rows[0]["RequestCount"].ToString();
              //dt = objData.GetRequestCount(userName, "disposedof");
              //lbl_Request_DisposedOf.Text = dt.table.Rows[0]["RequestCount"].ToString();

    }// End of Bind Data


} // End of Login Detail