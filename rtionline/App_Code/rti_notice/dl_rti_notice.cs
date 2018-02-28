using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;


/// <summary>
/// Summary description for dl_rti_notice
/// </summary>
/// by ekta
public class dl_rti_notice
{

    //notice entry methods... Naveen


    public ReturnClass.ReturnDataTable Get_Count()
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();

        string str = @"  select count(*) as count_rti_dept_ofc_emp from rti_detail A inner join rti_status B on A.rti_id=B.rti_id where B.IsValid='Y' and B.Payment_status='Y'
                        union all
                        select count(dept_name) as count_dept from basedepartment 
                        union all
                        select count(*) as office_count from office ofc
                        inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
                        inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
                        inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and ol.StateCode= ofc.StateCode  and ol.BaseDeptCode=ofc.BaseDeptCode 
                        union all
                        select count(*) as officer_count from employee_table emp inner join emp_office_mapping eom on eom.emp_code=emp.emp_id where eom.designation_id='2'";


        rd = db.executeSelectQuery(str);
        return rd;


    }

    public ReturnClass.ReturnDataTable Get_Office_For_Notice(Notice_Entry bl)
    {
        string strquery = @"SELECT NewOfficeCode, OfficeName FROM office WHERE  
                           office.BaseDeptCode=@dept AND office.DistrictCodeNew=@district
                           order by OfficeName ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("dept",bl.Base_dept_id),
            new MySqlParameter("district",bl.District_id)
            
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(strquery, pm);
        return rd;
    }

    public ReturnClass.ReturnDataTable Get_Notice_Data1()
    {
        db_maria_connection db = new db_maria_connection();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();

        string qr = @"select nb.notice_id,nb.file_id,nb.Header,nb.Hyperlink,nb.File_Type,nb.active,nb.status,nb.bold,nb.blink,nb.priority,nb.image,nb.permanent,NOW(),

(CASE WHEN Validity_From IS NOT NULL OR Validity_From !='' THEN DATE_FORMAT(Validity_From,'%d/%m/%Y') ELSE 'Not Available' END)Validity_From,  
(CASE WHEN Validity_To IS NOT NULL OR Validity_To!='' THEN DATE_FORMAT(Validity_To,'%d/%m/%Y') ELSE 'Not Available' END)Validity_To, 
(CASE WHEN DATE_FORMAT(Publish_Date,'%d/%m/%Y') IS NULL OR DATE_FORMAT(Publish_Date,'%d/%m/%Y')='' THEN DATE_FORMAT(validity_from,'%d/%m/%Y') ELSE DATE_FORMAT(Publish_Date,'%d/%m/%Y') END)Publish_Date, 
(case when Permanent='Y' then 'Yes' when Permanent='N' then 'No' end)permanent,
 (case when color is null or color='' then '#000000' else color end)color,
 (case when url is null or url='' then 'admin/notice_download.aspx' else url end)url
 
 from notice_board nb
 left outer join notice_file_details fd on nb.file_id=fd.file_id  
 where active='A' and status='A'
 and  case when 
 permanent = 'N' then NOW() between validity_from AND Validity_To  else  NOW() >= Publish_Date end order by Publish_Date DESC limit 4";

        dt = db.executeSelectQuery(qr);
        return dt;
    }


    public ReturnClass.ReturnDataTable GetDataForDeptManualHandler(string File_ID)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @"select file_data, content_type as mime_type  from dept_user_manual_file where Dept_User_Manual_ID=@File_ID";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("File_ID",File_ID)
        };
        dt = db.executeSelectQuery(str, pm);
        return dt;

    }
    public ReturnClass.ReturnDataTable GetDataForHandler(string File_ID)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @"select file_data, mime_type  from notice_file_details where file_id=@File_ID";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("File_ID",File_ID)
        };
        dt = db.executeSelectQuery(str, pm);
        return dt;

    }

    public ReturnClass.ReturnDataTable Get_NoticeRecords_For_Edit(Notice_Entry bl)
    {

        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @" SELECT A.Notice_Id, Header, A.File_Id,  DATE_FORMAT(Validity_From,'%d/%m/%Y') as publishdate, 
                       ( CASE WHEN File_Header is NULL THEN 'No File' ELSE File_Header END ) as filename, mime_type, file_data, 
                       dist.District_Name as dist_name, dept.dept_name as dept_name, ofc.OfficeName as ofc_name 
                       FROM  notice_board A left join notice_file_details B on A.File_Id=B.file_id 
                       left join districts dist on A.district_id = dist.District_ID 
                       left join basedepartment dept on A.base_dept_id = dept.dept_id 
                       left join office ofc on A.office_id = ofc.NewOfficeCode and dept.dept_id= ofc.BaseDeptCode and dist.District_ID = ofc.DistrictCodeNew 
                       where 1 and 1 ";
        string end_where = "  ORDER BY Validity_From  DESC  ";
        string and_where = "";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();

            if (bl.NoticeID != null && bl.NoticeID != "")
            {
                MySqlParameter fa = new MySqlParameter("notice_id", "%" + bl.NoticeID + "%");
                pm.Add(fa);
                and_where += " and A.Notice_Id LIKE @notice_id ";

            }
            if (bl.Header != null && bl.Header != "")
            {
                MySqlParameter fa = new MySqlParameter("Header", "%" + bl.Header + "%");
                pm.Add(fa);
                and_where += " and Header LIKE  @Header  ";
            }
            if (bl.Validity_From != null && bl.Validity_From != "" && bl.Validity_To != null & bl.Validity_To != "")
            {
                MySqlParameter fa = new MySqlParameter("Start_Date", bl.Validity_From);
                pm.Add(fa);
                fa = new MySqlParameter("End_Date", bl.Validity_To);
                pm.Add(fa);
                and_where += " and ( Validity_From >=  @Start_Date  and Validity_To <=  @End_Date ) ";
            }
            else if (bl.Validity_From != null && bl.Validity_From != "" && (bl.Validity_To == null || bl.Validity_To == ""))
            {
                MySqlParameter fa = new MySqlParameter("Start_Date", bl.Validity_From);
                pm.Add(fa);
                fa = new MySqlParameter("NextDate", bl.Next_date);
                pm.Add(fa);
                and_where += " and startdate >= @Start_Date and  startdate < @NextDate ";
            }

            if (bl.KeywordSearch != null && bl.KeywordSearch != "")
            {
                MySqlParameter fa = new MySqlParameter("KeywordSearch", "%" + bl.KeywordSearch + "%");
                pm.Add(fa);
                and_where += " and Notice_keyword LIKE  @KeywordSearch ";
            }
            if (bl.District_id != "0")
            {
                MySqlParameter fa = new MySqlParameter("District_ID", bl.District_id);
                pm.Add(fa);
                and_where += " and A.district_id = @District_ID ";

            }
            if (bl.Base_dept_id != "0")
            {
                MySqlParameter fa = new MySqlParameter("Base_department_id", bl.Base_dept_id);
                pm.Add(fa);
                and_where += " and base_dept_id = @Base_department_id ";

            }
            if (bl.Office_id != "0")
            {
                MySqlParameter fa = new MySqlParameter("Office_id", bl.Office_id);
                pm.Add(fa);
                and_where += " and office_id = @Office_id ";

            }
            str = str + and_where + end_where;
            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");
        }
        return rd;


    }

    public ReturnClass.ReturnDataTable Get_DeptUserManual_File(Notice_Entry bl)
    {

        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @" SELECT A.Dept_User_Manual_ID, A.User_ID, file_name, content_type as mime_type, file_data, file_description ,Year_Issue,
                       dist.District_Name as dist_name, dept.dept_name as dept_name, ofc.OfficeName as ofc_name 
                       FROM  dept_user_manual_file A 
                       left join districts dist on A.district_id_ofc = dist.District_ID 
                       left join basedepartment dept on A.base_department_id = dept.dept_id 
                       left join office ofc on A.office_id = ofc.NewOfficeCode and dept.dept_id= ofc.BaseDeptCode and dist.District_ID = ofc.DistrictCodeNew 
                       where 1 and 1 ";
        string end_where = "  ORDER BY Year_Issue  DESC  ";
        string and_where = "";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
                                   
            if (bl.District_id != "0")
            {
                MySqlParameter fa = new MySqlParameter("District_ID", bl.District_id);
                pm.Add(fa);
                and_where += " and A.district_id_ofc = @District_ID ";

            }
            if (bl.Base_dept_id != "0")
            {
                MySqlParameter fa = new MySqlParameter("Base_department_id", bl.Base_dept_id);
                pm.Add(fa);
                and_where += " and base_department_id = @Base_department_id ";

            }
            if (bl.Office_id != "0")
            {
                MySqlParameter fa = new MySqlParameter("Office_id", bl.Office_id);
                pm.Add(fa);
                and_where += " and office_id = @Office_id ";

            }
            if ((bl.IssueYear != "0") && (bl.IssueYear !="" ) )
            {
                MySqlParameter fa = new MySqlParameter("issueYear", bl.IssueYear);
                pm.Add(fa);
                and_where += " and Year_Issue = @issueYear ";

            }

            str = str + and_where + end_where;
            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");
        }
        return rd;


    }



    public ReturnClass.ReturnDataTable Get_NoticeRecords(Notice_Entry bl)
    {

        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @" SELECT A.Notice_Id, Header, A.File_Id,  DATE_FORMAT(Validity_From,'%d/%m/%Y') as publishdate, 
                       ( CASE WHEN File_Header is NULL THEN 'No File' ELSE File_Header END ) as filename, mime_type, file_data, 
                       dist.District_Name as dist_name, dept.dept_name as dept_name, ofc.OfficeName as ofc_name 
                       FROM  notice_board A left join notice_file_details B on A.File_Id=B.file_id 
                       left join districts dist on A.district_id = dist.District_ID 
                       left join basedepartment dept on A.base_dept_id = dept.dept_id 
                       left join office ofc on A.office_id = ofc.NewOfficeCode and dept.dept_id= ofc.BaseDeptCode and dist.District_ID = ofc.DistrictCodeNew 
                       where 1 and 1 " ;
        string end_where = "  ORDER BY Validity_From  DESC  ";
        string and_where = "";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();

            if (bl.NoticeID != null && bl.NoticeID != "")
            {
                MySqlParameter fa = new MySqlParameter("notice_id", "%" + bl.NoticeID + "%");
                pm.Add(fa);
                and_where += " and A.Notice_Id LIKE @notice_id ";
                
            }
            if (bl.Header != null && bl.Header != "")
            {
                MySqlParameter fa = new MySqlParameter("Header", "%" + bl.Header + "%");
                pm.Add(fa);
                and_where += " and Header LIKE  @Header  ";
            }
            if (bl.Validity_From != null && bl.Validity_From != "" && bl.Validity_To != null & bl.Validity_To != "")
            {
                MySqlParameter fa = new MySqlParameter("Start_Date", bl.Validity_From);
                pm.Add(fa);
                fa = new MySqlParameter("End_Date", bl.Validity_To);
                pm.Add(fa);
                and_where += " and ( Validity_From >=  @Start_Date  and Validity_To <=  @End_Date ) ";
            }
            else if (bl.Validity_From != null && bl.Validity_From != "" && (bl.Validity_To == null || bl.Validity_To == ""))
            {
                MySqlParameter fa = new MySqlParameter("Start_Date", bl.Validity_From);
                pm.Add(fa);
                fa = new MySqlParameter("NextDate", bl.Next_date);
                pm.Add(fa);
                and_where += " and startdate >= @Start_Date and  startdate < @NextDate ";
            }

            if (bl.KeywordSearch != null && bl.KeywordSearch != "")
            {
                MySqlParameter fa = new MySqlParameter("KeywordSearch", "%" + bl.KeywordSearch + "%");
                pm.Add(fa);
                and_where += " and Notice_keyword LIKE  @KeywordSearch ";
            }
            if (bl.District_id != "0")
            {
                MySqlParameter fa = new MySqlParameter("District_ID", bl.District_id);
                pm.Add(fa);
                and_where += " and A.district_id = @District_ID ";

            }
            if (bl.Base_dept_id != "0")
            {
                MySqlParameter fa = new MySqlParameter("Base_department_id", bl.Base_dept_id);
                pm.Add(fa);
                and_where += " and base_dept_id = @Base_department_id ";

            }
            if (bl.Office_id != "0")
            {
                MySqlParameter fa = new MySqlParameter("Office_id", bl.Office_id);
                pm.Add(fa);
                and_where += " and office_id = @Office_id ";

            }
            str = str + and_where + end_where;
            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");
        }
        return rd;


    }

    public ReturnClass.ReturnBool Insert_Notice(Notice_Entry ne)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();

        MySqlParameter[] param = new MySqlParameter[]
        {
            new MySqlParameter("Header",ne.Header),
            new MySqlParameter("Pernament",ne.Permanent),
            new MySqlParameter("Publish_Date",ne.Publish_date),
            new MySqlParameter("Validity_From",ne.Datefrom),
            new MySqlParameter("Validity_To",ne.Dateto),
            new MySqlParameter("Active",ne.Active),
            new MySqlParameter("Hyperlink",ne.Hyperlink),
            new MySqlParameter("File_Type",ne.Filetype),
            new MySqlParameter("File_Id",ne.file_ID),
            new MySqlParameter("Url",ne.Url),
            new MySqlParameter("Client_Ip",ne.Client_ip),
            new MySqlParameter("User_Id",ne.User_id),
            new MySqlParameter("Date_Time",ne.Datetime),
            new MySqlParameter("Bold",ne.Bold),
           new MySqlParameter("Blink",ne.Blink),
           new MySqlParameter("Priority",ne.Priority),
            new MySqlParameter("Color",ne.Color),
            new MySqlParameter("Image",ne.Image),
             new MySqlParameter("status",ne.status),
             new MySqlParameter("district_id",ne.District_id),
              new MySqlParameter("office_id",ne.Office_id),
               new MySqlParameter("base_dept_id",ne.Base_dept_id)
               
        };

        string qur = @"insert into Notice_Board(Header,Permanent,Publish_Date,Validity_From,Validity_To,Active,Hyperlink,File_Type,File_Id,Url,Client_Ip,User_Id,Date_Time,
                        bold,blink,priority,color,image,status,district_id, base_dept_id, office_id) 
                        values(@Header,@Pernament,@Publish_Date,@Validity_From,@Validity_To,@Active,@Hyperlink,@File_Type,@File_Id,@Url,@Client_Ip,@User_Id,@Date_Time,
                         @Bold,@Blink,@Priority,@Color,@Image,@status,@district_id,@base_dept_id,@office_id)";
        rb = db.executeInsertQuery(qur, param);
        return rb;
    }

    public ReturnClass.ReturnBool Insert_FileDetails(Upload_doc ne)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();

        MySqlParameter[] param = new MySqlParameter[]
        {
            new MySqlParameter("Header",ne.Filename),
            new MySqlParameter("file_ext",ne.File_Extn),
            new MySqlParameter("file_data",ne.File_Data),
            new MySqlParameter("Client_Ip",ne.Client_ip),
            new MySqlParameter("User_Id",ne.User_id),
            new MySqlParameter("Date_Time",ne.Upload_Date)
        };

        string qur = "insert into notice_file_details(File_Header,mime_type,file_data,client_ip,user_code,upload_date) "
                                                + " values(@Header,@file_ext,@file_data,@Client_Ip,@User_Id,@Date_Time);";
        rb = db.executeInsertQuery(qur, param);

        return rb;
    }

    public ReturnClass.ReturnDataTable Get_Max_ID()
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string qur = "select MAX(file_id) from notice_file_details";
        dt = db.executeSelectQuery(qur);
        return dt;
    }

    public ReturnClass.ReturnDataTable File_preview(Upload_doc ur)
    {

        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string qr = "select file_id,File_Header,mime_type,file_data " +
                   " from notice_file_details where file_id=@file_id";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("file_id",ur.File_id)
            };
        dt = db.executeSelectQuery(qr, pm);
        return dt;
    }

    public ReturnClass.ReturnDataTable load_FilterData(Notice_Entry ne)
    {
        ReturnClass.ReturnDataTable dts = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        MySqlParameter[] param = new MySqlParameter[]
        {
           new MySqlParameter("Header","%"+ne.Header+"%"),
           new MySqlParameter("Validity_From",ne.Datefrom),
           new MySqlParameter("Validity_To",ne.Dateto),
            new MySqlParameter("district_id",ne.district_id),
             new MySqlParameter("active",ne.active),
              new MySqlParameter("status",ne.status)
           
        };

        string query = "", where = "";

        query = @"select nb.Notice_Id as Notice_Id,nb.File_Id as File_Id,nb.Header as Header,nb.bold,nb.blink,nb.priority,nb.color,nb.image,fd.mime_type as mime_type,fd.file_data as file_data,nb.district_id,
                    (CASE WHEN Validity_From IS NOT NULL OR Validity_From !='' THEN DATE_FORMAT(Validity_From,'%d/%m/%Y') ELSE 'Not Available' END)Validity_From,  
                    (CASE WHEN Validity_To IS NOT NULL OR Validity_To!='' THEN DATE_FORMAT(Validity_To,'%d/%m/%Y') ELSE 'Not Available' END)Validity_To, 
                    (CASE WHEN DATE_FORMAT(Publish_Date,'%d/%m/%Y') IS NULL OR DATE_FORMAT(Publish_Date,'%d/%m/%Y')='' THEN DATE_FORMAT(validity_from,'%d/%m/%Y') ELSE DATE_FORMAT(Publish_Date,'%d/%m/%Y') END)Publish_Date, 
                    (case when url is null or url='' then 'Not Available' else url end)url, 
                    (case when Permanent='Y' then 'Yes' when Permanent='N' then 'No' end)permanent, 
                    (case when hyperlink='Y' then 'Yes' when hyperlink='N' then 'No' end)hyperlink, 
                    (case when Active='A' then 'Active' when Active='D' then 'Inactive' end)Active, 
                    (case when file_type='I' then 'Internal' when file_type='E' then 'External' end)file_type 
                    from notice_board nb 
                    left outer join notice_file_details fd on nb.file_id=fd.file_id  
                    where 1=1";
        where += " and nb.district_id=@district_id   ";
        if (ne.Subject_status == "Y")
        {
            //where += "and Header like '%'+ @Header +'%'";
            where += " and nb.Header like @Header ";
        }
        else
        {

        }

        if (ne.Validity_status == "A")
        {
//            where += @" and (permanent = 'N' AND
//                          Date_Format(NOW(),'%d/%m/%Y') between Date_Format(validity_from,'%d/%m/%Y' ) AND Date_Format(Validity_To, '%d/%m/%Y')) 
//                          OR (permanent = 'Y' and (Date_Format(NOW(),'%d/%m/%Y') >= Date_Format(Publish_Date,'%d/%m/%Y'))) ";

            where += @" and case when 
					permanent = 'N' then NOW() between validity_from AND Validity_To  else  NOW() >= Publish_Date end";
        }
        else if (ne.Validity_status == "E")
        {
            //where += @" and (nb.permanent='N' and Date_Format(NOW(),'%d/%m/%Y') > Date_Format(Validity_To,'%d/%m/%Y'))";

            where += @" and nb.permanent='N' and NOW() > Validity_To";
        }


        if (ne.Fromto_status == "YY")
        {
            where += " and validity_from>=@Validity_From and validity_to<=@Validity_To ";
        }
        else { }

        if (ne.Fromto_status == "Y")
        {
            where += " and validity_from>=@Validity_From ";
        }
        else { }

        where += " order by publish_date DESC";

        query += where;

        dts = db.executeSelectQuery(query, param);
        return dts;
    }


    public ReturnClass.ReturnBool update_notice(Notice_Entry ne)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        //BA_layer ba = new BA_layer();

        MySqlParameter[] param = new MySqlParameter[] { 
            new MySqlParameter("Header",ne.Header),
            new MySqlParameter("Pernament",ne.Permanent),
            new MySqlParameter("Publish_Date",ne.Publish_date),
            new MySqlParameter("Validity_From",ne.Datefrom),
            new MySqlParameter("Validity_To",ne.Dateto),
            new MySqlParameter("Active",ne.Active),
            new MySqlParameter("Hyperlink",ne.Hyperlink),
            new MySqlParameter("File_Type",ne.Filetype),
            new MySqlParameter("File_Id",ne.file_ID),
            new MySqlParameter("Url",ne.Url),
            new MySqlParameter("Client_Ip",ne.Client_ip),
            new MySqlParameter("User_Id",ne.User_id),
            new MySqlParameter("Date_Time",ne.Datetime),
            new MySqlParameter("Notice_Id",ne.NoticeID),

             new MySqlParameter("bold",ne.Bold),
             new MySqlParameter("blink",ne.Blink),
             new MySqlParameter("priority",ne.Priority),
             new MySqlParameter("color",ne.Color),
             new MySqlParameter("image",ne.Image),
             new MySqlParameter("district_id",ne.District_id),
             new MySqlParameter("dept_id",ne.Base_dept_id),
             new MySqlParameter("ofc_id",ne.Office_id)


        };

        try
        {
            using (TransactionScope ts = new TransactionScope())
            {
                string qur = "insert into notice_board_log select * from Notice_Board where Notice_Id=@Notice_Id";
                rb = db.executeInsertQuery(qur, param);
                if (rb.status == true)
                {
                    string upqur = "update notice_board set Header=@Header,Permanent=@Pernament,Publish_Date=@Publish_Date,Validity_From=@Validity_From, "
                                            + " Validity_To=@Validity_To,Active=@Active,Hyperlink=@Hyperlink,File_Type=@File_Type, "
                                            + " Url=@Url,Client_Ip=@Client_Ip,User_Id=@User_Id,Date_Time=@Date_Time,bold=@bold,blink=@blink,priority=@priority,color=@color,image=@image,File_Id=@File_Id , "
                                            + " district_id=@district_id, base_dept_id=@dept_id, office_id=@ofc_id "
                                            + " where Notice_Id=@Notice_Id ";
                    rb = db.executeUpdateQuery(upqur, param);
                }
                if (rb.status == true)
                {
                    ts.Complete();
                }
            }
        }
        catch (Exception ex)
        {
            rb.status = false;
            Gen_Error_Rpt.Write_Error("Notice Edit Form - Update Method() :- " + ex.Message.ToString());
        }

        return rb;
    }

    public ReturnClass.ReturnBool update_file_details(Upload_doc ud)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        //BA_layer ba = new BA_layer();

        MySqlParameter[] param = new MySqlParameter[] { 
           new MySqlParameter("f_id",ud.file_ID),
           //new MySqlParameter("file_id",ud.file_id),

            new MySqlParameter("Header",ud.Filename),
            new MySqlParameter("mime_type",ud.File_Extn),
            new MySqlParameter("file_data",ud.File_Data),
            new MySqlParameter("Client_Ip",ud.Client_ip),
            new MySqlParameter("User_Id",ud.User_id),
            new MySqlParameter("Date_Time",ud.Upload_Date)
        };

        try
        {
            using (TransactionScope ts = new TransactionScope())
            {
                string qur = "insert into notice_file_details_log select * from notice_file_details where file_id=@f_id";
                rb = db.executeInsertQuery(qur, param);

                if (rb.status == true)
                {
                    if (ud.Status != "Y")
                    {
                        string delQUr = "delete from notice_file_details where file_id=@f_id";
                        rb = db.executeDeleteQuery(delQUr, param);
                    }
                    else if (ud.Status == "Y")
                    {
                        string upqur = "update notice_file_details set File_Header=@Header,mime_type=@mime_type,file_data=@file_data,"
                                                    + "client_ip=@Client_Ip,user_code=@User_Id,upload_date=@Date_Time "
                                                    + "where file_id=@f_id";
                        rb = db.executeUpdateQuery(upqur, param);
                    }
                }

                if (rb.status == true)
                {
                    ts.Complete();
                }
            }
        }
        catch (Exception ex)
        {
            rb.status = false;
            Gen_Error_Rpt.Write_Error("Notice Edit Form - Update Method() :- " + ex.Message.ToString());
        }

        return rb;
    }

    public ReturnClass.ReturnBool update_noticeID(Upload_doc ud)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        //BA_layer ba = new BA_layer();

        MySqlParameter[] param = new MySqlParameter[] {
            new MySqlParameter("Notice_Id",ud.noticeID),
            new MySqlParameter("File_Id",ud.File_id)
        };

        string upnotQur = "update notice_board set File_Id=@File_Id where Notice_Id=@Notice_Id";
        rb = db.executeUpdateQuery(upnotQur, param);

        return rb;
    }

    public ReturnClass.ReturnDataTable LoadNotice(Notice_Entry n)
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        MySqlParameter[] param = new MySqlParameter[] { 
           new MySqlParameter("noticeId",n.NoticeID)
            //new MySqlParameter("district_id",n.district_id)
        };
        string qur = @"select nb.notice_id,nb.district_id,nb.file_id,nb.header,nb.bold,nb.blink,nb.priority,nb.color,nb.image,fd.file_id,fd.File_Header,fd.file_data,fd.mime_type,fd.upload_date,
                              (case when DATE_FORMAT(Publish_Date,'%d/%m/%Y') is null or DATE_FORMAT(Publish_Date,'%d/%m/%Y')='' 
                              then DATE_FORMAT(validity_from,'%d/%m/%Y %h:%m:%s') else DATE_FORMAT(Publish_Date,'%d/%m/%Y') end)Publish_Date, 
                              (case when Validity_From is not null or Validity_From!='' then DATE_FORMAT(Validity_From,'%d/%m/%Y') 
                              else 'Not Available' end)Validity_From, 
                              (case when Validity_To is not null or Validity_To!='' then DATE_FORMAT(Validity_To,'%d/%m/%Y') 
                              else 'Not Available' end)Validity_To, 
                              (case when url is null or url='' then 'Not Available' else url end)url, 
                              (case when Permanent='Y' then 'Yes' when Permanent='N' then 'No' end)permanent, 
                              (case when hyperlink='Y' then 'Yes' when hyperlink='N' then 'No' end)hyperlink, 
                              (case when Active='A' then 'Active' when Active='D' then 'Inactive' end)active, 
                              (case when file_type='I' then 'Internal' when file_type='E' then 'External' 
                              when file_type is null or file_type='' then 'Not Available' end)file_type , nb.district_id, nb.base_dept_id, nb.office_id
                              from notice_board nb
                              left outer join notice_file_details fd on fd.file_id=nb.File_Id
                              where Notice_Id=@noticeId ";

        dt = db.executeSelectQuery(qur, param);
        return dt;
    }
    public ReturnClass.ReturnDataTable select_file_details(Notice_Entry n)
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        MySqlParameter[] param = new MySqlParameter[] { 
           new MySqlParameter("file_id",n.file_id)
        };
        string qur = @"select file_id,File_Header,mime_type,file_data,upload_date from notice_file_details
                       where file_id=@file_id";

        dt = db.executeSelectQuery(qur, param);
        return dt;
    }
    public ReturnClass.ReturnDataTable bindimage()
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string qr = "select image as name,image_id as value from notice_image";


        dt = db.executeSelectQuery(qr);
        return dt;
    }

    public ReturnClass.ReturnDataTable load_FilterData_preview(Notice_Entry ne)
    {
        ReturnClass.ReturnDataTable dts = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        MySqlParameter[] param = new MySqlParameter[]
        {
           new MySqlParameter("user_id",ne.User_id),
           
        };

        string query = "", where = "";

        query = @"select nb.Notice_Id as Notice_Id,nb.File_Id as File_Id,nb.Header as Header,nb.bold,nb.blink,nb.priority,nb.color,nb.image,fd.mime_type as mime_type,fd.file_data as file_data, 
                    (CASE WHEN Validity_From IS NOT NULL OR Validity_From !='' THEN DATE_FORMAT(Validity_From,'%d/%m/%Y') ELSE 'Not Available' END)Validity_From,  
                    (CASE WHEN Validity_To IS NOT NULL OR Validity_To!='' THEN DATE_FORMAT(Validity_To,'%d/%m/%Y') ELSE 'Not Available' END)Validity_To, 
                    (CASE WHEN DATE_FORMAT(Publish_Date,'%d/%m/%Y') IS NULL OR DATE_FORMAT(Publish_Date,'%d/%m/%Y')='' THEN DATE_FORMAT(validity_from,'%d/%m/%Y') ELSE DATE_FORMAT(Publish_Date,'%d/%m/%Y') END)Publish_Date, 
                    (case when url is null or url='' then 'Not Available' else url end)url, 
                    (case when Permanent='Y' then 'Yes' when Permanent='N' then 'No' end)permanent, 
                    (case when hyperlink='Y' then 'Yes' when hyperlink='N' then 'No' end)hyperlink, 
                    (case when Active='A' then 'Active' when Active='D' then 'Inactive' end)Active, 
                    (case when file_type='I' then 'Internal' when file_type='E' then 'External' end)file_type 
                    from notice_board nb 
                    left outer join notice_file_details fd on nb.file_id=fd.file_id where 1=1
                    ";
        where += " order by Header DESC";

        query += where;

        dts = db.executeSelectQuery(query, param);
        return dts;
    }
     //-------------------------pending for approval page---------------------------------------------------------------------//
    public ReturnClass.ReturnBool delete_notice_and_file(Notice_Entry ne)
    {

        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();

        using (TransactionScope transScope = new TransactionScope())
        {

            string query = "insert into notice_board_log select * from Notice_Board where Notice_Id=@NoticeID";

            MySqlParameter[] pm = new MySqlParameter[]{
                           new MySqlParameter("NoticeID",ne.NoticeID)
                };


            rb = db.executeInsertQuery(query, pm);
            if (rb.status == true)
            {

                string query2 = @"delete from Notice_Board where Notice_Id=@NoticeID ";
                MySqlParameter[] pm2 = new MySqlParameter[]{
                           new MySqlParameter("NoticeID",ne.NoticeID)
                          // new MySqlParameter("district_id",ne.district_id)
                };
                rb = db.executeDeleteQuery(query2, pm2);
            }
            if (ne.file_id == null)
            { }
            else
            {
                if (rb.status == true)
                {
                    string query3 = "insert into notice_file_details_log select * from notice_file_details where file_id=@file_id";
                    MySqlParameter[] pm3 = new MySqlParameter[] { 
                         new MySqlParameter("file_id",ne.file_id)
                         };
                    rb = db.executeInsertQuery(query3, pm3);

                }
                if (rb.status == true)
                {
                    string query4 = "delete from notice_file_details where file_id=@file_id";

                    MySqlParameter[] pm4 = new MySqlParameter[]{
                    new MySqlParameter("file_id",ne.file_id)
                      };
                    rb = db.executeInsertQuery(query4, pm4);
                }
            }

            if (rb.status == true)
            {
                transScope.Complete();
            }
        }

        return rb;

    }
    public ReturnClass.ReturnBool approve_notice(Notice_Entry ne)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        using (TransactionScope ts = new TransactionScope())
        {
            string qr1 = @"insert into notice_board_log select * from Notice_Board where Notice_Id=@NoticeID";
            MySqlParameter[] pm = new MySqlParameter[]{
                           new MySqlParameter("NoticeID",ne.NoticeID)
                };

            rb = db.executeInsertQuery(qr1, pm);

            if (rb.status == true)
            {
                string qr = " UPDATE Notice_Board SET status=@status where  Notice_Id=@NoticeID  ";
                MySqlParameter[] pm1 = new MySqlParameter[]{
                           new MySqlParameter("NoticeID",ne.NoticeID),
                           new MySqlParameter("status",ne.status)
                          //  new MySqlParameter("district_id",ne.district_id)
                };
                rb = db.executeUpdateQuery(qr, pm1);
                ts.Complete();
            }
        }

        return rb;

    }
    public ReturnClass.ReturnDataTable pending_for_approval(Notice_Entry ne)
    {
        ReturnClass.ReturnDataTable dts = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        MySqlParameter[] param = new MySqlParameter[]
        {
           new MySqlParameter("Pending",ne.Pending)
           //new MySqlParameter("district_id",ne.district_id)

        };

        string query = "";

        query = @"select nb.status,nb.Notice_Id as Notice_Id,nb.File_Id as File_Id,nb.Header as Header,nb.bold,nb.blink,nb.priority,nb.color,nb.image,fd.mime_type as mime_type,fd.file_data as file_data, 
                    (CASE WHEN Validity_From IS NOT NULL OR Validity_From !='' THEN DATE_FORMAT(Validity_From,'%d/%m/%Y') ELSE 'Not Available' END)Validity_From,  
                    (CASE WHEN Validity_To IS NOT NULL OR Validity_To!='' THEN DATE_FORMAT(Validity_To,'%d/%m/%Y') ELSE 'Not Available' END)Validity_To, 
                    (CASE WHEN DATE_FORMAT(Publish_Date,'%d/%m/%Y') IS NULL OR DATE_FORMAT(Publish_Date,'%d/%m/%Y')='' THEN DATE_FORMAT(validity_from,'%d/%m/%Y') ELSE DATE_FORMAT(Publish_Date,'%d/%m/%Y') END)Publish_Date, 
                    (case when url is null or url='' then 'Not Available' else url end)url, 
                    (case when Permanent='Y' then 'Yes' when Permanent='N' then 'No' end)permanent, 
                    (case when hyperlink='Y' then 'Yes' when hyperlink='N' then 'No' end)hyperlink, 
                    (case when Active='A' then 'Active' when Active='D' then 'Inactive' end)Active, 
                    (case when file_type='I' then 'Internal' when file_type='E' then 'External' end)file_type 
                    from notice_board nb 
                    left outer join notice_file_details fd on nb.file_id=fd.file_id where status=@Pending ";

        dts = db.executeSelectQuery(query, param);
        return dts;
    }
    //------------------------- district dmf home page-------24 april-2017----------------------------------------------------//
