using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_RTI_Fees
/// </summary>
public class bl_RTI_Fees
{
	public bl_RTI_Fees()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string rti_provisional_no, fees_id, paid_date, treasury_ref_no, treasury_ref_date, treasury_major_head, treasury_sub_major_head,
        treasury_minor_head, head_description, user_id, client_ip, serial_no;


    float fees_amount;


    public string RTI_provisional_no { get { return rti_provisional_no; } set { rti_provisional_no = value; } }
    public string Serial_no { get { return serial_no; } set { serial_no = value; } }
    public string Fees_id { get { return fees_id; } set { fees_id = value; } }
    public string Paid_date { get { return paid_date; } set { paid_date = value; } }
    public string Treasury_ref_no { get { return treasury_ref_no; } set { treasury_ref_no = value; } }
    public string Treasury_ref_date { get { return treasury_ref_date; } set { treasury_ref_date = value; } }
    public string Treasury_major_head { get { return treasury_major_head; } set { treasury_major_head = value; } }
    public string Treasury_sub_major_head { get { return treasury_sub_major_head; } set { treasury_sub_major_head = value; } }
    public string Treasury_minor_head { get { return treasury_minor_head; } set { treasury_minor_head = value; } }
    public string Head_description { get { return head_description; } set { head_description = value; } }
    public string User_id { get { return user_id; } set { user_id = value; } }
    public string Client_ip { get { return client_ip; } set { client_ip = value; } }


    public float Fees_amount { get { return fees_amount; } set { fees_amount = value; } }

}
public class echallan
{
    string tr_ref_no, serial_no, amount, major_head, sub_major_head, sub_head, purpose, minor_head, fees_master_code;
    string challan_Category, date_ac, tr_entry_date, tin_cin, society_provisional_no, date_Time, client_ip, user_id;

    public string Fees_master_code { get { return fees_master_code; } set { fees_master_code = value; } }
    public string Tr_ref_no { get { return tr_ref_no; } set { tr_ref_no = value; } }
    public string Serial_no { get { return serial_no; } set { serial_no = value; } }
    public string Amount { get { return amount; } set { amount = value; } }
    public string Major_head { get { return major_head; } set { major_head = value; } }
    public string Sub_major_head { get { return sub_major_head; } set { sub_major_head = value; } }
    public string Sub_head { get { return sub_head; } set { sub_head = value; } }
    public string Purpose { get { return purpose; } set { purpose = value; } }
    public string Minor_head { get { return minor_head; } set { minor_head = value; } }
    public string Challan_Category { get { return challan_Category; } set { challan_Category = value; } }
    public string Date_ac { get { return date_ac; } set { date_ac = value; } }
    public string Tr_entry_date { get { return tr_entry_date; } set { tr_entry_date = value; } }
    public string Tin_cin { get { return tin_cin; } set { tin_cin = value; } }
    public string Society_provisional_no { get { return society_provisional_no; } set { society_provisional_no = value; } }
    public string Date_Time { get { return date_Time; } set { date_Time = value; } }
    public string Client_ip { get { return client_ip; } set { client_ip = value; } }
    public string User_id { get { return user_id; } set { user_id = value; } }




}