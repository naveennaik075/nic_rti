using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;

/// <summary>
/// Summary description for dlApprove
/// </summary>
public class dlApprove
{
	public dlApprove()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    db_maria_connection db = new db_maria_connection();
    public ReturnClass.ReturnDataTable select_admin_info(blApprove bl)
    {
        string str = @"select lo.UserID, emp.Name_en, emp.state_id,emp.base_department_id, emp.NewOfficeCode, emp.district_id,lo.RollID from login lo
inner join employee_table emp on emp.emp_id=lo.LoginID
 where lo.UserID=@user";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("user",bl.Userid)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;

    }
    public ReturnClass.ReturnDataTable district_code(blApprove bl)
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
    public ReturnClass.ReturnDataTable Bind_grid(blApprove bl)
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
    ofc.OfficeName as ofice,ofc.OfficeCategory , ofc.OfficeAddress as address, ofc.ContactNo as mobile,ofc.Fax as Fax, ofc.Email as email, ofc.OfficeURL as ofc_url
     from office ofc
  inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
   inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
inner join officecategory oc on oc.OfficeCategoryCode=ofc.OfficeCategory
   inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and ol.StateCode= ofc.StateCode 
and ol.BaseDeptCode=ofc.BaseDeptCode and Status='n'" + where + "  order by ofc.NewOfficeCode asc";

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;
    }
    public ReturnClass.ReturnDataTable select(blApprove bl)
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
    ofc.OfficeName as ofice,ofc.OfficeCategory , ofc.OfficeAddress as address, ofc.ContactNo as mobile,ofc.Fax as Fax, ofc.Email as email, ofc.OfficeURL as ofc_url
     from office ofc
  inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
   inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
inner join officecategory oc on oc.OfficeCategoryCode=ofc.OfficeCategory
   inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and ol.StateCode= ofc.StateCode  and ol.BaseDeptCode=ofc.BaseDeptCode and isvalid='n'" + where + "  order by ofc.NewOfficeCode asc";

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;
    }

    public ReturnClass.ReturnBool Update_status1(blApprove bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();

        using (TransactionScope transScope = new TransactionScope())
        {

            string query = @" SELECT * from  office where NewOfficeCode=@NewOfficeCode";
            MySqlParameter[] pm = new MySqlParameter[]{
                             new MySqlParameter("NewOfficeCode",bl.Officeid)
            };

            rb = db.executeInsertQuery(query, pm);
            if (rb.status == true)
            {
                string str = @"Insert into office_temp (NewOfficeCode,DistrictCodeNew,OfficeLevel,OfficeName,BaseDeptCode,
                               countrycode,
                               Statecode,Officeaddress,contactno,email,fax,Officeurl,OfficeCategory,isvalid,Ip_address,
                               user_os,User_browser,useragent,userid,form)
                               values
                               (@NewOfficeCode,@DistrictCodeNew,@officelevel,@OfficeName,@BaseDeptCode,@countrycode,
                               @Statecode,@Officeaddress,@contactno,@email,@fax,@Officeurl,@OfficeCategory,@isvalid,@Ip_address,
                               @user_os,@User_browser,@useragent,@userid,@form)";
        MySqlParameter[] pm2 = new MySqlParameter[]{
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
        new MySqlParameter("isvalid",bl.Isvalid),
        new MySqlParameter("Ip_address",bl.Ipaddress),
        new MySqlParameter("user_os",bl.Operating_system),
        new MySqlParameter("User_browser",bl.Browser),
        new MySqlParameter("useragent",bl.Useragent),
        new MySqlParameter("OfficeCategory",bl.OfficeCategory),
        new MySqlParameter("userid",bl.Userid),
        new MySqlParameter("form",bl.Form),


        };
                rb = db.executeInsertQuery(str, pm2);
            }
            if (rb.status == true)
            {
                string str1 = @"update office set Status=@status where NewOfficeCode=@NewOfficeCode";

                MySqlParameter[] pm1 = new MySqlParameter[]{
               new MySqlParameter("NewOfficeCode",bl.Officeid),
               new MySqlParameter("status",bl.Status)
            };

                rb = db.executeUpdateQuery(str1, pm1);
            }

            if (rb.status == true)
            {
                transScope.Complete();
            }
        }


        return rb;
    }
}