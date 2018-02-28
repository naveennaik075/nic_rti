using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_UserActionDetail : BasePage  // System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)

    {
        bl_rti_emp bl = new bl_rti_emp();
        dl_rti_emp dl = new dl_rti_emp();
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        if (Session["username"] != null)
        {


            bl.User_id = Session["username"].ToString();
            string rollid = null;
            rt = dl.GetRoll_ID(bl);
            if (rt.table.Rows.Count > 0)
            {
                if (Session["role"] == null)
                {
                    rollid = rt.table.Rows[0]["RollID"].ToString();
                }
                else
                {
                    rollid = Session["role"].ToString();         // This has been added on 26/sep/2017
                }


                    if (rollid == "2")  // User  ROll ID
                    {
                        this.MasterPageFile = "~/UserMaster.master";
                    }
                    else if (rollid == "3")
                    {
                        this.MasterPageFile = "~/Master_employee.master";
                    }
                //else if (rollid == "1")
                //{
                    
                //    this.MasterPageFile = "~/admin_master.master";
                //}
                //else if (rollid == "4" || rollid == "5")
                //{
                //    this.MasterPageFile = "~/master_dio.master";
                //}
            }
        }
     

    }





    protected void Page_Load(object sender, EventArgs e)
    {

    }

}