using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class bl_report
{
	public bl_report()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    string emp_id,name_en,name_hi, user_id, mobileno, emailid, district,department,active,client_ip;
    string emp_date_time, cid, state_id, state_name_e, state_name_h, designation_ID, newOfficeCode, stateCode;
    string district_id, district_nm_e, district_nm_h, mobile_no, email_id, designation, role, office, role_id, districtCodeNew;
    string base_department,employee,state,officelvl, userid, officelevelecode;
    string address, contact, fax, email, url, officeid,country,category;
    string rollName, welcomepage;
    string office_mapping_id, emp_code, office_id, designation_id, base_department_id, emp_name;
    string office_level_id, district_id_ofc, office_category, charge_type, date_time;
    string review, reject, forward, dispose, approve, useragent, clientos, clientbrowser;
    string userID;
   
    string status, todate, fromdate, ofc_mappingid, type, ipaddress, client_browser, useros;
    public string Office { get { return office; } set { office = value; } }
    public string RollName { get { return rollName; } set { rollName = value; } }
    public string Role { get { return role; } set { role = value; } }
    public string Base_department { get { return base_department; } set { base_department = value; } }
 
    public string Employee { get { return employee; } set { employee = value; } }

    public string State { get { return state; } set { state = value; } }
    public string Officelvl { get { return officelvl; } set { officelvl = value; } }
    public string Userid { get { return userid; } set { userid = value; } }
    public string Officelevelcode { get { return officelevelecode; } set { officelevelecode = value; } }
  
    public string Contact { get { return contact; } set { contact = value; } }
    public string Fax { get { return fax; } set { fax = value; } }
    public string Email { get { return email; } set { email = value; } }
    public string Url { get { return url; } set { url = value; } }
   
    public string Country { get { return country; } set { country = value; } }
    public string Category { get { return category; } set { category = value; } }

  
   
    public string Status { get { return status; } set { status = value; } }
    public string Todate { get { return todate; } set { todate = value; } }
    public string Fromdate { get { return fromdate; } set { fromdate = value; } }
    public string Ofc_mappingid { get { return ofc_mappingid; } set { ofc_mappingid = value; } }
    public string Type { get { return type; } set { type = value; } }
    public string Ipaddress { get { return ipaddress; } set { ipaddress = value; } }
    public string Clientos { get { return clientos; } set { clientos = value; } }
    public string Client_browser { get { return client_browser; } set { client_browser = value; } }
   

    public string Useros { get { return useros; } set { useros = value; } }

    public string UserID { get { return userID; } set { userID = value; } }


   
    public string Office_mapping_id { get { return office_mapping_id; } set { office_mapping_id = value; } }
    public string Emp_code { get { return emp_code; } set { emp_code = value; } }
    public string Emp_name { get { return emp_name; } set { emp_name = value; } }
    public string Office_id { get { return office_id; } set { office_id = value; } }
    public string Designation_id { get { return designation_id; } set { designation_id = value; } }
    public string Base_department_id { get { return base_department_id; } set { base_department_id = value; } }
    public string Office_level_id { get { return office_level_id; } set { office_level_id = value; } }
    public string District_id_ofc { get { return district_id_ofc; } set { district_id_ofc = value; } }
    public string Office_category { get { return office_category; } set { office_category = value; } }
   
    public string Charge_type { get { return charge_type; } set { charge_type = value; } }
  
    public string Client_ip { get { return client_ip; } set { client_ip = value; } }
    public string Date_time { get { return date_time; } set { date_time = value; } }
   
   
  
    public string District { get { return district; } set { district = value; } }
   
  
    public string Address { get { return address; } set { address = value; } }
  
   
    public string Officeid { get { return officeid; } set { officeid = value; } }
  
  

    public string Welcomepage { get { return welcomepage; } set { welcomepage = value; } }
   


    public string NewOfficeCode { get { return newOfficeCode; } set { newOfficeCode = value; } }
    public string StateCode { get { return stateCode; } set { stateCode = value; } }
    public string DistrictCodeNew { get { return districtCodeNew; } set { districtCodeNew = value; } }
    public string Designation_ID { get { return designation_ID; } set { designation_ID = value; } }
    public string Designation { get { return designation; } set { designation = value; } }
    public string Role_id { get { return role_id; } set { role_id = value; } }
  
   
    public string Mobile_no { get { return mobile_no; } set { mobile_no = value; } }
    public string Email_id { get { return email_id; } set { email_id = value; } }
    public string Emp_id { get { return emp_id; } set { emp_id = value; } }
    public string Emp_date_time { get { return emp_date_time; } set { emp_date_time = value; } }
    public string Name_en { get { return name_en; } set { name_en = value; } }
    public string Name_hi { get { return name_hi; } set { name_hi = value; } }
    public string User_id { get { return user_id; } set { user_id = value; } }
    public string Mobileno { get { return mobileno; } set { mobileno = value; } }
    public string Emailid { get { return emailid; } set { emailid = value; } }
    public string State_id { get { return state_id; } set { state_id = value; } }
    
    public string Department { get { return department; } set { department = value; } }
    public string Active { get { return active; } set { active = value; } }
  
    public string State_name_h { get { return state_name_h; } set { state_name_h = value; } }
    public string State_name_e { get { return state_name_e; } set { state_name_e = value; } }
    public string District_id { get { return district_id; } set { district_id = value; } }
    public string District_nm_e { get { return district_nm_e; } set { district_nm_e = value; } }
    public string District_nm_h { get { return district_nm_h; } set { district_nm_h = value; } }

    public string Review { get { return review; } set { review = value; } }
    public string Reject { get { return reject; } set { reject = value; } }
    public string Forward { get { return forward; } set { forward = value; } }
    public string Dispose { get { return dispose; } set { dispose = value; } }
    public string Approve { get { return approve; } set { approve = value; } }
    public string Useragent { get { return useragent; } set { useragent = value; } }
   
    public string ClientBrowser { get { return clientbrowser; } set { clientbrowser = value; } }
}

 