//    public ReturnClass.ReturnDataTable Call_index(bl_home ur)
//    {
//        db_maria_connection db = new db_maria_connection();
//        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
//        MySqlParameter[] pm = new MySqlParameter[]
//        {
//           new MySqlParameter("District_id",ur.District_id)
//        };

////        string qr = @"select notice_id,file_id,Header,Hyperlink,File_Type,active,status,bold,blink,priority,image,permanent,NOW(),
////
////(CASE WHEN Validity_From IS NOT NULL OR Validity_From !='' THEN DATE_FORMAT(Validity_From,'%d/%m/%Y') ELSE 'Not Available' END)Validity_From,  
////(CASE WHEN Validity_To IS NOT NULL OR Validity_To!='' THEN DATE_FORMAT(Validity_To,'%d/%m/%Y') ELSE 'Not Available' END)Validity_To, 
////(CASE WHEN DATE_FORMAT(Publish_Date,'%d/%m/%Y') IS NULL OR DATE_FORMAT(Publish_Date,'%d/%m/%Y')='' THEN DATE_FORMAT(validity_from,'%d/%m/%Y') ELSE DATE_FORMAT(Publish_Date,'%d/%m/%Y') END)Publish_Date, 
////(case when Permanent='Y' then 'Yes' when Permanent='N' then 'No' end)permanent,
//// (case when color is null or color='' then '#000000' else color end)color,
//// (case when url is null or url='' then '../user/notice_download.aspx' else url end)url
//// 
//// from notice_board where active='A' and status='A' and district_id=@District_id
//// and  case when 
//// permanent = 'N' then NOW() between validity_from AND Validity_To  else  NOW() >= Publish_Date end";

