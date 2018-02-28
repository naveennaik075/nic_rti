using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;
public class dl_empMap
{
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    db_maria_connection db = new db_maria_connection();
    public ReturnClass.ReturnDataTable select_admin_info(bl_empMap bl)
    {
        ReturnClass.ReturnDataTable dt = null;
        db_maria_connection db = new db_maria_connection();
        string str = @"select em.district_id_ofc as district_id,e.state_id as state_id, dis.District_Name_En, em.base_department_id ,em.office_id,em.office_category as category,em.office_level_id
 from emp_office_mapping em
  inner join employee_table e on e.emp_id=em.emp_code
  inner join districts dis on em.district_id_ofc= dis.District_ID
        where em.office_mapping_id=@officemap and em.role_id=@role";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("officemap",bl.Emp_code1),
            new MySqlParameter("role",bl.Role_id)
        };

        dt = db.executeSelectQuery(str, pm);
        return dt;

    }
    public ReturnClass.ReturnDataTable get_role(bl_empMap bl)
    {
        ReturnClass.ReturnDataTable dt = null;
        db_maria_connection db = new db_maria_connection();
        string where = " where  Role_id <> 2  ";
        if (bl.Role_id == "4" || bl.Role_id == "5")
        {
            where += "  and Role_id='3' ";   // For dio and nodel select only employee
        }
        string str = @" select Role_id, RoleName from role_table " + where;
        dt = db.executeSelectQuery(str);
        return dt;
    }
    public ReturnClass.ReturnDataTable GetRole()
    {
        ReturnClass.ReturnDataTable dt = null;
        db_maria_connection db = new db_maria_connection();
        try
        {

            string qr = "SELECT Role_id,RoleName FROM role_table  ";


            dt = db.executeSelectQuery(qr);
            dt.status = true;
        }

        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }
        return dt;

    }// end of GetRole
    public ReturnClass.ReturnDataTable GetCharge(bl_empMap bl)
    {
        ReturnClass.ReturnDataTable dt = null;// = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {

            string qr = "SELECT DDL_Name_Value , DisplayName_en FROM ddl_list WHERE Category=@ddl_category order by DisplayOrder";

            MySqlParameter[] pr = new MySqlParameter[]{
              new MySqlParameter("ddl_category", bl.Charge_type)
                };
            dt = db.executeSelectQuery(qr, pr);
            // dt = db.executeSelectQuery(qr);
            dt.status = true;
        }

        catch (Exception Ex)
        {
            dt.status = false;
            dt.message = Ex.Message;
        }
        return dt;

    }// end of get GetGender
    public ReturnClass.ReturnDataTable GetDesignation(bl_empMap bl)
    {
        string str = "select Designation_ID, Designation_Name from designation  ORDER BY Designation_Name asc ";

        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable BindDistrict(bl_empMap bl)
    {
        //string str = "select District_ID,District_Name_En from Districts  where StateCode=@state ORDER BY District_Name_En asc ";
        string str = "select District_ID,District_Name from Districts  where StateCode=@state ORDER BY District_Name asc ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("state",bl.State)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable Get_BaseDepartment(bl_empMap bl)
    {
        //string str = "select District_ID,District_Name_En from Districts  where StateCode=@state ORDER BY District_Name_En asc ";
        string str = "SELECT dept_id, dept_name FROM basedepartment order by dept_name ";
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable Get_OfficeCategory(bl_empMap bl)
    {
        //string str = "select District_ID,District_Name_En from Districts  where StateCode=@state ORDER BY District_Name_En asc ";
        string str = "SELECT OfficeCategoryCode, OfficeCategoryName FROM officeCategory order by OfficeCategoryName ";
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable Get_OfficeLevel(bl_empMap bl)
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = "select ol.OfficeLevelName as Office_level, ol.OfficeLevelCode as olc from officelevel ol   ";
        string where = " where 1 = 1";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
            MySqlParameter fa = new MySqlParameter("BaseDeptCode", bl.Base_department_id);
            pm.Add(fa);
            where += " and ol.BaseDeptCode=@BaseDeptCode";
            //  }

            if (bl.Office_level_id == "5")
            {
                string officeLevel = "00";
                MySqlParameter ca = new MySqlParameter("level", officeLevel);
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

        // string str = @"SELECT OfficeLevelCode, OfficeLevelName FROM officelevel WHERE officelevel.BaseDeptCode=@dept order by OfficeLevelName ";

    }
    public ReturnClass.ReturnDataTable Get_Office_Data(bl_empMap bl)
    {
        string strquery = @"SELECT NewOfficeCode, OfficeName FROM office WHERE  
                           office.BaseDeptCode=@dept AND office.DistrictCodeNew=@district
                           AND office.OfficeCategory=@officeCategory AND office.OfficeLevel=@officeLevel order by OfficeName ";
        //string str = @"SELECT OfficeLevelCode, OfficeLevelName FROM officelevel WHERE officelevel.BaseDeptCode=@dept order by OfficeLevelName ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("dept",bl.Base_department_id),
            new MySqlParameter("district",bl.District_id_ofc),
            new MySqlParameter("officeCategory",bl.Office_category),
            new MySqlParameter("officeLevel",bl.Office_level_id)
        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(strquery, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable GetEmployeeDataAllocated(bl_empMap bl)
    {
        string str = @"select distinct(em.Name_en), em.emp_id from emp_office_mapping et 
inner join employee_table em on em.emp_id=et.emp_code ";
        string where = "  where  1=1   ";
        List<MySqlParameter> pm = new List<MySqlParameter>();

        if (bl.Base_department_id != "0" && bl.Base_department_id != "" && bl.Base_department_id != "Select" && bl.Base_department_id != null)
        {
            MySqlParameter base_dept = new MySqlParameter("base_dept", bl.Base_department_id);
            pm.Add(base_dept);
            where += " and et.base_department_id = @base_dept  ";
        }
        if (bl.District_id_ofc != "0" && bl.District_id_ofc != "" && bl.District_id_ofc != "Select" && bl.District_id_ofc != null)
        {
            MySqlParameter dist = new MySqlParameter("dist", bl.District_id_ofc);
            pm.Add(dist);
            where += " and et.district_id_ofc=@dist ";
        }
        if (bl.Office_category != "0" && bl.Office_category != "" && bl.Office_category != "Select" && bl.Office_category != null)
        {
            MySqlParameter ofc_category = new MySqlParameter("ofc_category", bl.Office_category);
            pm.Add(ofc_category);
            where += " and et.office_category=@ofc_category  ";
        }

        if (bl.Office_level_id != "0" && bl.Office_level_id != "" && bl.Office_level_id != "Select" && bl.Office_level_id != null)
        {
            MySqlParameter ofc_level = new MySqlParameter("ofc_level", bl.Office_level_id);
            pm.Add(ofc_level);
            where += " and et.office_level_id=@ofc_level ";
        }
        if (bl.Office_id != "0" && bl.Office_id != "" && bl.Office_id != "Select" && bl.Office_id != null)
        {
            MySqlParameter ofc_name = new MySqlParameter("ofc_name", bl.Office_id);
            pm.Add(ofc_name);
            where += " and  et.office_id=@ofc_name ";
        }
        str = str + where + "    Order By  em.Name_en";

        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm.ToArray());
        return rd;
    }
    public ReturnClass.ReturnDataTable GetEmployeeData1(bl_empMap bl)
    {
        string str = @"select A.emp_id as id,A.Name_en as label from  employee_table A where A.base_department_id= @base_department_id and A.district_id=@district_id_ofc 
                       and Active='Y' and emp_id not in ( select emp_code from emp_office_mapping ) order by Name_en asc";
        //string str = @"select  emp_id, Name_en from  employee_table A where Active='Y' order by Name_en asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("base_department_id",bl.Base_department_id),
            new MySqlParameter("district_id_ofc",bl.District_id_ofc)

        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable GetEmployeeData(bl_empMap bl)
    {
        //string str = @"select  emp_id, Name_en from  employee_table A where A.base_department_id= @base_department_id and A.district_id=@district_id_ofc and Active='Y' order by Name_en asc";
        string str = @"select  emp_id, Name_en from  employee_table A where Active='Y' order by Name_en asc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("base_department_id",bl.Base_department_id),
            new MySqlParameter("district_id_ofc",bl.District_id_ofc)

        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        // rd = db.executeSelectQuery(str, pm);
        rd = db.executeSelectQuery(str);
        return rd;
    }
    // Get employee data from employee mapping table
    public ReturnClass.ReturnDataTable GetSelectedEmpData(bl_empMap bl)
    {
        string str = @" select  office_category, office_level_id, office_id, designation_id from  emp_office_mapping A
                      where A.base_department_id= @base_department_id and A.district_id_ofc=@district_id_ofc and A.emp_code=@empCode and active='Y' ";

        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("base_department_id",bl.Base_department_id),
            new MySqlParameter("district_id_ofc",bl.District_id_ofc),
            new MySqlParameter("empCode",bl.Emp_code)

        };
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        rd = db.executeSelectQuery(str, pm);

        return rd;
    } // End of GetSelectedEmpData
    public string GetMappingID(string empCode)
    {
        string sg = "";
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = "select MAX(SUBSTRING(office_mapping_id,10,12)) as ID from emp_office_mapping em where em.emp_code = @empCode";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("empCode",empCode)
        };
        rd = db.executeSelectQuery(str, pm);

        if (rd.table.Rows.Count > 0)
        {
            if (rd.table.Rows[0]["ID"].ToString() != "")
            {
                int nextId = Convert.ToInt32(rd.table.Rows[0]["ID"].ToString());
                nextId++;
                string lid = Convert.ToString(nextId);
                sg = (empCode + lid.PadLeft(3, '0'));
                return sg;
            }

            else
            {
                sg = (empCode + "001");

            }
        }
        else
        {

            sg = (empCode + "001");
        }
        return sg;
    }
    public ReturnClass.ReturnBool Insert_empOfficeMapAllocated(bl_empMap ur)
    {
        db_maria_connection db = new db_maria_connection();

        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        ReturnClass.ReturnBool rb1 = new ReturnClass.ReturnBool();
        using (TransactionScope ts = new TransactionScope())
        {
            string updateQuery = @" update  emp_office_mapping set  active='N', ToActive=@toActive1  where emp_code=@emp_code1 and base_department_id=@base_department_id1 
                                 and district_id_ofc=@district_id_ofc1 and office_category=@office_category1 and office_level_id=@office_level_id1
                                 and office_id=@office_id1 and designation_id=@designation_id1 and role_id=@role_id1 ";

            MySqlParameter[] pm1 = new MySqlParameter[]{
                new MySqlParameter("emp_code1",ur.Emp_code1),
                new MySqlParameter("office_id1",ur.Office_id1),
                new MySqlParameter("designation_id1",ur.Designation_id1),
                new MySqlParameter("base_department_id1",ur.Base_department_id1 ),
                new MySqlParameter("office_level_id1",ur.Office_level_id1 ),
                new MySqlParameter("district_id_ofc1",ur.District_id_ofc1),
                new MySqlParameter("office_category1",ur.Office_category1),
                new MySqlParameter("toActive1",ur.ToActive1),
                new MySqlParameter("role_id1",ur.Role_id1)
              };
            rb1 = db.executeUpdateQuery(updateQuery, pm1);
            if (rb1.status == true)
            {
                string strQuery = @"INSERT INTO emp_office_mapping
                          (office_mapping_id,emp_code,office_id,designation_id,base_department_id,office_level_id,district_id_ofc,office_category,user_id,charge_type,Active,client_ip, FromActive, ToActive,role_id)
                           VALUES(@office_mapping_id,@emp_code,@office_id,@designation_id,@base_department_id,@office_level_id,@district_id_ofc,@office_category,@user_id,@charge_type,@Active,@client_ip, @fromActive, @toActive,@roleid)";

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
                new MySqlParameter("client_ip",ur.Client_ip),
                new MySqlParameter("fromActive",ur.FromActive),
                new MySqlParameter("toActive",ur.ToActive),
                new MySqlParameter("roleid",ur.Role_id)

              };
                rb = db.executeInsertQuery(strQuery, pm);
                if (rb.status == true)
                {
                    foreach (string str12 in ur.Permission)
                    {
                        ur.Approve = str12;
                        if (rb.status == true)
                        {
                            string quert = @"insert into permission (emp_map_id,ActionPermission,userid,ipaddress,useragent,useros,clientbrowser)
Values(@emp_map_id,@ActionPermission,@userid,@ipaddress,@useragent,@useros,@clientbrowser)";
                            MySqlParameter[] pm11 = new MySqlParameter[]
                            {
                    new MySqlParameter("emp_map_id",ur.Office_mapping_id),
                    new MySqlParameter("ActionPermission",ur.Approve),
                    new MySqlParameter("userid",ur.User_id),
                    new MySqlParameter("ipaddress",ur.Client_ip),
                    new MySqlParameter("useragent",ur.Useragent),
                    new MySqlParameter("useros",ur.ClientOS),
                    new MySqlParameter("clientbrowser",ur.ClientBrowser)
                            };
                            rb = db.executeInsertQuery(quert, pm11);
                        }

                    }
                    if (rb.status == true)
                    {
                        ts.Complete();
                    }
                }

            }
        }
        return rb;
    }
    public ReturnClass.ReturnBool Insert_empOfficeMap(bl_empMap ur)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        using (TransactionScope ts = new TransactionScope())
        {
            string strQuery = @"INSERT INTO emp_office_mapping
                          (office_mapping_id,emp_code,office_id,designation_id,base_department_id,office_level_id,district_id_ofc,office_category,user_id,charge_type,Active,client_ip, FromActive, ToActive,role_id)
                           VALUES(@office_mapping_id,@emp_code,@office_id,@designation_id,@base_department_id,@office_level_id,@district_id_ofc,@office_category,@user_id,@charge_type,@Active,@client_ip, @fromActive, @toActive,@role_id)";
            MySqlParameter[] pm = new MySqlParameter[]
            {
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
                new MySqlParameter("client_ip",ur.Client_ip),
                new MySqlParameter("fromActive",ur.FromActive),
                new MySqlParameter("toActive",ur.ToActive),
                new MySqlParameter("role_id",ur.Role_id)
      };
            rb = db.executeInsertQuery(strQuery, pm);
            if (rb.status == true)
            {
                foreach (string str12 in ur.Permission)
                {
                    ur.Approve = str12;
                    if (rb.status == true)
                    {

                        string quert = @"insert into permission (emp_map_id,ActionPermission,userid,ipaddress,useragent,useros,clientbrowser)
Values(@emp_map_id,@ActionPermission,@userid,@ipaddress,@useragent,@useros,@clientbrowser)";
                        MySqlParameter[] pm1 = new MySqlParameter[]
                        {
                    new MySqlParameter("emp_map_id",ur.Office_mapping_id),
                    new MySqlParameter("ActionPermission",ur.Approve),
                    new MySqlParameter("userid",ur.User_id),
                    new MySqlParameter("ipaddress",ur.Client_ip),
                    new MySqlParameter("useragent",ur.Useragent),
                    new MySqlParameter("useros",ur.ClientOS),
                    new MySqlParameter("clientbrowser",ur.ClientBrowser)
                        };
                        rb = db.executeInsertQuery(quert, pm1);
                    }
                }
                if (rb.status == true)
                {
                    ts.Complete();
                }
            }
        }
        return rb;
    }
    public ReturnClass.ReturnBool checkuserid(bl_emp bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        db_maria_connection objData = new db_maria_connection();
        string query = " SELECt *  FROM empl_office_mapping WHERE UserID= @user_id";
        MySqlParameter[] pm = new MySqlParameter[]{
                new MySqlParameter("user_id",bl.User_id)
               };
        dt = objData.executeSelectQuery(query, pm);
        if (dt.table.Rows.Count == 0)
        {
            rb.status = false;

        }
        else
        {
            rb.status = true;
        }
        return rb;
    }
    public ReturnClass.ReturnDataTable showEmpMap(bl_empMap bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = @"select distinct A.office_mapping_id as mapping_id, A.emp_code as EmployeeCode, B.Name_en as EmpName, C.OfficeName as officeName,DD.Designation_Name as designation_name,R.RoleName as role_name,
                        D.dept_name as DeptName , E.OfficeLevelName as OfficeLevelName , F.District_Name_En as DistName_En ,
                        G.OfficeCategoryName as OfficeCategoryName, A.user_id as UserID , H.DisplayName_en as ChangeTypeName, IF(A.active = 'Y', 'YES', 'NO') as activeStatus
                        from emp_office_mapping as A  
                    inner join employee_table as B on A.emp_code = B.emp_id  
                    inner join office as C on A.office_id = C.NewOfficeCode
                    inner join basedepartment as D on A.base_department_id = D.dept_id
                    inner join officelevel as E on A.office_level_id = E.OfficeLevelCode and A.base_department_id=E.BaseDeptCode
                    inner join districts as F on A.district_id_ofc = F.District_ID
                    inner join officecategory as G on A.office_category = G.OfficeCategoryCode
                    inner join ddl_list as H on A.charge_type = H.DDL_Name_Value
 inner join designation as DD on A.designation_id=DD.Designation_ID
                    inner join role_table as R on A.role_id=R.Role_id
                    where H.Category = 'ChargeType' ";
        string where = "";
        List<MySqlParameter> pm = new List<MySqlParameter>();

        if (bl.Base_department_id != "0" && bl.Base_department_id != "")
        {
            MySqlParameter base_dept = new MySqlParameter("base_dept", bl.Base_department_id);
            pm.Add(base_dept);
            where += " and A.base_department_id=@base_dept ";
        }
        if (bl.District_id_ofc != "0" && bl.District_id_ofc != "")
        {
            MySqlParameter dist = new MySqlParameter("dist", bl.District_id_ofc);
            pm.Add(dist);
            where += " and A.district_id_ofc=@dist ";
        }
        if (bl.Office_category != "0" && bl.Office_category != "")
        {
            MySqlParameter ofc_category = new MySqlParameter("ofc_category", bl.Office_category);
            pm.Add(ofc_category);
            where += " and A.office_category=@ofc_category ";
        }

        if (bl.Office_level_id != "0" && bl.Office_level_id != "")
        {
            MySqlParameter ofc_level = new MySqlParameter("ofc_level", bl.Office_level_id);
            pm.Add(ofc_level);
            where += " and A.office_level_id=@ofc_level ";
        }
        if (bl.Office_id != "0" && bl.Office_id != "")
        {
            MySqlParameter ofc_name = new MySqlParameter("ofc_name", bl.Office_id);
            pm.Add(ofc_name);
            where += " and A.office_id=@ofc_name ";
        }

        st += where;

        rt = db.executeSelectQuery(st, pm.ToArray());
        return rt;


    }
    public ReturnClass.ReturnDataTable district_code(bl_empMap bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select District_ID,District_Name from districts where StateCode=@StateCode order by District_Name ";
        MySqlParameter[] pm = new MySqlParameter[]{
        new MySqlParameter("StateCode",bl.State)
        };


        rt = db.executeSelectQuery(st, pm);
        return rt;


    }

    public ReturnClass.ReturnDataTable officelevel(bl_empMap bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select OfficeLevelCode,OfficeLevelName from officelevel ";


        rt = db.executeSelectQuery(st);
        return rt;


    }
    public ReturnClass.ReturnDataTable department(bl_empMap bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select dept_id,dept_name from basedepartment ";


        rt = db.executeSelectQuery(st);
        return rt;


    }

    public ReturnClass.ReturnBool update(bl_empMap bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();

        string str = @"update emp_office_mapping set Name_en= @Name_en,Active=@Active,
                    state_id=@state_id,base_department_id=@base_department_id,district_id=@district_id,
                    NewOfficeCode=@NewOfficeCode,user_id=@user_id 
                    where emp_id=@emp_id ";
        MySqlParameter[] pm = new MySqlParameter[]
            {
                new MySqlParameter("Name_en",bl.Emp_name),
                   new MySqlParameter("Active",bl.Active),

                       new MySqlParameter("base_department_id",bl.Base_department_id),
                       new MySqlParameter("district_id",bl.District_id_ofc),


                       new MySqlParameter("NewOfficeCode",bl.Office_id),

                             new MySqlParameter("emp_id",bl.Emp_code),
                              new MySqlParameter("user_id",bl.User_id)

            };
        rb = db.executeUpdateQuery(str, pm);
        return rb;
    }
    public ReturnClass.ReturnDataTable office(bl_empMap bl)
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select NewOfficeCode,OfficeName from office ";


        rt = db.executeSelectQuery(st);
        return rt;


    }
    public ReturnClass.ReturnDataTable permissions_all()
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string str = "select dl.DDL_Name_Value as value, dl.DisplayName_en as name from ddl_list dl where dl.Category='permission' order by name asc ";
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable department_id()
    {
        ReturnClass.ReturnDataTable rt = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string st = "select dept_id,dept_name from basedepartment";
        rt = db.executeSelectQuery(st);
        return rt;
    }
    public ReturnClass.ReturnDataTable check_roll(bl_empMap bl)
    {
        string str = @"select em.office_mapping_id,em.emp_code, et.Name_en,o.OfficeName , d.District_Name_En from emp_office_mapping em 
inner join employee_table et on et.emp_id = em.emp_code
inner join office o on o.NewOfficeCode = em.office_id
inner join districts d on d.District_ID = em.district_id_ofc";
        string where = "    Where em.role_id = @roleid   and em.Active='Y'  ";
        if (bl.District_id_ofc != "" || bl.District_id_ofc != null || bl.District_id_ofc != "0" || bl.District_id_ofc != "select")
        {
            where = where + "   and em.district_id_ofc =@district_id_ofc     ";
        }
        if (bl.Office_id != "" || bl.Office_id != null || bl.Office_id != "0" || bl.Office_id != "select")
        {
            where = where + "   and em.office_id = @office_id    ";
        }
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("roleid",bl.Role_id),
            new MySqlParameter("district_id_ofc",bl.District_id_ofc),
            new MySqlParameter("office_id",bl.Office_id)
        };
        str = str + where;
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
}