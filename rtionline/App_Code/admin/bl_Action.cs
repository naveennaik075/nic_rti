using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_Action
/// </summary>
public class bl_Action
{
	public bl_Action()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    string userid, actionname, dDL_Name_Value, role, actionid, remark, displayOrder, ddl_id, category, dDL_List_ID;
    public string Userid { get { return userid; } set { userid = value; } }
    public string Ddl_id { get { return ddl_id; } set { ddl_id = value; } }
    public string Actionid { get { return actionid; } set { actionid = value; } }
    public string Role { get { return role; } set { role = value; } }
    public string Remark { get { return remark; } set { remark = value; } }
    public string DisplayOrder { get { return displayOrder; } set {displayOrder = value; } }
    public string Actionname { get { return actionname; } set { actionname = value; } }
    public string DDL_Name_Value { get { return dDL_Name_Value; } set { dDL_Name_Value = value; } }
    public string Category { get { return category; } set { category = value; } }
    public string DDL_List_ID { get { return dDL_List_ID; } set { dDL_List_ID = value; } }



}