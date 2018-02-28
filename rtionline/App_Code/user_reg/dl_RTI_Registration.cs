using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;
/// <summary>
/// Summary description for RTI_RegistrationDL
/// </summary>
public class dl_RTI_Registration
{
    public dl_RTI_Registration()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /*
     * This method is to check if there is any user with this user ID. It will check uniqueness of user ID.
     */
    public ReturnClass.ReturnDataTable CheckUserID(bl_RTI_Registration bl)
    {

        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection objData = new db_maria_connection();
        string query = " SELECT * FROM login WHERE UserID= @user_id ";
        MySqlParameter[] pm = new MySqlParameter[]
        {
                new MySqlParameter("user_id",bl.UserID )
               };
        dt = objData.executeSelectQuery(query, pm);
        return dt;

    }
    // End of CheckUserName


    public string GetMaxValue()
    {
        string value = "";
        string query = " SELECT IFNULL(MAX(LoginID),0) as NO FROM login ";

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
    } // End of GetMaxValue
    /* 
       It will return the Login id ans password and roll while login which will be
     * used to check the valid login id with password.
     */
    public ReturnClass.ReturnDataTable GetLoginPassword(string userID)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {

            string qr = "SELECT UserID, Password, RollID FROM login WHERE UserID=@user_id";

            MySqlParameter[] pr = new MySqlParameter[]{
              new MySqlParameter("user_id", userID)
                };
            dt = db.executeSelectQuery(qr, pr);
          
            dt.status = true;
        }

        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }
        return dt;

    }// end of get GetLoginPassword


    public ReturnClass.ReturnDataTable GetDDL_Data(string qr)
    {
        ReturnClass.ReturnDataTable dt = null;
        db_maria_connection db = new db_maria_connection();
        try
        {

            dt = db.executeSelectQuery(qr);
            dt.status = true;
        }

        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }
        return dt;

    }
    // end of get GetDistrict


    public ReturnClass.ReturnDataTable GetGender(string category)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {

            string qr = "SELECT CategoryID, Name FROM ddl_category WHERE Category=@gender order by OrderNo";

            MySqlParameter[] pr = new MySqlParameter[]{
              new MySqlParameter("gender", category)
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

    }    // end of get GetGender

    


    public ReturnClass.ReturnBool Update_registration(bl_RTI_Registration bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        using (TransactionScope ts = new TransactionScope())
        {
            string str1 = "insert into user_registration_log select * from user_registration   WHERE user_registration.RegistrationID = @regid1";

            MySqlParameter[] pm1 = new MySqlParameter[]
            {            
            new MySqlParameter("regid1",bl.RegistrationID)
                           };
            string str = "update user_registration us set us.IsValid=@isvalid where us.RegistrationID = @regid";
            MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("isvalid",bl.IsValid),
            new MySqlParameter("regid",bl.RegistrationID)
        };


            rb = db.executeInsertQuery(str1, pm1);
            if (rb.status == true)
            {
                rb = db.executeUpdateQuery(str, pm);
            }
            if (rb.status == true)
            {
                ts.Complete();

            }
        };
        return rb;
    }
    public ReturnClass.ReturnBool Insert_Login(bl_RTI_Registration ur)
    {

        string strQuery = "INSERT INTO login " +
                          "(LoginID, UserID, Password, RollID, PasswordChange, DisableTime, Active,Client_ip,ClientOS,ClientBrowser,user_agent) " +
                          " VALUES  (@loginID, @userID, @password, @rollID, @passwordChange, @disableTime, @active, @Client_ip, @ClientOS, @ClientBrowser, @user_agent)";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("loginID", ur.RegistrationID),
                new MySqlParameter("userID", ur.UserID),
                new MySqlParameter("password", ur.Password),
                new MySqlParameter("rollID", ur.RollID),
                new MySqlParameter("passwordChange", ur.PasswordChange ),
                new MySqlParameter("disableTime", ur.DisableTime),
                new MySqlParameter("active", ur.Active),
                 new MySqlParameter("Client_ip", ur.UserIP),
                new MySqlParameter("ClientOS", ur.UserOS ),
                new MySqlParameter("ClientBrowser", ur.User_browser),
                new MySqlParameter("user_agent", ur.UserAgent)
                                
              };
        ReturnClass.ReturnBool dt = null;
        db_maria_connection objDB = new db_maria_connection();
        dt = objDB.executeInsertQuery(strQuery, pm);

        return dt;
    }
    //end of Insert_Login
    public ReturnClass.ReturnBool Insert_user_Registration(bl_RTI_Registration ur)
    {

        string strQuery = @"INSERT INTO user_registration  (RegistrationID, UserID,Password, Name_hi, Name_en, Gender,MobileNo, EmailID, Address,DistrictCode,District_name,StateCode, 
Country,Country_name ,PinCode, IsValid, UserType,  Registration_Year, client_ip,useragent,client_os,client_browser )
                           VALUES  (@registrationID, @userID,@Password, @nameHindi, @nameEnglish,@gender, @mobileNo, @emailID, @address, @districtCode,@distname, @statecode ,@country,
@Country_name, @pinCode,@isValid, @userType, @registration_Year,@client_ip, @useragent, @client_os, @client_browser)";
        MySqlParameter[] pm = new MySqlParameter[]
        {
              new MySqlParameter("registrationID", ur.RegistrationID),
              new MySqlParameter("userID", ur.UserID),
              new MySqlParameter("Password", ur.Password),
              new MySqlParameter("nameHindi", ur.NameHindi),
              new MySqlParameter("nameEnglish", ur.NameEnglish),
              new MySqlParameter("gender", ur.Gender),
              new MySqlParameter("mobileNo", ur.MobileNo),
              new MySqlParameter("emailID", ur.EmailID ),
              new MySqlParameter("address", ur.Address),
              new MySqlParameter("districtCode", ur.DistrictCode),
              new MySqlParameter("distname",ur.DistrictName),
              new MySqlParameter("statecode",ur.Statecode),
              new MySqlParameter("country", ur.Country),
              new MySqlParameter("Country_name",ur.Country_name),
              new MySqlParameter("pinCode", ur.PinCode),
              new MySqlParameter("isValid", ur.IsValid),
              new MySqlParameter("userType", ur.UserType),
              new MySqlParameter("registration_Year", ur.Registration_Year),
              new MySqlParameter("client_ip",ur.UserIP),
              new MySqlParameter("useragent",ur.UserAgent),
              new MySqlParameter("client_os",ur.UserOS),
              new MySqlParameter("client_browser",ur.User_browser)
              };
        ReturnClass.ReturnBool dt = new ReturnClass.ReturnBool();
        db_maria_connection objDB = new db_maria_connection();
        dt = objDB.executeInsertQuery(strQuery, pm);

        return dt;
    }
    //end of Insert_user_Registration
    public string Get_unique_Registration_code()
    {
        string reg_year = DateTime.Now.Year.ToString();
        string sg = (reg_year + "000001");
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = "select MAX(SUBSTRING(RegistrationID,5,10)) as ID from user_registration Where Registration_Year=@registration_year  ";
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
        }
        else
        {
            sg = (reg_year + "000001");

        }

        return sg;
    }

    public string GenOTPString(int length)
    {
        const string chars = "0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public ReturnClass.ReturnBool InsertOtp(bl_RTI_Registration bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        string str = "insert into otp_mobile (user_id,mobile_no,type,client_ip,sms_detail,application_id) values(@user_id,@mobile_no,@type,@client_ip,@sms_detail,@application_id)";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("user_id",bl.UserID),
            new MySqlParameter("mobile_no",bl.MobileNo),             
            new MySqlParameter("type",bl.Type),
            new MySqlParameter("client_ip",bl.UserIP),
            new MySqlParameter("sms_detail",bl.Sms_detail),
                new MySqlParameter("application_id",bl.RegistrationID),
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }
    public ReturnClass.ReturnDataTable BindState(bl_RTI_Registration bl)
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
    public ReturnClass.ReturnDataTable BindDistrict(bl_RTI_Registration bl)
    {
        string str = "select district_id,District_Name_En from districts  where StateCode=@state ORDER BY District_Name_En asc ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("state",bl.Statecode)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable BindGender()
    {
        string str = "select DDL_Name_Value, DisplayName_en, DisplayName_hi  from ddl_list where Category = 'Gender'";
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable BindUser()
    {
        string str = "select DDL_Name_Value, DisplayName_en from ddl_list where Category = 'UserType'";
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable Select_User_detail(bl_RTI_Registration bl)
    {
        string str = "select * From user_registration where RegistrationID=@RegistrationID";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("RegistrationID",bl.RegistrationID)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
}// End of RTI-registrationDL class