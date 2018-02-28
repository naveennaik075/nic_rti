using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTI_Request_Info : BasePage // System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_Submit_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/user/RTIRequest.aspx");
    }
}