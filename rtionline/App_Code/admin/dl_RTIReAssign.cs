using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;

/// <summary>
/// Summary description for dl_RTIReAssign
/// </summary>
public class dl_RTIReAssign
{
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    db_maria_connection db = new db_maria_connection();
	public dl_RTIReAssign()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public ReturnClass.ReturnDataTable BindOfficerDL(bl_RTI_Request bl)
    {
        string str = @"select  office_mapping_id,concat( A.Name_en,'/', dg.Designation_Name  ) as Name_en from  employee_table as A 
                       inner join  emp_office_mapping  as B on A.emp_id=B.emp_code
                       inner join  designation as dg on  dg.Designation_ID = B.designation_id  
                       inner join  office as ofc on  ofc.NewOfficeCode = B.office_id  
                       where B.base_department_id= @base_department_id and B.district_id_ofc=@district_id_ofc
                             and B.office_id=@office_id  and B.Active='Y' order by Name_en asc";
        //string str = "select  office_mapping_id, Name_en from  employee_table A, emp_office_mapping  B where A.emp_id=B.emp_code and " +
        //                  "  B.base_department_id= @base_department_id and B.office_level_id= @office_level_id " +
        //                   " and B.district_id_ofc=@district_id_ofc and B.office_category= @office_category and B.office_id=@office_id  and B.Active='Y' order by Name_en asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("base_department_id",bl.Base_department_id),
            new MySqlParameter("district_id_ofc",bl.District_id_ofc),
            new MySqlParameter("office_id",bl.Office_id)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }

    public ReturnClass.ReturnDataTable BindRTIStatusDL()
    {
        string str = @" select DDL_Name_Value as Value, DisplayName_en as Name  from ddl_list where Category='Permission' order by DisplayOrder ";
        rd = db.executeSelectQuery(str);
        return rd;
    }

