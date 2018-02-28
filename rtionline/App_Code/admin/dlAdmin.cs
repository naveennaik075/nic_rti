using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;
/// <summary>
/// Summary description for dlAdmin
/// </summary>
public class dlAdmin
{
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    db_maria_connection db = new db_maria_connection();
    public ReturnClass.ReturnDataTable countEmp(bl_Dio bl)
    {
        string str = "", where = "   where 1 = 1   ";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
            if (bl.State != "" && bl.State != null && bl.State != "0" && bl.State != "Select")
            {
                MySqlParameter da = new MySqlParameter("state", bl.State);
                pm.Add(da);
                where += "   and state_id= @state  ";
            }
            if (bl.District != "0" && bl.District != null && bl.District != "" && bl.District != "Select")
            {
                MySqlParameter ba = new MySqlParameter("district", bl.District);
                pm.Add(ba);
                where += "   and district_id = @district   ";
            }
            if (bl.Role == "5")
            {
                MySqlParameter ba = new MySqlParameter("ofc_code", bl.Officeid);
                pm.Add(ba);
                where += "   and NewOfficeCode = @ofc_code   ";
            }
                             
           
            str = @" select count(Name_en) as empcount from employee_table " + where ;

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;
        
    }
    public ReturnClass.ReturnDataTable countDepartment(blAdmin bl)
    {
        string str = "select count(dept_name) as depcount from basedepartment";
        rd = db.executeSelectQuery(str);
        return rd;
    }

    public ReturnClass.ReturnDataTable countOffice(bl_Dio bl)
    {
        string str = "", where = "   where 1 = 1   ";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
            if (bl.State != "" && bl.State != null && bl.State != "0" && bl.State != "Select")
            {
                MySqlParameter da = new MySqlParameter("state", bl.State);
                pm.Add(da);
                where += "   and ofc.StateCode= @state  ";
            }
            if (bl.District != "0" && bl.District != null && bl.District != "" && bl.District != "Select")
            {
                MySqlParameter ba = new MySqlParameter("district", bl.District);
                pm.Add(ba);
                where += "   and ofc.DistrictCodeNew = @district   ";
            }
            if (bl.Role == "4" )
            {
                MySqlParameter ba = new MySqlParameter("officeLevelType", bl.Officelevelcode);
                pm.Add(ba);
                where += "   and ol.OfficeLevelType = @officeLevelType   ";
            }

            if ( bl.Role == "5")
            {
                MySqlParameter ba = new MySqlParameter("officeid", bl.Officeid);
                pm.Add(ba);
                where += "   and ofc.NewOfficeCode = @officeid   ";
            }

            str = @" select count(*) as office_count from office ofc
                       inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
                       inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
                       inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and ol.StateCode= ofc.StateCode  and ol.BaseDeptCode=ofc.BaseDeptCode " 
                       + where;

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;

        //string str = "select count(NewOfficeCode) as office_count from office";
//        string str = @"select count(*) as office_count from office ofc
//                       inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
//                       inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
//                       inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and ol.StateCode= ofc.StateCode  and ol.BaseDeptCode=ofc.BaseDeptCode";
//        rd = db.executeSelectQuery(str);
//        return rd;
    }
    public ReturnClass.ReturnDataTable countMappedEmp(bl_Dio bl)
    {
        string str = "", where = "   where 1 = 1   ";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
           
            if (bl.District != "0" && bl.District != null && bl.District != "" && bl.District != "Select")
            {
                MySqlParameter ba = new MySqlParameter("district", bl.District);
                pm.Add(ba);
                where += "   and district_id_ofc = @district   ";
            }

            if (bl.Role == "4" )
            {
                MySqlParameter ba = new MySqlParameter("officeLevelType", bl.Officelevelcode);
                pm.Add(ba);
                where += "   and ol.OfficeLevelType = @officeLevelType   ";
            }

            if ( bl.Role == "5")
            {
                MySqlParameter ba = new MySqlParameter("office_id", bl.Officeid);
                pm.Add(ba);
                where += "   and ofc.NewOfficeCode = @office_id   ";
            }
            //str = @" select count(office_mapping_id) as office_map_count from emp_office_mapping "  + where;
            str = @"select  count(office_mapping_id) as office_map_count from emp_office_mapping as emp_map
                    inner  join office ofc on ofc.NewOfficeCode = emp_map.office_id
                    inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
                    inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
                    inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and
                    ol.StateCode= ofc.StateCode  and ol.BaseDeptCode=ofc.BaseDeptCode  " + where;
            //  emp_map.district_id_ofc='11' and ol.OfficeLevelType='00'  "
            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;
        //string str = "select count(office_mapping_id) as office_map_count from emp_office_mapping";
        //rd = db.executeSelectQuery(str);
        //return rd;
    }

