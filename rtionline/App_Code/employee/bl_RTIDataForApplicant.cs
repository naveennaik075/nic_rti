using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for bl_RTIDataForApplicant
/// </summary>
public class bl_RTIDataForApplicant
{
	public bl_RTIDataForApplicant()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    string rti_data_id, rti_id, action_id, is_file_upload, isAdditionalFees, fees_amount, meeting_date, isMeeting, result_description;
    string action_fileID, fileName, fileType, fileDescription, rti_request_id, office_mapping_id;
    Byte[] fileData;
    string ipaddress, useros, user_browser, useragent, userid;

    public string Action_id { get { return action_id; } set { action_id = value; } }
    public string Meeting_date { get { return meeting_date; } set { meeting_date = value; } }
    public string IsMeeting { get { return isMeeting; } set { isMeeting = value; } }
    public string IsAdditionalFees { get { return isAdditionalFees; } set { isAdditionalFees = value; } }
    public string Rti_id { get { return rti_id; } set { rti_id = value; } }
    public string RTI_data_id { get { return rti_data_id; } set { rti_data_id = value; } }
    public string Is_file_upload { get { return is_file_upload; } set { is_file_upload = value; } }
    public string Result_description { get { return result_description; } set { result_description = value; } }
    public string Fees_amount { get { return fees_amount; } set { fees_amount = value; } }

    // for file upload
    public string RTI_fileID { get { return action_fileID; } set { action_fileID = value; } }
    public string RTI_fileName { get { return fileName; } set { fileName = value; } }
    public string RTI_fileType { get { return fileType; } set { fileType = value; } }
    public Byte[] RTI_fileData { get { return fileData; } set { fileData = value; } }
    public string FileDescription { get { return fileDescription; } set { fileDescription = value; } }
    public string RTI_Request_id { get { return rti_request_id; } set { rti_request_id = value; } }
    public string Office_mapping_id { get { return office_mapping_id; } set { office_mapping_id = value; } }

    // Users Legal Pc Detail 
    public string Ipaddress { get { return ipaddress; } set { ipaddress = value; } }
    public string Useros { get { return useros; } set { useros = value; } }
    public string User_browser { get { return user_browser; } set { user_browser = value; } }
    public string Useragent { get { return useragent; } set { useragent = value; } }
    public string Userid { get { return userid; } set { userid = value; } }



}