//        string qr = @"select nb.notice_id,nb.file_id,nb.Header,nb.Hyperlink,nb.File_Type,nb.active,nb.status,nb.bold,nb.blink,nb.priority,nb.image,nb.permanent,NOW(),
//
//(CASE WHEN Validity_From IS NOT NULL OR Validity_From !='' THEN DATE_FORMAT(Validity_From,'%d/%m/%Y') ELSE 'Not Available' END)Validity_From,  
//(CASE WHEN Validity_To IS NOT NULL OR Validity_To!='' THEN DATE_FORMAT(Validity_To,'%d/%m/%Y') ELSE 'Not Available' END)Validity_To, 
//(CASE WHEN DATE_FORMAT(Publish_Date,'%d/%m/%Y') IS NULL OR DATE_FORMAT(Publish_Date,'%d/%m/%Y')='' THEN DATE_FORMAT(validity_from,'%d/%m/%Y') ELSE DATE_FORMAT(Publish_Date,'%d/%m/%Y') END)Publish_Date, 
//(case when Permanent='Y' then 'Yes' when Permanent='N' then 'No' end)permanent,
// (case when color is null or color='' then '#000000' else color end)color,
// (case when url is null or url='' then '../user/notice_download.aspx' else url end)url
// 
// from notice_board nb
// left outer join notice_file_details fd on nb.file_id=fd.file_id  
// where active='A' and status='A' and district_id=@District_id
// and  case when 
// permanent = 'N' then NOW() between validity_from AND Validity_To  else  NOW() >= Publish_Date end limit 10";
       
//        dt = db.executeSelectQuery(qr,pm);
//        return dt;
//    }
    //-------------------------image entry form-------------------------------------------------------------------------------//
    public ReturnClass.ReturnBool Insert_notice_image(Upload_doc bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();

        MySqlParameter[] param = new MySqlParameter[]
        {
            new MySqlParameter("image",bl.filename),
            new MySqlParameter("user_id",bl.user_id),
            new MySqlParameter("district_id",bl.district_id)
             
        };

        string qur = "insert into notice_image(image,user_id,district_id) "
                                                + " values(@image,@user_id,@district_id);";
        rb = db.executeInsertQuery(qur, param);

        return rb;
    }

     
   
   
     
}