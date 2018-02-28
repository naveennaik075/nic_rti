using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


/// <summary>
/// Summary description for dl_rti_emp
/// </summary>
public class dl_rti_emp
{
	public dl_rti_emp()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public ReturnClass.ReturnDataTable GetRequestCount(bl_rti_emp bl)
    {
        ReturnClass.ReturnDataTable dt = null;//= new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string qr = ""; 
        try

        {

            string query = @"select distinct(select count(status) from rti_status st where officer_maping_id=@ofc_map_id and st.IsValid='Y' and st.Payment_status='Y' ) as total,
                (select count(status) from rti_status st where status=@status and officer_maping_id=@ofc_map_id and st.IsValid='Y' and st.Payment_status='Y' )as complete ,
                (select count(status) from rti_status st where status<>@status and officer_maping_id=@ofc_map_id and st.IsValid='Y' and st.Payment_status='Y' )as pending
                 from rti_status rd";
            //if (bl.RequestStatus == "REG")
            //{
            //    qr = " select count(status) as RequestCount from rti_status where officer_maping_id=@ofc_map_id AND IsValid='Y' and Payment_status='Y' ";
            //}
            //else if (bl.RequestStatus == "PEN")
            //{
            //    qr = " select count(status) as RequestCount from rti_status where officer_maping_id=@ofc_map_id AND IsValid='Y' and Payment_status='Y' " + 
            //         " and (status='PEN' OR status='REG') ";
            //}
            //else {
            //    qr = " select count(status) as RequestCount from rti_status where officer_maping_id=@ofc_map_id AND IsValid='Y' and Payment_status='Y' " +
            //         " and status='CLT' ";
            //}

            MySqlParameter[] pr = new MySqlParameter[]{
              new MySqlParameter("ofc_map_id", bl.Office_map_id),
              new MySqlParameter("status", bl.RequestStatus)
                };
            dt = db.executeSelectQuery(query, pr);
            dt.status = true;
        }

        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }
        return dt;

    } // End of GetRequestCount

    public ReturnClass.ReturnDataTable Get_EmpDesName(bl_rti_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = @"select et.Name_en, emp.office_mapping_id, dg.Designation_Name, of.OfficeName, bd.dept_name							   from emp_office_mapping emp
    inner join employee_table et on et.emp_id=emp.emp_code
     inner join designation dg on dg.Designation_ID= emp.designation_id
     inner join office of on of.NewOfficeCode=emp.office_id
     inner JOIN basedepartment bd on bd.dept_id=emp.base_department_id                       
           where emp.office_mapping_id=@empmapid";
            //@" select lo.LoginID, emp.Name_en from login as lo 
            //           inner join employee_table as emp on lo.LoginID = emp.emp_id                      
            //           where lo.UserID=@UserId ";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("UserId",bl.User_id),
            new MySqlParameter("empmapid",bl.Office_map_id)
        };
        rt = db.executeSelectQuery(st, pm);
        return rt;


    }  // End of Get_EmpDesName

    public ReturnClass.ReturnDataTable Get_OfficeDesignation(bl_rti_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = @"  select lo.LoginID, emp_map.office_mapping_id, emp_map.office_id, emp_map.designation_id, 
                        concat (ofc.OfficeName,'-', des.Designation_Name) as ofc_des_Text 
                        from login as lo 
                        inner join emp_office_mapping as emp_map on lo.LoginID = emp_map.emp_code
                        inner join designation as des on emp_map.designation_id = des.Designation_ID
                        inner join office as ofc on ofc.NewOfficeCode = emp_map.office_id
                        where  lo.UserID=@UserId ";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("UserId",bl.User_id)
        };
        rt = db.executeSelectQuery(st, pm);
        return rt;


    }  // End of Get_EmpDesName


    public ReturnClass.ReturnDataTable Get_Office(bl_rti_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = @"  select lo.LoginID, emp_map.office_mapping_id, emp_map.designation_id, des.Designation_Name, emp_map.office_id,  
                        ofc.OfficeName from login as lo 
                        inner join emp_office_mapping as emp_map on lo.LoginID = emp_map.emp_code
                        inner join designation as des on emp_map.designation_id = des.Designation_ID
                        inner join office as ofc on ofc.NewOfficeCode = emp_map.office_id
                        where lo.UserID=@UserId ";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("UserId",bl.User_id)
        };
        rt = db.executeSelectQuery(st, pm);
        return rt;


    }  // End of Get_EmpDesName

    public ReturnClass.ReturnDataTable Get_RTI_Data(bl_rti_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = @"  select rd.rti_id, rd.Applicant_Name_en, rd.rti_Subject, 
                        IF(rd.Is_BPL = 'Y', 'YES', 'NO') as Is_BPL,  DATE_FORMAT(rd.rti_DateTime,'%m-%d-%Y :%H:%i:%s') as rti_DateTime, 
                        rd.Mobile_No, ddl.DisplayName_en as Gender, st.action_id, ddl2.DisplayName_en as rti_status, st.IsNew, ddl3.DisplayName_en as DeptStatus from rti_detail rd 
                        inner join rti_status st on rd.rti_id=st.rti_id
                        inner join ddl_list ddl on rd.Gender = ddl.DDL_Name_Value and ddl.Category='Gender'
                        inner join ddl_list ddl2 on st.status = ddl2.DDL_Name_Value and ddl2.Category='RTIStatus'
                        left join ddl_list ddl3 on st.DeptStatus = ddl3.DDL_Name_Value and ddl3.Category='Permission'
                        where st.officer_maping_id = @office_map_id and st.IsValid='Y' and st.Payment_status='Y' order by rd.rti_id DESC";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("office_map_id",bl.Office_map_id)
        };
        rt = db.executeSelectQuery(st, pm);
        return rt;


    }  // End  


    public ReturnClass.ReturnDataTable GetRoll_ID(bl_rti_emp bl)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @"select lo.RollID from login lo
                       where lo.UserID=@user";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("user",bl.User_id)
        };
        dt = db.executeSelectQuery(str, pm);
        return dt;

    }

    public ReturnClass.ReturnDataTable GetDataForHandler(string fileid)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @"select fileData, fileType as ContentType from action_files where action_fileID=@FileID";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("FileID",fileid)
        };
        dt = db.executeSelectQuery(str, pm);
        return dt;

    }
    public ReturnClass.ReturnDataTable Get_Action_Detail(bl_rti_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string where = "";
        if (bl.Roll_id == "2") {
            where += " and rd.securitycode=@securitycode " ; 
        }
        string st = @"select ra.action_id, ra.rti_id, ra.officer_mapping_id, ra.action_detail,
        IF(ra.IsMeeting = 'Y', 'YES', 'NO') as IsMeeting, DATE_FORMAT(ra.meeting_date,'%d-%m-%Y') as meeting_date,
        IF(ra.IsUpload = 'Y', 'YES', 'NO') as IsUpload, dl.DisplayName_en as rti_status, DATE_FORMAT(rd.rti_DateTime,'%d-%m-%Y: %H:%i:%s') as rti_date,
		 emp_map.emp_code, emp.Name_en as Emp_Name, emp.NewOfficeCode, ofc.OfficeName, DATE_FORMAT(ra.action_date,'%d-%m-%Y') as action_date ,
         emp_map.base_department_id, bd.dept_name, emp_map.district_id_ofc, dis.District_Name_En, af.action_fileID, 
        (case when ra.IsUpload ='Y' then af.action_fileID else 'No File' end) as file_ID
		 from rti_action as ra 
       inner join emp_office_mapping as emp_map on ra.officer_mapping_id = emp_map.office_mapping_id
       inner join employee_table as emp on emp_map.emp_code = emp.emp_id
       inner join office as ofc on ofc.NewOfficeCode = emp.NewOfficeCode
       inner join basedepartment as bd on emp_map.base_department_id = bd.dept_id
       inner join districts as dis on dis.District_ID = emp_map.district_id_ofc
       inner join ddl_list as dl on dl.DDL_Name_Value = ra.`dept_status` and dl.Category='Permission'
       inner join rti_detail as rd on ra.rti_id = rd.rti_id 
       left join action_files as af on af.action_id = ra.action_id and af.rti_id = ra.rti_id
       where ra.rti_id=@rti_id  " + where + " order by entry_date_time ASC";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("rti_id",bl.Rti_id),
            new MySqlParameter("securitycode", bl.Security_code)
        };
        rt = db.executeSelectQuery(st, pm);
        return rt;


    }  // End  


    public ReturnClass.ReturnBool Set_IsNew(bl_rti_emp bl)
    {
        ReturnClass.ReturnBool rt = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        string st = @" update rti_status  set IsNew='N' where rti_id=@Rti_id ";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("Rti_id",bl.Rti_id)
        };
        rt = db.executeUpdateQuery(st, pm);
        //rt = db.executeSelectQuery(st, pm);
        return rt;


    }  // End of Get_EmpDesName

}