using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;
/// <summary>
/// Summary description for dl_RTIDataForApplicant
/// </summary>
public class dl_RTIDataForApplicant
{
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    db_maria_connection db = new db_maria_connection();
	public dl_RTIDataForApplicant()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string max_RTI_Data_id()
    {
        string rtidataid;
        string str = @"select IFNULL(Max(rti_data_id),0) as rti_data_id from rti_data_for_applicant ";
        rd = db.executeSelectQuery(str);
        if (rd.table.Rows.Count > 0)
        {
            int count = Convert.ToInt32(rd.table.Rows[0]["rti_data_id"]);
            count = count + 1;
            rtidataid = count.ToString();
            rtidataid = rtidataid.PadLeft(10, '0');
        }
        else
        {
            rtidataid = "0000000001";
        }
        return rtidataid;
    }

    public string max_file_id()
    {
        string fileid;
        string str = @"select IFNULL(Max(action_fileID),0) as fileid from action_files ";
        rd = db.executeSelectQuery(str);
        if (rd.table.Rows.Count > 0)
        {
            int count = Convert.ToInt32(rd.table.Rows[0]["fileid"]);
            count = count + 1;
            fileid = count.ToString();
            fileid = fileid.PadLeft(6, '0');
        }
        else
        {
            fileid = "000001";
        }
        return fileid;
    }

    public ReturnClass.ReturnBool Insert_Action_Info(bl_RTIDataForApplicant bl)
    {
        using (TransactionScope transScope = new TransactionScope())
        {
            //#region Status IS Pending
            rb.status = true;
           
                if (bl.Is_file_upload == "Y")
                {
                    rb = insert_action_file(bl);
                }
                if (rb.status == true)
                {
                                        
                      rb = insert_action(bl);
                       
                            if (rb.status == true)
                            {
                                transScope.Complete();
                            }
                                       
                }
           
        }
        return rb;
    }

    public ReturnClass.ReturnBool insert_action_file(bl_RTIDataForApplicant bl1)
    {
        string str = @"insert into action_files
                ( action_fileID, fileName, fileType, fileData, FileDescription, action_id, emp_mapping_id, rti_id ) 
                values
                 ( @action_file_id, @file_name, @file_type, @file_data, @file_description, @action_id, @emp_map_id, @rti_id)";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("action_file_id",bl1.RTI_fileID),
            new MySqlParameter("file_name",bl1.RTI_fileName),
            new MySqlParameter("file_type",bl1.RTI_fileType),
            new MySqlParameter("file_data",bl1.RTI_fileData),
             new MySqlParameter("file_description",bl1.FileDescription),
            new MySqlParameter("action_id",bl1.Action_id),
            new MySqlParameter("emp_map_id",bl1.Office_mapping_id),
            new MySqlParameter("rti_id",bl1.Rti_id)
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }


    public ReturnClass.ReturnBool insert_action(bl_RTIDataForApplicant bl1)
    {
        string str = @"insert into rti_data_for_applicant
                       (rti_data_id, rti_id, action_id, action_file_id, rti_prepared_emp, result_description, IsFileUpload, IsMeetingFix, MeetingDate, 
                        IsAdditionalFees, FeesAmount, ClientOS, ClientIP, ClientBrowser, ClientAgent ) values
                        ( @rti_data_id, @rti_id, @action_id, @action_file_id, @rti_prepared_emp, @result_description, @IsFileUpload, @IsMeetingFix, @MeetingDate,
                         @IsAdditionalFees, @FeesAmount, @client_os, @ipaddress, @client_browser,@useragent )";
        MySqlParameter[] pm = new MySqlParameter[]{
             new MySqlParameter("rti_data_id",bl1.RTI_data_id),
             new MySqlParameter("rti_id",bl1.Rti_id),
             new MySqlParameter("action_id",bl1.Action_id),
             new MySqlParameter("action_file_id",bl1.RTI_fileID),
             new MySqlParameter("rti_prepared_emp",bl1.Userid),
             new MySqlParameter("result_description",bl1.Result_description),
             new MySqlParameter("IsFileUpload",bl1.Is_file_upload),
             new MySqlParameter("IsMeetingFix",bl1.IsMeeting),
             new MySqlParameter("MeetingDate",bl1.Meeting_date),
             new MySqlParameter("IsAdditionalFees",bl1.IsAdditionalFees),
             new MySqlParameter("FeesAmount",bl1.Fees_amount),
             new MySqlParameter("client_os",bl1.Useros),
             new MySqlParameter("ipaddress",bl1.Ipaddress),
             new MySqlParameter("client_browser",bl1.User_browser),
             new MySqlParameter("useragent",bl1.Useragent)
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }
    //insert new action in table    

}