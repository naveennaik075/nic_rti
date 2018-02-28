using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for dl_user_login
/// </summary>
public class dl_user_login
{
	public dl_user_login()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public ReturnClass.ReturnDataTable Get_District(bl_user_login bl)
    {
       // string df = @"select  B.District_ID, B.District_Name from employee_table as A
         //             inner join Districts as B on A.district_id = B.District_ID  where StateCode = @state ORDER BY B.District_Name asc ";
         string str = @"  select d.District_ID , d.District_Name from districts d where d.StateCode='22' ORDER BY d.District_Name asc "; 
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("state",bl.State)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }

    public ReturnClass.ReturnDataTable Get_BaseDepartment(bl_user_login bl)
    {
        string str = @" SELECT distinct B.dept_id, B.dept_name FROM employee_table as A 
                        inner join basedepartment as B on A.base_department_id = B.dept_id 
                        where A.district_id = @district_id order by B.dept_name ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("district_id",bl.District_id)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }


    public ReturnClass.ReturnDataTable Get_Office(bl_user_login bl)
    {
        string str;
        string where = "";
        if (bl.Role == "4") {
            where += " and C.OfficeLevelType = '00' ";
        }

       
        str = @" SELECT distinct B.NewOfficeCode, B.OfficeName FROM employee_table as A 
                        inner join office as B on A.NewOfficeCode = B.NewOfficeCode and A.base_department_id=B.BaseDeptCode
                        inner join officelevel as C on B.OfficeLevel = C.OfficeLevelCode and B.BaseDeptCode=C.BaseDeptCode 
                        where A.district_id=@district_id and A.base_department_id=@department_id " + where + " order by B.OfficeName";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("district_id", bl.District_id),
            new MySqlParameter("department_id",bl.Department_id)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        if (
           bl.Role == "5")
        {
            str = "select NewOfficeCode,OfficeName from office o where o.NewOfficeCode=@newoffice";
            pm = new MySqlParameter[]
            {
                new MySqlParameter("newoffice",bl.Office_id)
            };
        }
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }

    public ReturnClass.ReturnDataTable Get_Employee(bl_user_login bl)
    {
        string str = @"select e.emp_id , e.Name_en from employee_table e where
e.emp_id not in (select l.LoginID from login l) and e.district_id=@district_id and e.base_department_id=@department_id
and e.NewOfficeCode=@office_id order by e.Name_en ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("district_id", bl.District_id),
            new MySqlParameter("department_id",bl.Department_id),
            new MySqlParameter("office_id",bl.Office_id)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }



}