    public ReturnClass.ReturnDataTable GetTotalEmployeeInGrid(bl_Dio bl)
    {
        string str = "", where = "  where 1 = 1   "; 
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
            if (bl.State != "" && bl.State != null && bl.State != "0")
            {
                MySqlParameter da = new MySqlParameter("state", bl.State);
                pm.Add(da);
                where += "   and emp.state_id= @state  ";
            }
            if (bl.District != "0" && bl.District != null && bl.District != "" )
            {
                MySqlParameter ba = new MySqlParameter("district", bl.District);
                pm.Add(ba);
                where += "   and emp.district_id = @district   ";
            }

            if (bl.Role == "5")
            {
                MySqlParameter ba = new MySqlParameter("ofc_code", bl.Officeid);
                pm.Add(ba);
                where += "   and emp.NewOfficeCode = @ofc_code   ";
            }

            str = @"  select emp.emp_id as id, emp.Name_en as empname,emp.mobile_no as mblname, dic.District_Name as dist, bd.dept_name as depnm,  ofc.OfficeName as office, emp.email_id as email,
  st.state_name_e as state      from employee_table emp         
  left join office ofc on ofc.DistrictCodeNew=emp.district_id and ofc.BaseDeptCode= emp.base_department_id and ofc.StateCode = emp.state_id and ofc.NewOfficeCode=emp.NewOfficeCode       left join districts dic on dic.StateCode = emp.state_id and dic.District_ID = emp.district_id      left join basedepartment bd on bd.dept_id = emp.base_department_id                  
    left join state st on st.state_id=emp.state_id
	  " + where + " order by emp.emp_id ";

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;
    }

    public ReturnClass.ReturnDataTable GetTotalEmpMapInGrid(bl_Dio bl)
    {

        string str = "", where = "  where 1 = 1   "; 
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
           
            if (bl.District != "0" && bl.District != null && bl.District != "")
            {
                MySqlParameter ba = new MySqlParameter("district", bl.District);
                pm.Add(ba);
                where += "   and A.district_id_ofc = @district   ";
            }
            if (bl.Role == "4")
            {
                MySqlParameter ba = new MySqlParameter("officeLevelType", bl.Officelevelcode);
                pm.Add(ba);
                where += "   and E.OfficeLevelType = @officeLevelType   ";
            }
            if (bl.Role == "5")
            {
                MySqlParameter ba = new MySqlParameter("office_id", bl.Officeid);
                pm.Add(ba);
                where += "   and C.NewOfficeCode = @office_id   ";
            }

            str = @"select distinct A.office_mapping_id as mapping_id, A.emp_code as EmployeeCode, B.Name_en as EmpName, C.OfficeName as officeName,
                        D.dept_name as DeptName , E.OfficeLevelName as OfficeLevelName , F.District_Name_En as DistName_En ,
                        G.OfficeCategoryName as OfficeCategoryName, A.user_id as UserID , H.DisplayName_en as ChangeTypeName, IF(A.active = 'Y', 'YES', 'No') as activeStatus
                        from emp_office_mapping as A  
                    inner join employee_table as B on A.emp_code = B.emp_id  
                    inner join office as C on A.office_id = C.NewOfficeCode
                    inner join basedepartment as D on A.base_department_id = D.dept_id
                    inner join officelevel as E on A.office_level_id = E.OfficeLevelCode and A.base_department_id=E.BaseDeptCode and E.StateCode = C.StateCode
                    inner join districts as F on A.district_id_ofc = F.District_ID
                    inner join officecategory as G on A.office_category = G.OfficeCategoryCode
                    inner join ddl_list as H on A.charge_type = H.DDL_Name_Value " + where +  " order by A.office_mapping_id ";

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;
    }

