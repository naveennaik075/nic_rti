using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_user_login
/// </summary>
public class bl_user_login
{
	public bl_user_login()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string  state, district_id, department_id, office_id, role ;
    public string State { get { return state; } set { state = value; } }
    public string District_id { get { return district_id; } set { district_id = value; } }

    public string Department_id { get { return department_id; } set { department_id = value; } }
    public string Office_id { get { return office_id; } set { office_id = value; } }
    public string Role { get { return role; } set { role = value; } }
}