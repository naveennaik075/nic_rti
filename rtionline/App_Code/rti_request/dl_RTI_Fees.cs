using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;


/// <summary>
/// Summary description for dl_RTI_Fees
/// </summary>
public class dl_RTI_Fees
{
	public dl_RTI_Fees()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    
    #region
    /// <summary>
    /// Aim of method: The Method is used to get amount of e-challan details 
    /// Database Table :society_aims
    /// bl layer use:bl_society_fee- (MEMORANDUM_OF_SOCIETY_From_1_Gridview3())
    /// </summary>
    /// <returns></returns>
    public ReturnClass.ReturnDataTable getamount(bl_RTI_Fees reg)
    {
        ReturnClass.ReturnDataTable rb = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {
            string qr = "select SUM(CONVERT(amount, INT)) as Fees from echallan_detail where RTI_provisional_no=@RTI_provisional_no ";
            MySqlParameter[] pr = new MySqlParameter[]{
            new MySqlParameter("RTI_provisional_no",reg.RTI_provisional_no)
            };
            rb = db.executeSelectQuery(qr, pr);

        }
        catch (Exception Ex)
        {
            rb.status = false;
            rb.message = Ex.Message;
        }


        return rb;
    }

    
    #endregion
    //#region
    ///// <summary>
    ///// Aim of method: The Method is used to get fee from society_fees_master 
    ///// Database Table :society_aims
    ///// bl layer use:bl_society_fee- (MEMORANDUM_OF_SOCIETY_From_1_Gridview3())
    ///// </summary>
    ///// <returns></returns>
    //public ReturnClass.ReturnDataTable getfee(bl_RTI_Fees reg)
    //{
    //    ReturnClass.ReturnDataTable rb = new ReturnClass.ReturnDataTable();
    //    db_maria_connection db = new db_maria_connection();
    //    try
    //    {
    //        string qr = "select fees_code,fees_details,Amount from society_fees_master where society_type_code=(select society_type_code from society where RTI_provisional_no=@RTI_provisional_no) ";
    //        MySqlParameter[] pr = new MySqlParameter[]{
    //        new MySqlParameter("RTI_provisional_no",reg.RTI_provisional_no)
    //        };
    //        rb = db.executeSelectQuery(qr, pr);

    //    }
    //    catch (Exception Ex)
    //    {
    //        rb.status = false;
    //        rb.message = Ex.Message;
    //    }


    //    return rb;
    //}

    //#endregion
    #region
    /// <summary>
    /// Aim of method: The Method is used to get record from e-challan
    /// Database Table :society_aims
    /// bl layer use:bl_society_fee- (MEMORANDUM_OF_SOCIETY_From_1_Gridview3())
    /// </summary>
    /// <returns></returns>
    public ReturnClass.ReturnDataTable getrecord(bl_RTI_Fees reg)
    {
        ReturnClass.ReturnDataTable rb = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {
            string qr = "select * from echallan_detail where RTI_provisional_no=@RTI_provisional_no  order by serial_no";
            MySqlParameter[] pr = new MySqlParameter[]{
            new MySqlParameter("RTI_provisional_no",reg.RTI_provisional_no)
            };
            rb = db.executeSelectQuery(qr, pr);

        }
        catch (Exception Ex)
        {
            rb.status = false;
            rb.message = Ex.Message;
        }


        return rb;
    }

    #endregion
    #region
    /// <summary>
    /// Aim of method: The Method is used to get serial no 
    /// Database Table :society_aims
    /// bl layer use:bl_society_fee- (MEMORANDUM_OF_SOCIETY_From_1_Gridview3())
    /// </summary>
    /// <returns></returns>
    public ReturnClass.ReturnDataTable serial_no(bl_RTI_Fees reg)
    {
        ReturnClass.ReturnDataTable rb = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {
            string qr = "select max(CONVERT(serial_no,INT))as ID from echallan_detail where RTI_provisional_no=@RTI_provisional_no";
            MySqlParameter[] pr = new MySqlParameter[]{
            new MySqlParameter("RTI_provisional_no",reg.RTI_provisional_no)
            };
            rb = db.executeSelectQuery(qr, pr);

        }
        catch (Exception Ex)
        {
            rb.status = false;
            rb.message = Ex.Message;
        }


        return rb;
    }

