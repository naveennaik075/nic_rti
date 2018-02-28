using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Transactions;


/// <summary>
/// Summary description for dl_login
/// </summary>
/// 

public class dl_login : ReturnClass
{
    ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
    ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
    db_maria_connection db = new db_maria_connection();

    public ReturnClass.ReturnDataTable Select_Rti_Count(bl_login bl)
    {
        string str = @"select count(rs.rti_id) as count1 from rti_status rs
                        inner join rti_detail rdtl on rdtl.rti_id= rs.rti_id
                        where rdtl.User_ID=@userid and rs.IsValid='Y' and rs.Payment_status='Y' 
                        union all
                        select count(rs.rti_id) as count1 from rti_status rs 
                        inner join rti_detail rdtl on rdtl.rti_id= rs.rti_id
                        where rdtl.User_ID=@userid and rs.IsValid='Y' and rs.Payment_status='Y' and rs.`status`='CLT' 
                        union all
                        select count(rs.rti_id) as count1 from rti_status rs 
                        inner join rti_detail rdtl on rdtl.rti_id= rs.rti_id
                        where rdtl.User_ID=@userid and rs.IsValid='Y' and rs.Payment_status='Y' and (rs.`status`='PEN' or rs.`status`='REG')
                        union all
                        select count(rs.rti_id) as count1 from rti_status rs 
                        inner join rti_detail rdtl on rdtl.rti_id= rs.rti_id
                        where rdtl.User_ID=@userid and rs.IsValid='Y' and rs.Payment_status='Y' and rs.`status`='REJD'
                        union all
                        select count(rs.rti_id) as count1 from rti_status rs 
                        inner join rti_detail rdtl on rdtl.rti_id= rs.rti_id
                        where rdtl.User_ID=@userid and rs.IsValid='Y' and rs.Payment_status='Y' and rs.`status`='CLR' ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("userid",bl.UserID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }

    public ReturnClass.ReturnDataTable Select_Rti_By_User1(bl_login bl)
    {
        string where = "  where rdtl.User_ID=@userid and rs.IsValid='Y' and rs.Payment_status='Y' ";
        if (bl.Status == "CLT")
        {
          where += " and rs.`status`='CLT'  " ;
        }
        else if (bl.Status == "PEN" || bl.Status == "REG")
        {
            where += " and (rs.`status`='PEN' or rs.`status`='REG')  ";
        }
        else if (bl.Status == "REJD")
        {
            where += " and rs.`status`='REJD'  ";
        }
        else if (bl.Status == "CLR")
        {
            where += " and rs.`status`='CLR'  ";
        }
        string str = @"select  rs.rti_id as RequestID, rdtl.Applicant_Name_en as ApplicantName, rdtl.rti_Subject as subject, dld.DisplayName_en as status,ra.action_detail as action, emp.Name_en as officer,ofc.OfficeName as Office, bsd.dept_name as department 
                        from rti_status rs
                        inner join emp_office_mapping eom on eom.office_mapping_id= rs.officer_maping_id
                        inner join employee_table emp on emp.emp_id= eom.emp_code 
                        inner join office ofc on ofc.NewOfficeCode= eom.office_id
                        inner join rti_detail rdtl on rdtl.rti_id= rs.rti_id
                        inner join basedepartment bsd on bsd.dept_id= eom.base_department_id
                        left join rti_action ra on ra.action_id= rs.action_id and ra.rti_id= rs.rti_id 
                        inner join ddl_list dld on dld.DDL_Name_Value = rs.`status` ";

        string orderby = " order by RequestID desc ";
        str = str + where + orderby;

        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("userid",bl.UserID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }


    public ReturnClass.ReturnDataTable Select_Rti_By_User(bl_login bl)
    {
        string str = @"select  rs.rti_id as RequestID, rdtl.Applicant_Name_en as ApplicantName, rdtl.rti_Subject as subject, dld.DisplayName_en as status,ra.action_detail as action, emp.Name_en as officer,ofc.OfficeName as Office, bsd.dept_name as department 
from rti_status rs
inner join emp_office_mapping eom on eom.office_mapping_id= rs.officer_maping_id
inner join employee_table emp on emp.emp_id= eom.emp_code 
inner join office ofc on ofc.NewOfficeCode= eom.office_id
inner join rti_detail rdtl on rdtl.rti_id= rs.rti_id
inner join basedepartment bsd on bsd.dept_id= eom.base_department_id
left join rti_action ra on ra.action_id= rs.action_id and ra.rti_id= rs.rti_id 
inner join ddl_list dld on dld.DDL_Name_Value = rs.`status`
where rdtl.User_ID=@userid and rs.IsValid='Y' and rs.Payment_status='Y' order by RequestID desc ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("userid",bl.UserID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public string get_unique_session_id()
    {
        ReturnClass.ReturnDataTable rd = new ReturnClass.ReturnDataTable();
        db_maria_connection db = new db_maria_connection();
        string reg_year = DateTime.Now.Year.ToString();
        string str = "select MAX(SUBSTRING(SessionID,6,10)) as ID from logintrial Where SessionID=@sessionid";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("sessionid",reg_year)
        };
        rd = db.executeSelectQuery(str, pm);
        if (rd.table.Rows.Count > 0)
        {
            if (rd.table.Rows[0]["ID"].ToString() != "")
            {
                int nextId = Convert.ToInt32(rd.table.Rows[0]["ID"].ToString());
                nextId++;
                string lid = Convert.ToString(nextId);
                reg_year = (reg_year + lid.PadLeft(6, '0'));
            }
            else
            {
                reg_year = (reg_year + "000001");

            }
        }
        return reg_year;

    }
    public ReturnClass.ReturnDataTable Select_user_detail(bl_login bl)
    {
        string str = "SELECT RegistrationID, IsValid,UserID,Name_en,EmailID,Address,PinCode,MobileNo,Gender,Registration_Year from user_registration where UserID=@UserID and IsValid='Y' ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("UserID",bl.UserID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable Select_user_LoginTime(bl_login bl)
    {
        string str = @"select logintime from logintrial lt where lt.SuccessFull_Login='Y'  and lt.UserID=@UserID order by lt.SessionID desc";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("UserID",bl.UserID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_user(bl_login bl)
    {
        //string str = "select * from login l where l.UserID=@loginid and l.Password = @password";
        string str = @"select l.UserID,t.Role_id,t.WelcomePage,l.LoginID from login l join role_table t on t.Role_id = l.RollID where l.UserID = @loginid and l.Password = @password";


        MySqlParameter[] pm = new MySqlParameter[]
    {
        new MySqlParameter("password",bl.Password),
        new MySqlParameter("loginid",bl.UserID)
    };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }

    public ReturnClass.ReturnDataTable check_login(bl_login bl)
    {
        string str = @"CALL `check_login`(@loginid, @password);";


        MySqlParameter[] pm = new MySqlParameter[]
    {
        new MySqlParameter("password",bl.Password),
        new MySqlParameter("loginid",bl.UserID)
    };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }



    public ReturnDataTable select_empmap(bl_login bl)
    {
        return rd;
    }
    public ReturnClass.ReturnDataTable select_emp(bl_login bl)
    {

        string str = @" select distinct  e.role_id, l.UserID, rt.WelcomePage  from emp_office_mapping e
 join login l on l.LoginID= e.emp_code
 join role_table rt on rt.Role_id= e.role_id  where l.UserID=@loginid and  e.active=@active ";
        MySqlParameter[] pm = new MySqlParameter[]
    {
        //new MySqlParameter("password",bl.Password),
           new MySqlParameter("active","Y"),
        new MySqlParameter("loginid",bl.UserID)
    };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_deg_emp(bl_login bl)
    {
        string str = @"select  e.office_mapping_id as ID ,concat( dg.Designation_Name ,'/',ofc.OfficeName,'/',dic.District_Name_En  ) as name,
rt.WelcomePage as page from emp_office_mapping e
 join login l on l.LoginID= e.emp_code
 inner join office ofc on ofc.NewOfficeCode=e.office_id and ofc.BaseDeptCode=e.base_department_id and ofc.DistrictCodeNew=e.district_id_ofc
 inner join designation dg on dg.Designation_ID=e.designation_id
 inner join districts dic on dic.District_ID=e.district_id_ofc
 inner join role_table rt on rt.Role_id= e.role_id
  where l.UserID=@loginid and  e.active=@active and e.role_id= @role_id ";
        MySqlParameter[] pm = new MySqlParameter[]
    {
        new MySqlParameter("role_id",bl.RollID),
           new MySqlParameter("active","Y"),
        new MySqlParameter("loginid",bl.UserID)
    };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_roll_emp(bl_login bl)
    {

        string str = @" select distinct  e.role_id as id, l.UserID, rt.WelcomePage as page, rt.RoleName as name from emp_office_mapping e
 join login l on l.LoginID= e.emp_code
 join role_table rt on rt.Role_id= e.role_id  where l.UserID=@loginid and  e.active=@active ";
        MySqlParameter[] pm = new MySqlParameter[]
    {
        //new MySqlParameter("password",bl.Password),
           new MySqlParameter("active","Y"),
        new MySqlParameter("loginid",bl.UserID)
    };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_welcomepage(bl_login bl)
    {
        string str = @"  select rd.WelcomePage as page from role_table rd where rd.Role_id=@roleid ";
        MySqlParameter[] pm = new MySqlParameter[]
    {
        new MySqlParameter("roleid",bl.RollID)
    };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnBool update_password(bl_login bl)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            string str = @"insert into login_log select * from login WHERE login.UserID =@regid1";
            MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("regid1",bl.RegistrationID)
        };
            rb = db.executeInsertQuery(str, pm);
            if (rb.status == true)
            {
                string str2 = @"update login set Password=@Password , Client_ip=@Client_ip, ClientOS=@ClientOS,
ClientBrowser=@ClientBrowser, user_agent=@user_agent , PasswordChange=@PAssworchng where UserID=@regid and Password=@pass";
                MySqlParameter[] pm2 = new MySqlParameter[]{
                            new MySqlParameter("Password",bl.NewPass),
                            new MySqlParameter("PAssworchng",bl.PasswordChange),
                            new MySqlParameter("regid",bl.RegistrationID),
                            new MySqlParameter("Client_ip",bl.UserIP),
                            new MySqlParameter("ClientOS",bl.UserOS),
                            new MySqlParameter("ClientBrowser",bl.User_browser ),
                            new MySqlParameter("user_agent",bl.UserAgent),
                            new MySqlParameter("pass",bl.Password)
                        };
                rb = db.executeUpdateQuery(str2, pm2);
                if (rb.status == true)
                {
                    ts.Complete();
                }
            }

        }
        return rb;
    }
    public ReturnClass.ReturnBool insert_logintrail(bl_login bl)
    {
        string str = @"insert into logintrial(UserID,Client_OS,ClientBrowser,UserAgent,SuccessFull_Login,UserIP)
values (@userid,@clientos,@clientbrowser,@useragent,@succesful_login,@userip)";
        MySqlParameter[] pm = new MySqlParameter[]
    {
        new MySqlParameter("sessionid",bl.SesssionId),
        new MySqlParameter("userid",bl.UserID),
        new MySqlParameter("clientos",bl.UserOS),
        new MySqlParameter("clientbrowser",bl.User_browser),
        new MySqlParameter("useragent",bl.UserAgent),
        new MySqlParameter("succesful_login",bl.Succesful_login),
        new MySqlParameter("userip",bl.UserIP)
    };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }
    public ReturnClass.ReturnDataTable select_role(bl_login bl)
    {
        string str = "select Rolename,WelcomePage from role_table where Role_id=@RollID";
        MySqlParameter[] pm = new MySqlParameter[]
    {
        new MySqlParameter("RollID",bl.RollID)
    };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable Select_Rti_Action_detail(bl_login bl)
    {
        string str = @"select dl.DisplayName_en as status ,rta.action_detail as action, et.Name_en as nameofficer, 
deg.Designation_Name as namedeg, ofc.OfficeName as office, bde.dept_name as department ,
DATE_FORMAT(rta.action_date,'%d/%M/%y') as action_date ,dl.DisplayName_en
from rti_status rs
inner  join rti_action rta on rta.rti_id= rs.rti_id
INNER join emp_office_mapping eop on eop.office_mapping_id= rta.officer_mapping_id
inner join employee_table et on et.emp_id	= eop.emp_code
inner join office ofc on ofc.NewOfficeCode= eop.office_id
inner join basedepartment bde on bde.dept_id = eop.base_department_id
inner join designation deg on deg.Designation_ID = eop.designation_id
inner join ddl_list dl on dl.DDL_Name_Value= rs.`status`
where rs.rti_id=@userid and rs.`status`='CLT'
 order by rta.action_id DESC";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("userid",bl.RTIID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_password_log(bl_login bl)
    {
        string str = "select * from login_log where UserID=@login and Password=@password";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("login",bl.UserID),
            new MySqlParameter("password",bl.NewPass)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_password_by_username(bl_login bl)
    {
        string str = "Select Password from login where UserId=@userid";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("userid",bl.UserID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable Select_rti_action_detail(bl_login bl)
    {
        //action_detail
        string str = @" select * from rti_action where rti_id=@rtiid 
                        and action_id = ( select  action_id from rti_status where rti_id=@rtiid) ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("rtiid",bl.RegistrationID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable Select_unique_rti_detail(bl_login bl)
    {
        string str = @"select  rd.Applicant_Name_en as applicant, ddl.DisplayName_en as applicant_type,dl1.DisplayName_en as status,
dl.DisplayName_en as gender, rd.Mobile_No as mobile,rd.Email_ID as email, rd.rti_Subject as subject,rd.rti_Details as detail, 
rd.Address as address, dic.District_Name_En as district, st.state_name_e as statename,co.name as country,
rd.Country_Name as o_country,rd.Pin_Code as pincode, rf.fileName as filename ,rf.FileDescription as filedesc,
rf.fileType as filetype, rf.fileData as filedata ,date_format(dat.meeting_date,'%d/%M/%Y') as meetingdate ,
rdf.result_description as result,rdf.IsMeetingFix as meeting_req, rdf.IsAdditionalFees as fees_req, rdf.FeesAmount as fees,
date_format(rdf.EntryDateTime,'%d/%m/%Y') as strdate,
rdf.FileDescription as act_fl_desc, rdf.IsFileUpload as upld, rdf.FileData, rdf.FileType,rdf.FileName as upld_fl_name
  from rti_detail rd
left join rti_files rf on rf.rti_id= rd.rti_id
left join ddl_list ddl on ddl.DDL_Name_Value = rd.applicant_type
left join districts dic on dic.District_ID = rd.District_Code
left join state st on st.state_id = rd.State_Code
left join ddl_list dl on dl.DDL_Name_Value = rd.Gender
left join countries co on co.id= rd.Country_Code
INNER JOIN rti_status rs on rs.rti_id = rd.rti_id
inner join ddl_list dl1 on dl1.DDL_Name_Value=rs.`status`
left join date_slot_for_meeting dat on dat.Rti_id = rs.rti_id
left join rti_data_for_applicant rdf on rdf.rti_id=rs.rti_id
where rd.rti_id =@rtiid and rs.IsValid='Y'  ";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("rtiid",bl.RegistrationID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_rti_files_detail(bl_login bl)
    {
        string str = @"select * from rti_Files rf where rf.rti_id=@rti_id and rf.BPL_RTI_FileType ='RTI_DOC'";
        MySqlParameter[] pm = new MySqlParameter[]{
            new MySqlParameter("rti_id",bl.RegistrationID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnClass.ReturnDataTable select_result_file(bl_login bl)
    {
        string str = "select rd.FileName,rd.FileData,rd.FileType from rti_data_for_applicant rd where rd.rti_id=@rtiid";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("rtiid",bl.RegistrationID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
       
    public ReturnClass.ReturnDataTable get_emp()
    {
        string str = @"select emp_id, Name_en from employee_table ";
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable get_role(bl_user_login bl)
    {
        string where = "";
        if (bl.Role == "4" || bl.Role == "5")
        {
            where += " where Role_id='3' ";   // For dip and nodel select only employee
        }
        string str = @" select Role_id, RoleName from role_table " + where;
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable get_login_data()
    {
        string str = @"select lo.UserID, lo.LoginID, emp.Name_en,IF(lo.PasswordChange = 'Y', 'YES', 'No') as PasswordChange, IF(lo.active = 'Y', 'YES', 'No') as Active from login lo
                       inner  join employee_table emp on emp.emp_id=lo.LoginID  ";
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable detailsfor_meeting(bl_login bl)
    {
        string str = @"select rs.rti_id as rti, em.office_id as office, date_format(rs.status_date_time, '%Y/%m/%d') as startdate from rti_status rs
 inner join emp_office_mapping em on em.office_mapping_id = rs.officer_maping_id
 where rs.rti_id =@rti";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("rti", bl.RTIID)
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }
    public ReturnBool insertmeetdate(bl_login bl)
    {
        string str = @"insert into date_slot_for_meeting (Rti_id,office_code,UserId,meeting_date,client_browser,clientip,client_os,useragent) values(@Rti_id,@office_code,@UserId,@meeting_date,@client_browser,@clientip,@client_os,@useragent)";
        MySqlParameter[] pm = new MySqlParameter[]
        {
           new MySqlParameter("Rti_id",bl.RTIID),
            new MySqlParameter("office_code",bl.Office),
            new MySqlParameter("UserId",bl.UserID),
            new MySqlParameter("meeting_date",bl.Meetingdate),
            new MySqlParameter("client_browser",bl.User_browser),
            new MySqlParameter("clientip",bl.UserIP),
            new MySqlParameter("client_os",bl.UserOS),
            new MySqlParameter("useragent",bl.UserAgent)
        };
        rb = db.executeInsertQuery(str, pm);
        return rb;
    }
    public ReturnClass.ReturnDataTable get_count_slot(bl_login bl)
    {
        string str = @"select ds.Rti_id as rti from date_slot_for_meeting ds where date_format(ds.meeting_date,'%m/%d/%Y') = @meetingdate and ds.office_code=@officecode";
        MySqlParameter[] pm = new MySqlParameter[]
        {
            new MySqlParameter("meetingdate",bl.Meetingdate),
            new MySqlParameter("officecode",bl.Office),
        };
        rd = db.executeSelectQuery(str, pm);
        return rd;
    }


    //-----------------------neha 28 march------------login page report---//
    public ReturnClass.ReturnDataTable countEmp(bl_Dio bl)
    {
        string str = "", where = "   where 1 = 1   ";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
            if (bl.State != "" && bl.State != null && bl.State != "0" && bl.State != "Select")
            {
                MySqlParameter da = new MySqlParameter("state", bl.State);
                pm.Add(da);
                where += "   and state_id= @state  ";
            }
            if (bl.District != "0" && bl.District != null && bl.District != "" && bl.District != "Select")
            {
                MySqlParameter ba = new MySqlParameter("district", bl.District);
                pm.Add(ba);
                where += "   and district_id = @district   ";
            }
            if (bl.Role == "5")
            {
                MySqlParameter ba = new MySqlParameter("ofc_code", bl.Officeid);
                pm.Add(ba);
                where += "   and NewOfficeCode = @ofc_code   ";
            }


            str = @" select count(Name_en) as empcount from employee_table " + where;

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;

    }

    public ReturnClass.ReturnDataTable countDepartment(bl_Dio bl)
    {
        string str = "select count(dept_name) as depcount from basedepartment";
        rd = db.executeSelectQuery(str);
        return rd;
    }
    public ReturnClass.ReturnDataTable countOffice(bl_Dio bl)
    {
        string str = "", where = "   where 1 = 1   ";
        try
        {
            List<MySqlParameter> pm = new List<MySqlParameter>();
            if (bl.State != "" && bl.State != null && bl.State != "0" && bl.State != "Select")
            {
                MySqlParameter da = new MySqlParameter("state", bl.State);
                pm.Add(da);
                where += "   and ofc.StateCode= @state  ";
            }
            if (bl.District != "0" && bl.District != null && bl.District != "" && bl.District != "Select")
            {
                MySqlParameter ba = new MySqlParameter("district", bl.District);
                pm.Add(ba);
                where += "   and ofc.DistrictCodeNew = @district   ";
            }
            if (bl.Role == "4")
            {
                MySqlParameter ba = new MySqlParameter("officeLevelType", bl.Officelevelcode);
                pm.Add(ba);
                where += "   and ol.OfficeLevelType = @officeLevelType   ";
            }

            if (bl.Role == "5")
            {
                MySqlParameter ba = new MySqlParameter("officeid", bl.Officeid);
                pm.Add(ba);
                where += "   and ofc.NewOfficeCode = @officeid   ";
            }

            str = @" select count(*) as office_count from office ofc
                       inner join districts dic on  dic.StateCode=ofc.StateCode and dic.district_id = ofc.DistrictCodeNew 
                       inner join basedepartment bd on bd.dept_id = ofc.BaseDeptCode
                       inner join officelevel ol on ol.OfficeLevelCode = ofc.OfficeLevel  and ol.StateCode= ofc.StateCode  and ol.BaseDeptCode=ofc.BaseDeptCode "
                       + where;

            rd = db.executeSelectQuery(str, pm.ToArray());
        }
        catch (Exception ex)
        {
            Gen_Error_Rpt.Write_Error("../dio/dl_Dio/Bind_Grid log.txt");

        }

        return rd;


    }
    public ReturnClass.ReturnDataTable countOfficer(bl_Dio bl)
    {

        string str = @" select count(*) as officer_count from employee_table emp inner join emp_office_mapping eom on eom.emp_code=emp.emp_id where eom.designation_id='2'";


        rd = db.executeSelectQuery(str);



        return rd;


    }
}