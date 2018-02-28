using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceReference1;

public partial class PayFees : BasePage   //System.Web.UI.Page
{
    bl_RTI_Fees bl = new bl_RTI_Fees();
    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    dl_RTI_Fees dl = new dl_RTI_Fees();
    echallan ec = new echallan();
    Utilities util = new Utilities();
    string fee = "0", amounttobepaid;
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }

        if (Session["username"] == null)
        {
           // Response.Redirect("../Logout.aspx");
        }

        txtamount.Attributes.Add("readonly", "readonly");
        txt_major_head.Attributes.Add("readonly", "readonly");
        txt_minor_head.Attributes.Add("readonly", "readonly");
        txt_sub_head.Attributes.Add("readonly", "readonly");
        txt_sub_major_head.Attributes.Add("readonly", "readonly");
        txt_tin_cin.Attributes.Add("readonly", "readonly");
        txt_purpose.Attributes.Add("readonly", "readonly");
        txt_ent_date.Attributes.Add("readonly", "readonly");
        txt_acc_date.Attributes.Add("readonly", "readonly");

        if (Session["RTI_ID"] != null)
        {
           bindfee();
        }
        if (!IsPostBack)
        {

             getrecord();
        }

    }

    public void bindfee()
    {
        //try
        //{
            bl.RTI_provisional_no = Session["RTI_ID"].ToString();
            //dt = dl.getfee(bl);
            //if (dt.table.Rows.Count > 0)
            {
                if (Session["language"].ToString() == "en-GB")
                {
                    feesname.Text = " RTI Fees -: ";
                    feeamount.Text = " Rs 10 ";
                }
                else
                {
                    feesname.Text = " आर. टी. आई. फीस -: ";
                    feeamount.Text = " रु 10 ";
                }
                
                //fee = dt.table.Rows[0]["fees_code"].ToString();
                amounttobepaid = "10";//dt.table.Rows[0]["Amount"].ToString();
            }
            //else
            {
               // feesname.Text = "";
               // feeamount.Text = "";

            }
        //}
        //catch (NullReferenceException)
        {
          //  Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "Your Session is Expire...Please login Again ", "../Logout.aspx");
        }
    }
      public void getrecord()
    {
        string payment, due;
        ReturnClass.ReturnDataTable dtt = new ReturnClass.ReturnDataTable();
        try
        {
            bl.RTI_provisional_no = Session["RTI_ID"].ToString();
            dt = dl.getrecord(bl);
            if (dt.table.Rows.Count > 0)
            {
                dtt = dl.getamount(bl);
                if (dtt.table.Rows.Count > 0)
                {
                        payment = dtt.table.Rows[0]["Fees"].ToString();
                        if (Convert.ToInt32(payment) == (Convert.ToInt32(amounttobepaid)))
                        {
                            Button2.Visible = false;
                            
                        }
                        else
                        {
                            Button2.Visible = true;
                            
                            due = ((Convert.ToInt32(amounttobepaid)) - (Convert.ToInt32(payment))).ToString();
                            if (((Convert.ToInt32(amounttobepaid)) - (Convert.ToInt32(payment))) >= 0)
                            {
                                if (Session["language"].ToString() == "en-GB")
                                {
                                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please Pay Due Amount Rs. " + due + " By e-challan than Proceed.");
                                }
                                else
                                {
                                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "कृपया देय राशि का भुगतान करें रु. " + due + " ई-चालान के द्वारा फिर आगे बढ़ें|");
                                }
                                              
                            }
                            //Button2.Visible = false;
               
                        }
                    }
                
            }
            else
            {
                Button2.Visible = true;

                if (Session["language"].ToString() == "en-GB")
                {
                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please Pay Due Amount Rs. " + amounttobepaid + " By e-challan than Proceed.");
                }
                else
                {
                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "कृपया देय राशि का भुगतान करें रु. " + amounttobepaid + " ई-चालान के द्वारा फिर आगे बढ़ें|");
                }
               // Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please Pay Due Amount Rs. " + amounttobepaid + " By e-challan than Proceed");
               
            }
            GridView1.DataSource = dt.table;
            GridView1.DataBind();
            
            
        }
        catch (NullReferenceException)
        {
            if (Session["language"].ToString() == "en-GB")
            {
                Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "Your Session is Expired...Please login Again ", "../Logout.aspx");
            }
            else
            {

                Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "आपका सत्र समाप्त हो गया है...कृपया फिर से लॉग इन करें ", "../Logout.aspx");
            }
            
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

                Echallan_DS ds = new Echallan_DS();
                echallanSoapClient cl = new echallanSoapClient();

                ds = cl.eChallan_srv(txt_Ref_No.Text.Trim(), "11");
                // ds = cl.eChallan_srv(txt_Ref_No.Text.Trim(), "01");
                if (ds.TR_REFNO == txt_Ref_No.Text.Trim())
                {
                    // Minor head can be 000 ,103, 105, 108
                    //if (ds.MAJORHEAD == "0051" && ds.SUBMAJORHEAD == "00" && (ds.MINORHEAD == "000" || ds.MINORHEAD == "103" || ds.MINORHEAD == "105" || ds.MINORHEAD == "108") && ds.SUBHEAD == "0000")
                    if (ds.MAJORHEAD == "1475" && ds.SUBMAJORHEAD == "00" && ds.MINORHEAD == "200" && ds.SUBHEAD == "0000")
                    {
                        txt_tin_cin.Text = ds.TIN_CIN;
                        txtamount.Text = ds.AMOUNT;
                        txt_ent_date.Text = ds.ENTRYDATE;
                        txt_acc_date.Text = ds.DATE_AC;
                        txt_major_head.Text = ds.MAJORHEAD;
                        txt_sub_major_head.Text = ds.SUBMAJORHEAD;
                        txt_sub_head.Text = ds.SUBHEAD;
                        txt_major_head.Text = ds.MAJORHEAD;
                        txt_purpose.Text = ds.PURPOSE;
                        txt_minor_head.Text = ds.MINORHEAD;

                    }
                    else
                    {
                        if (Session["language"].ToString() == "en-GB")
                        {
                            Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please Enter Correct Treasury Reference Number...(Treasury Heads are Incorrect)");
                        }
                        else
                        {

                            Utilities.MessageBox_UpdatePanel(UpdatePanel3, "कृपया सही खजाना संदर्भ संख्या दर्ज करें...(खजाना हेड्स गलत हैं)");
                        }
                         
                        
                    }
                }
                else
                {
                    if (Session["language"].ToString() == "en-GB")
                    {
                        Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please Enter Correct Treasury Reference Number...");
                    }
                    else
                    {

                        Utilities.MessageBox_UpdatePanel(UpdatePanel3, "कृपया सही खजाना संदर्भ संख्या दर्ज करें.");
                    }
                    
                   
                }
            }
        }
        catch(Exception ex)
        {
            Gen_Error_Rpt.Write_Error("PayFees.aspx.cs-Button1_Click", ex);
            if (Session["language"].ToString() == "en-GB")
            {
                Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Unable to get Challan details...");
            }
            else
            {

                Utilities.MessageBox_UpdatePanel(UpdatePanel3, "चालान विवरण प्राप्त करने में असमर्थ...");
            }
           
        }


    }

    public void MYClear()
    {
        txt_Ref_No.Text = "";
        txt_tin_cin.Text = "";
        txtamount.Text = "";
        txt_ent_date.Text = "";
        txt_acc_date.Text = "";
        txt_major_head.Text = "";
        txt_sub_major_head.Text = "";
        txt_sub_head.Text = "";
        txt_major_head.Text = "";
        txt_purpose.Text = "";
        txt_minor_head.Text = "";
    }

    public string serialno()
    {
        try
        {
            bl.RTI_provisional_no = Session["RTI_ID"].ToString();
            dt = dl.serial_no(bl);
            if (dt.table.Rows.Count > 0)
            {

                string maxno = dt.table.Rows[0]["ID"].ToString();
                if (maxno == "")
                {
                    maxno = "0";
                }

                Int32 nextid = Convert.ToInt32(maxno) + 1;
                string kkk = Convert.ToString(nextid);
                bl.Serial_no = kkk.ToString().PadLeft(2, '0');
            }

        }
        catch (NullReferenceException)
        {
            if (Session["language"].ToString() == "en-GB")
            {
                Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "Your Session is Expire...Please login Again ", "../Logout.aspx");
            }
            else
            {

                Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "आपका सत्र समाप्त हो गया है...कृपया फिर से लॉग इन करें", "../Logout.aspx");
            }
           
        }
        return bl.Serial_no;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {        
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();

        try
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

                if (Page.IsValid)
                {


                    ec.Tr_ref_no = txt_Ref_No.Text.Trim();
                    ec.Serial_no = serialno();
                    ec.Amount = txtamount.Text.Trim();
                    ec.Major_head = txt_major_head.Text;
                    ec.Sub_major_head = txt_sub_major_head.Text;
                    ec.Sub_head = txt_sub_head.Text;
                    ec.Minor_head = txt_minor_head.Text;
                    ec.Purpose = txt_purpose.Text;
                    ec.Challan_Category = "RTI Registration";
                   // ec.Challan_Category = "Society Registration";
                    //ec.Firm_society_category = "Society";
                    ec.Fees_master_code = fee;
                    if (txt_acc_date.Text.Trim() == "")
                    {
                        ec.Date_ac = "";
                    }
                    else
                    {

                        DateTime date_ac = Convert.ToDateTime(txt_acc_date.Text);
                        ec.Date_ac = date_ac.ToString("yyyy/MM/dd");
                    }
                    if (txt_ent_date.Text.Trim() == "")
                    {
                        ec.Tr_entry_date = "";
                    }
                    else
                    {
                        DateTime tr_entry_date = Convert.ToDateTime(txt_ent_date.Text);
                        ec.Tr_entry_date = tr_entry_date.ToString("yyyy/MM/dd");
                    }
                    ec.Tin_cin = txt_tin_cin.Text;
                    ec.Society_provisional_no = Session["RTI_ID"].ToString();
                    try
                    {
                        dt = dl.select_tr_ref_no(ec);
                        if (dt.table.Rows.Count > 0)
                        {
                            if (dt.table.Rows[0]["tr_ref_no"].ToString() == txt_Ref_No.Text.Trim())
                            {
                                if (Session["language"].ToString() == "en-GB")
                                {
                                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Ref No. Already Exists...");
                                }
                                else
                                {

                                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "संदर्भ संख्या पहले से मौजूद है...");
                                }
                              
                             }
                            MYClear();
                        }
                        else
                        {
                            ec.Date_Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            ec.Client_ip = util.GetClientIpAddress(this.Page);
                            ec.User_id = Session["username"].ToString();
                            //ec.User_id = "12345";
                            rb = dl.insert_echallan_details(ec);
                            if (rb.status == true)
                            {
                                if (Session["language"].ToString() == "en-GB")
                                {
                                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "e-challan details saved successfully");
                                }
                                else
                                {

                                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "ई-चालान विवरण सफलतापूर्वक सुरक्षित किए गए");
                                }
                                
                                MYClear();
                                btn_close.Visible = true;
                            }

                            else
                            {
                                if (Session["language"].ToString() == "en-GB")
                                {
                                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Records could not be saved: Please Try Again");
                                }
                                else
                                {

                                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "रिकॉर्ड्स को सुरक्षित नहीं किया जा सका: कृपया पुनः प्रयास करें");
                                }
                                
                             
                            }

                            getrecord();
                        }
                    }
                    catch (Exception ex)
                    {
                        Gen_Error_Rpt.Write_Error("eChallan.aspx (save) :", ex);
                        if (Session["language"].ToString() == "en-GB")
                        {
                            Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Records could not be saved: Please Try Again");
                        }
                        else
                        {

                            Utilities.MessageBox_UpdatePanel(UpdatePanel3, "रिकॉर्ड्स को सुरक्षित नहीं किया जा सका: कृपया पुनः प्रयास करें");
                        }

                       // Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Records could not be saved: Please Try Again");
                    }
                    finally
                    { }


                }
                else
                {
                    if (Session["language"].ToString() == "en-GB")
                    {
                        Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please fill all the required fields.");
                    }
                    else
                    {

                        Utilities.MessageBox_UpdatePanel(UpdatePanel3, "कृपया सभी आवश्यक फ़ील्ड भरें।");
                    }
                    
                  
                 }
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {
                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Page refresh or back button is not allowed.");
                }
                else
                {

                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, " पेज रिफ्रेश या बैक बटन की अनुमति नहीं है");
                }
               // Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Page refresh or back button is not allowed.");
               
            }
        }
        catch
        {
            if (Session["language"].ToString() == "en-GB")
            {
                Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Records could not be saved: Please Try Again");
            }
            else
            {

                Utilities.MessageBox_UpdatePanel(UpdatePanel3, "रिकॉर्ड्स को सुरक्षित नहीं किया जा सका: कृपया पुनः प्रयास करें");
            }
            //Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Records could not be saved: Please Try Again");
            
        }
        finally
        { }


    }// Button2_click End
    protected void btn_close_Click(object sender, EventArgs e)
    {
        
        string otp_sc=null;
        if (Session["OTP_SC"] != null)
        {
            otp_sc = Session["OTP_SC"].ToString();
        }
        string rti_id = Session["RTI_ID"].ToString();
        string payment, due;
        ReturnClass.ReturnDataTable dtt = new ReturnClass.ReturnDataTable();
        bl_RTI_Request blrti = new bl_RTI_Request();
        dl_RTI_Request dlrti = new dl_RTI_Request();
        ReturnClass.ReturnBool rtbool = new ReturnClass.ReturnBool();

        try
        {
            bl.RTI_provisional_no = Session["RTI_ID"].ToString();
            blrti.RTI_Request_id = bl.RTI_provisional_no;
            blrti.IsPaymentMade = "Y";
            dt = dl.getrecord(bl);
            if (dt.table.Rows.Count > 0)
            {
                dtt = dl.getamount(bl);
                if (dtt.table.Rows.Count > 0)
                {
                    payment = dtt.table.Rows[0]["Fees"].ToString();
                    if (Convert.ToInt32(payment) >= (Convert.ToInt32(amounttobepaid)))
                    {
                        // Update payment status
                        rtbool = dlrti.update_Rti_Payment_status(blrti);
                        if (rtbool.status == true)
                        {

                            if (otp_sc != null)
                            {      // Directly go to success page No Need OTP


                                string rti_message = "";
                                if (Session["language"].ToString() == "en-GB")
                                {
                                    rti_message = "RTI Submission successfull your Registration no. is: " + rti_id + "  And  Your Security Code is:  " + otp_sc + "   please save  details for further Information.";
                                }
                                else
                                {
                                    rti_message = "आपका आर. टी. आई. सफलता पूर्वक सबमिट हो गया, आपका रजिस्ट्रेशन न. है " + rti_id + " और सुरक्षा कोड है " + otp_sc + "कृपया विवरण को उपयोग के लिए सुरक्षित रखें|";
                                }

                              //  string rti_message = "RTI Submition successfull your Registration no. is: " + rti_id + "  And  Your Security Code is:  " + otp_sc + "   please save  details for further Information.";
                                Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, rti_message, "../user/UserWelcome.aspx");
                            }
                            else
                            {
                                
                                if (Session["language"].ToString() == "en-GB")
                                {
                                    Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "Your RTI Request has been submited Need Mobile Verification for Registeration", "../user/User_Mobile_Varification.aspx");
                                }
                                else
                                {
                                    Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "आपका आर. टी. आई. सबमिट हो गया, आपको रजिस्ट्रेशन के लिए मोबाईल वेरिफिकेसन की जरुरत है| ", "../user/User_Mobile_Varification.aspx");
                                
                                }
                                //Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "Your RTI Request has been submited Need Mobile Verification for Registeration", "../user/User_Mobile_Varification.aspx");
                            }

                        }
                        else {

                            if (Session["language"].ToString() == "en-GB")
                            {
                                Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Failed to update Payment status ! Try again");
                            }
                            else
                            {
                                Utilities.MessageBox_UpdatePanel(UpdatePanel3, "भुगतान की स्थिति अपडेट करने में विफल ! पुनः प्रयास करें");

                            }
                           // Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Failed to update Payment status ! Try again");
                                                 
                        }
                        

                    }
                    else
                    {
                       
                        due = ((Convert.ToInt32(amounttobepaid)) - (Convert.ToInt32(payment))).ToString();
                        if (((Convert.ToInt32(amounttobepaid)) - (Convert.ToInt32(payment))) >= 0)
                        {
                            if (Session["language"].ToString() == "en-GB")
                            {
                                Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please Pay Due Amount Rs. " + due + " By e-challan than Proceed.");
                            }
                            else
                            {
                                Utilities.MessageBox_UpdatePanel(UpdatePanel3, "कृपया देय राशि का भुगतान करें रु. " + due + " ई-चालान के द्वारा फिर आगे बढ़ें|");
                            }
                                            
                          //  Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please Pay Due Amount " + due + " By e-challan than Proceed");

                        }
                        
                    }
                }

            }
            else
            {
                Button2.Visible = true;
                if (Session["language"].ToString() == "en-GB")
                {
                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please Pay Due Amount Rs. " + amounttobepaid + " By e-challan than Proceed.");
                }
                else
                {
                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "कृपया देय राशि का भुगतान करें रु. " + amounttobepaid + " ई-चालान के द्वारा फिर आगे बढ़ें|");
                }
                  
               // Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Please Pay Due Amount " + amounttobepaid + " By e-challan than Proceed");

            }
           
        }
        catch (NullReferenceException)
        {
            if (Session["language"].ToString() == "en-GB")
            {
                Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "Your Session is Expired...Please login Again ", "../Logout.aspx");
            }
            else
            {

                Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "आपका सत्र समाप्त हो गया है...कृपया फिर से लॉग इन करें ", "../Logout.aspx");
            }
           // Utilities.MessageBox_UpdatePanel_Redirect(UpdatePanel3, "Your Session is Expire...Please login Again ", "../Logout.aspx");
        }

       
    
    } // End of btn_close_Click

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bl.RTI_provisional_no = GridView1.DataKeys[e.RowIndex].Values["rti_provisional_no"].ToString();
        bl.Serial_no = GridView1.DataKeys[e.RowIndex].Values["serial_no"].ToString();
        try
        {
            rb = dl.delete_echallan(bl);
            if (rb.status == true)
            {
                if (Session["language"].ToString() == "en-GB")
                {
                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Deleted Successfully");
                }
                else
                {

                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "सफलतापूर्वक मिटाया गया");
                }
                //    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Deleted Successfully");
               
            }
            else
            {
                if (Session["language"].ToString() == "en-GB")
                {
                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Not Deleted");
                }
                else
                {

                    Utilities.MessageBox_UpdatePanel(UpdatePanel3, "नहीं हटाए गए ");
                }
              //   Utilities.MessageBox_UpdatePanel(UpdatePanel3, "Not Deleted ");
                
            }
            getrecord();
        }

        finally
        { }
    }

}