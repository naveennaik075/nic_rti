using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : BasePage  //System.Web.UI.Page
{
    bl_login bl = new bl_login();
    dl_login dl = new dl_login();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnDataTable rd1 = new ReturnClass.ReturnDataTable();
    Utilities ul = new Utilities();
    // dl_Dio dlDio = new dl_Dio();
    bl_Dio blDio = new bl_Dio();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblCount.Text = Application["NoOfVisitors"].ToString();
         if (!Page.IsPostBack)
         {             
            //rd = dl.countEmp(blDio);
            //if (rd.table.Rows.Count > 0)
            //{
            //    lbl_employee_count.Text = rd.table.Rows[0]["empcount"].ToString();

            //}
            //rd = dl.countDepartment(blDio);
            //if (rd.table.Rows.Count > 0)
            //{
            //    lbl_department_count.Text = rd.table.Rows[0]["depcount"].ToString();

            //}
            //rd = dl.countOffice(blDio);
            //if (rd.table.Rows.Count > 0)
            //{
            //    lbl_office_count.Text = rd.table.Rows[0]["office_count"].ToString();

            //}
            //rd = dlDio.countOfficer(blDio);
            //if (rd.table.Rows.Count > 0)
            //{
            //    lbl_officer.Text = rd.table.Rows[0]["officer_count"].ToString();

            //}
            notice_board();
            
       }
            counter_box();
    }

    protected void login_form_submit_Click(object sender, EventArgs e)
    {
        HttpBrowserCapabilities browse = Request.Browser;
       
        Captcha1.ValidateCaptcha(txt_captcha.Text.Trim());
        if (Captcha1.UserValidated)
        {

            bl.UserID = txt_usr_id.Text;
            bl.Password = ul.GenerateMd5Hash(txt_Password.Text);
            bl.UserIP = ul.GetClientIpAddress(this.Page);
            bl.UserOS = Utilities.System_Info(this.Page);
            bl.UserAgent = browse.Version;
            bl.User_browser = browse.Browser;
            bl.Succesful_login = "n";

            rd = dl.check_login(bl);
            if (rd.table.Rows.Count > 0)
            {
                bl.Succesful_login = "Y";
                rb = dl.insert_logintrail(bl);
                string b = "true";
                Session["checkroll"] = b;
                //Session["username"] = txt_usr_id.Text;
                Session["username"] = rd.table.Rows[0]["UserID"].ToString();
                Session["role"] = rd.table.Rows[0]["Role_id"].ToString();
                Session["EmpMapID"] = rd.table.Rows[0]["office_mapping_id"].ToString();
                //    Session["WelcomePage"] = rd.table.Rows[0]["WelcomePage"].ToString();
                if (rd.table.Rows.Count > 1)
                {
                    string role, rolenew = "";
                    int i = 0;
                    //  rd1 = dl.select_emp(bl);

                    foreach (DataRow row in rd.table.Rows)
                    {
                        if (i == 0)
                        {
                            role = row["role_id"].ToString();
                            rolenew = row["role_id"].ToString();
                            i++;
                        }
                        else
                        {
                            if (rolenew != row["role_id"].ToString())
                            {
                                b = "false";
                                Session["checkroll"] = b;
                            }
                        }
                    }
                    string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
                    Utilities ut = new Utilities();
                    string encrypt_b = ut.Encrypt_AES(b, key);

                    Response.Redirect("employee_erole.aspx?b=" + encrypt_b);

                }
                else
                {
                    Response.Redirect(rd.table.Rows[0]["WelcomePage"].ToString());
                }
            }
            else
            {

                bl.Succesful_login = "N";

                rb = dl.insert_logintrail(bl);
                Utilities.MessageBoxShow("Check Your Username And Password That You Inserted");
                txt_captcha.Text = "";
                txt_Password.Text = "";
                txt_usr_id.Text = "";

            }

        }
        else
        {
            Utilities.MessageBoxShow("Please Enter Right Text In Captcha Box");
            txt_captcha.Text = "";
        }


    }

    protected void lnk_Click_showAllNotice(object sender, EventArgs e)
    {

      Response.Redirect("~/NoticeAll.aspx");
    }
    //public void notice_board()
    //{

    //    try
    //    {
    //        dl_Notice dl = new dl_Notice();
    //        bl_Notice bl = new bl_Notice();
    //        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
    //        // ur.District_id = Session["di"].ToString();
    //        dt = dl.Get_Notice_Data(bl);


    //        if (dt.table.Rows.Count > 0)
    //        {
    //            Literal1.Text = "<div class='col-sm-12  pull-left'>";
    //            //Literal1.Text = "<div class='col-sm-12' >";
    //            Literal1.Text = Literal1.Text + "<div class='col-sm-12 emphasis '>";
    //            Literal1.Text = Literal1.Text + "<h3 class='text-center'> Notice Board </h3 >";
    //           // Literal1.Text = Literal1.Text + "<div style='height:215px; padding-left:30px; overflow:auto;'>";
    //            Literal1.Text = Literal1.Text + " <table border=0  >";

    //            for (int i = 0; i < dt.table.Rows.Count; i++)
    //            {
    //                //Literal11.Text = Literal11.Text + "<li style=padding-bottom:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + "<img src=" + dt.table.Rows[i]["image"].ToString() + " />" + "<a target='_blank' href=" + dt.table.Rows[i]["url"].ToString() + "?fid=" + dt.table.Rows[i]["file_id"].ToString() + ">" + "<span id='blinker'>" + "<b>" + dt.table.Rows[i]["Header"].ToString() + "</b> </span> " + "<span class='spa'><b>" + dt.table.Rows[i]["Publish_Date"].ToString() + "</b>" + " </span> " + "</a>" + " </li>";
    //                if (dt.table.Rows[i]["notice_file_id"].ToString() == "NOT_AVL")
    //                {
    //                    Literal1.Text = Literal1.Text + "<tr> <td>" + (i+1) +" </td>" + "<td><span class=\"spa\" style=\"color: #FFAAFF\">" + dt.table.Rows[i]["Subject"].ToString() + "</span> </td>" + " <td> <span class=\"spa\" style=\"color: #00AA22\" ><b>" + dt.table.Rows[i]["Validity_From"].ToString() + "</b>" + " </span> </td>" + "  </tr>";
    //                }
    //                else
    //                {
    //                    string key = System.Configuration.ConfigurationManager.AppSettings["EncKey"].ToString();
    //                    Utilities ut = new Utilities();
    //                    string encrypt_rti_id = dt.table.Rows[i]["notice_id"].ToString();
    //                    string notice_id = ut.Encrypt_AES(encrypt_rti_id, key);

    //                    Literal1.Text = Literal1.Text + "<tr> <td>" + (i + 1) + " </td> <td> <a target='_blank'  href=\"NoticeFileHandler.ashx?fid=" + notice_id + "\">" + " <span class=\"spa\" style=\"color: #ffff00\">" + dt.table.Rows[i]["Subject"].ToString() + "  </span>" + "</a> </td> <td>" + "<span class=\"spa\" style=\"color: #ffff00\" ><b>" + dt.table.Rows[i]["Validity_From"].ToString() + "</b>" + " </span> " + " </td>";
    //                }
    //            }
    //            Literal1.Text = Literal1.Text + "</table>" + "</div></div>";// "</div></div></div>";
    //        }
    //    }

    //    catch
    //    {
    //    }


    //}// End of function notice_board()

    //-------------------------Naveen notice board------------------------//
    public void notice_board()
    {
        try
        {
            dl_rti_notice dl = new dl_rti_notice();
            //bl_Notice bl = new bl_Notice();
            ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
            dt = dl.Get_Notice_Data1();
           
            if (dt.table.Rows.Count > 0)
            {
                //Literal1.Text = "<div class='col-sm-12 noticeboard pull-left'>";

                ////Literal1.Text = Literal1.Text + "<div class='col-sm-12 emphasis '>";
                //Literal1.Text = Literal1.Text + "<div class='col-sm-12'>";
                ////Literal1.Text = Literal1.Text + "<h2 class='text-center'> <%=Resources.Resource.NoticeBoard %> </h2 >";
                //if (Session["language"].ToString() == "hi-IN")
                //{
                //    Literal1.Text = Literal1.Text + "<h2 class='text-center'> सूचना पटल  </h2 >";
                //}
                //else
                //{
                //    Literal1.Text = Literal1.Text + "<h2 class='text-center'> Notice Board </h2 >";
                //}
                Literal1.Text = Literal1.Text + "<div >";
                //Literal1.Text = Literal1.Text + "<div style='height:215px; padding-left:15px; overflow:auto;'>";

                Literal1.Text = Literal1.Text;
 
                for (int i = 0; i < dt.table.Rows.Count; i++)
                {
                    if (dt.table.Rows[i]["Hyperlink"].ToString() == "Y")
                    {
                        if (dt.table.Rows[i]["bold"].ToString() == "Y")
                        {
                            if (dt.table.Rows[i]["image"].ToString() != "")
                            {
                                if (dt.table.Rows[i]["blink"].ToString() == "Y")
                                {
                                    Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><span id='blinker' class='blink_me'><img height=\"20\" width=\"32\" src=" + dt.table.Rows[i]["image"].ToString() + " /></span>" + "<a target='_blank' href=" + dt.table.Rows[i]["url"].ToString() + "?fid=" + dt.table.Rows[i]["file_id"].ToString() + ">" + "<b style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Header"].ToString() + "</b>" + "<span class='spa' style=color:" + dt.table.Rows[i]["color"].ToString() + ">" + " </span> " + "<b>" + dt.table.Rows[i]["Publish_Date"].ToString() + "</b>" + "</a>" + "</li>";


                                    //Literal11.Text = Literal11.Text + "<li style=padding-bottom:10px;><span id='blinker' class='blink_me'><img style=padding-bottom:10px; src=" + dt.table.Rows[i]["image"].ToString() + " /></span>" + "<a target='_blank' href=" + dt.table.Rows[i]["url"].ToString() + "?fid=" + dt.table.Rows[i]["file_id"].ToString() + ">" + "<span style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + "<b>" + dt.table.Rows[i]["Header"].ToString() + "</b> </span> " + "<span class='spa' style=color:" + dt.table.Rows[i]["color"].ToString() + "><b>" + dt.table.Rows[i]["Publish_Date"].ToString() + "</b>" + " </span> " + "</a>" + " </li>";
                                }
                                else
                                {
                                    Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><img height=\"20\" width=\"32\" src=" + dt.table.Rows[i]["image"].ToString() + " />" + "<a target='_blank' href=" + dt.table.Rows[i]["url"].ToString() + "?fid=" + dt.table.Rows[i]["file_id"].ToString() + ">" + "<b style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Header"].ToString() + "</b>" + "<span class='spa' style=color:" + dt.table.Rows[i]["color"].ToString() + ">" + " </span> " + "<b>" + dt.table.Rows[i]["Publish_Date"].ToString() + "</b>" + "</a>" + "</li>";
                                }
                            }
                            else
                            {
                                Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><a target='_blank' href=" + dt.table.Rows[i]["url"].ToString() + "?fid=" + dt.table.Rows[i]["file_id"].ToString() + ">" + "<b style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Header"].ToString() + "</b>" + "<span class='spa' style=color:" + dt.table.Rows[i]["color"].ToString() + ">" + " </span> " + "<b>" + dt.table.Rows[i]["Publish_Date"].ToString() + "</b>" + "</a>" + "</li>";
                            }
                        }
                        else
                        {
                            if (dt.table.Rows[i]["image"].ToString() != "")
                            {
                                if (dt.table.Rows[i]["blink"].ToString() == "Y")
                                {
                                    Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><span id='blinker' class='blink_me'><img height=\"20\" width=\"32\" style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + " src=" + dt.table.Rows[i]["image"].ToString() + " /></span>" + "<a style=color:" + dt.table.Rows[i]["color"].ToString() + " target='_blank' href=" + dt.table.Rows[i]["url"].ToString() + "?fid=" + dt.table.Rows[i]["file_id"].ToString() + ">" + "<span style=color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Header"].ToString() + "</span>" + "<span class='spa' style=color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Publish_Date"].ToString() + "</span>" + "</a>" + "</li>";
                                }
                                else
                                {
                                    Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><img height=\"20\" width=\"32\" style=color:" + dt.table.Rows[i]["color"].ToString() + " src=" + dt.table.Rows[i]["image"].ToString() + " />" + "<a style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + " target='_blank' href=" + dt.table.Rows[i]["url"].ToString() + "?fid=" + dt.table.Rows[i]["file_id"].ToString() + ">" + dt.table.Rows[i]["Header"].ToString() + "<span class='spa' style=color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Publish_Date"].ToString() + "</span>" + "</a>" + "</li>";
                                }
                            }
                            else
                            {
                                Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><a target='_blank' style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + " href=" + dt.table.Rows[i]["url"].ToString() + "?fid=" + dt.table.Rows[i]["file_id"].ToString() + ">" + dt.table.Rows[i]["Header"].ToString() + "<span class='spa' style=color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Publish_Date"].ToString() + "</span>" + "</a>" + "</li>";
                            }
                        }
                    }
                    else if (dt.table.Rows[i]["Hyperlink"].ToString() == "N")
                    {
                        if (dt.table.Rows[i]["bold"].ToString() == "Y")
                        {
                            if (dt.table.Rows[i]["image"].ToString() != "")
                            {
                                if (dt.table.Rows[i]["blink"].ToString() == "Y")
                                {
                                    Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><span id='blinker' class='blink_me'>" + "<img height=\"20\" width=\"32\" src=" + dt.table.Rows[i]["image"].ToString() + " /></span>" + "<b style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Header"].ToString() + "</b>" + "<span class='spa' style=color:" + dt.table.Rows[i]["color"].ToString() + ">" + "</span>" + "<b>" + dt.table.Rows[i]["Publish_Date"].ToString() + "</b>" + "</li>";
                                }
                                else
                                {

                                    Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><img height=\"20\" width=\"32\" src=" + dt.table.Rows[i]["image"].ToString() + " />" + "<b style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Header"].ToString() + "</b>" + "<span class='spa' style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + "</span>" + "<b>" + dt.table.Rows[i]["Publish_Date"].ToString() + "</b>" + "</li>";
                                }
                            }
                            else
                            {
                                Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><b>" + dt.table.Rows[i]["Header"].ToString() + "</b>" + "<span class='spa' style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + "</span>" + "<b>" + dt.table.Rows[i]["Publish_Date"].ToString() + "</b>" + "</li>";
                            }
                        }
                        else
                        {
                            if (dt.table.Rows[i]["image"].ToString() != "")
                            {
                                if (dt.table.Rows[i]["blink"].ToString() == "Y")
                                {
                                    Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><span id='blinker' class='blink_me' style=color:" + dt.table.Rows[i]["color"].ToString() + ">" + "<img height=\"20\" width=\"32\" style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + " src=" + dt.table.Rows[i]["image"].ToString() + " /></span>" + dt.table.Rows[i]["Header"].ToString() + "<span class='spa' style=padding-bottom:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Publish_Date"].ToString() + "</span>" + "</li>";
                                }
                                else
                                {
                                    Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;><img height=\"20\" width=\"32\"  style=color:" + dt.table.Rows[i]["color"].ToString() + " src=" + dt.table.Rows[i]["image"].ToString() + " /><span style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Header"].ToString() + "</span><span class='spa' style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Publish_Date"].ToString() + "</span>" + "</li>";
                                }
                            }
                            else
                            {
                                Literal1.Text = Literal1.Text + "<li style=padding-bottom:10px;>" + dt.table.Rows[i]["Header"].ToString() + "<span class='spa' style=padding-left:10px;color:" + dt.table.Rows[i]["color"].ToString() + ">" + dt.table.Rows[i]["Publish_Date"].ToString() + "</span>" + "</li>";
                            }
                        }
                    }
                  }

                 
                //Literal1.Text = Literal1.Text + "</div></div><div style=\"color:black; padding-left:400px; padding-top:310px;\"> <a href=\"../district_dmf/Notice_All.aspx\"  Target=\"_new\" >Show All Notice</a></div></div>";
                Literal1.Text = Literal1.Text + "</div>";

                //Literal1.Text = Literal1.Text + "</div></div></div>";

            }
        }

        catch
        {
        }
    }
    //-------------------------end notice borad--------------------//
    
    // -----------------------Naveen CounterBox-------------------------
    public void counter_box() {

        try
        {
            dl_rti_notice dl = new dl_rti_notice();
            ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
            dt = dl.Get_Count();
            string[] arr_name;
            if (Session["language"].ToString() == "en-GB")
            {
                arr_name = new string[] { "Total RTI Submited", "Total Department", "Total Offices", "Total Employees" };
               
            }
            else {
                arr_name = new string[] { "कुल आर.टी. आई.", "कुल विभाग", "कुल कार्यालय", "कुल कर्मचारी" };
            }

            string[] arr_color = new string[] { "box-green", "box-red", "box-blue", "col_last box-purple" };
            string str_count = "";
            if (dt.table.Rows.Count > 0)
            {
                Literal_Count.Text = "";
                for (int i = 0; i < dt.table.Rows.Count; i++)
                {
                    str_count= dt.table.Rows[i]["count_rti_dept_ofc_emp"].ToString();
                    Literal_Count.Text = Literal_Count.Text + "<div class=\"col_one_fourth nobottommargin center " + arr_color[i] + "\">";
                    Literal_Count.Text = Literal_Count.Text + "<div class=\"counter counter-large\"> <span data-from=\"100\" data-to=\"" + str_count + "\" data-refresh-interval=\"50\" data-speed=\"2000\">" + str_count + "</span> </div>";
                    Literal_Count.Text = Literal_Count.Text +"<h5>"+arr_name[i]+"</h5>";
                    Literal_Count.Text = Literal_Count.Text + "</div>";
                                
                }
                                           
            }

            
        } // End of try
        catch { 
        
        }
    
    } // End of counter box


}