    #endregion
    #region
    /// <summary>
    /// Aim of method: The Method is used to delete e-challan from e-challan 
    /// Database Table :society_aims
    /// bl layer use:bl_society_fee- (MEMORANDUM_OF_SOCIETY_From_1_Gridview3())
    /// </summary>
    /// <returns></returns>
    public ReturnClass.ReturnBool delete_echallan(bl_RTI_Fees reg)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        try
        {
            using (TransactionScope ts = new TransactionScope())
            {
                MySqlParameter[] pr = new MySqlParameter[]{
            new MySqlParameter("RTI_provisional_no",reg.RTI_provisional_no),
              new MySqlParameter("serial_no",reg.Serial_no)
          };
                MySqlParameter[] pr1 = new MySqlParameter[]{
            new MySqlParameter("RTI_provisional_no",reg.RTI_provisional_no),
              new MySqlParameter("serial_no",reg.Serial_no)
            };

                string qr = "insert into echallan_detail_log select * from echallan_detail where RTI_provisional_no=@RTI_provisional_no and serial_no=@serial_no ";

                rb = db.executeInsertQuery(qr, pr);

                if (rb.status == true)
                {
                    string qrr = "delete from echallan_detail where RTI_provisional_no=@RTI_provisional_no and serial_no=@serial_no";
                    rb = db.executeDeleteQuery(qrr, pr1);
                }

                if (rb.status == true)
                {
                    ts.Complete();
                    rb.status = true;
                }
            }
        }
        catch (Exception Ex)
        {
            rb.status = false;
            rb.message = Ex.Message;
        }
        return rb;
    }

    #endregion
    #region
    /// <summary>
    /// Aim of method: The Method is used to select_tr_ref_no from e-challan 
    /// Database Table :society_aims
    /// bl layer use:bl_society_fee- (MEMORANDUM_OF_SOCIETY_From_1_Gridview3())
    /// </summary>
    /// <returns></returns>
    public ReturnClass.ReturnDataTable select_tr_ref_no(echallan ec)
    {
        ReturnClass.ReturnDataTable rb = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        try
        {
            string qr = "select tr_ref_no from echallan_detail where tr_ref_no=@tr_ref_no";
            MySqlParameter[] pr = new MySqlParameter[]{
            new MySqlParameter("tr_ref_no",ec.Tr_ref_no)
            };
            rb = db.executeSelectQuery(qr, pr);

        }
        catch (Exception Ex)
        {
            rb.status = false;
            rb.message = Ex.Message;
        }


        return rb;
    }

    #endregion
    #region
    /// <summary>
    /// Aim of method: The Method is used to Insert into  e-challan 
    /// Database Table :society_aims
    /// bl layer use:bl_society_fee- (MEMORANDUM_OF_SOCIETY_From_1_Gridview3())
    /// </summary>
    /// <returns></returns>
    public ReturnClass.ReturnBool insert_echallan_details(echallan ec)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        try
        {
            using (TransactionScope ts = new TransactionScope())
            {
                MySqlParameter[] pr1 = new MySqlParameter[]{

             new MySqlParameter("RTI_provisional_no",ec.Society_provisional_no), // Need to change by Naveen
            new MySqlParameter("serial_no",ec.Serial_no),
            new MySqlParameter("tr_ref_no",ec.Tr_ref_no),
            new MySqlParameter("amount",ec.Amount),
            new MySqlParameter("tin_cin",ec.Tin_cin),
            new MySqlParameter("tr_entry_date",ec.Tr_entry_date),
            new MySqlParameter("date_ac",ec.Date_ac),
            new MySqlParameter("major_head",ec.Major_head),
            new MySqlParameter("sub_major_head",ec.Sub_major_head),
            new MySqlParameter("minor_head", ec.Minor_head),
            new MySqlParameter("sub_head",ec.Sub_head),
            new MySqlParameter("purpose",ec.Purpose),
            new MySqlParameter("Challan_Category",ec.Challan_Category),
            new MySqlParameter("client_ip",ec.Client_ip),
            new MySqlParameter("user_id",ec.User_id),
            new MySqlParameter("fees_master_code",ec.Fees_master_code)
            };

                string qr = " INSERT INTO echallan_detail(RTI_provisional_no,serial_no,tr_ref_no,amount,tin_cin,tr_entry_date,date_ac,major_head,"
                                                + " sub_major_head,minor_head,sub_head,purpose,Challan_Category, client_ip, user_id,fees_master_code) values"
                                                + " (@RTI_provisional_no,@serial_no,@tr_ref_no,@amount,@tin_cin,@tr_entry_date,@date_ac,@major_head,@sub_major_head,"
                                                + " @minor_head,@sub_head,@purpose,@Challan_Category, @client_ip, @user_id,@fees_master_code)";

                rb = db.executeInsertQuery(qr, pr1);


                if (rb.status == true)
                {
                    ts.Complete();
                }
            }
        }
        catch (Exception Ex)
        {
            rb.status = false;
            rb.message = Ex.Message;
        }
        return rb;
    }

    #endregion
    #region
    /// <summary>
    /// Aim of method: The Method is used to update e-challan 
    /// Database Table :society_aims
    /// bl layer use:bl_society_fee- (MEMORANDUM_OF_SOCIETY_From_1_Gridview3())
    /// </summary>
    /// <returns></returns>
    public ReturnClass.ReturnBool update_echallan_status(bl_RTI_Fees reg)
    {
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        db_maria_connection db = new db_maria_connection();
        try
        {
            using (TransactionScope ts = new TransactionScope())
            {
                MySqlParameter[] pr = new MySqlParameter[]{
            new MySqlParameter("RTI_provisional_no",reg.RTI_provisional_no)
          };
                string qrr = "UPDATE appli_parameter_complete_status SET complete_status='Y' where firm_RTI_provisional_no=@RTI_provisional_no and appli_parameter_code IN ('S12') ";
                    rb = db.executeUpdateQuery(qrr, pr);

                if (rb.status == true)
                {
                    ts.Complete();
                    rb.status = true;
                }
            }
        }
        catch (Exception Ex)
        {
            rb.status = false;
            rb.message = Ex.Message;
        }
        return rb;
    }

    #endregion

} // ENd of Class