using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_login
/// </summary>
public class bl_login
{
	public bl_login()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    string loginID, password, rollID, passwordChange, disableTime, active, userid, registrationID, office, meetingdate;
    string userIP, userAgent, userOS, succesful_login, user_browser, sesssionId, logintime,newpass;
    string rtiid, status;

    public string Status { get { return status; } set { status = value; } }
    public string Meetingdate { get { return meetingdate; } set { meetingdate = value; } }

    public string Office { get { return office; } set { office = value; } }
    public string UserID { get { return userid; } set { userid = value; } }
    public string LoginID { get { return loginID; } set { loginID = value; } }
    public string Password { get { return password; } set { password = value; } }
    public string RollID { get { return rollID; } set { rollID = value; } }
    public string PasswordChange { get { return passwordChange; } set { passwordChange = value; } }
    public string DisableTime { get { return disableTime; } set { disableTime = value; } }
    public string Active { get { return active; } set { active = value; } }
    //login trial
    public string UserIP { get { return userIP; } set { userIP = value; } }
    public string UserAgent { get { return userAgent; } set { userAgent = value; } }
    public string UserOS { get { return userOS; } set { userOS = value; } }
    public string Succesful_login { get { return succesful_login; } set { succesful_login = value; } }
    public string User_browser { get { return user_browser; } set { user_browser = value; } }
    public string SesssionId { get { return sesssionId; } set { sesssionId = value; } }
    public string Logintime { get { return logintime; } set { logintime = value; } }
    public string RegistrationID { get { return registrationID; } set { registrationID = value; } }
    public string NewPass { get { return newpass; } set { newpass = value; } }

    public string RTIID { get { return rtiid; } set { rtiid = value; } }
}