using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class office_print : System.Web.UI.Page
{
    dl_Dio dl = new dl_Dio();
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    bl_Dio bl = new bl_Dio();
     
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["OFFICE_ID"] = "2201000001";
        approve_farm();
    }
    void approve_farm()
    {
        bl.Officeid = Session["office_print"].ToString();
        rd = dl.select_unvalidate_office(bl);
        if (rd.table.Rows.Count > 0)
        {

            StringBuilder strtbl = new StringBuilder();
            //StringBuilder strtbl = new StringBuilder();
            strtbl.AppendLine("<div style=\"  width:1100px; border:2px solid #000000;  padding-left:5px; \" >");
            strtbl.AppendLine("<table width=1100px style=table-layout:fixed; text-align:center; >");
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<th colspan=2><span style=\"font-family: 'Times New Roman'; text-align:center; font-size:30px; font-weight:bold\"> Online RTI Portal </span> </th>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + "<span style=\"font-family: 'Times New Roman'; text-align:center; font-size:25px; font-weight:bold\"> General Administration Department </span> " + " </td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + "<span style=\"font-family: 'Times New Roman'; text-align:center; font-size:25px; font-weight:bold\">  Mantralaya, Raipur  </span></br> </td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("</table>");

            strtbl.AppendLine("<table width=1100px style=table-layout:fixed; >");
            strtbl.AppendLine("<tr >");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana';  font-size:15px;font-weight:bold ;  text-align:center;\"> ");
            strtbl.AppendLine("</br></br> Request Form For Office Enrollement on RTI Portal Chhattishgarh </br> </br>");
            strtbl.AppendLine("</td>");
            strtbl.AppendLine("</tr>");

            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana';text-align:center; font-size:17px;font-weight:bold; \" >  Office Details </br> ");
            strtbl.AppendLine("</td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana';  font-size:15px;font-weight:200; \"></br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; We want to enroll Our office on RTI Portal of Government of Chhatishgarh our Details are as Below. </br> </br>");
            strtbl.AppendLine(" </td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b> Department</b> </td>");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("" + rd.table.Rows[0]["dept_name"].ToString() + "</td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b> District </b></td>");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("" + rd.table.Rows[0]["District_Name"].ToString() + "</td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("<b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Office Level</b> </td>");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("" + rd.table.Rows[0]["OfficeLevelName"].ToString() + "</td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Office Category</b> </td>");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("" + rd.table.Rows[0]["OfficeCategoryName"].ToString() + "</td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Office Name</b></td>");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("" + rd.table.Rows[0]["OfficeName"].ToString() + " </td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Address</b> </td>");
            strtbl.AppendLine("<td colspan:1;>");
            strtbl.AppendLine("" + rd.table.Rows[0]["OfficeAddress"].ToString() + " </td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
            strtbl.AppendLine("<td colspan:1;>");
            if (rd.table.Rows[0]["ContactNo"].ToString() != "" && rd.table.Rows[0]["ContactNo"] != null && rd.table.Rows[0]["ContactNo"].ToString() != "NULL")
            {
                strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Contact Number</b> </td>");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>" + rd.table.Rows[0]["ContactNo"].ToString() + "</b></td>");
                strtbl.AppendLine("</tr>");
            }
            else
            {
                strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Contact Number</b> </td>");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b> Not Available</b></td>");
                strtbl.AppendLine("</tr>");
            }
            if (rd.table.Rows[0]["Fax"].ToString() != "" && rd.table.Rows[0]["Fax"] != null && rd.table.Rows[0]["Fax"].ToString() != "NULL")
            {
                strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax </b></td>");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>" + rd.table.Rows[0]["Fax"].ToString() + "</b></td>");
                strtbl.AppendLine("</tr>");
            }
            else
            {
                strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax </b></td>");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>Not Available</b> </td>");
                strtbl.AppendLine("</tr>");
            }
            if (rd.table.Rows[0]["Email"].ToString() != "" && rd.table.Rows[0]["Email"] != null && rd.table.Rows[0]["Email"].ToString() != "NULL")
            {
                strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Email Address</b> </td>");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>" + rd.table.Rows[0]["Email"].ToString() + "</b></td>");
                strtbl.AppendLine("</tr>");
            }
            else
            {
                strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Email Address</b> </td>");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b> Not Available</b></td>");
                strtbl.AppendLine("</tr>");
            }
            if (rd.table.Rows[0]["OfficeURL"].ToString() != "" && rd.table.Rows[0]["OfficeURL"] != null && rd.table.Rows[0]["OfficeURL"].ToString() != "NULL")
            {
                strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Office Website</b> </td>");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>" + rd.table.Rows[0]["OfficeURL"].ToString() + "</b></td>");
                strtbl.AppendLine("</tr>");
            }
            else
            {
                strtbl.AppendLine("<tr style=\"font-family: 'Verdana';  font-size:15px;font-weight:200;  \">");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Office Website</b> </td>");
                strtbl.AppendLine("<td colspan:1;>");
                strtbl.AppendLine("<b> Not Available</b></td>");
                strtbl.AppendLine("</tr>");
            }
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana'; font-size:15px;font-weight:200; \">");
            strtbl.AppendLine("</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; I here by declere that i have read all the documents,<b> terms and conditions </b> carefully. All information given are true in all my Knowledge. </td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana';text-align:center; font-size:17px;font-weight:bold; \" ></br> Applicant Detail  ");
            strtbl.AppendLine("</td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana'; font-size:17px;font-weight:200; \" ></br> </br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Place &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Name and Signature  ");
            strtbl.AppendLine("</td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana'; font-size:17px;font-weight:200; \" >  </br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  ");
            strtbl.AppendLine("</td>");
            strtbl.AppendLine("</tr>");

            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana';text-align:center; font-size:17px;font-weight:bold; \" ></br></br>  Competent Authority  ");
            strtbl.AppendLine("</td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana'; font-size:17px;font-weight:200; \" ></br></br>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Place &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Signature and Stamp  ");
            strtbl.AppendLine("</td>");
            strtbl.AppendLine("</tr>");
            strtbl.AppendLine("<tr>");
            strtbl.AppendLine("<td colspan=2; style=\"font-family: 'Verdana'; font-size:17px;font-weight:200; \" >  </br>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name  ");
            strtbl.AppendLine("</br> </br> </br> </br> </td>");
            strtbl.AppendLine("</tr>");

            strtbl.AppendLine("</table>");
            strtbl.AppendLine("</div >");

            bl.File_data = strtbl.ToString();
            bl.Officeid = Session["office_print"].ToString();
            rb = dl.insert_ofc_farm(bl);
            if (rb.status == true)
            {
                ltr_farm.Text = strtbl.ToString();
            }
        }
    }
}