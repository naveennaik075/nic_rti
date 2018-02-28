using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RTI_RequestBL
/// </summary>
public class bl_RTI_Request
{
	public bl_RTI_Request()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    // To insert in the table rti_filed_user 
    string rti_filed_user_id, login_username,  nameEnglish,nameHindi, gender, email, mobile, address, state, pincode, country, userType, district, countryName ;
    string rti_request_id, is_bpl,  bpl_Card_No, bpl_Issue_Year, bpl_Issuing_Authority, rti_Fees;
    string base_department_id, office_level_id, district_id_ofc, office_category, office_id, office_mapping_id;
    string rti_status, action_id, action_date, isValid,securitycode;
    string client_os, clientBrowser, userAgent, isRTIFileUpload, isNew;
    string isPaymentMade;

    public string IsPaymentMade { get { return isPaymentMade; } set { isPaymentMade = value; } }
    public string IsNew { get { return isNew; } set { isNew = value; } }
    public string Client_os { get { return client_os; } set { client_os = value; } }
    public string ClientBrowser { get { return clientBrowser; } set { clientBrowser = value; } }
    public string UserAgent { get { return userAgent; } set { userAgent = value; } }
    public string IsRTIFileUpload { get { return isRTIFileUpload; } set { isRTIFileUpload = value; } }

    public string Rti_Status { get { return rti_status; } set { rti_status = value; } }
    public string Action_id { get { return action_id; } set { action_id = value; } }
    public string Action_date { get { return action_date; } set { action_date = value; } }

    public string IsValid { get { return isValid; } set { isValid = value; } }   
    public string Base_department_id { get { return base_department_id; } set { base_department_id = value; } }
    public string Office_level_id { get { return office_level_id; } set { office_level_id = value; } }
    public string District_id_ofc { get { return district_id_ofc; } set { district_id_ofc = value; } }
    public string Office_category { get { return office_category; } set { office_category = value; } }
    public string Office_id { get { return office_id; } set { office_id = value; } }
    public string Office_mapping_id { get { return office_mapping_id; } set { office_mapping_id = value; } }
    public string RTI_filed_user_id { get { return rti_filed_user_id; } set { rti_filed_user_id = value; } }
    //public string Login_Username { get { return login_username; } set { login_username = value; } }

    public string NameEnglish { get { return nameEnglish; } set { nameEnglish = value; } }
    public string NameHindi { get { return nameHindi; } set { nameHindi = value; } }
    public string Pincode { get { return pincode; } set { pincode = value; } }
    public string Gender { get { return gender; } set { gender = value; } }
    public string Email { get { return email; } set { email = value; } }
    public string Mobile { get { return mobile; } set { mobile = value; } }
    public string Address { get { return address; } set { address = value; } }

    public string District { get { return district; } set { district = value; } }
    public string Country { get { return country; } set { country = value; } }
    public string CountryName { get { return countryName; } set { countryName = value; } }
    public string State { get { return state; } set { state = value; } }

    public string UserType { get { return userType; } set { userType = value; } }
    public string RTI_Request_id { get { return rti_request_id; } set { rti_request_id = value; } }
    public string RTI_Is_bpl { get { return is_bpl; } set { is_bpl = value; } }

    public string BPL_Card_No { get { return bpl_Card_No; } set { bpl_Card_No = value; } }
    public string BPL_Issue_Year { get { return bpl_Issue_Year; } set { bpl_Issue_Year = value; } }
    public string BPL_Issuing_Authority { get { return bpl_Issuing_Authority; } set { bpl_Issuing_Authority = value; } }
    public string RTI_Fees { get { return rti_Fees; } set { rti_Fees = value; } }
   

    // To insert in the table rti_request
    string rti_officer_id, rti_login_userName, rti_text, ipaddress,  rti_Subject;
    public string RTI_officer_id { get { return rti_officer_id; } set { rti_officer_id = value; } }
    public string RTI_login_userName { get { return rti_login_userName; } set { rti_login_userName = value; } }
    
    public string RTI_Text { get { return rti_text; } set { rti_text = value; } }

    public string Rti_Subject { get { return rti_Subject; } set { rti_Subject = value; } }
    public string RTI_ipaddress { get { return ipaddress; } set { ipaddress = value; } }

    public string Securitycode { get { return securitycode; } set { securitycode = value; } }
    //// To insert in the table rti_files
    //string rti_fileID, fileName, fileType ,fileDescription;
    //Byte[] fileData;

    //public string RTI_fileID { get { return rti_fileID; } set { rti_fileID = value; } }
    //public string RTI_fileName { get { return fileName; } set { fileName = value; } }
    //public string RTI_fileType { get { return fileType; } set { fileType = value; } }
    //public Byte[] RTI_fileData { get { return fileData; } set { fileData = value; } }
    //public string FileDescription { get { return fileDescription; } set { fileDescription = value; } }
}