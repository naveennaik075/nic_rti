using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;
/// <summary>
/// Summary description for dl_RTI_Request
/// </summary>
public class dl_RTI_Request
{
    public dl_RTI_Request()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public ReturnClass.ReturnDataTable GetMobileNumForVerification(bl_RTI_Request bl)
    {
        string str = "select User_ID, Mobile_No From rti_detail where rti_id=@RTI_RequestID";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("RTI_RequestID",bl.RTI_Request_id)
        };
        ReturnClass.ReturnDataTable rd = null;// new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable BindOfficerDL(bl_RTI_Request bl)
    {
        string str = @"select  office_mapping_id,concat( A.Name_en,'/', dg.Designation_Name ,'/',ofc.OfficeName  ) as Name_en from  employee_table as A 
                       inner join  emp_office_mapping  as B on A.emp_id=B.emp_code
                       inner join  designation as dg on  dg.Designation_ID = B.designation_id  
                       inner join  office as ofc on  ofc.NewOfficeCode = B.office_id  
                       where B.base_department_id= @base_department_id and B.office_level_id= @office_level_id 
                             and B.district_id_ofc=@district_id_ofc and B.office_category= @office_category and B.office_id=@office_id  and B.Active='Y' and dg.Designation_ID='2' order by Name_en asc";
        //string str = "select  office_mapping_id, Name_en from  employee_table A, emp_office_mapping  B where A.emp_id=B.emp_code and " +
        //                  "  B.base_department_id= @base_department_id and B.office_level_id= @office_level_id " +
        //                   " and B.district_id_ofc=@district_id_ofc and B.office_category= @office_category and B.office_id=@office_id  and B.Active='Y' order by Name_en asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("base_department_id",bl.Base_department_id),
            new MySqlParameter("office_level_id",bl.Office_level_id),
            new MySqlParameter("district_id_ofc",bl.District_id_ofc),
            new MySqlParameter("office_category",bl.Office_category),
            new MySqlParameter("office_id",bl.Office_id)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable BindState(bl_RTI_Request bl)
    {
        string str = "select  state_id, state_name_e from state where cid= @cid order by state_name_e asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("cid",bl.Country)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable BindDistrict(bl_RTI_Request bl)
    {
        //string str = "select District_ID,District_Name_En from Districts  where StateCode=@state ORDER BY District_Name_En asc ";
        string str = "select District_ID,District_Name from Districts  where StateCode=@state ORDER BY District_Name asc ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("state",bl.State)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable Get_RTI_DDL_Data(string qr)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {

            // string qr = "SELECT District_ID, District_Name_En FROM districts ";

            //MySqlParameter[] pr = new MySqlParameter[]{
            //  new MySqlParameter("gender", category)
            //    };
            //dt = db.executeSelectQuery(qr, pr);
            dt = db.executeSelectQuery(qr);
            dt.status = true;
        }

        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }
        return dt;

    }// end of get GetDistrict

