using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

/// <summary>
/// Summary description for dl_Dio
/// </summary>
public class dl_Dio
{
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    db_maria_connection db = new db_maria_connection();
    public ReturnClass.ReturnDataTable select_admin_info(bl_Dio bl)
    {
//        string str = @"select lo.UserID, emp.Name_en, emp.state_id,emp.base_department_id, emp.NewOfficeCode, emp.district_id,lo.RollID from login lo
//inner join employee_table emp on emp.emp_id=lo.LoginID
// where lo.UserID=@user";  // Commented by Naveen on 28-Aug_2017 and added below
        string str = @"select  lo.UserID, emp.Name_en, emp.state_id,emp.base_department_id, emp.NewOfficeCode, emp.district_id,emp_map.role_id RollID from login lo
                        inner join employee_table emp on emp.emp_id=lo.LoginID 
                        inner join emp_office_mapping emp_map on emp_map.emp_code=lo.LoginID
                        where lo.UserID=@user" ;
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("user",bl.Userid)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;

    }

    public ReturnClass.ReturnDataTable delete_action(bl_Dio bl)
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
    public string Max_office_code(bl_Dio bl)
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
            oficecode = bl.State + bl.District + bl.Officelvl + code1;
        }
        else
        {
            oficecode = bl.State + bl.District + bl.Officelvl + "0001";
        }
        return oficecode;
    }
    public ReturnClass.ReturnDataTable category(bl_Dio bl)
    {
        string str = "select oc.OfficeCategoryCode as code , oc.OfficeCategoryName as name from  officecategory oc";
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnBool insert_office(bl_Dio bl)
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
        new MySqlParameter("officelevel",bl.Officelvl),
        new MySqlParameter("OfficeName",bl.Office),
        new MySqlParameter("BaseDeptCode",bl.Base_department),
        new MySqlParameter("countrycode",bl.Country),
        new MySqlParameter("Statecode",bl.State),
        new MySqlParameter("Officeaddress",bl.Address),
        new MySqlParameter("contactno",bl.Contact),
        new MySqlParameter("email",bl.Email),
        new MySqlParameter("fax",bl.Fax),
        new MySqlParameter("Officeurl",bl.Url),
          new MySqlParameter("OfficeCategory",bl.Category)
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }
    //insert in ofice function 
    public ReturnClass.ReturnDataTable office_level(bl_Dio bl)
    {
        string str = "select ol.OfficeLevelName as Office_level, ol.OfficeLevelCode as olc from officelevel ol   ";
        string where = " where 1 = 1";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
            //  if(bl.Base_department != "" && bl.Base_department != null && bl.Base_department != "0" && bl.Base_department != "Select")
            // {
            MySqlParameter fa = new MySqlParameter("BaseDeptCode", bl.Base_department);
            pm.Add(fa);
            where += " and ol.BaseDeptCode=@BaseDeptCode";
            //  }
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
    public ReturnClass.ReturnDataTable Bind_grid(bl_Dio bl)
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
            if (bl.Officelvl != "0" && bl.Officelvl != null && bl.Officelvl != "" && bl.Officelvl != "Select")
            {
                MySqlParameter da = new MySqlParameter("ofc_lvl", bl.Officelvl);
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
    public ReturnClass.ReturnDataTable base_department(bl_Dio bl)
    {
        string str = "select dept_id, dept_name from basedepartment order by dept_name asc";
        rd = db.executeSelectQuery(str);
        return rd;

    }
    public ReturnClass.ReturnDataTable district(bl_Dio bl)
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
    public ReturnClass.ReturnBool insertroll(bl_Dio bl)
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
    public ReturnClass.ReturnDataTable select_employeemapping_rec_dis(bl_Dio bl)
    {
        string str = @"select eom.office_mapping_id as mappingid, eom.emp_code as empcode , et.Name_en as name from emp_office_mapping eom 
inner join employee_table et on et.emp_id=eom.emp_code
where eom.office_id=@Ofcid and eom.emp_code=@empcode and eom.active='Y'  order by et.Name_en asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("Ofcid",bl.Officeid),
            new MySqlParameter("empcode",bl.Employee)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_employee_rec_dis(bl_Dio bl)
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
    public ReturnClass.ReturnDataTable select_officedtl(bl_Dio bl)
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
    public ReturnClass.ReturnBool insert_rec_dep(bl_Dio bl)
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
    public ReturnClass.ReturnBool insert_ofc_new(bl_Dio bl, bl_RTI_RequestFiles blr)
    {
        using(TransactionScope ts = new TransactionScope())
        {
            string str = @"insert into office_req(office_id,filename,file_type,file_data,
ipaddress,user_agent,user_system,user_browser,approved_by_user_id)
values(@office_id,@filename,@file_type,@file_data,@ipaddress,
@user_agent,@user_system,@user_browser,@approved_by_user_id)";
            MySqlParameter[] pm = new MySqlParameter[]
            { new MySqlParameter("office_id",bl.Officeid),
                new MySqlParameter("filename",blr.RTI_fileName),
                new MySqlParameter("file_type",blr.RTI_fileType),
                new MySqlParameter("file_data",blr.RTI_fileData),
                new MySqlParameter("ipaddress",bl.Ipaddress),
                new MySqlParameter("user_agent",bl.Useragent),
                new MySqlParameter("user_system",bl.Clientos),
                new MySqlParameter("user_browser",bl.Client_browser),
                new MySqlParameter("approved_by_user_id",bl.Userid)

            };
            rb = db.executeInsertQuery(str, pm);
            if (rb.status == true)
            {
                string str1 = @"insert into office(NewOfficeCode,OfficeName,DistrictCodeNew,BaseDeptCode,
OfficeLevel,OfficeCategory,CountryCode,StateCode,OfficeAddress,ContactNo,Fax,Email,OfficeURL,Status) 
values(@NewOfficeCode,@OfficeName,@DistrictCodeNew,@BaseDeptCode,@OfficeLevel,@OfficeCategory,@CountryCode
,@StateCode,@OfficeAddress,@ContactNo,@Fax,@Email,@OfficeURL,@isvalid)";
                 MySqlParameter[] pm1 = new MySqlParameter[]{
                     new MySqlParameter("NewOfficeCode",bl.Officeid),
                     new MySqlParameter("OfficeName",bl.Office),
                     new MySqlParameter("DistrictCodeNew",bl.District),
                     new MySqlParameter("BaseDeptCode",bl.Base_department),
                     new MySqlParameter("OfficeLevel",bl.Officelvl),
                     new MySqlParameter("OfficeCategory",bl.Category),
                     new MySqlParameter("CountryCode",bl.Country),
                     new MySqlParameter("StateCode",bl.State),
                     new MySqlParameter("OfficeAddress",bl.Address),
                     new MySqlParameter("ContactNo",bl.Contact),
                     new MySqlParameter("Fax",bl.Fax),
                     new MySqlParameter("Email",bl.Email),
                     new MySqlParameter("OfficeURL",bl.Url),
                     new MySqlParameter("isvalid",bl.Active)
                };
                rb = db.executeInsertQuery(str1, pm1);
                if (rb.status == true)
                {
                string temp= @"insert into office_temp(NewOfficeCode,OfficeName,DistrictCodeNew,BaseDeptCode,
OfficeLevel,OfficeCategory,CountryCode,StateCode,OfficeAddress,ContactNo,Fax,Email,OfficeURL,isvalid,userid,useragent,user_os,User_browser,Ip_address) 
values(@NewOfficeCode,@OfficeName,@DistrictCodeNew,@BaseDeptCode,@OfficeLevel,@OfficeCategory,@CountryCode
,@StateCode,@OfficeAddress,@ContactNo,@Fax,@Email,@OfficeURL,@isvalid,@userid,@useragent,@user_os,@User_browser,@Ip_address)";

                    MySqlParameter[] pm2 = new MySqlParameter[]{
                     new MySqlParameter("NewOfficeCode",bl.Officeid),
                     new MySqlParameter("OfficeName",bl.Office),
                     new MySqlParameter("DistrictCodeNew",bl.District),
                     new MySqlParameter("BaseDeptCode",bl.Base_department),
                     new MySqlParameter("OfficeLevel",bl.Officelvl),
                     new MySqlParameter("OfficeCategory",bl.Category),
                     new MySqlParameter("CountryCode",bl.Country),
                     new MySqlParameter("StateCode",bl.State),
                     new MySqlParameter("OfficeAddress",bl.Address),
                     new MySqlParameter("ContactNo",bl.Contact),
                     new MySqlParameter("Fax",bl.Fax),
                     new MySqlParameter("Email",bl.Email),
                     new MySqlParameter("OfficeURL",bl.Url),
                     new MySqlParameter("isvalid",bl.Active),
                      new MySqlParameter("userid",bl.Userid),
                     new MySqlParameter("useragent",bl.Useragent),
                     new MySqlParameter("user_os",bl.Clientos),
                     new MySqlParameter("User_browser",bl.Client_browser),
                     new MySqlParameter("Ip_address",bl.Ipaddress)
                };
                    rb = db.executeInsertQuery(temp, pm2);
                                    }
                    if (rb.status)
                {
                    ts.Complete();
                }
            }
        }
        return rb;
    }
    public ReturnClass.ReturnBool insert_ofc_farm(bl_Dio bl)
    {
        string str = @"update office_temp set form=@form where NewOfficeCode=@newofficecode";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("form",bl.File_data),
            new MySqlParameter("newofficecode",bl.Officeid)
        };
        rb = db.executeUpdateQuery(str, pm);
        return rb;
    }
    public ReturnClass.ReturnDataTable select_unvalidate_office(bl_Dio bl)
    {
        string str = @"select of.NewOfficeCode, of.OfficeName, of.BaseDeptCode,b.dept_name ,of.DistrictCodeNew,d.District_Name,
 of.OfficeCategory,c.OfficeCategoryName ,of.OfficeLevel,ol.OfficeLevelName, of.OfficeAddress,of.OfficeURL,of.ContactNo, of.Fax,of.Email   from office of
inner JOIN basedepartment b  on b.dept_id=of.BaseDeptCode
inner join districts d on d.District_ID=of.DistrictCodeNew
inner join officecategory c on c.OfficeCategoryCode=of.OfficeCategory
inner join officelevel ol on ol.BaseDeptCode=of.BaseDeptCode and ol.OfficeLevelCode=of.OfficeLevel
 where of.status=@isvalid and of.NewOfficeCode=@NewOfficeCode ";

        MySqlParameter[] pm = new MySqlParameter[]
        {
           new MySqlParameter("isvalid","N"),
           new MySqlParameter("NewOfficeCode",bl.Officeid)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
}
