using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Configuration;


/// <summary>
/// Summary description for db_connection
/// </summary>
public class db_maria_connection
{

    public MySqlDataAdapter Adapter;
    private static string con_str = WebConfigurationManager.ConnectionStrings["Industry_MSME_mysql"].ToString();
    public string monthly_con = "Monthly_Details";
    public MySqlDataReader reader;
    public MySqlCommand command;
    public MySqlParameter  objMySqlParameter ;
    //public db_maria_connection()
    //{
    //}
    /// <summary>
    /// Get Connection String
    /// </summary>
    /// <param name="_query"></param>
    /// <returns></returns>
    public static string GetConnectionString(string ls)
    {
        string con_str = "";
        try
        {
            con_str = ConfigurationManager.ConnectionStrings[ls].ToString();
        }

        catch (Exception ex) { Gen_Error_Rpt.Write_Error("GetConnectionString : ", ex); }
        return con_str;
    }
    /// <summary>
    /// Execute Select Query
    /// </summary>
    /// <param name="_query"></param>
    /// <returns></returns>
    public ReturnClass.ReturnDataTable executeSelectQuery(String _query)
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();

        try
        {

            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        Adapter.Fill(dt.table);
                        dt.status = true;
                        dt.value = "";
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            
                Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ex);
            
            dt.status = false;
            dt.message = ex.Message;
        }
        return dt;
    }
    public ReturnClass.ReturnDataTable executeSelectQuery(String _query, String ls)
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();

        try
        {
            string con_str1 = GetConnectionString(ls);
            using (MySqlConnection con = new MySqlConnection(con_str1))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        Adapter.Fill(dt.table);
                        dt.status = true;
                        dt.value = "";
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
           
                Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ex);
            
            dt.status = false;
            dt.message = ex.Message;
        }
        return dt;
    }
    /// <summary>
    /// Execute Select Query With Parameters
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public ReturnClass.ReturnDataTable executeSelectQuery(String _query, MySqlParameter [] sqlParameter)
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        try
        {

            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(sqlParameter);

                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        Adapter.Fill(dt.table);
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ec)
        {
            
                Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ec);
           
            dt.status = false;
            dt.message = ec.Message;
        }
        return dt;
    }
    /// <summary>
    /// Execute Select Query With Parameters using DataSet
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public ReturnClass.ReturnDataSet executeSelectQuery_dataset(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnClass.ReturnDataSet dt = new ReturnClass.ReturnDataSet();
        try
        {

            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(sqlParameter);

                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        Adapter.Fill(dt.dataset);
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ec)
        {

            Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ec);

            dt.status = false;
            dt.message = ec.Message;
        }
        return dt;
    }
    public ReturnClass.ReturnDataTable executeSelectQuery(String _query, MySqlParameter [] sqlParameter, String ls)
    {
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        try
        {
            string con_str1 = GetConnectionString(ls);
            using (MySqlConnection con = new MySqlConnection(con_str1))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(sqlParameter);

                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        Adapter.Fill(dt.table);
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ec)
        {
           
                Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ec);
            
            dt.status = false;
            dt.message = ec.Message;
        }
        return dt;
    }
    /// <summary>
    /// Execute Insert Query
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public ReturnClass.ReturnBool executeInsertQuery(String _query, MySqlParameter [] sqlParameter)
    {
        ReturnClass.ReturnBool dt = new ReturnClass.ReturnBool();
        try
        {

            using (Adapter = new MySqlDataAdapter())
            {
                using (MySqlConnection con = new MySqlConnection(con_str))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = _query;
                        cmd.Parameters.AddRange(sqlParameter);
                        Adapter.InsertCommand = cmd;
                        cmd.ExecuteNonQuery();
                        
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ex2)
        {
           
                Gen_Error_Rpt.Write_Error("executeInsertQuery - Query: " + _query + "\n   error - ", ex2);
          
            dt.status = false;
            dt.message = ex2.Message;
        }
        return dt;
    }

    public ReturnClass.ReturnBool executeInsertQuery(String _query, MySqlParameter [] sqlParameter, String ls)
    {
        ReturnClass.ReturnBool dt = new ReturnClass.ReturnBool();
        try
        {
            string con_str1 = GetConnectionString(ls);
            using (Adapter = new MySqlDataAdapter())
            {
                using (MySqlConnection con = new MySqlConnection(con_str1))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = _query;
                        cmd.Parameters.AddRange(sqlParameter);
                        Adapter.InsertCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ex2)
        {
           
                Gen_Error_Rpt.Write_Error("executeInsertQuery - Query: " + _query + "\n   error - ", ex2);
           
            dt.status = false;
            dt.message = ex2.Message;
        }
        return dt;
    }
    /// <summary>
    /// Execute Update Query
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public ReturnClass.ReturnBool executeUpdateQuery(String _query, MySqlParameter [] sqlParameter)
    {
        ReturnClass.ReturnBool dt = new ReturnClass.ReturnBool();
        try
        {
            using (Adapter = new MySqlDataAdapter())
            {
                using (MySqlConnection con = new MySqlConnection(con_str))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = _query;
                        cmd.Parameters.AddRange(sqlParameter);
                        Adapter.UpdateCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exu)
        {
          
                Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exu);
         
            dt.status = false;
            dt.message = exu.Message;
        }
        return dt;
    }

    public ReturnClass.ReturnBool executeUpdateQuery(String _query, MySqlParameter [] sqlParameter, String ls)
    {
        ReturnClass.ReturnBool dt = new ReturnClass.ReturnBool();
        try
        {
            string con_str1 = GetConnectionString(ls);
            using (Adapter = new MySqlDataAdapter())
            {
                using (MySqlConnection con = new MySqlConnection(con_str1))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = _query;
                        cmd.Parameters.AddRange(sqlParameter);
                        Adapter.UpdateCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exu)
        {
           
                Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exu);
          
            dt.status = false;
            dt.message = exu.Message;
        }
        return dt;
    }
    public ReturnClass.ReturnBool executeDeleteQuery(String _query, MySqlParameter [] sqlParameter)
    {
        ReturnClass.ReturnBool dt = new ReturnClass.ReturnBool();
        try
        {
            using (Adapter = new MySqlDataAdapter())
            {
                using (MySqlConnection con = new MySqlConnection(con_str))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = _query;
                        cmd.Parameters.AddRange(sqlParameter);
                        Adapter.DeleteCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exp)
        {
           
                Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exp);
           
            dt.status = false;
            dt.message = exp.Message;
        }
        return dt;
    }

    public ReturnClass.ReturnBool executeDeleteQuery(String _query, MySqlParameter [] sqlParameter, String ls)
    {
        ReturnClass.ReturnBool dt = new ReturnClass.ReturnBool();
        try
        {
            string con_str1 = GetConnectionString(ls);
            using (Adapter = new MySqlDataAdapter())
            {
                using (MySqlConnection con = new MySqlConnection(con_str1))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = _query;
                        cmd.Parameters.AddRange(sqlParameter);
                        Adapter.DeleteCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exp)
        {
         
                Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exp);
         
            dt.status = false;
            dt.message = exp.Message;
        }
        return dt;
    }
}