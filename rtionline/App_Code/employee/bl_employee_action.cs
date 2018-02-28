using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_employee_action
/// </summary>
public class bl_employee_action
{
    //public bl_employee_action()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

    string action_date, action_discription, rti_status, rti_id, action_id, is_file_upload, emp_id, isnew, isforward, dept_status;
    string district_id_ofc, base_department_id, office_level_id, office_category, office_id, office_mapping_id, designation;
    string rti_fileID, fileName, fileType, fileDescription, rti_request_id, bpl_rti_FileType;
    string review, reject, forward, dispose, approve, permission, approve_auth;
    string datafile_id, datafiletype, data_filename, datafile_description, additional_fees, rti_data_id, is_additional_fee, fee_ammount;
    List<string> para_string = new List<string>();
    Byte[] fileData;
    string ipaddress, useros, user_browser, useragent, userid, state;
    string sending_method, r_office_map_id, s_mapping_id, section_mapping_id;
    string meeting_date, isMeeting, isAdditionalFees, rti_Discription, is_RTI_Data, is_rti_file_upload;
    public string Is_rti_file_upload { get { return is_rti_file_upload; } set { is_rti_file_upload = value; } }
    public string Is_RTI_Data { get { return is_RTI_Data; } set { is_RTI_Data = value; } }
    public string Rti_Discription { get { return rti_Discription; } set { rti_Discription = value; } }
    public string Action_id { get { return action_id; } set { action_id = value; } }
    public string Meeting_date { get { return meeting_date; } set { meeting_date = value; } }
    public string IsMeeting { get { return isMeeting; } set { isMeeting = value; } }
    public string IsAdditionalFees { get { return isAdditionalFees; } set { isAdditionalFees = value; } }
    public string Emp_id { get { return emp_id; } set { emp_id = value; } }
    public string Rti_id { get { return rti_id; } set { rti_id = value; } }
    public string Action_date { get { return action_date; } set { action_date = value; } }
    public string Action_discription { get { return action_discription; } set { action_discription = value; } }
    public string Rti_status { get { return rti_status; } set { rti_status = value; } }
    public string Is_file_upload { get { return is_file_upload; } set { is_file_upload = value; } }
    public string Is_forward { get { return isforward; } set { isforward = value; } }
    public string Dept_Status { get { return dept_status; } set { dept_status = value; } }
    //sending to another office
    public string Base_department_id { get { return base_department_id; } set { base_department_id = value; } }
    public string Office_level_id { get { return office_level_id; } set { office_level_id = value; } }
    public string District_id_ofc { get { return district_id_ofc; } set { district_id_ofc = value; } }
    public string Office_category { get { return office_category; } set { office_category = value; } }
    public string Office_id { get { return office_id; } set { office_id = value; } }
    public string Office_mapping_id { get { return office_mapping_id; } set { office_mapping_id = value; } }
    public string State { get { return state; } set { state = value; } }
    public string Designation { get { return designation; } set { designation = value; } }
    public string Isnew { get { return isnew; } set { isnew = value; } }

    // for file upload
    public string RTI_fileID { get { return rti_fileID; } set { rti_fileID = value; } }
    public string RTI_fileName { get { return fileName; } set { fileName = value; } }
    public string RTI_fileType { get { return fileType; } set { fileType = value; } }
    public Byte[] RTI_fileData { get { return fileData; } set { fileData = value; } }
    public string FileDescription { get { return fileDescription; } set { fileDescription = value; } }
    public string RTI_Request_id { get { return rti_request_id; } set { rti_request_id = value; } }
    public string BPL_RTI_FileType { get { return bpl_rti_FileType; } set { bpl_rti_FileType = value; } }

    // Users Legal Pc Detail 
    public string Ipaddress { get { return ipaddress; } set { ipaddress = value; } }
    public string Useros { get { return useros; } set { useros = value; } }
    public string User_browser { get { return user_browser; } set { user_browser = value; } }
    public string Useragent { get { return useragent; } set { useragent = value; } }
    public string Userid { get { return userid; } set { userid = value; } }

    //forward table 
    public string Sending_method { get { return sending_method; } set { sending_method = value; } }
    public string R_office_map_id { get { return r_office_map_id; } set { r_office_map_id = value; } }
    public string S_officemapping_id { get { return s_mapping_id; } set { s_mapping_id = value; } }
    public string Section_mapping_id { get { return section_mapping_id; } set { section_mapping_id = value; } }
    public byte[] Filedata { get { return fileData; } set { fileData = value; } }

    //permission table
    public string Review { get { return review; } set { review = value; } }
    public string Reject { get { return reject; } set { reject = value; } }
    public string Forward { get { return forward; } set { forward = value; } }
    public string Dispose { get { return dispose; } set { dispose = value; } }
    public string Approve { get { return approve; } set { approve = value; } }
    public string Permission { get { return permission; } set { permission = value; } }
    public string Approve_auth { get { return approve_auth; } set { approve_auth = value; } }
    public List<string> Para_string { get { return para_string; } set { para_string = value; } }

    //dispose data for User
    public string DatafileID { get { return datafile_id; } set { datafile_id = value; } }
    public string Data_fileName { get { return data_filename; } set { data_filename = value; } }
    public string Data_fileType { get { return datafiletype; } set { datafiletype = value; } }
    public Byte[] Data_fileData { get { return fileData; } set { fileData = value; } }
    public string Data_FileDescription { get { return datafile_description; } set { datafile_description = value; } }
    public string RTI_data_id { get { return rti_data_id; } set { rti_data_id = value; } }
    public string Additional_fees { get { return additional_fees; } set { additional_fees = value; } }
    public string Fee_Ammount { get { return fee_ammount; } set { fee_ammount = value; } } 
}