using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for bl_rti_emp
/// </summary>
public class bl_rti_emp
{
    public bl_rti_emp()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    string user_id, office_map_id, rti_id, requestStatus, roll_id, security_code;

    public string User_id { get { return user_id; } set { user_id = value; } }
    public string Office_map_id { get { return office_map_id; } set { office_map_id = value; } }
    public string Rti_id { get { return rti_id; } set { rti_id = value; } }
    public string RequestStatus { get { return requestStatus; } set { requestStatus = value; } }
    public string Roll_id { get { return roll_id; } set { roll_id = value; } }
    public string Security_code { get { return security_code; } set { security_code = value; } }
}