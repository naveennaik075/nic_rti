using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class bl_empMap
{


    string office_mapping_id, emp_code, office_id, designation_id, base_department_id, emp_name, mstatus;
    string office_level_id, district_id_ofc, office_category, user_id, charge_type, active, client_ip, date_time;
    string state, fromActive, toActive;
    string emp_code1, office_id1, designation_id1, base_department_id1, office_level_id1, district_id_ofc1, office_category1, toActive1;
    string role_id, role_id1;
    List<string> permission = new List<string>() ;
    //permissions
    string review, reject, forward, dispose, approve, useragent, clientos, clientbrowser;

    public string Role_id { get { return role_id; } set { role_id = value; } }
    public string Role_id1 { get { return role_id1; } set { role_id1 = value; } }
    public string Emp_code1 { get { return emp_code1; } set { emp_code1 = value; } }
    public string Office_id1 { get { return office_id1; } set { office_id1 = value; } }
    public string Designation_id1 { get { return designation_id1; } set { designation_id1 = value; } }
    public string Base_department_id1 { get { return base_department_id1; } set { base_department_id1 = value; } }
    public string Office_level_id1 { get { return office_level_id1; } set { office_level_id1 = value; } }
    public string District_id_ofc1 { get { return district_id_ofc1; } set { district_id_ofc1 = value; } }
    public string Office_category1 { get { return office_category1; } set { office_category1 = value; } }
    public string ToActive1 { get { return toActive1; } set { toActive1 = value; } }
    public string Mstatus { get { return mstatus; } set { mstatus = value; } }
    public string FromActive { get { return fromActive; } set { fromActive = value; } }
    public string ToActive { get { return toActive; } set { toActive = value; } }
    public string State { get { return state; } set { state = value; } }
    //string base_department_id, office_level_id, district_id_ofc, office_category, office_id, office_mapping_id;
    public string Office_mapping_id { get { return office_mapping_id; } set { office_mapping_id = value; } }
    public string Emp_code { get { return emp_code; } set { emp_code = value; } }
    public string Emp_name { get { return emp_name; } set { emp_name = value; } }
    public string Office_id { get { return office_id; } set { office_id = value; } }
    public string Designation_id { get { return designation_id; } set { designation_id = value; } }
    public string Base_department_id { get { return base_department_id; } set { base_department_id = value; } }
    public string Office_level_id { get { return office_level_id; } set { office_level_id = value; } }
    public string District_id_ofc { get { return district_id_ofc; } set { district_id_ofc = value; } }
    public string Office_category { get { return office_category; } set { office_category = value; } }
    public string User_id { get { return user_id; } set { user_id = value; } }
    public string Charge_type { get { return charge_type; } set { charge_type = value; } }
    public string Active { get { return active; } set { active = value; } }
    public string Client_ip { get { return client_ip; } set { client_ip = value; } }
    public string Date_time { get { return date_time; } set { date_time = value; } }
    //review,Reject,Forward,Dispose,Approve
    public string Review { get { return review; } set { review = value; } }
    public string Reject { get { return reject; } set { reject = value; } }
    public string Forward { get { return forward; } set { forward = value; } }
    public string Dispose { get { return dispose; } set { dispose = value; } }
    public string Approve { get { return approve; } set { approve = value; } }
    public string Useragent { get { return useragent; } set { useragent = value; } }
    public string ClientOS { get { return clientos; } set { clientos = value; } }
    public string ClientBrowser { get { return clientbrowser; } set { clientbrowser = value; } }
    public List<string> Permission { get { return permission; } set { permission = value; } }
}