    public ReturnClass.ReturnDataTable GetDDL_data(string category)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {

            string qr = "SELECT DDL_Name_Value , DisplayName_en FROM ddl_list WHERE Category=@ddl_category order by DisplayOrder";

            MySqlParameter[] pr = new MySqlParameter[]{
              new MySqlParameter("ddl_category", category)
                };
            dt = db.executeSelectQuery(qr, pr);
            // dt = db.executeSelectQuery(qr);
            dt.status = true;
        }

        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }
        return dt;

    }// end of get GetGender
    public ReturnClass.ReturnDataTable GetUserData(bl_RTI_Request ur)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {

            string qr = "SELECT Name_hi, Name_en, EmailID, Gender, Address, PinCode, Country, StateCode, UserType,   " +
                        " MobileNo, DistrictCode, Country_name " +
                        " FROM user_registration WHERE UserID=@userid AND IsValid='Y' ";

            MySqlParameter[] pr = new MySqlParameter[]{
              new MySqlParameter("userid", ur.RTI_login_userName)
                };
            dt = db.executeSelectQuery(qr, pr);
            //dt = db.executeSelectQuery(qr);
            if (dt.table != null)
            {
                ur.NameEnglish = dt.table.Rows[0]["Name_en"].ToString();
                ur.NameHindi = dt.table.Rows[0]["Name_hi"].ToString();
                ur.Email = dt.table.Rows[0]["EmailID"].ToString();
                ur.Gender = dt.table.Rows[0]["Gender"].ToString();
                ur.Address = dt.table.Rows[0]["Address"].ToString();
                ur.Pincode = dt.table.Rows[0]["PinCode"].ToString();
                ur.Country = dt.table.Rows[0]["Country"].ToString();
                ur.State = dt.table.Rows[0]["StateCode"].ToString();
                ur.UserType = dt.table.Rows[0]["UserType"].ToString();
                ur.Mobile = dt.table.Rows[0]["MobileNo"].ToString();
                ur.District = dt.table.Rows[0]["DistrictCode"].ToString();
                ur.CountryName = dt.table.Rows[0]["Country_name"].ToString();
            }
            dt.status = true;
        }

        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }
        return dt;

    }// end of get GetUserData
    public string GetMaxValueRtiFiledUser()
    {
        string value = "";
        string query = " SELECT IFNULL(MAX(rti_FiledUserID),0) as NO FROM rti_filed_user";

        ReturnClass.ReturnDataTable dt;//= new ReturnClass.ReturnDataTable();
        db_maria_connection objData = new db_maria_connection();

        try
        {
            dt = objData.executeSelectQuery(query);
            int count = Convert.ToInt32(dt.table.Rows[0]["NO"]) + 1;
            value = count.ToString();
        }
        catch (Exception ex)
        {

        }
        return value;
    } // End // it is not required
    public string GetMaxValueRtiFiles()
    {
        string value = "";
        string query = " SELECT IFNULL(MAX(rti_fileID),0) as NO FROM rti_files";

        ReturnClass.ReturnDataTable dt;//= new ReturnClass.ReturnDataTable();
        db_maria_connection objData = new db_maria_connection();

        try
        {
            dt = objData.executeSelectQuery(query);
            int count = Convert.ToInt32(dt.table.Rows[0]["NO"]) + 1;
            value = count.ToString();
        }
        catch (Exception ex)
        {

        }
        return value;
    } // End

    //ReturnClass.ReturnDataTable GetRtiStatus_detail(bl_RTI_Request bl)
    //{
    //    ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
    //    db_maria_connection db = new db_maria_connection();
    //    string str = "select rti_id, Isvalid from rti_status  where rti_id = @rti_id";
    //    MySqlParameter[] pm = new MySqlParameter[]
    //    {
    //        new MySqlParameter("rti_id",bl.RTI_Request_id)
    //    };
    //    dt = db.executeSelectQuery(str, pm);
    //    return dt;
    //}
    ////end of  Get Rti Status ISvalid
    public ReturnClass.ReturnBool update_Rti_status(bl_RTI_Request bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        using (TransactionScope ts = new TransactionScope())
        {
            string str = "insert into rti_status_log select * from rti_status WHERE rti_status.rti_id = @regid1";
            MySqlParameter[] pm = new MySqlParameter[]
            {
            new MySqlParameter("regid1",bl.RTI_Request_id)
        };
            rb = db.executeInsertQuery(str, pm);
            if (rb.status == true)
            {
                string str1 = "Update  rti_status set  Isvalid=@isvalid where rti_id = @rtiid ";
                MySqlParameter[] pm1 = new MySqlParameter[]
                {
                new MySqlParameter("isvalid",bl.IsValid),
                new MySqlParameter("rtiid",bl.RTI_Request_id)
            };
                rb = db.executeUpdateQuery(str1, pm1);
                if (rb.status == true)
                {
                    ts.Complete();
                }
            }
        }
        return rb;
    }
    // end of MObile Varification Update

    public ReturnClass.ReturnBool update_Rti_Payment_status(bl_RTI_Request bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        using (TransactionScope ts = new TransactionScope())
        {
            string str = "insert into rti_status_log select * from rti_status WHERE rti_status.rti_id = @regid1";
            MySqlParameter[] pm = new MySqlParameter[]
            {
            new MySqlParameter("regid1",bl.RTI_Request_id)
        };
            rb = db.executeInsertQuery(str, pm);
            if (rb.status == true)
            {
                string str1 = "Update  rti_status set  Payment_status=@is_paid where rti_id = @rtiid ";
                MySqlParameter[] pm1 = new MySqlParameter[]
                {
                new MySqlParameter("is_paid",bl.IsPaymentMade),
                new MySqlParameter("rtiid",bl.RTI_Request_id)
            };
                rb = db.executeUpdateQuery(str1, pm1);
                if (rb.status == true)
                {
                    ts.Complete();
                }
            }
        }
        return rb;
    }


    public ReturnClass.ReturnBool Insert_RTI_Info(bl_RTI_Request ur, List<bl_RTI_RequestFiles> ur_files)
    {
        ReturnClass.ReturnBool dt = new ReturnClass.ReturnBool();
        ReturnClass.ReturnBool dt1 = null;
        ReturnClass.ReturnBool dt2 = null;
        ReturnClass.ReturnBool dt3 = null;
        using (TransactionScope transScope = new TransactionScope())
        {
            dt1 = Insert_RTI_detail(ur);  //Insert_RTI_Request(objBL);
            if (dt1.status == true)
            {
                dt2 = Insert_rti_status(ur);
                if (dt2.status == true)
                {
                    foreach (var ur_file in ur_files)
                    {
                        dt3 = Insert_RTI_files(ur_file);
                        if (dt3.status == false)
                        {
                            break;
                        }
                    } // end of foreach

                }//if dt1 status
            }  // if dt2 status
            if ((dt3 == null || dt3.status == true) && dt1.status == true && dt2.status == true)
            {
                try
                {
                    transScope.Complete();
                    dt.status = true;
                }
                catch (Exception ex)
                {
                    dt.status = false;
                }
            } //End of dt3.status

        } // endof transcope
        return dt;
    }
    public ReturnClass.ReturnBool Insert_RTI_detail(bl_RTI_Request ur)
    {


        string strQuery = "INSERT INTO rti_detail " +
                          "( rti_id, applicant_type, Applicant_Name_en, Gender, Email_ID, Address, Country_Code, Country_Name," +
                          " State_Code, District_Code, Pin_Code, Mobile_No, Is_BPL, BPL_Card_No, BPL_Year_Issue, BPL_Issuing_Authority, " +
                          " rti_Subject, rti_Details, User_ID, Client_IP,securitycode , IsRTIFileUpload, Client_OS, ClientBrowser, UserAgent ) " +
                          " VALUES  (@rti_id, @applicant_type, @Applicant_Name_en, @Gender, @Email_ID, @Address, @Country_Code, @country_Name, @State_Code, " +
                          " @District_Code, @Pin_Code, @Mobile_No, @Is_BPL, @BPL_Card_No, @BPL_Year_Issue, @BPL_Issuing_Authority, " +
                          " @rti_Subject, @rti_Details, @User_ID, @Client_IP, @securitycode, @is_RTI_FILE_Upload, @ClientOS, @clientBrowser, @userAgent  )";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("rti_id", ur.RTI_Request_id),
                new MySqlParameter("applicant_type", ur.UserType),
                new MySqlParameter("Applicant_Name_en", ur.NameEnglish),
                new MySqlParameter("Gender", ur.Gender),
                new MySqlParameter("Email_ID", ur.Email ),
                new MySqlParameter("Address", ur.Address ),
                new MySqlParameter("Country_Code", ur.Country),
                new MySqlParameter("State_Code", ur.State),
                new MySqlParameter("District_Code", ur.District),
                new MySqlParameter("Pin_Code", ur.Pincode),
                new MySqlParameter("Mobile_No", ur.Mobile),
                new MySqlParameter("Is_BPL", ur.RTI_Is_bpl),
                new MySqlParameter("BPL_Card_No", ur.BPL_Card_No),
                new MySqlParameter("BPL_Year_Issue", ur.BPL_Issue_Year),
                new MySqlParameter("BPL_Issuing_Authority", ur.BPL_Issuing_Authority),
                new MySqlParameter("rti_Subject", ur.Rti_Subject),
                new MySqlParameter("rti_Details", ur.RTI_Text),
                new MySqlParameter("User_ID", ur.RTI_login_userName),
                new MySqlParameter("country_Name", ur.CountryName),
                new MySqlParameter("Client_IP", ur.RTI_ipaddress),
                new MySqlParameter("securitycode",ur.Securitycode),
                new MySqlParameter("is_RTI_FILE_Upload",ur.IsRTIFileUpload),
                new MySqlParameter("userAgent",ur.UserAgent),
                new MySqlParameter("ClientOS",ur.Client_os),
                new MySqlParameter("clientBrowser",ur.ClientBrowser)
              };
        ReturnClass.ReturnBool dt = null;
        db_maria_connection objDB = new db_maria_connection();
        dt = objDB.executeInsertQuery(strQuery, pm);

        return dt;
    }//end 
    public ReturnClass.ReturnBool Insert_rti_status(bl_RTI_Request ur)
    {

        string strQuery = "INSERT INTO rti_status " +
                          "( rti_id, user_id, status, action_id, officer_maping_id, IPAddress, action_date, IsValid, IsNew , client_OS, useragent , client_browser, Payment_status)" +
                " VALUES  ( @rti_ID, @user_id, @status, @action_id, @officer_maping_id, @IPAddress, @action_date, @isValid, @isnew, @client_OS  , @useragent, @client_browser, @Payment_status )";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("rti_ID", ur.RTI_filed_user_id),
                new MySqlParameter("user_id", ur.RTI_login_userName),
                new MySqlParameter("status", ur.Rti_Status),
                new MySqlParameter("action_id", ur.Action_id),
                new MySqlParameter("officer_maping_id", ur.Office_mapping_id),
                new MySqlParameter("IPAddress", ur.RTI_ipaddress),
                new MySqlParameter("action_date", ur.Action_date),
                new MySqlParameter("isValid", ur.IsValid),
                new MySqlParameter("isnew", ur.IsNew),
                 new MySqlParameter("client_OS", ur.Client_os),
                new MySqlParameter("useragent", ur.UserAgent),
                new MySqlParameter("client_browser", ur.ClientBrowser),
                new MySqlParameter("Payment_status", ur.IsPaymentMade)
                
              };
        ReturnClass.ReturnBool dt = null;
        db_maria_connection objDB = new db_maria_connection();
        dt = objDB.executeInsertQuery(strQuery, pm);

        return dt;
    }//end of Insert_rti_filed_user

    public ReturnClass.ReturnBool Insert_RTI_files(bl_RTI_RequestFiles ur)
    {

        string strQuery = "INSERT INTO rti_files " +
                          "( rti_fileID, fileName, fileType, fileData, FileDescription, rti_id, BPL_RTI_FileType ) " +
                          " VALUES  (@rti_fileID, @fileName, @fileType, @fileData,@FileDescription, @rti_requestID,@bpl_rti_FileType )";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("rti_fileID", ur.RTI_fileID),
                new MySqlParameter("fileName", ur.RTI_fileName),
                new MySqlParameter("fileType", ur.RTI_fileType),
                new MySqlParameter("fileData", ur.RTI_fileData),
                new MySqlParameter("FileDescription", ur.FileDescription),
                new MySqlParameter("rti_requestID", ur.RTI_Request_id ),
                new MySqlParameter("bpl_rti_FileType", ur.BPL_RTI_FileType )
                
              };
        ReturnClass.ReturnBool dt = null;
        db_maria_connection objDB = new db_maria_connection();
        dt = objDB.executeInsertQuery(strQuery, pm);

        return dt;
    }//end of Insert_RTI_files   
    public string Get_unique_RtiRequest_code()
    {
        string reg_year = DateTime.Now.Year.ToString();
        string sg = "";
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = "select MAX(SUBSTRING(rti_id,5,10)) as ID from rti_detail Where SUBSTRING(rti_id,1,4)=@registration_year  ";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("registration_year",reg_year)
        };
        rd = db.executeSelectQuery(str, pm);

        if (rd.table.Rows.Count > 0)
        {
            if (rd.table.Rows[0]["ID"].ToString() != "")
            {
                int nextId = Convert.ToInt32(rd.table.Rows[0]["ID"].ToString());
                nextId++;
                string lid = Convert.ToString(nextId);
                sg = (reg_year + lid.PadLeft(6, '0'));
                return sg;
            }

            else
            {
                sg = (reg_year + "000001");

            }
        }
        return sg;
    }

    public ReturnClass.ReturnDataTable GetRtiStatus_detail(bl_RTI_Request bl)
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @"select rs.rti_id as rtiid, rs.IsValid as isvalid, rd.securitycode as seqcode from rti_status  rs
inner join rti_detail rd on rd.rti_id = rs.rti_id
where rs.rti_id = @rti_id";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("rti_id",bl.RTI_Request_id)
        };
        dt = db.executeSelectQuery(str, pm);
        return dt;
    }

    



}