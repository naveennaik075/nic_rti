using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;

/// <summary>
/// Summary description for dl_employee_action
/// </summary>
public class dl_employee_action
{
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    db_maria_connection db = new db_maria_connection();
    public ReturnClass.ReturnDataTable applicantDetail(bl_employee_action bl1)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = @"select rd.rti_id, rd.Applicant_Name_en, rd.rti_Subject, rd.rti_Details, 
                    date_format( rd.rti_DateTime,'%d / %M / %Y')as rti_DateTime, rd.Address, rd.Mobile_No, 
                    ofc.OfficeName, ofc.NewOfficeCode ,dg.Designation_Name, bd.dept_name, rs.`status`,dl.DisplayName_en, 
                    EMP.Name_en,eom.designation_id as degid from rti_status rs
                    inner join emp_office_mapping eom on eom.office_mapping_id= rs.officer_maping_id
                    inner join office ofc on ofc.NewOfficeCode=eom.office_id
                    and ofc.BaseDeptCode= eom.base_department_id and ofc.OfficeLevel=eom.office_level_id and 
                    ofc.DistrictCodeNew= eom.district_id_ofc and ofc.StateCode
                    inner join basedepartment bd on bd.dept_id= eom.base_department_id
                    inner join rti_detail rd on rd.rti_id=rs.rti_id
                    inner join  designation dg on dg.Designation_ID= eom.designation_id
                    inner join employee_table EMP ON EMP.emp_id= eom.emp_code
                    inner join ddl_list dl on dl.DDL_Name_value =rs.status
                    where rs.rti_id=@Rti_id";
        MySqlParameter[] pm = new MySqlParameter[]{
          new MySqlParameter("Rti_id",bl1.Rti_id)
        };

