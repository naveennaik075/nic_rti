using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for dl_Action
/// </summary>
public class dl_Action
{
	public dl_Action()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    db_maria_connection db = new db_maria_connection();
    public ReturnClass.ReturnDataTable select_admin_info(bl_Action bl)
    {
        string str = @"select lo.UserID, emp.Name_en, emp.state_id,emp.base_department_id, emp.NewOfficeCode, 
                    emp.district_id,lo.RollID from login lo
                    inner join employee_table emp on emp.emp_id=lo.LoginID
                    where lo.UserID=@user";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("user",bl.Userid)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;

    }

    public ReturnClass.ReturnDataTable Max_id()
    {

        string str = "select  IFNULL(max(CAST(DDL_List_ID as int)),0) as id from ddl_list";
      
        rd = db.executeSelectQuery(str);
       
        return rd;
    }
    public ReturnClass.ReturnBool insertAction(bl_Action bl)
    {
        string str = "insert into ddl_list(DDL_List_ID,DDL_Name_Value,DisplayName_en,Remarks,DisplayOrder,Category) values(@Ddl_id,@DDL_Name_Value,@ActionName,@Remarks,@DisplayOrder,@Category)";
        MySqlParameter[] pm = new MySqlParameter[]{
             new MySqlParameter("Ddl_id",bl.Ddl_id),
            new MySqlParameter("DDL_Name_Value",bl.DDL_Name_Value),
            new MySqlParameter("ActionName",bl.Actionname),
            new MySqlParameter("Remarks",bl.Remark),
            new MySqlParameter("DisplayOrder",bl.DisplayOrder),
            new MySqlParameter("Category",bl.Category)
           
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }

    public ReturnClass.ReturnDataTable select_action()
    {
        string str = "select DDL_List_ID,DDL_Name_Value,DisplayName_en from ddl_list where category='permission' order by DisplayOrder";
        rd = db.executeSelectQuery(str);
        return rd;
    }

    public ReturnClass.ReturnDataTable delete_action(bl_Action bl)
    {
        string str = "delete FROM ddl_list where DDL_List_ID=@DDL_List_ID";
        MySqlParameter[] pm = new MySqlParameter[]{
             new MySqlParameter("DDL_List_ID",bl.DDL_List_ID)
        };
        rd = db.executeSelectQuery(str,pm);
        return rd;
    }
}