    public ReturnClass.ReturnDataTable GetTotalDepartmentInGrid()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = "";
        try
        {
            str = @"select dept_id, dept_name from basedepartment order by dept_id";

            rt = db.executeSelectQuery(str);
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rt;
    }

    public ReturnClass.ReturnDataTable GetTotalOfficeInGrid(bl_Dio bl)
    {
        string str = "", where = "  where 1 = 1   "; 
        try
        {


            List<MySqlParameter> pm = new List<MySqlParameter>();
            if (bl.State != "" && bl.State != null && bl.State != "0" && bl.State != "Select")
            {
                MySqlParameter da = new MySqlParameter("state", bl.State);
                pm.Add(da);
                where += "   and ofc.StateCode= @state  ";
            }
            if (bl.District != "0" && bl.District != null && bl.District != "" && bl.District != "Select")
            {
                MySqlParameter ba = new MySqlParameter("district", bl.District);
                pm.Add(ba);
                where += "   and ofc.DistrictCodeNew = @district   ";
            }
            if (bl.Role == "4")
            {
                MySqlParameter ba = new MySqlParameter("officeLevelType", bl.Officelevelcode);
                pm.Add(ba);
                where += "   and ol.OfficeLevelType = @officeLevelType   ";
            }
            if (bl.Role == "5")
            {
                MySqlParameter ba = new MySqlParameter("officeid", bl.Officeid);
                pm.Add(ba);
                where += "   and ofc.NewOfficeCode = @officeid   ";
            }


            str = @"select ofc.NewOfficeCode , dic.District_Name_En as district, bd.dept_name as basedept, ol.OfficeLevelName as ofclvl, 
                     ofc.OfficeName as office, ofc.OfficeAddress as address, ofc.ContactNo as mobile, ofc.Email as email, ofc.OfficeURL as ofc_url
                    from office ofc
                     inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
                     inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
                     inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and ol.StateCode= ofc.StateCode  and ol.BaseDeptCode=ofc.BaseDeptCode "
                + where + "  order by ofc.NewOfficeCode asc ";

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;
    }

    public ReturnClass.ReturnBool Insert_DeptUserManual(blAdmin ur)
    {
        db_maria_connection db = new db_maria_connection();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        string strQuery = @" INSERT INTO dept_user_manual_file
                          (User_ID,base_department_id,office_id,office_level_id,district_id_ofc,office_category,Client_OS,ClientBrowser,UserAgent,Client_IP,file_name,content_type,
                           file_data,file_description, Year_Issue)
                           VALUES(@User_ID,@base_department_id,@office_id,@office_level_id,@district_id_ofc,@office_category,@Client_OS,@ClientBrowser,
                                  @UserAgent,@Client_IP,@file_name,@content_type, @file_data, @file_description, @Year_Issue ) ";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("User_ID",ur.UserID),
                new MySqlParameter("base_department_id",ur.Base_department_id),
                new MySqlParameter("office_id",ur.Office_id),
                new MySqlParameter("office_level_id",ur.Office_level_id),
                new MySqlParameter("district_id_ofc",ur.District_id_ofc ),
                new MySqlParameter("office_category",ur.Office_category ),
                new MySqlParameter("Client_OS",ur.ClientOS),
                new MySqlParameter("ClientBrowser",ur.ClientBrowser),
                new MySqlParameter("UserAgent",ur.Useragent),
                new MySqlParameter("Client_IP",ur.Client_ip),
                new MySqlParameter("file_name",ur.FileName),
                new MySqlParameter("content_type",ur.ContentType),
                new MySqlParameter("file_data",ur.FileData) ,
                new MySqlParameter("file_description",ur.FileDescription),
                new MySqlParameter("Year_Issue",ur.Year_issue)
                
              };



        rb = db.executeInsertQuery(strQuery, pm);


        return rb;
    }



} // End of Class