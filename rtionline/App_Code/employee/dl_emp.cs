using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;


public class dl_emp
{

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
    public ReturnClass.ReturnDataTable select_admin_info(bl_emp bl)
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = @"select lo.UserID, emp.Name_en, emp.state_id,emp.base_department_id, emp.NewOfficeCode, emp.district_id,lo.RollID from login lo
inner join employee_table emp on emp.emp_id=lo.LoginID
 where lo.UserID=@user";
        string pp = @"select em.district_id_ofc as district_id,e.state_id as state_id, dis.District_Name_En, em.base_department_id ,em.office_id from emp_office_mapping em
        inner join employee_table e on e.emp_id=em.emp_code
        inner join districts dis on em.district_id_ofc= dis.District_ID
        where em.office_mapping_id=@officemap and em.role_id=@role";

        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("officemap",bl.Emp_code),
            new MySqlParameter("role",bl.Role_id),
            new MySqlParameter("user",bl.Userid)
        };
        rd = db.executeSelectQuery(pp, pm);
        return rd;

    }
    public ReturnClass.ReturnBool update(bl_emp bl)
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

    public ReturnClass.ReturnBool Insert_emp(bl_emp ur)
    {
        db_maria_connection db = new db_maria_connection();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        string strQuery = @"INSERT INTO employee_table
                          (emp_id,user_id,Name_hi,Name_en,Active,state_id,base_department_id,district_id,NewOfficeCode,mobile_no,email_id,emp_date_time,ip_address)
                           VALUES(@emp_id,@user_id,@Name_hi,@Name_en,@Active,@state_id,@base_department_id,@district_id,@NewOfficeCode,@mobile_no,@email_id,@emp_date_time,@ip_address)";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("emp_id",ur.Emp_id),

                new MySqlParameter("user_id",ur.User_id),

                new MySqlParameter("Name_hi",ur.Name_hi),
                new MySqlParameter("Name_en",ur.Name_en),
                new MySqlParameter("email_id",ur.Emailid ),
                new MySqlParameter("Active",ur.Active ),

                new MySqlParameter("mobile_no",ur.Mobileno),
                new MySqlParameter("district_id",ur.District_id),
                new MySqlParameter("state_id",ur.State_id),
                new MySqlParameter("base_department_id",ur.Department),


                new MySqlParameter("NewOfficeCode",ur.NewOfficeCode),
                new MySqlParameter("emp_date_time",ur.Emp_date_time),
                new MySqlParameter("ip_address",ur.Client_ip)
              };


        rb = db.executeInsertQuery(strQuery, pm);


        return rb;
    }
    public ReturnClass.ReturnBool Insert_empmap(bl_emp ur)
    {
        db_maria_connection db = new db_maria_connection();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        string strQuery = @"INSERT INTO emp_office_mapping
                          (office_mapping_id,emp_code,office_id,designation_id,base_department_id,office_level_id,district_id_ofc,office_category,user_id,charge_type,Active,client_ip)
                           VALUES(@office_mapping_id,@emp_code,@office_id,@designation_id,@base_department_id,@office_level_id,@district_id_ofc,@office_category,@user_id,@charge_type,@Active,@client_ip)";

        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("office_mapping_id",ur.Office_mapping_id),
                new MySqlParameter("emp_code",ur.Emp_code),
                new MySqlParameter("office_id",ur.Office_id),
                new MySqlParameter("designation_id",ur.Designation_id),
                new MySqlParameter("base_department_id",ur.Base_department_id ),
                new MySqlParameter("office_level_id",ur.Office_level_id ),
                new MySqlParameter("district_id_ofc",ur.District_id_ofc),
                new MySqlParameter("office_category",ur.Office_category),
                new MySqlParameter("user_id",ur.User_id),
                new MySqlParameter("charge_type",ur.Charge_type),
                new MySqlParameter("Active",ur.Active),
                new MySqlParameter("client_ip",ur.Client_ip)
              };



        rb = db.executeInsertQuery(strQuery, pm);


        return rb;
    }



    public ReturnClass.ReturnDataTable Bind_grid(bl_emp bl)
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();

        string qr = "", where = "";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();

            if (bl.State_id != "0" && bl.State_id != null && bl.State_id != "")
            {
                MySqlParameter ca = new MySqlParameter("State", bl.State_id);
                pm.Add(ca);
                where += " and emp.state_id=@State";
            }
            if (bl.District_id != "0" && bl.District_id != null && bl.District_id != "" && bl.District_id != "Select")
            {
                MySqlParameter ba = new MySqlParameter("District", bl.District_id);
                pm.Add(ba);
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

            qr = @"select emp.emp_id as id, emp.Name_en as empname,emp.mobile_no as mblname, dic.District_Name as dist, bd.dept_name as depnm,
                    ofc.OfficeName as office,emp.email_id as email, st.state_name_e as state, st.state_id as state_id,
                    ofc.DistrictCodeNew as dist_id, emp.NewOfficeCode as ofc_id,  emp.base_department_id as dept_id
                    from employee_table emp
                    INNER join office ofc on ofc.DistrictCodeNew=emp.district_id and ofc.BaseDeptCode= emp.base_department_id and 
                    ofc.StateCode = emp.state_id and ofc.NewOfficeCode=emp.NewOfficeCode
                    inner join districts dic on dic.StateCode = emp.state_id and dic.District_ID = emp.district_id
                    inner join basedepartment bd on bd.dept_id = emp.base_department_id
                    
                    inner join state st on st.state_id=emp.state_id where 1=1";


            where = where + " order by Name_en";

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


    public ReturnClass.ReturnDataTable state_code()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select state_id,state_name_e from state order by state_name_e";
        rt = db.executeSelectQuery(st);
        return rt;


    }
    public ReturnClass.ReturnDataTable role_id(bl_emp bl)
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

    public ReturnClass.ReturnDataTable office(bl_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = " select os.NewOfficeCode, os.OfficeName from office os where os.DistrictCodeNew=@DistrictCodeNew and os.StateCode=@StateCode and os.BaseDeptCode=@BaseDeptCode ";

        MySqlParameter[] pm = new MySqlParameter[]{
        new MySqlParameter("DistrictCodeNew",bl.DistrictCodeNew),
        new MySqlParameter("StateCode",bl.StateCode),
          new MySqlParameter("BaseDeptCode",bl.Department)
        };

        rt = db.executeSelectQuery(st, pm);
        return rt;
    }
    public ReturnClass.ReturnDataTable office1(bl_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();

        string st = @"select et.state_id as state_id, em.base_department_id as base_department_id,em.district_id_ofc as district_id, em.office_id, em.office_id as newofficecode, of.OfficeName from emp_office_mapping em 
inner join employee_table et on et.emp_id = em.emp_code
inner join office of on of.NewOfficeCode=em.office_id
where em.office_mapping_id  = @eom";
        MySqlParameter[] pm = new MySqlParameter[]{
        new MySqlParameter("DistrictCodeNew",bl.DistrictCodeNew),
        new MySqlParameter("StateCode",bl.StateCode),
        new MySqlParameter("BaseDeptCode",bl.Department),
          new  MySqlParameter("eom",bl.Emp_code)
        };
        rt = db.executeSelectQuery(st, pm);
        return rt;
    }

    public ReturnClass.ReturnDataTable designation(bl_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = " select Designation_ID, Designation_Name from designation  ";

        rt = db.executeSelectQuery(st);
        return rt;


    }
    public ReturnClass.ReturnDataTable office_level(bl_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "SELECT OfficeLevelName FROM officelevel where OfficeLevelCode=@OfficeLevelCode ";
        MySqlParameter[] pm = new MySqlParameter[]{
        new MySqlParameter(" DistrictCodeNew",bl. DistrictCodeNew)
        };
        rt = db.executeSelectQuery(st);
        return rt;


    }
    public ReturnClass.ReturnDataTable designation()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select Designation_ID,Designation_Name from designation";
        rt = db.executeSelectQuery(st);
        return rt;
    }
    public ReturnClass.ReturnDataTable role()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select Role_id,RoleName from role_table";
        rt = db.executeSelectQuery(st);
        return rt;
    }
    public ReturnClass.ReturnDataTable district_code(bl_emp bl)
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
    public ReturnClass.ReturnDataTable department_id(bl_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select dept_id,dept_name from basedepartment order by dept_name  ";


        rt = db.executeSelectQuery(st);
        return rt;
    }
    public ReturnClass.ReturnDataTable designation_id(bl_emp bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select Designation_ID,Designation_Name from designation order by Designation_Name ";


        rt = db.executeSelectQuery(st);
        return rt;
    }

    public ReturnClass.ReturnDataTable showEmployee(bl_emp bl)
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
    public ReturnClass.ReturnDataTable employee_permission(bl_emp bl)
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

            if (bl.State != "0" && bl.State != null && bl.State != "")
            {
                MySqlParameter ca = new MySqlParameter("State", bl.State);
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
            if (bl.Office != "" && bl.Office != null && bl.Office != "0" && bl.Office != "Select")
            {
                MySqlParameter ca = new MySqlParameter("Office", bl.Office);
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
    public ReturnClass.ReturnDataTable permission_employee(bl_emp bl)
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
    public ReturnClass.ReturnBool Update_permission(bl_emp bl)
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
                    new MySqlParameter("useros",bl.ClientOS),
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