        rt = db.executeSelectQuery(st, pm);
        return rt;


    }
    //Detail of unique RTI

    public ReturnClass.ReturnDataTable GetRTIDetails(bl_employee_action bl1)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = @"select result_description, FileName, FileDescription, IsMeetingFix, IsAdditionalFees, FeesAmount
                    from rti_data_for_applicant as rd
                    where rd.rti_id=@Rti_id";
        MySqlParameter[] pm = new MySqlParameter[]{
          new MySqlParameter("Rti_id",bl1.Rti_id)
        };


        rt = db.executeSelectQuery(st, pm);
        return rt;


    }


    public string max_action_id(bl_employee_action bl)
    {
        string action;
        int count;
        string str = "select IFNULL(Max(action_id),0) as actionid from rti_action where rti_id=@rti";
        MySqlParameter[] pm = new MySqlParameter[]{
           new MySqlParameter("rti",bl.Rti_id)
       };
        rd = db.executeSelectQuery(str, pm);
        if (rd.table.Rows.Count > 0)
        {
            count = Convert.ToInt32(rd.table.Rows[0]["actionid"]);
            count = count + 1;
            action = count.ToString();
            action = action.PadLeft(3, '0');
        }
        else
        {
            action = "001";
        }
        return action;
    }
    //max action id for Rti
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


    public ReturnClass.ReturnBool Insert_Clarification_Action(bl_employee_action bl)
    {
        using (TransactionScope transScope = new TransactionScope())
        {

            rb.status = true;


            if (rb.status == true)
            {
                rb = insert_action(bl);
                if (rb.status == true)
                {
                    rb = Update_status(bl);
                    if (rb.status == true)
                    {
                        transScope.Complete();
                    }

                }
            }

        }
        return rb;
    }


    public ReturnClass.ReturnBool Insert_Action_Info(bl_employee_action bl)
    {
        using (TransactionScope transScope = new TransactionScope())
        {

            rb.status = true;

            if (bl.Is_file_upload == "Y")
            {
                rb = insert_action_file(bl);
            }
            if (rb.status == true)
            {
                rb = insert_forward(bl);

                if (rb.status == true)
                {
                    rb = insert_action(bl);
                    if (rb.status == true)
                    {
                        rb = Update_status(bl);
                        if (rb.status == true)
                        {
                            if (bl.Rti_Discription != "")
                            {
                                rb = Insert_RTI_Data_Applicant(bl);
                            }
                            if (rb.status == true)
                            {
                                transScope.Complete();
                            }

                        }
                    }
                }
            }

        }
        return rb;
    }


    #region right code
    //    if (bl.Rti_status == "PEN")
    //    {
    //        if (bl.Is_file_upload == "Y" )
    //        {
    //            rb = insert_action_file(bl);

    //            if (rb.status == true)
    //            {

    //                rb = insert_forward(bl);
    //                if (rb.status == true)
    //                {
    //                    rb = insert_action(bl);
    //                    if (rb.status == true)
    //                    {
    //                        Update_status(bl);
    //                    }
    //                }
    //            }
    //        }
    //        else if (bl.Is_file_upload == "N")
    //        {
    //            rb = insert_forward(bl);
    //            if (rb.status == true)
    //            {
    //                rb = insert_action(bl);
    //                if (rb.status == true)
    //                {
    //                    Update_status(bl);
    //                }
    //            }

    //        }

    //    }
    //    #endregion
    //    else if (bl.Rti_status == "CLT")
    //    {
    //        if (bl.Is_file_upload == "Y")
    //        {
    //            rb = insert_action_file(bl);
    //            if (rb.status == true)
    //            {
    //                rb = insert_action(bl);
    //                if (rb.status == true)
    //                {
    //                    Update_status(bl);
    //                }
    //            }

    //        }
    //        else if (bl.Is_file_upload == "N")
    //        {
    //            rb = insert_action(bl);
    //            if (rb.status == true)
    //            {
    //                Update_status(bl);
    //            }
    //        }

    //    }
    //    if (rb.status == true)
    //    {
    //        transScope.Complete();
    //    }
    //}
    //return rb;
    #endregion
    public ReturnClass.ReturnBool Insert_RTI_Data_Applicant(bl_employee_action bl1)
    {
        string update_query = "";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("rti_data_id",bl1.RTI_data_id),
            new MySqlParameter("rti_id",bl1.Rti_id),
            new MySqlParameter("action_id",bl1.Action_id),
            new MySqlParameter("action_file_id",bl1.RTI_fileID),
             new MySqlParameter("FileName",bl1.Data_fileName),
            new MySqlParameter("FileType",bl1.Data_fileType),
             new MySqlParameter("FileDescription",bl1.Data_FileDescription),
              new MySqlParameter("FileData",bl1.Data_fileData),
            new MySqlParameter("rti_prepared_emp",bl1.Userid),
            new MySqlParameter("result_description",bl1.Rti_Discription),
             new MySqlParameter("IsFileUpload",bl1.Is_rti_file_upload),
            new MySqlParameter("IsMeetingFix",bl1.IsMeeting),
            new MySqlParameter("MeetingDate",bl1.Meeting_date),
            new MySqlParameter("IsAdditionalFees",bl1.IsAdditionalFees),
            new MySqlParameter("FeesAmount",bl1.Additional_fees),
            new MySqlParameter("ClientOS",bl1.Useros),
            new MySqlParameter("ClientIP",bl1.Ipaddress),
            new MySqlParameter("ClientBrowser",bl1.User_browser),
            new MySqlParameter("ClientAgent",bl1.Useragent)
        };
        if (bl1.Is_RTI_Data == "Y")
        {
            string str_log_query = @" insert into rti_data_for_applicant_log select * from rti_data_for_applicant  rd  WHERE rd.rti_id =@rti_Id";
            MySqlParameter[] pm_log = new MySqlParameter[]
                {
                new MySqlParameter("rti_Id",bl1.Rti_id)
                };


            update_query = " update rti_data_for_applicant set result_description=@result_description ";
            if (bl1.Is_rti_file_upload == "Y")
            {
                update_query += " , FileName=@FileName , FileType=@FileType , FileDescription=@FileDescription , FileData=@FileData ";
            }
            if (bl1.IsMeeting == "Y")
            {
                update_query += " , MeetingDate=@MeetingDate ";
            }
            if (bl1.IsAdditionalFees == "Y")
            {
                update_query += " , FeesAmount=@FeesAmount ";
            }
            update_query += " , ClientOS=@ClientOS , ClientIP=@ClientIP , ClientBrowser=@ClientBrowser , ClientAgent=@ClientAgent ";
            update_query = update_query + " WHERE rti_id=@rti_Id ";

            rb = db.executeInsertQuery(str_log_query, pm_log);
            if (rb.status == true)
            {
                rb = db.executeUpdateQuery(update_query, pm);

            }
        }
        else
        {

            string str = @"insert into rti_data_for_applicant
                       (rti_data_id, rti_id, action_id, action_file_id, FileName, FileType, FileDescription, FileData, rti_prepared_emp, 
                        result_description, IsFileUpload, IsMeetingFix, MeetingDate, IsAdditionalFees, FeesAmount, 
                        ClientOS, ClientIP, ClientBrowser, ClientAgent) values
                      ( @rti_data_id, @rti_id, @action_id, @action_file_id, @FileName, @FileType, @FileDescription, @FileData,
                        @rti_prepared_emp, @result_description, @IsFileUpload, @IsMeetingFix, @MeetingDate, @IsAdditionalFees,
                        @FeesAmount, @ClientOS, @ClientIP, @ClientBrowser, @ClientAgent 
                        )";

            rb = db.executeInsertQuery(str, pm);
        } // Else Insert Query
        return rb;
    }



    public ReturnClass.ReturnBool Update_status(bl_employee_action bl)
    {
        string str = @"insert into rti_status_log select * from rti_status  rd  WHERE rd.rti_id =@rti_Id";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("rti_Id",bl.Rti_id)
        };

        string str1 = @"update rti_status set status=@status, DeptStatus=@DeptStatus , action_id=@actionid,
                    officer_maping_id=@ofcmapid,IPAddress=@ipaddress,action_date=@actiondate,client_browser=@clientbr, 
                    client_OS=@clintos,useragent=@useragent,IsNew=@isnew, user_id=@userid
                    where rti_id=@rtiid";
        MySqlParameter[] pm1 = new MySqlParameter[]{
               new MySqlParameter("status",bl.Rti_status),
               new MySqlParameter("DeptStatus",bl.Dept_Status),
               new MySqlParameter("actionid",bl.Action_id),
               new MySqlParameter("ofcmapid",bl.R_office_map_id),
               new MySqlParameter("ipaddress",bl.Ipaddress),
               new MySqlParameter("actiondate",bl.Action_date),
               new MySqlParameter("clientbr",bl.User_browser),
               new MySqlParameter("clintos",bl.Useros),
               new MySqlParameter("useragent",bl.Useragent),
               new MySqlParameter("isnew",bl.Isnew),
               new MySqlParameter("userid",bl.Userid),
               new MySqlParameter("rtiid",bl.Rti_id)
            };
        rb = db.executeInsertQuery(str, pm);
        if (rb.status == true)
        {
            rb = db.executeUpdateQuery(str1, pm1);

        }

        return rb;
    }
    public ReturnClass.ReturnBool insert_action_file(bl_employee_action bl1)
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
    //insert new action in table    
    public ReturnClass.ReturnBool insert_action(bl_employee_action bl1)
    {
        string str = @"insert into rti_action
                       (action_id,rti_id,officer_mapping_id,action_detail,action_date,IsUpload,IsMeeting,meeting_date,ipaddress,
                        dept_status,client_OS,client_browser,userAgent, RejectReason) values
                        (@action_id,@rti_id,@officer_mapping_id,@action_detail,@action_date,@Isupload,@IsMeeting,@meeting_date,
                        @ipaddress,@status,@client_os,@client_browser,@useragent, @RejectReason)";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("action_id",bl1.Action_id),
            new MySqlParameter("rti_id",bl1.Rti_id),
            new MySqlParameter("officer_mapping_id",bl1.Office_mapping_id),
            new MySqlParameter("action_detail",bl1.Action_discription),
             new MySqlParameter("action_date",bl1.Action_date),
            new MySqlParameter("Isupload",bl1.Is_file_upload),
             new MySqlParameter("IsMeeting",bl1.IsMeeting),
              new MySqlParameter("meeting_date",bl1.Meeting_date),
            new MySqlParameter("ipaddress",bl1.Ipaddress),
            new MySqlParameter("status",bl1.Dept_Status),
             new MySqlParameter("client_os",bl1.Useros),
            new MySqlParameter("client_browser",bl1.User_browser),
            new MySqlParameter("useragent",bl1.Useragent),
            new MySqlParameter("RejectReason",bl1.Reject),
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }
    //insert new action in table    
    public ReturnClass.ReturnBool insert_forward(bl_employee_action bl)
    {
        string str = @"insert into forward 
(rti_id,action_id,file_id,sendingMethod,r_officeMapping_id,s_officeMapping_id,section_mapping_id,ip_address,client_OS,client_browser,userAgent)
values 
(@rti_id,@action_id,@file_id,@sendingMethod,@r_officeMapping_id,@s_officeMapping_id,@section_mapping_id,@ip_address,@client_OS,@client_browser,@userAgent)       
";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("rti_id",bl.Rti_id),
            new MySqlParameter("action_id",bl.Action_id),
            new MySqlParameter("file_id",bl.RTI_fileID),
            new MySqlParameter("sendingMethod",bl.Sending_method),
            new MySqlParameter("r_officeMapping_id",bl.R_office_map_id),
            new MySqlParameter("s_officeMapping_id",bl.S_officemapping_id),
            new MySqlParameter("section_mapping_id",bl.Section_mapping_id),
              new MySqlParameter("ip_address",bl.Ipaddress),
             new MySqlParameter("client_OS",bl.Useros),
            new MySqlParameter("client_browser",bl.User_browser),
            new MySqlParameter("userAgent",bl.Useragent)
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }
    //insert into forward table
    public ReturnClass.ReturnDataTable bind_withinoffice(bl_employee_action bl)
    {
        /*  
          string str1 = @"select eom.office_mapping_id as mapid, eom.emp_code as empid, et.Name_en as name
  from emp_office_mapping as eom
  inner join employee_table as et on et.emp_id= eom.emp_code and et.base_department_id= eom.base_department_id and et.Active= eom.active 
  inner join permission as pm on eom.office_mapping_id = pm.emp_map_id and pm.ActionPermission=@actionPermission 
  where eom.office_id= (select eom.office_id as officeid  FROM emp_office_mapping as eom where 
                         eom.office_mapping_id=@officemapping and eom.active='Y')
  and eom.active='Y' and eom.emp_code != (select eom.emp_code  FROM emp_office_mapping as eom where 
                         eom.office_mapping_id=@officemapping and eom.active='Y')
  order by name asc  ";
          */
        string str1 = @"select eom.office_mapping_id as mapid, eom.emp_code as empid, concat( et.Name_en,'/', dg.Designation_Name ,'/',ofc.OfficeName  ) as  name
from emp_office_mapping as eom
inner join employee_table as et on et.emp_id= eom.emp_code  and et.Active= eom.active 
inner join  designation as dg on  dg.Designation_ID = eom.designation_id
inner join  office as ofc on  ofc.NewOfficeCode = eom.office_id
inner join permission as pm on eom.office_mapping_id = pm.emp_map_id and pm.ActionPermission=@actionPermission 
where eom.office_id= (select eom.office_id as officeid  FROM emp_office_mapping as eom where 
                       eom.office_mapping_id=@officemapping and eom.active='Y')
and eom.active='Y' and eom.emp_code != (select eom.emp_code  FROM emp_office_mapping as eom where 
                       eom.office_mapping_id=@officemapping and eom.active='Y')
 and eom.role_id=3 order by name asc  ";
        MySqlParameter[] pm = new MySqlParameter[]{
             new MySqlParameter("officemapping",bl.Office_mapping_id),
             new MySqlParameter("actionPermission",bl.Permission)
              };
        rd = db.executeSelectQuery(str1, pm);

        return rd;
    }
    // to forward officers within  office 
    public ReturnClass.ReturnDataTable Rti_action_detail_last(bl_employee_action bl)
    {
        string count = "";
        string str = "select IFNULL(Max(action_id),0) as actionid from rti_action where rti_id=@rti";
        MySqlParameter[] pm = new MySqlParameter[]
        {
           new MySqlParameter("rti",bl.Rti_id)
         };
        rd = db.executeSelectQuery(str, pm);
        if (rd.table.Rows.Count > 0)
        {
            count = rd.table.Rows[0]["actionid"].ToString();
        }
        string str1 = "select * from rti_action where action_id= @count and rti_id=@rti";
        MySqlParameter[] pm1 = new MySqlParameter[]
        {
            new MySqlParameter("count",count),
              new MySqlParameter("rti",bl.Rti_id)
         };
        rd = db.executeSelectQuery(str1, pm1);
        return rd;
    }
    //last action detail for selected rti
    public ReturnClass.ReturnDataTable all_rti_actions(bl_employee_action bl)
    {
        string str = "select * from  rti_action where rti_id=@rti order by action_date asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("rti",bl.Rti_id)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    //select all actions have taken on rti
    public ReturnClass.ReturnDataTable GetRejectReason()
    {
        string str = @"select dl.DDL_Name_Value as Value , dl.DisplayName_en as Name from ddl_list dl 
                        where dl.Category='RejReason' ";
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable Status()
    {
        string str = @"select dl.DDL_Name_Value as id , dl.DisplayName_en as name from ddl_list dl 
                        where dl.Category='RTIStatus' and dl.DDL_Name_Value <> 'REG'";
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable district_code(bl_employee_action bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select District_ID,District_Name from districts where StateCode=@statecode  order by District_Name ";
        MySqlParameter[] pm = new MySqlParameter[]{
        new MySqlParameter("statecode",bl.State)
        };
        rt = db.executeSelectQuery(st, pm);
        return rt;


    }
    public ReturnClass.ReturnDataTable department_id()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select dept_id,dept_name from basedepartment order by dept_name ";
        rt = db.executeSelectQuery(st);
        return rt;
    }
    public ReturnClass.ReturnDataTable officecategory()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = " select oc.OfficeCategoryCode, oc.OfficeCategoryName from officecategory oc ";
        rt = db.executeSelectQuery(st);
        return rt;
    }

    public ReturnClass.ReturnDataTable designation()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = " select Designation_ID, Designation_Name from designation  ";


        rt = db.executeSelectQuery(st);
        return rt;
    }
    public ReturnClass.ReturnDataTable officelevel(bl_employee_action bl)
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = "select ol.OfficeLevelName as Office_level, ol.OfficeLevelCode as olc from officelevel ol   ";
        string where = " where 1 = 1 and ol.BaseDeptCode=@BaseDeptCode";
        MySqlParameter[] fa = new MySqlParameter[]
        {
        new MySqlParameter("BaseDeptCode", bl.Base_department_id)
        };
        str = str + where + " order by Office_level asc";
        rd = db.executeSelectQuery(str, fa);
        return rd;
    }
    public ReturnClass.ReturnDataTable office(bl_employee_action bl)
    {

        string str = "", where = "   where 1 = 1   ";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();

            if (bl.District_id_ofc != "0" && bl.District_id_ofc != null && bl.District_id_ofc != "" && bl.District_id_ofc != "Select")
            {
                MySqlParameter ba = new MySqlParameter("dstrict", bl.District_id_ofc);
                pm.Add(ba);
                where += "   and ofc.DistrictCodeNew = @dstrict   ";
            }
            if (bl.Base_department_id != "" && bl.Base_department_id != null && bl.Base_department_id != "0" && bl.Base_department_id != "Select")
            {
                MySqlParameter ca = new MySqlParameter("bs_dept", bl.Base_department_id);
                pm.Add(ca);
                where += " and ofc.BaseDeptCode = @bs_dept  ";
            }
            if (bl.Office_level_id != "0" && bl.Office_level_id != null && bl.Office_level_id != "" && bl.Office_level_id != "Select")
            {
                MySqlParameter da = new MySqlParameter("ofc_lvl", bl.Office_level_id);
                pm.Add(da);
                where += "  and ofc.OfficeLevel = @ofc_lvl   ";
            }
            if (bl.Office_category != "0" && bl.Office_category != null && bl.Office_category != "" && bl.Office_category != "Select")
            {
                MySqlParameter fa = new MySqlParameter("category", bl.Office_category);
                pm.Add(fa);
                where += "  and ofc.OfficeCategory=@category   ";
            }
            str = @"  select ofc.NewOfficeCode , dic.District_Name_En as district, bd.dept_name as basedept, ol.OfficeLevelName as ofclvl, 
    ofc.OfficeName as ofice, ofc.OfficeAddress as address, ofc.ContactNo as mobile, ofc.Email as email, ofc.OfficeURL as ofc_url
     from office ofc
  inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
   inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
   inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and ol.StateCode= ofc.StateCode  and ol.BaseDeptCode=ofc.BaseDeptCode" + where + "  order by ofc.NewOfficeCode asc";

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;
    }
    public ReturnClass.ReturnDataTable employee(bl_employee_action bl)
    {
        string str = @"select eom.office_mapping_id as map_id , eom.emp_code as empcode , et.Name_en as name from emp_office_mapping eom
inner join employee_table as et on et.emp_id= eom.emp_code 
inner join permission as pm on eom.office_mapping_id = pm.emp_map_id and pm.ActionPermission='FOR'
where eom.office_id=@ofcid and eom.designation_id=@desgid and eom.base_department_id=@bsdepid and
eom.district_id_ofc = @dist and eom.office_category= @ofc_cat  and eom.active='Y'";
        //and eom.office_mapping_id<> @eomp
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("ofcid",bl.Office_id),
             new MySqlParameter("desgid",bl.Designation),
              new MySqlParameter("bsdepid",bl.Base_department_id),
               new MySqlParameter("dist",bl.District_id_ofc),
                new MySqlParameter("ofc_cat",bl.Office_category),
                new MySqlParameter("eomp",bl.Office_mapping_id)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable bind_action_grid(bl_employee_action bl)
    {
        string str = @"select ra.rti_id,ra.action_id,ra.action_detail as detail,
date_format(ra.action_date,'%d/%M/%Y') as date,dl.DisplayName_en as status,et.Name_en as name,
ofc.OfficeName ,deg.Designation_Name, concat(  bd.dept_name,' - ', dt.District_Name_En ) as deptName,
af.fileName, af.action_fileID as fileid, af.FileDescription as f_desc
from rti_action ra
 inner join emp_office_mapping eom on eom.office_mapping_id=ra.officer_mapping_id 
 inner join employee_table et ON et.emp_id =eom.emp_code
 inner join designation	deg on deg.Designation_ID = eom.designation_id	
 inner join office ofc on ofc.NewOfficeCode= eom.office_id 
 left outer join forward fwd on fwd.action_id= ra.action_id and fwd.rti_id=ra.rti_id
 left outer join action_files af on af.action_id= ra.action_id and af.rti_id= ra.rti_id
 inner JOIN  ddl_list dl on dl.DDL_Name_Value= ra.dept_status
 inner join basedepartment bd on bd.dept_id = eom.base_department_id
 inner join districts dt on dt.District_ID=eom.district_id_ofc
 where ra.rti_id=@rtiid order by ra.action_id ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("rtiid",bl.Rti_id)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }


    public ReturnClass.ReturnDataTable select_rti_file(bl_employee_action bl)
    {
        string str = "select FileName, FileType, FileDescription, FileData from rti_data_for_applicant af where af.rti_id=@rtiid ";
        MySqlParameter[] pm = new MySqlParameter[]{
         new MySqlParameter("rtiid",bl.RTI_fileID)
        
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_Action_file(bl_employee_action bl)
    {
        string str = "select * from action_files af where af.rti_id=@rtiid and af.action_id=@actid and af.action_fileID= @actfileid";
        MySqlParameter[] pm = new MySqlParameter[]{
         new MySqlParameter("rtiid",bl.Rti_id),
         new MySqlParameter("actid",bl.Action_id),
         new MySqlParameter("actfileid",bl.RTI_fileID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable bind_grid_approve(bl_employee_action bl)
    {
        string str = @" select * from rti_status rs
                    inner join forward fo on  fo.rti_id= rs.rti_id
                inner join rti_action ra on ra.action_id= rs.action_id and ra.rti_id= rs.rti_id
 left outer join action_files af on af.action_id=rs.action_id and af.rti_id =rs.rti_id
where 1= 1 and fo.sendingMethod='oso' and rs.out_ofcr_eom<> '' and  rs.officer_maping_id =@ofcmapid";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("ofcmapid",bl.Office_mapping_id)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_detail_approve(bl_employee_action bl)
    {
        string str = @"select rs.rti_id as rti,rd.Applicant_Name_en as applicant, dll.DisplayName_en as type_applicant, rd.rti_Subject as subject_rti,
rd.rti_Details as detail_rti , rs.user_id as officeuser, rs.status as stats ,rs.out_ofcr_eom as reciever ,ra.action_id as actionid,
 date_format(ra.action_date,' %D-%M-%Y') as actiondate, ra.action_detail as detail_action ,fo.s_officeMapping_id as sender, af.action_fileID as fileid, et.Name_en as name_Rec, ofc.OfficeName as r_officename, bd.dept_name as r_depname, dic.District_Name_En as district, dl.DisplayName_en,et1.Name_en as name_sender
from rti_status rs 
inner join rti_action ra on ra.action_id=rs.action_id and ra.rti_id =rs.rti_id
inner join forward fo on fo.rti_id=rs.rti_id and fo.action_id= rs.action_id
left outer join action_files af on af.rti_id=rs.rti_id and af.action_id= rs.action_id
inner join emp_office_mapping eom on eom.office_mapping_id=rs.out_ofcr_eom
inner join office ofc on ofc.NewOfficeCode=eom.office_id
inner join basedepartment bd on bd.dept_id=eom.base_department_id
inner join districts dic on dic.District_ID= eom.district_id_ofc
inner join employee_table et on et.emp_id=eom.emp_code
inner join ddl_list dl on dl.DDL_Name_Value	= rs.`status`
inner join emp_office_mapping eom1 on eom1.office_mapping_id=fo.s_officeMapping_id
inner join employee_table et1 on et1.emp_id=eom1.emp_code
inner join rti_detail rd on rd.rti_id =rs.rti_id
inner join ddl_list dll on dll.DDL_Name_Value= rd.applicant_type
where rs.rti_id=@rtiid";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("rtiid",bl.Rti_id)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_permissions(bl_employee_action bl)
    {
        string where = "";
        string str1 = "select dl.DDL_Name_Value as value, dl.DisplayName_en as name from ddl_list dl where dl.DDL_Name_Value in (";
        foreach (string sr in bl.Para_string)
        {
            where = where + sr;
        }

        str1 = str1 + where + ") order by name asc ";
        rd = db.executeSelectQuery(str1);
        return rd;
    }
    public ReturnClass.ReturnDataTable get_employee_permissions(bl_employee_action bl)
    {
        string str = @" select ActionPermission as Value, DisplayName_en as Name  from permission as pm 
                        inner join ddl_list as dl on pm.ActionPermission = dl.DDL_Name_Value 
                        where pm.emp_map_id=@eom  and dl.Category='Permission'
                         order by DisplayOrder ";
        MySqlParameter[] pm = new MySqlParameter[] {
        new MySqlParameter("eom",bl.Office_mapping_id)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable Permission_auth(bl_employee_action bl)
    {
        string str1 = @"select et.emp_id as emp_id, Name_en as Name, eom.office_mapping_id as mapid   from employee_table as et 
inner join  emp_office_mapping as eom on eom.emp_code= et.emp_id and et.Active='Y'
inner join  permission as pm on pm.emp_map_id = eom.office_mapping_id
inner join  ddl_list as dl on  dl.DDL_Name_Value=pm.ActionPermission and dl.Category='Permission'
where eom.office_mapping_id!=@ofc_map_id and pm.ActionPermission=@action_permission ";
        MySqlParameter[] ps1 = new MySqlParameter[]
            {
                new MySqlParameter("ofc_map_id",bl.Office_mapping_id),
                new MySqlParameter("action_permission",bl.Permission)
            };
        rd = db.executeSelectQuery(str1, ps1);

        return rd;
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
}
