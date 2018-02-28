using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RTI_RegistrationBL
/// </summary>
public class bl_RTI_Registration
{
    public bl_RTI_Registration()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string registrationID, userID, nameHindi, nameEnglish, emailID, address, userType, pinCode, isValid, mobileNo, districtCode,district_name, country,country_name, registration_Year, gender,statecode;
    string loginID, password, rollID, passwordChange, disableTime, active, userIP, userAgent, userOS, user_browser, otp,category, type,sms_detail;
    public string User_browser { get { return user_browser; } set { user_browser = value; } }
    public string UserIP { get { return userIP; } set { userIP = value; } }
    public string UserAgent { get { return userAgent; } set { userAgent = value; } }
    public string UserOS { get { return userOS; } set { userOS = value; } }
    public string RegistrationID { get { return registrationID; } set { registrationID = value; } }
    public string UserID { get { return userID; } set { userID = value; } }
    public string NameHindi { get { return nameHindi; } set { nameHindi = value; } }
    public string NameEnglish { get { return nameEnglish; } set { nameEnglish = value; } }
    public string EmailID { get { return emailID; } set { emailID = value; } }
    public string Address { get { return address; } set { address = value; } }
    public string UserType { get { return userType; } set { userType = value; } }
    public string PinCode { get { return pinCode; } set { pinCode = value; } }
    public string IsValid { get { return isValid; } set { isValid = value; } }
    public string MobileNo { get { return mobileNo; } set { mobileNo = value; } }
    public string DistrictCode { get { return districtCode; } set { districtCode = value; } }
    public string DistrictName { get { return district_name; } set { district_name = value; } }
    public string Country { get { return country; } set { country = value; } }
    public string Country_name { get { return country_name; } set { country_name = value; } }

    public string Registration_Year { get { return registration_Year; } set { registration_Year = value; } }
    public string Statecode { get { return statecode; } set { statecode = value; } }
    // Login table
    public string LoginID { get { return loginID; } set { loginID = value; } }
    public string Password { get { return password; } set { password = value; } }
    public string RollID { get { return rollID; } set { rollID = value; } }
    public string PasswordChange { get { return passwordChange; } set { passwordChange = value; } }
    public string DisableTime { get { return disableTime; } set { disableTime = value; } }
    public string Active { get { return active; } set { active = value; } }
    public string Gender { get { return gender; } set { gender = value; } }
    //mobile otp table
    public string OTP { get { return otp; } set { otp = value; } }
    public string Category { get { return category; } set { category = value; } }
    public string Type { get { return type; } set { type = value; } }
    public string Sms_detail { get { return sms_detail; } set { sms_detail = value; } }
}