using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
/// <summary>
/// Summary description for dl_LoginOfficerDetail
/// </summary>
public class dl_LoginOfficerDetail
{
	public dl_LoginOfficerDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public ReturnClass.ReturnDataTable GetLoginOfficerDetail(string userName)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {

            string qr = "SELECT Name, UserType, DATE_FORMAT(LoginTimeStamp,'%d/%m/%Y/%T') as LoginTimeStamp FROM rti_login, usertype WHERE UserName=@uname AND rti_login.UserTypeID=usertype.UserTypeID";

            MySqlParameter[] pr = new MySqlParameter[]{
              new MySqlParameter("uname", userName)
                };
            dt = db.executeSelectQuery(qr, pr);
            //dt = db.executeSelectQuery(qr);
            dt.status = true;
        }

        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }
        return dt;

    }// end of get GetLoginOfficerDetail

    public ReturnClass.ReturnDataTable GetRequestCount(string loginName, string RequestStatus)
    {
        ReturnClass.ReturnDataTable dt = null;//= new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {
            string qr = " select count(RequestStatus) as RequestCount from rti_request where rti_login_userName=@uname AND RequestStatus=@requestStatus ";


            MySqlParameter[] pr = new MySqlParameter[]{
              new MySqlParameter("uname", loginName),
              new MySqlParameter("requestStatus", RequestStatus)
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

    } // End of GetRequestCount

    public ReturnClass.ReturnBool UpdateTimeStamp(string userCode)
    {

        string strQuery = "UPDATE  rti_login SET" +
                          " LoginTimeStamp=now() WHERE UserName=@userName";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("userName", userCode)
               
              };
        ReturnClass.ReturnBool dt = null;
        db_maria_connection objDB = new db_maria_connection();
        dt = objDB.executeUpdateQuery(strQuery, pm);//executeInsertQuery(strQuery, pm);

        return dt;
    }//end of UpdateTimeStamp

}