    public ReturnClass.ReturnDataTable Bind_grid(bl_emp bl)
    {
       // ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        
        string qr = "", where_outer = "", where_inner=" and officer_maping_id  in ( select emp_map.office_mapping_id as emp_map_id from emp_office_mapping as emp_map where 1=1 ";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
                       
            if (bl.District_id != "0" && bl.District_id != null && bl.District_id != "" && bl.District_id != "Select")
            {
                MySqlParameter ba = new MySqlParameter("District", bl.District_id);
                pm.Add(ba);
                where_inner += " and emp_map.district_id_ofc =@District";
            }
            if (bl.Department != "" && bl.Department != null && bl.Department != "0" && bl.Department != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Department", bl.Department);
                pm.Add(ca);
                where_inner += " and emp_map.base_department_id=@Department";

            }
            if (bl.Office_id != "" && bl.Office_id != null && bl.Office_id != "0" && bl.Office_id != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Office", bl.Office_id);
                pm.Add(ca);
                where_inner += " and emp_map.office_id=@Office  ";
            }
            if (bl.Office_mapping_id != "" && bl.Office_mapping_id != null && bl.Office_mapping_id != "0" && bl.Office_mapping_id != "Select")
            {
                MySqlParameter ca = new MySqlParameter("emp_map_code", bl.Office_mapping_id);
                pm.Add(ca);
                where_inner += "  and emp_map.office_mapping_id=@emp_map_code  ";
            }
            if (bl.RTI_dept_status != "" && bl.RTI_dept_status != null && bl.RTI_dept_status != "0" && bl.RTI_dept_status != "Select")
            {
                MySqlParameter ca = new MySqlParameter("dept_status", bl.RTI_dept_status);
                pm.Add(ca);
                where_outer += "  and rst.DeptStatus=@dept_status  ";
            }
            where_inner += " ) ";

            qr = @"select rst.rti_id , rtd.Applicant_Name_en as Ap_Name, rtd.rti_Subject as subject , dst.District_Name_En as dist,
                    bd.dept_name as dept_name, ofc.OfficeName as ofc_name, emp.Name_en as emp_name, ddl.DisplayName_en as dept_status, dst.District_ID as dist_id,
                    bd.dept_id as dept_id, e_map.office_id as ofc_id,  rst.officer_maping_id as ofc_map_id
                    from rti_status as rst
                    inner join rti_detail as  rtd on rst.rti_id=rtd.rti_id
                    inner join emp_office_mapping as e_map on e_map.office_mapping_id = rst.officer_maping_id
                    inner join districts as dst on dst.District_ID= e_map.district_id_ofc
                    inner join basedepartment as bd on bd.dept_id = e_map.base_department_id
                    inner join office as ofc on ofc.DistrictCodeNew=e_map.district_id_ofc and ofc.BaseDeptCode= e_map.base_department_id and ofc.NewOfficeCode=e_map.office_id
                    inner join employee_table as emp on emp.emp_id = e_map.emp_code
                    left join ddl_list as ddl on ddl.DDL_Name_Value=rst.DeptStatus and ddl.Category='Permission'
                    where rst.IsValid='Y' and rst.Payment_status='Y'";
 //           qr = @"select rst.rti_id , rtd.Applicant_Name_en as Ap_Name, rtd.rti_Subject as subject , dst.District_Name_En as dist, bd.dept_name as dept_name, ofc.OfficeName as ofc_name, emp.Name_en as emp_name, ddl.DisplayName_en as dept_status from rti_status as rst
//                inner join rti_detail as  rtd on rst.rti_id=rtd.rti_id
//                inner join emp_office_mapping as e_map on e_map.office_mapping_id = rst.officer_maping_id
//                inner join districts as dst on dst.District_ID= e_map.district_id_ofc
//                inner join basedepartment as bd on bd.dept_id = e_map.base_department_id
//                inner join office as ofc on ofc.DistrictCodeNew=e_map.district_id_ofc and ofc.BaseDeptCode= e_map.base_department_id and ofc.NewOfficeCode=e_map.office_id
//                inner join employee_table as emp on emp.emp_id = e_map.emp_code
//                left join ddl_list as ddl on ddl.DDL_Name_Value=rst.DeptStatus and ddl.Category='Permission'
//                where rst.IsValid='Y' and rst.Payment_status='Y'
//                -- and rst.DeptStatus='NREV' 
//                and officer_maping_id  in (
//                select emp_map.office_mapping_id as emp_map_id from emp_office_mapping as emp_map 
//                where 1=1 and emp_map.base_department_id='B001' and emp_map.district_id_ofc='11'
//                --  and office_id='2211030318' 
//                 ) ";

            string where = where_outer + where_inner + " order by rst.rti_id";

            qr = qr + where;
            rd = db.executeSelectQuery(qr, pm.ToArray());

        }
        catch (Exception Ex)
        {
            rd.status = false;
            rd.message = Ex.Message;
        }


        return rd;
    }

    public ReturnClass.ReturnBool Update_RTIstatus(bl_employee_action bl)
    {
        string str = @"insert into rti_status_log select * from rti_status  rd  WHERE rd.rti_id =@rti_Id";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("rti_Id",bl.Rti_id)
        };

        string str1 = @"update rti_status set status=@status, DeptStatus=@DeptStatus ,
                    officer_maping_id=@ofcmapid,IPAddress=@ipaddress,action_date=@actiondate,client_browser=@clientbr, 
                    client_OS=@clintos,useragent=@useragent,IsNew=@isnew, user_id=@userid
                    where rti_id=@rtiid";
        MySqlParameter[] pm1 = new MySqlParameter[]{
               new MySqlParameter("status",bl.Rti_status),
               new MySqlParameter("DeptStatus",bl.Dept_Status),
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
        using (TransactionScope transScope = new TransactionScope())
        {

            rb = db.executeInsertQuery(str, pm);
            if (rb.status == true)
            {
                rb = db.executeUpdateQuery(str1, pm1);

            }
        if (rb.status == true)
        {
           transScope.Complete();
         }
       } // Transaction end
        return rb;
    }
}