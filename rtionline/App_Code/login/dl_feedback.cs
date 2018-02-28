using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dl_feedback
/// </summary>
public class dl_feedback
{
	public dl_feedback()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public ReturnClass.ReturnBool insert_feedback(bl_feedback bl)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        string str = "insert Into feedback(user_id,feedback,subject,specify) VALUES (@userid,@feedback,@subject,@specify)";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("userid",bl.Userid),
            new MySqlParameter("feedback",bl.Feedback),
            new MySqlParameter("subject",bl.Subject),
            new MySqlParameter("specify",bl.Specify)
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }
}