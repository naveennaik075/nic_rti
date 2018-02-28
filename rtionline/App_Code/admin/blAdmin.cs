using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for blAdmin
/// </summary>
public class blAdmin
{
    string userID;
    string office_level_id, district_id_ofc, office_category, office_id, base_department_id, useragent, clientos, clientbrowser, client_ip, date_time;
    string year_issue, fileName, contentType,  fileDescription;
    Byte[] fileData;
    public string UserID { get { return userID; } set { userID = value; } }
   
    public string Office_id { get { return office_id; } set { office_id = value; } }
    public string Base_department_id { get { return base_department_id; } set { base_department_id = value; } }
    public string Office_level_id { get { return office_level_id; } set { office_level_id = value; } }
    public string District_id_ofc { get { return district_id_ofc; } set { district_id_ofc = value; } }
    public string Office_category { get { return office_category; } set { office_category = value; } }

    public string Client_ip { get { return client_ip; } set { client_ip = value; } }
    public string Date_time { get { return date_time; } set { date_time = value; } }
    public string Useragent { get { return useragent; } set { useragent = value; } }
    public string ClientOS { get { return clientos; } set { clientos = value; } }
    public string ClientBrowser { get { return clientbrowser; } set { clientbrowser = value; } }

    public string Year_issue { get { return year_issue; } set { year_issue = value; } }
    public string FileName { get { return fileName; } set { fileName = value; } }
    public string ContentType { get { return contentType; } set { contentType = value; } }
    public Byte[] FileData { get { return fileData; } set { fileData = value; } }
    public string FileDescription { get { return fileDescription; } set { fileDescription = value; } }

}