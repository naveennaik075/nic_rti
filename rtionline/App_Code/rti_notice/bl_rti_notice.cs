using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// /// ekta
/// 1-oct-2016
/// notice board
/// </summary>
public class bl_rti_notice
{
    public bl_rti_notice()
    {
        //
        // TODO: Add constructor logic here
        //
    }



}
public class Notice_Entry
{
    public string header, permanent, active, hyperlink, client_ip, user_id, datetime, publish_date, datefrom, dateto, filetype, file_Extn, Url, file_id, noticeID, validity_status, subject_status, fromto_status;
    public string bold, image, blink, priority, color, status, pending, district_id, validity_from, validity_to, base_dept_id, office_id, next_date, keywordSearch;
    public int file_ID;
    byte[] file_data;
    public string issueYear;

    public string IssueYear { get { return issueYear; } set { issueYear = value; } }
    public string District_id { get { return district_id; } set { district_id = value; } }
    public string Base_dept_id { get { return base_dept_id; } set { base_dept_id = value; } }
    public string Office_id { get { return office_id; } set { office_id = value; } }
    public string Header { get { return header; } set { header = value; } }
    public string Permanent { get { return permanent; } set { permanent = value; } }
    public string Active { get { return active; } set { active = value; } }
    public string Hyperlink { get { return hyperlink; } set { hyperlink = value; } }
    public string Client_ip { get { return client_ip; } set { client_ip = value; } }
    public string User_id { get { return user_id; } set { user_id = value; } }
    public string Datetime { get { return datetime; } set { datetime = value; } }
    public string Publish_date { get { return publish_date; } set { publish_date = value; } }
    public string Datefrom { get { return datefrom; } set { datefrom = value; } }
    public string Dateto { get { return dateto; } set { dateto = value; } }
    public string Filetype { get { return filetype; } set { filetype = value; } }
    public string File_id { get { return file_id; } set { file_id = value; } }
    public string url { get { return Url; } set { Url = value; } }
    public int File_ID { get { return file_ID; } set { file_ID = value; } }
    public string NoticeID { get { return noticeID; } set { noticeID = value; } }

    public string Validity_status { get { return validity_status; } set { validity_status = value; } }
    public string Subject_status { get { return subject_status; } set { subject_status = value; } }
    public string Fromto_status { get { return fromto_status; } set { fromto_status = value; } }
    public string Bold { get { return bold; } set { bold = value; } }
    public string Image { get { return image; } set { image = value; } }
    public string Blink { get { return blink; } set { blink = value; } }
    public string Priority { get { return priority; } set { priority = value; } }
    public string Color { get { return color; } set { color = value; } }
    public string Status { get { return status; } set { status = value; } }
    public string Pending { get { return pending; } set { pending = value; } }
    public string Validity_From { get { return validity_from; } set { validity_from = value; } }
    public string Validity_To { get { return validity_to; } set { validity_to = value; } }

    public string Next_date { get { return next_date; } set { next_date = value; } }

    public string KeywordSearch { get { return keywordSearch; } set { keywordSearch = value; } }

}
public class Upload_doc
{
    public string file_Extn, url, filename, file_id, content_type, upload_Date, user_id, client_ip, status, noticeID,district_id;

    byte[] file_data;
    public int file_ID;
    public int File_ID { get { return file_ID; } set { file_ID = value; } }
    public string File_Extn { get { return file_Extn; } set { file_Extn = value; } }
    public byte[] File_Data { get { return file_data; } set { file_data = value; } }
    public string URL { get { return url; } set { url = value; } }
    public string Filename { get { return filename; } set { filename = value; } }
    public string File_id { get { return file_id; } set { file_id = value; } }
    public string Content_type { get { return content_type; } set { content_type = value; } }
    public string Upload_Date { get { return upload_Date; } set { upload_Date = value; } }
    public string Client_ip { get { return client_ip; } set { client_ip = value; } }
    public string User_id { get { return user_id; } set { user_id = value; } }
    public string Status { get { return status; } set { status = value; } }
    public string NoticeID { get { return noticeID; } set { noticeID = value; } }
    public string District_id { get { return district_id; } set { district_id = value; } }
}