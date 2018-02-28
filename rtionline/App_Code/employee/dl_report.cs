using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;


public class dl_report
{
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    db_maria_connection db = new db_maria_connection();
    public ReturnClass.ReturnDataTable state_code1()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select state_id,state_name_e from state order by state_name_e";
        rt = db.executeSelectQuery(st);
        return rt;


    }
    public ReturnClass.ReturnDataTable select_admin_info(bl_report bl)
    {
        string str = @"select lo.UserID, emp.Name_en, emp.state_id,emp.base_department_id, emp.NewOfficeCode, emp.district_id,lo.RollID from login lo
inner join employee_table emp on emp.emp_id=lo.LoginID
 where lo.UserID=@user";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("user",bl.User_id)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;

    }
    public ReturnClass.ReturnDataTable countDepartment(bl_report bl)
    {
        string str = "select count(dept_name) as depcount from basedepartment";
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable countOffice(bl_report bl)
    {
        string str = "", where = "   where 1 = 1   ";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
            if (bl.State_id != "" && bl.State_id != null && bl.State_id != "0" && bl.State_id != "Select")
            {
                MySqlParameter da = new MySqlParameter("state", bl.State_id);
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

      
    }
    public ReturnClass.ReturnDataTable countOfficer(bl_report bl)
    {

        string str = @" select count(*) as officer_count from employee_table emp inner join emp_office_mapping eom on eom.emp_code=emp.emp_id where eom.designation_id='2'";


        rd = db.executeSelectQuery(str);



        return rd;


    }
    public ReturnClass.ReturnDataTable delete_action(bl_report bl)
    {
        string str = "delete FROM role_table where Role_id=@Role_id";
        MySqlParameter[] pm = new MySqlParameter[]{
             new MySqlParameter("Role_id",bl.Role)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_role()
    {
        string str = "select * from role_table";
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public string Max_office_code(bl_report bl)
    {
        string oficecode;
        string str = "select max(substring(ofc.NewOfficeCode,7,10) )as max  from office ofc where ofc.DistrictCodeNew=@district";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("district",bl.District)

        };
        rd = db.executeSelectQuery(str, pm);
        if (rd.table.Rows.Count > 0)
        {
            int code = Convert.ToInt32(rd.table.Rows[0]["max"]);
            code = code + 1;
            string code1 = code.ToString("0000");
            oficecode = bl.State + bl.District + bl.Office_level_id + code1;
        }
        else
        {
            oficecode = bl.State + bl.District + bl.Office_level_id + "0001";
        }
        return oficecode;
    }
    public ReturnClass.ReturnDataTable category(bl_report bl)
    {
        string str = "select oc.OfficeCategoryCode as code , oc.OfficeCategoryName as name from  officecategory oc";
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnBool insert_office(bl_report bl)
    {
        string str = @"insert into office 
(NewOfficeCode,DistrictCodeNew,officelevel,OfficeName,BaseDeptCode,countrycode,
Statecode,Officeaddress,contactno,email,fax,Officeurl,OfficeCategory)
values
(@NewOfficeCode,@DistrictCodeNew,@officelevel,@OfficeName,@BaseDeptCode,@countrycode,
@Statecode,@Officeaddress,@contactno,@email,@fax,@Officeurl,@OfficeCategory)";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("NewOfficeCode", bl.Officeid),
        new MySqlParameter("DistrictCodeNew",bl.District),
        new MySqlParameter("officelevel",bl.Office_level_id),
        new MySqlParameter("OfficeName",bl.Office_id),
        new MySqlParameter("BaseDeptCode",bl.Base_department),
        new MySqlParameter("countrycode",bl.Country),
        new MySqlParameter("Statecode",bl.State),
        new MySqlParameter("Officeaddress",bl.Address),
        new MySqlParameter("contactno",bl.Contact),
        new MySqlParameter("email",bl.Email_id),
        new MySqlParameter("fax",bl.Fax),
        new MySqlParameter("Officeurl",bl.Url),
          new MySqlParameter("OfficeCategory",bl.Category)
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }

    public ReturnClass.ReturnDataTable office_level(bl_report bl)
    {
        string str = "select ol.OfficeLevelName as Office_level, ol.OfficeLevelCode as olc from officelevel ol   ";
        string where = " where 1 = 1";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
           
            MySqlParameter fa = new MySqlParameter("BaseDeptCode", bl.Base_department);
            pm.Add(fa);
            where += " and ol.BaseDeptCode=@BaseDeptCode";
           
            if (bl.State != "" && bl.State != null && bl.State != "0" && bl.State != "Select")
            {
                MySqlParameter da = new MySqlParameter("state", bl.State);
                pm.Add(da);
                where += "  and ol.StateCode= @state ";
            }
            if (bl.Role == "4")
            {
                MySqlParameter ca = new MySqlParameter("level", bl.Officelevelcode);
                pm.Add(ca);
                where += " and OfficeLevelType=@level ";
            }
            str = str + where + " order by Office_level asc";
            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");
        }
        return rd;
    }
    public ReturnClass.ReturnDataTable countEmp(bl_report bl)
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


            str = @" select count(Name_en) as empcount from employee_table " + where;

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;

    }
    public ReturnClass.ReturnDataTable Bind_grid(bl_report bl)
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
                MySqlParameter ba = new MySqlParameter("dstrict", bl.District);
                pm.Add(ba);
                where += "   and ofc.DistrictCodeNew = @dstrict   ";
            }
            if (bl.Base_department != "" && bl.Base_department != null && bl.Base_department != "0" && bl.Base_department != "Select")
            {
                MySqlParameter ca = new MySqlParameter("bs_dept", bl.Base_department);
                pm.Add(ca);
                where += " and ofc.BaseDeptCode = @bs_dept  ";
            }
            if (bl.Office_level_id != "0" && bl.Office_level_id != null && bl.Office_level_id != "" && bl.Office_level_id != "Select")
            {
                MySqlParameter da = new MySqlParameter("ofc_lvl", bl.Office_level_id);
                pm.Add(da);
                where += "  and ofc.OfficeLevel = @ofc_lvl   ";
            }
            if (bl.Role == "4")
            {
                MySqlParameter ea = new MySqlParameter("level", bl.Officelevelcode);
                pm.Add(ea);
                where += "  and ol.OfficeLevelType=@level   ";
            }
            if (bl.Category != "0" && bl.Category != null && bl.Category != "" && bl.Category != "Select")
            {
                MySqlParameter fa = new MySqlParameter("category", bl.Category);
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
    public ReturnClass.ReturnDataTable base_department(bl_report bl)
    {
        string str = "select dept_id, dept_name from basedepartment order by dept_name asc";
        rd = db.executeSelectQuery(str);
        return rd;

    }
    public ReturnClass.ReturnDataTable district(bl_report bl)
    {
        string str = "select ds.District_ID, ds.District_Name from districts ds where ds.StateCode=@stateid order by ds.District_Name asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("stateid",bl.State)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public string role()
    {
        int id;
        string id1 = "", str = "select max(rt.Role_id) as id from role_table rt";
        rd = db.executeSelectQuery(str);
        if (rd.table.Rows.Count > 0)
        {
            id = Convert.ToInt32(rd.table.Rows[0]["id"].ToString());
            id = id + 1;
            id1 = id.ToString();
        }
        else
        {
            id1 = "1";
        }
        return id1;
    }
    public ReturnClass.ReturnBool insertroll(bl_report bl)
    {
        string str = "insert into role_table(Role_id,RoleName,WelcomePage) values(@Role_id,@RoleName,@WelcomePage)";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("Role_id",bl.Role),
            new MySqlParameter("RoleName",bl.RollName),
            new MySqlParameter("WelcomePage",bl.Welcomepage)
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }
    public ReturnClass.ReturnDataTable select_employeemapping_rec_dis(bl_report bl)
    {
        string str = @"select eom.office_mapping_id as mappingid, eom.emp_code as empcode , et.Name_en as name from emp_office_mapping eom 
inner join employee_table et on et.emp_id=eom.emp_code
where eom.office_id=@Ofcid and eom.emp_code=@empcode and eom.active='Y'  order by et.Name_en asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("Ofcid",bl.Officeid),
            new MySqlParameter("empcode",bl.Emp_code)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_employee_rec_dis(bl_report bl)
    {
        string str = @"select eom.office_mapping_id as mappingid, eom.emp_code as empcode , et.Name_en as name from emp_office_mapping eom 
inner join employee_table et on et.emp_id=eom.emp_code
where eom.office_id=@Ofcid and eom.active='Y' order by et.Name_en asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("Ofcid",bl.Officeid)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_recieve_chargetype()
    {
        string str = "select ddl.DDL_Name_Value as short, ddl.DisplayName_en as name from ddl_list ddl  where ddl.Category='Receiving'  order by ddl.DisplayName_en asc";
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_officedtl(bl_report bl)
    {
        string str = @"select oc.OfficeCategoryName as ofc_cat,oc.OfficeCategoryCode as ofc_cat_code,
ol.OfficeLevelCode as code, ol.OfficeLevelName as ol_name,
ofc.OfficeName as nameofc, ofc.NewOfficeCode as OfficeCode
from office ofc
inner join officecategory oc on oc.OfficeCategoryCode=ofc.OfficeCategory
inner join officelevel ol on ol.BaseDeptCode= ofc.BaseDeptCode and ol.OfficeLevelCode = ofc.OfficeLevel
 where ofc.NewOfficeCode=@ofclvl ";
        MySqlParameter[] pm = new MySqlParameter[]{
          new MySqlParameter("ofclvl" , bl.Officeid)
      };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnBool insert_rec_dep(bl_report bl)
    {
        string str = @"insert into receive_dispatch
(office_id,office_mapping_id,status,emp_type,joined_from,leave_to,ip_address,client_OS,client_browser,userAgent,users_id)
values
(@office_id,@office_mapping_id,@status,@type,@from,@to,@ip_address,@client_OS,@client_browser,@userAgent,@users_id)";
        MySqlParameter[] pm = new MySqlParameter[]{
        new MySqlParameter("office_id",bl.Officeid),
        new MySqlParameter("office_mapping_id",bl.Ofc_mappingid),
        new MySqlParameter("status",bl.State),
        new MySqlParameter("type",bl.Type),
        new MySqlParameter("from",bl.Fromdate),
        new MySqlParameter("to",bl.Todate),
        new MySqlParameter("ip_address",bl.Ipaddress),
        new MySqlParameter("client_OS",bl.Clientos),
        new MySqlParameter("client_browser",bl.Client_browser),
        new MySqlParameter("userAgent",bl.Useragent),
        new MySqlParameter("users_id",bl.Userid)
};
        rb = db.executeInsertQuery(str, pm);
        return rb;

    }


    public ReturnClass.ReturnDataTable countMappedEmp(bl_report bl)
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

            if (bl.Role == "4")
            {
                MySqlParameter ba = new MySqlParameter("officeLevelType", bl.Officelevelcode);
                pm.Add(ba);
                where += "   and ol.OfficeLevelType = @officeLevelType   ";
            }

            if (bl.Role == "5")
            {
                MySqlParameter ba = new MySqlParameter("office_id", bl.Officeid);
                pm.Add(ba);
                where += "   and ofc.NewOfficeCode = @office_id   ";
            }

            str = @"select  count(office_mapping_id) as office_map_count from emp_office_mapping as emp_map
                    inner  join office ofc on ofc.NewOfficeCode = emp_map.office_id
                    inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
                    inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
                    inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and
                    ol.StateCode= ofc.StateCode  and ol.BaseDeptCode=ofc.BaseDeptCode  " + where;

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;

    }

    public ReturnClass.ReturnDataTable GetTotalEmployeeInGrid(bl_report bl)
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
            if (bl.District != "0" && bl.District != null && bl.District != "")
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

    public ReturnClass.ReturnDataTable GetTotalEmpMapInGrid(bl_report bl)
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
                    inner join officelevel as E on A.office_level_id = E.OfficeLevelCode and A.base_department_id=E.BaseDeptCode
                    inner join districts as F on A.district_id_ofc = F.District_ID
                    inner join officecategory as G on A.office_category = G.OfficeCategoryCode
                    inner join ddl_list as H on A.charge_type = H.DDL_Name_Value " + where + " order by A.office_mapping_id ";

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

    public ReturnClass.ReturnDataTable GetTotalDepartmentInGrid1()
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

    public ReturnClass.ReturnDataTable GetTotalOfficeInGrid(bl_report bl)
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
    public ReturnClass.ReturnDataTable GetTotalOfficeInGrid1(bl_report bl)
    {
        string str = "", where = "  where 1 = 1    ";
        try
        {


            List<MySqlParameter> pm = new List<MySqlParameter>();

            if (bl.District != "0" && bl.District != null && bl.District != "" && bl.District != "Select")
            {
                MySqlParameter ba = new MySqlParameter("district", bl.District);
                pm.Add(ba);
                where += "   and ofc.DistrictCodeNew = @district   ";
            }

            if (bl.Base_department != "0" && bl.Base_department != null && bl.Base_department != "" && bl.Base_department != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Base_department", bl.Base_department);
                pm.Add(ca);
                where += "   and ofc.BaseDeptCode = @Base_department  ";
            }


            str = @"select ofc.NewOfficeCode , dic.District_Name_En as district, bd.dept_name as basedept, ol.OfficeLevelName as ofclvl, 
                     ofc.OfficeName as office, ofc.OfficeAddress as address, ofc.ContactNo as mobile, ofc.Email as email, 
                    ofc.OfficeURL as ofc_url
                    from office ofc
                     inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
                     inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
                     inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and ol.StateCode= ofc.StateCode  
                    and ol.BaseDeptCode=ofc.BaseDeptCode "
                + where + "  order by ofc.NewOfficeCode asc ";

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;
    }
    public ReturnClass.ReturnDataTable GetTotalPIOInGrid(bl_report bl)
    {


        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = "";
        try
        {
            str = @"select emp.Name_en from employee_table emp inner join emp_office_mapping eom on eom.emp_code=emp.emp_id where eom.designation_id='2' ";


            rt = db.executeSelectQuery(str);
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rt;
    }

    public string GetEmpCode()
    {
        string reg_year = DateTime.Now.Year.ToString();
        string sg = "";
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = "select MAX(SUBSTRING(emp_id,5,9)) as ID from employee_table Where SUBSTRING(emp_id,1,4)=@registration_year  ";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("registration_year",reg_year)
        };
        rd = db.executeSelectQuery(str, pm);

        if (rd.table.Rows.Count > 0)
        {
            if (rd.table.Rows[0]["ID"].ToString() != "")
            {
                int nextId = Convert.ToInt32(rd.table.Rows[0]["ID"].ToString());
                nextId++;
                string lid = Convert.ToString(nextId);
                sg = (reg_year + lid.PadLeft(5, '0'));
                return sg;
            }

            else
            {
                sg = (reg_year + "00001");

            }
        }
        return sg;
    }

    public ReturnClass.ReturnBool update(bl_report bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();

        string str = @"update employee_table set Name_en= @Name_en,Active=@Active,
                    state_id=@state_id,base_department_id=@base_department_id,district_id=@district_id,
                    NewOfficeCode=@NewOfficeCode,mobile_no=@mobile_no,email_id=@email_id,user_id=@user_id 
                    where emp_id=@emp_id ";
        MySqlParameter[] pm = new MySqlParameter[]
            {
                new MySqlParameter("Name_en",bl.Name_en),
                   new MySqlParameter("Active",bl.Active),
                      new MySqlParameter("state_id",bl.State_id),
                       new MySqlParameter("base_department_id",bl.Department),
                       new MySqlParameter("district_id",bl.District_id),
                       
                      
                       new MySqlParameter("NewOfficeCode",bl.NewOfficeCode),
                         new MySqlParameter("mobile_no",bl.Mobile_no),
                           new MySqlParameter("email_id",bl.Email_id),
                             new MySqlParameter("emp_id",bl.Emp_id),
                              new MySqlParameter("user_id",bl.User_id)

            };
        rb = db.executeUpdateQuery(str, pm);
        return rb;
    }

    public ReturnClass.ReturnBool Insert_emp(bl_report bl)
    {
        db_maria_connection db = new db_maria_connection();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        string strQuery = @"INSERT INTO employee_table
                          (emp_id,user_id,Name_hi,Name_en,Active,state_id,base_department_id,district_id,NewOfficeCode,mobile_no,email_id,emp_date_time,ip_address)
                           VALUES(@emp_id,@user_id,@Name_hi,@Name_en,@Active,@state_id,@base_department_id,@district_id,@NewOfficeCode,@mobile_no,@email_id,@emp_date_time,@ip_address)";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("emp_id",bl.Emp_id),

                new MySqlParameter("user_id",bl.User_id),

                new MySqlParameter("Name_hi",bl.Name_hi),
                new MySqlParameter("Name_en",bl.Name_en),
                new MySqlParameter("email_id",bl.Emailid ),
                new MySqlParameter("Active",bl.Active ),

                new MySqlParameter("mobile_no",bl.Mobileno),
                new MySqlParameter("district_id",bl.District_id),
                new MySqlParameter("state_id",bl.State_id),
                new MySqlParameter("base_department_id",bl.Department),

              
                new MySqlParameter("NewOfficeCode",bl.NewOfficeCode),
                new MySqlParameter("emp_date_time",bl.Emp_date_time),
                new MySqlParameter("ip_address",bl.Client_ip)
              };


        rb = db.executeInsertQuery(strQuery, pm);


        return rb;
    }
    public ReturnClass.ReturnBool Insert_empmap(bl_report bl)
    {
        db_maria_connection db = new db_maria_connection();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        string strQuery = @"INSERT INTO emp_office_mapping
                          (office_mapping_id,emp_code,office_id,designation_id,base_department_id,office_level_id,district_id_ofc,office_category,user_id,charge_type,Active,client_ip)
                           VALUES(@office_mapping_id,@emp_code,@office_id,@designation_id,@base_department_id,@office_level_id,@district_id_ofc,@office_category,@user_id,@charge_type,@Active,@client_ip)";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("office_mapping_id",bl.Office_mapping_id),
                new MySqlParameter("emp_code",bl.Emp_code),
                new MySqlParameter("office_id",bl.Office_id),
                new MySqlParameter("designation_id",bl.Designation_id),
                new MySqlParameter("base_department_id",bl.Base_department_id ),
                new MySqlParameter("office_level_id",bl.Office_level_id ),
                new MySqlParameter("district_id_ofc",bl.District_id_ofc),
                new MySqlParameter("office_category",bl.Office_category),
                new MySqlParameter("user_id",bl.User_id),
                new MySqlParameter("charge_type",bl.Charge_type),
                new MySqlParameter("Active",bl.Active),
                new MySqlParameter("client_ip",bl.Client_ip)
              };



        rb = db.executeInsertQuery(strQuery, pm);


        return rb;
    }



    public ReturnClass.ReturnDataTable Bind_gridreport(bl_report bl)
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();

        string qr = "", where = "";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();


            if (bl.State_id != "0" && bl.State_id != null && bl.State_id != "" && bl.State_id != "Select")
           
            {
                MySqlParameter ca = new MySqlParameter("State", bl.State_id);
                pm.Add(ca);
                where += " and emp.state_id=@State";
            }
            if (bl.District_id != "0" && bl.District_id != null && bl.District_id != "" && bl.District_id != "Select")
            {
                MySqlParameter ca = new MySqlParameter("District", bl.District_id);
                pm.Add(ca);
                where += " and emp.district_id =@District";
            }
            if (bl.Department != "" && bl.Department != null && bl.Department != "0" && bl.Department != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Department", bl.Department);
                pm.Add(ca);
                where += " and emp.base_department_id=@Department";

            }
            if (bl.Office_id != "" && bl.Office_id != null && bl.Office_id != "0" && bl.Office_id != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Office", bl.Office_id);
                pm.Add(ca);
                where += " and emp.NewOfficeCode=@Office  ";
            }
            if (bl.Designation_id != "" && bl.Designation_id != null && bl.Designation_id != "0" && bl.Designation_id != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Designation_ID", bl.Designation_id);
                pm.Add(ca);
                where += " and dg.Designation_ID=@Designation_ID  ";
            }

            qr = @"select emp.emp_id as id, emp.Name_en as empname,emp.mobile_no as mblname, dic.District_Name as dist, bd.dept_name as depnm,
                    ofc.OfficeName as office,emp.email_id as email 
                    from employee_table emp
                    INNER join office ofc on ofc.DistrictCodeNew=emp.district_id and ofc.BaseDeptCode= emp.base_department_id and 
                    ofc.StateCode = emp.state_id and ofc.NewOfficeCode=emp.NewOfficeCode
                    inner join districts dic on dic.StateCode = emp.state_id and dic.District_ID = emp.district_id
                    inner join basedepartment bd on bd.dept_id = emp.base_department_id
                    inner join emp_office_mapping eom on eom.emp_code=emp.emp_id
                    inner join designation dg on dg.Designation_ID=eom.designation_id where 1=1";


            where = where + " order by emp.Name_en";

            qr = qr + where;
            dt = db.executeSelectQuery(qr, pm.ToArray());

        }
        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }


        return dt;
    }

    public ReturnClass.ReturnDataTable GetDesignation(bl_report bl)
    {
        string str = "select Designation_ID, Designation_Name from designation  ORDER BY Designation_Name asc ";

        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable state_code(bl_report bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select state_id,state_name_e from state where state_id=@State_id";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("State_id",bl.State_id)
        };
     
        rt = db.executeSelectQuery(st,pm);
        return rt;


    }
    public ReturnClass.ReturnDataTable role_id(bl_report bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select RollID from login where UserId=@UserId";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("UserId",bl.User_id)
        };
        rt = db.executeSelectQuery(st, pm);
        return rt;


    }

    public ReturnClass.ReturnDataTable office(bl_report bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = " select os.NewOfficeCode, os.OfficeName from office os where os.StateCode=@StateCode and os.DistrictCodeNew=@DistrictCodeNew and os.BaseDeptCode=@BaseDeptCode";

        MySqlParameter[] pm = new MySqlParameter[]{
          new MySqlParameter("StateCode",bl.StateCode),
          new MySqlParameter("DistrictCodeNew",bl.DistrictCodeNew),
          new MySqlParameter("BaseDeptCode",bl.Department)
        };

        rt = db.executeSelectQuery(st, pm);
        return rt;
    }
    public ReturnClass.ReturnDataTable office1(bl_report bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();

        string st = "select os.NewOfficeCode, os.OfficeName from office os where os.DistrictCodeNew=@DistrictCodeNew  and os.StateCode=@StateCode and os.BaseDeptCode=@BaseDeptCode  and os.OfficeLevel!='01'";
        MySqlParameter[] pm = new MySqlParameter[]{
        new MySqlParameter("DistrictCodeNew",bl.DistrictCodeNew),
        new MySqlParameter("StateCode",bl.StateCode),
        new MySqlParameter("BaseDeptCode",bl.Department)
        };
        rt = db.executeSelectQuery(st, pm);
        return rt;
    }

    public ReturnClass.ReturnDataTable designation(bl_report bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = " select Designation_ID, Designation_Name from designation  ";

        rt = db.executeSelectQuery(st);
        return rt;


    }

    public string Max_office_code1(bl_report bl)
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string oficecode;
        string str = "select max(substring(ofc.NewOfficeCode,7,10) )as max  from office ofc where ofc.DistrictCodeNew=@district";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("district",bl.District)

        };
        rd = db.executeSelectQuery(str, pm);
        if (rd.table.Rows.Count > 0)
        {
            int code = Convert.ToInt32(rd.table.Rows[0]["max"]);
            code = code + 1;
            string code1 = code.ToString("0000");
            oficecode = bl.State_id + bl.District + bl.Office_level_id + code1;
        }
        else
        {
            oficecode = bl.State_id + bl.District + bl.Office_level_id + "0001";
        }
        return oficecode;
    }
  
    public ReturnClass.ReturnDataTable designation()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select Designation_ID,Designation_Name from designation";
        rt = db.executeSelectQuery(st);
        return rt;
    }

    public ReturnClass.ReturnDataTable district_code(bl_report bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select District_ID,District_Name from districts where StateCode=@StateCode order by District_Name ";
        MySqlParameter[] pm = new MySqlParameter[]{
        new MySqlParameter("StateCode",bl.StateCode)
        };

        rt = db.executeSelectQuery(st, pm);
        return rt;


    }

    public ReturnClass.ReturnDataTable district_code1()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select District_ID,District_Name from districts  order by District_Name ";
      

        rt = db.executeSelectQuery(st);
        return rt;


    }
    public ReturnClass.ReturnDataTable department_id(bl_report bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select dept_id,dept_name from basedepartment order by dept_name  ";


        rt = db.executeSelectQuery(st);
        return rt;
    }
    public ReturnClass.ReturnDataTable designation_id(bl_report bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select Designation_ID,Designation_Name from designation order by Designation_Name ";


        rt = db.executeSelectQuery(st);
        return rt;
    }

    public ReturnClass.ReturnDataTable showEmployee(bl_report bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = @"SELECT et.emp_id,et.Name_en,et.mobile_no,et.email_id,(select s.state_name_e from state s where s.state_id = et.state_id)state_name_e,
                    (select d.district_nm_e from district d where d.district_id = et.district_id)district_nm_e,
                    (select bd.dept_name from basedepartment bd where bd.dept_id = et.base_department_id)dept_name,
                   
                    (select o.OfficeName from office o where o.NewOfficeCode=et.NewOfficecode)OfficeName,
                    et.state_id,et.district_id,et.base_department_id,et.NewOfficeCode 
                     FROM employee_table et";


        rt = db.executeSelectQuery(st);
        return rt;


    }
    public ReturnClass.ReturnDataTable employee_permission(bl_report bl)
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @"select eom.office_mapping_id as emp_map_id,concat( et.Name_en, ' / ',ofc.OfficeName )  as name from permission ps
inner join emp_office_mapping eom on eom.office_mapping_id = ps.emp_map_id
inner join employee_table et on et.emp_id = eom.emp_code
inner join basedepartment bd on bd.dept_id = eom.base_department_id
inner join office ofc on ofc.NewOfficeCode = eom.office_id and ofc.BaseDeptCode = eom.base_department_id
where eom.active = 'Y' ";
        string where = "     ";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();

            if (bl.State_id != "0" && bl.State_id != null && bl.State_id != "")
            {
                MySqlParameter ca = new MySqlParameter("State", bl.State_id);
                pm.Add(ca);
                where += "   and eom.state_id=@State    ";
            }
            if (bl.District != "0" && bl.District != null && bl.District != "" && bl.District != "Select")
            {
                MySqlParameter ba = new MySqlParameter("District", bl.District);
                pm.Add(ba);
                where += "   and eom.district_id_ofc =@District   ";
            }
            if (bl.Base_department != "" && bl.Base_department != null && bl.Base_department != "0" && bl.Base_department != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Department", bl.Base_department);
                pm.Add(ca);
                where += " and eom.base_department_id=@Department   ";

            }
            if (bl.Office_id != "" && bl.Office_id != null && bl.Office_id != "0" && bl.Office_id != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Office", bl.Office_id);
                pm.Add(ca);
                where += " and emp.NewOfficeCode=@Office  ";
            }
            if (bl.Designation_ID != "" && bl.Designation_ID != null && bl.Designation_ID != "0" && bl.Designation_ID != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Designation_ID", bl.Designation_ID);
                pm.Add(ca);
                where += "   and emp.Designation_ID=@Designation_ID   ";
            }
            str = str + where + " ORDER by name ASC";
            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception Ex)
        {
            rd.status = false;
            rd.message = Ex.Message;
        }
        return rd;
    }
    public ReturnClass.ReturnDataTable permission_employee(bl_report bl)
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @"select pm.approve as approve , pm.review as review, pm.dispose as dispose, pm.forward as forward , pm.reject as reject from permission pm 
                       where pm.emp_map_id = @eom";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("eom",bl.Office_mapping_id)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnBool Update_permission(bl_report bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        using (TransactionScope ts = new TransactionScope())
        {
            string str = @"insert into permission_log select * from permission pr where pr.emp_map_id=@eom";
            MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("eom",bl.Office_mapping_id)
        };
            rb = db.executeInsertQuery(str, pm);
            if (rb.status == true)
            {
                string str1 = @"update permission pm  set  pm.approve=@approve , pm.review=@review, pm.dispose=@dispose, pm.forward=@forward, pm.reject=@reject,
pm.userid=@userid, pm.ipaddress=@ipad, pm.useragent=@userage,pm.useros=@useros,pm.clientbrowser=@clientbr where pm.emp_map_id=@eom";
                MySqlParameter[] pm1 = new MySqlParameter[]
                {
                    new MySqlParameter("approve",bl.Approve),
                    new MySqlParameter("review",bl.Review),
                    new MySqlParameter("dispose",bl.Dispose),
                    new MySqlParameter("forward",bl.Forward),
                    new MySqlParameter("reject",bl.Reject),
                    new MySqlParameter("userid",bl.User_id),
                    new MySqlParameter("ipad",bl.Client_ip),
                    new MySqlParameter("userage",bl.Useragent),
                    new MySqlParameter("useros",bl.Clientos),
                    new MySqlParameter("clientbr",bl.ClientBrowser),
                    new MySqlParameter("eom",bl.Office_mapping_id)

                };
                rb = db.executeUpdateQuery(str1, pm1);
                if (rb.status == true)
                {
                    ts.Complete();
                }
            }
            return rb;
        }
    }

}