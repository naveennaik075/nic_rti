-- --------------------------------------------------------
-- Host:                         10.132.36.138
-- Server version:               10.1.23-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for rti_nic_cg
CREATE DATABASE IF NOT EXISTS `rti_nic_cg` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `rti_nic_cg`;

-- Dumping structure for table rti_nic_cg.action_files
CREATE TABLE IF NOT EXISTS `action_files` (
  `action_fileID` varchar(11) COLLATE utf8_unicode_ci NOT NULL,
  `action_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rti_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `fileName` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `fileType` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `fileData` longblob,
  `FileDescription` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `emp_mapping_id` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FileDateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`action_fileID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='it contains actions file information';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.basedepartment
CREATE TABLE IF NOT EXISTS `basedepartment` (
  `dept_id` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `dept_name` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`dept_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for procedure rti_nic_cg.check_login
DELIMITER //
CREATE DEFINER=`root`@`127.0.0.1` PROCEDURE `check_login`(IN `login_id` VARCHAR(50), IN `password` VARCHAR(100))
BEGIN
CREATE TEMPORARY TABLE IF NOT EXISTS tempTable
(
UserID varchar(25),
Name_en varchar(150),
Role_id varchar(10),
WelcomePage varchar(100),
office_mapping_id varchar(12)
) ENGINE=MEMORY;-- Ol User Login-------
insert into tempTable
select distinct l.UserID,ur.Name_en ,rl.Role_id, rl.WelcomePage, '111111111111' as office_mapping_id
from login l
INNER JOIN role_table rl ON rl.Role_id= '2'
INNER jOIN user_registration ur ON ur.RegistrationID=l.LoginID
where l.UserID=login_id COLLATE utf8_unicode_ci and l.Active ='Y' and l.Password=password COLLATE utf8_unicode_ci;

-- Officer Login
insert into tempTable
SELECT l.UserID,emp.Name_en,e.Role_id,r.WelcomePage, e.office_mapping_id as office_mapping_id
FROM login l
INNER JOIN employee_table emp ON emp.emp_id = l.LoginID
inner join emp_office_mapping e on e.emp_code=emp.emp_id and e.active='Y'
inner join office f on f.NewOfficeCode =emp.NewOfficeCode
INNER JOIN role_table r on r.Role_id=e.role_id
where l.UserID =login_id COLLATE utf8_unicode_ci  and l.Password=password COLLATE utf8_unicode_ci ;

select * from tempTable;DROP TEMPORARY Table IF EXISTS tempTable;
END//
DELIMITER ;

-- Dumping structure for table rti_nic_cg.countries
CREATE TABLE IF NOT EXISTS `countries` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sortname` varchar(3) CHARACTER SET utf8 NOT NULL,
  `name` varchar(150) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=247 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.date_slot_for_meeting
CREATE TABLE IF NOT EXISTS `date_slot_for_meeting` (
  `Rti_id` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `office_code` varchar(12) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `UserId` varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `meeting_date` date NOT NULL,
  `client_browser` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `clientip` varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `client_os` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `useragent` varchar(200) NOT NULL,
  `entry_datetime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Rti_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.ddl_list
CREATE TABLE IF NOT EXISTS `ddl_list` (
  `DDL_List_ID` varchar(6) COLLATE utf8_unicode_ci NOT NULL,
  `DisplayName_hi` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DDL_Name_Value` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DisplayName_en` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DisplayOrder` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Remarks` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Category` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`DDL_List_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='To Show in the drop down list ';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.dept_user_manual_file
CREATE TABLE IF NOT EXISTS `dept_user_manual_file` (
  `Dept_User_Manual_ID` int(11) NOT NULL AUTO_INCREMENT,
  `User_ID` varchar(20) DEFAULT '0',
  `base_department_id` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `office_id` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `office_level_id` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `district_id_ofc` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `office_category` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Client_OS` varchar(50) DEFAULT NULL,
  `ClientBrowser` varchar(50) DEFAULT NULL,
  `UserAgent` varchar(500) DEFAULT NULL,
  `Client_IP` varchar(16) DEFAULT NULL,
  `file_name` varchar(50) NOT NULL,
  `content_type` varchar(50) NOT NULL,
  `file_data` longblob NOT NULL,
  `file_description` varchar(100) NOT NULL,
  `Year_Issue` year(4) DEFAULT NULL,
  `entry_datetime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Dept_User_Manual_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='This table will keep the department user manual 17 point ';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.designation
CREATE TABLE IF NOT EXISTS `designation` (
  `Designation_ID` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `Designation_Name` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`Designation_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='this table will keep information about designation';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.districts
CREATE TABLE IF NOT EXISTS `districts` (
  `CCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ST_CODE` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `District_ID1` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `District_ID` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `District_Name` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `District_Name_En` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `div_id` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CountryCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `StateCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Dist_MapID` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`District_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.echallan_detail
CREATE TABLE IF NOT EXISTS `echallan_detail` (
  `rti_provisional_no` varchar(13) COLLATE utf8_unicode_ci NOT NULL,
  `serial_no` varchar(2) COLLATE utf8_unicode_ci NOT NULL,
  `tr_ref_no` varchar(14) COLLATE utf8_unicode_ci DEFAULT NULL,
  `amount` double DEFAULT NULL,
  `tin_cin` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `tr_entry_date` datetime DEFAULT NULL,
  `date_ac` datetime DEFAULT NULL,
  `entry_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `major_head` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `sub_major_head` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `minor_head` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `sub_head` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `purpose` varchar(300) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Challan_Category` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_ip` varchar(80) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_id` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `fees_master_code` varchar(2) COLLATE utf8_unicode_ci NOT NULL,
  `usr_request_no` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`rti_provisional_no`,`serial_no`,`fees_master_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.echallan_detail_log
CREATE TABLE IF NOT EXISTS `echallan_detail_log` (
  `rti_provisional_no` varchar(13) COLLATE utf8_unicode_ci NOT NULL,
  `serial_no` varchar(2) COLLATE utf8_unicode_ci NOT NULL,
  `tr_ref_no` varchar(14) COLLATE utf8_unicode_ci DEFAULT NULL,
  `amount` double DEFAULT NULL,
  `tin_cin` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `tr_entry_date` datetime DEFAULT NULL,
  `date_ac` datetime DEFAULT NULL,
  `entry_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `major_head` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `sub_major_head` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `minor_head` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `sub_head` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `purpose` varchar(300) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Challan_Category` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_ip` varchar(80) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_id` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `fees_master_code` varchar(2) COLLATE utf8_unicode_ci NOT NULL,
  `usr_request_no` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.employee_table
CREATE TABLE IF NOT EXISTS `employee_table` (
  `emp_id` varchar(9) COLLATE utf8_unicode_ci NOT NULL,
  `Name_en` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name_hi` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Active` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `state_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_id` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `base_department_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `district_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `NewOfficeCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `mobile_no` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `email_id` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ip_address` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `emp_date_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`emp_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.employee_table_log
CREATE TABLE IF NOT EXISTS `employee_table_log` (
  `emp_id` varchar(9) CHARACTER SET utf8 DEFAULT NULL,
  `Name_en` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `Name_hi` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `Active` char(1) CHARACTER SET utf8 DEFAULT NULL,
  `state_id` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `user_id` varchar(20) CHARACTER SET utf8 DEFAULT NULL,
  `base_department_id` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `district_id` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `mobile_no` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `email_id` varchar(60) CHARACTER SET utf8 DEFAULT NULL,
  `emp_date_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `ip_address` varchar(30) CHARACTER SET utf8 DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.emp_office_mapping
CREATE TABLE IF NOT EXISTS `emp_office_mapping` (
  `office_mapping_id` varchar(12) COLLATE utf8_unicode_ci NOT NULL,
  `emp_code` varchar(9) COLLATE utf8_unicode_ci DEFAULT NULL,
  `office_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `designation_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `base_department_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `office_level_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `district_id_ofc` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `office_category` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `charge_type` char(3) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'Main-M, Additional-A',
  `active` char(1) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'Y/N',
  `client_ip` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `date_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `FromActive` datetime DEFAULT NULL,
  `ToActive` datetime DEFAULT NULL,
  `role_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`office_mapping_id`),
  UNIQUE KEY `emp_code_office_id_designation_id` (`emp_code`,`office_id`,`designation_id`),
  CONSTRAINT `FK_emp_office_mapping_employee_table` FOREIGN KEY (`emp_code`) REFERENCES `employee_table` (`emp_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.feedback
CREATE TABLE IF NOT EXISTS `feedback` (
  `user_id` varchar(15) CHARACTER SET utf8 NOT NULL,
  `feedback` varchar(500) CHARACTER SET utf8 NOT NULL,
  `subject` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `specify` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `datetime` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.forward
CREATE TABLE IF NOT EXISTS `forward` (
  `rti_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_id` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `file_id` varchar(11) COLLATE utf8_unicode_ci DEFAULT NULL,
  `sendingMethod` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `r_officeMapping_id` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL,
  `s_officeMapping_id` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL,
  `section_mapping_id` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `entry_dateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `ip_address` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `client_OS` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `client_browser` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `userAgent` varchar(500) CHARACTER SET utf8 DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.login
CREATE TABLE IF NOT EXISTS `login` (
  `LoginID` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `UserID` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Password` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RollID` int(11) DEFAULT NULL,
  `PasswordChange` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DisableTime` datetime DEFAULT NULL,
  `Client_ip` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientOS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientBrowser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_agent` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `entry_datetime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Active` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`LoginID`),
  UNIQUE KEY `UserID` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='This is table for login ';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.logintrial
CREATE TABLE IF NOT EXISTS `logintrial` (
  `SessionID` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `UserID` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Client_OS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientBrowser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserAgent` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SuccessFull_Login` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserIP` varchar(16) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LoginTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`SessionID`)
) ENGINE=InnoDB AUTO_INCREMENT=6569 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='Table to keep the login information';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.login_log
CREATE TABLE IF NOT EXISTS `login_log` (
  `LoginID` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `UserID` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Password` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RollID` int(11) DEFAULT NULL,
  `PasswordChange` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DisableTime` datetime DEFAULT NULL,
  `Client_ip` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientOS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientBrowser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_agent` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `entry_datetime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Active` char(1) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='This is table for login ';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.notice
CREATE TABLE IF NOT EXISTS `notice` (
  `notice_id` varchar(18) NOT NULL,
  `Subject` varchar(500) NOT NULL,
  `Publish_status` varchar(1) NOT NULL,
  `startdate` datetime NOT NULL,
  `enddate` datetime DEFAULT NULL,
  `base_dept_id` varchar(4) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `district_id` varchar(4) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `office_id` varchar(12) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `priority` varchar(2) NOT NULL,
  `user_id` varchar(20) NOT NULL,
  `notice_year` year(4) NOT NULL,
  `ipaddress` varchar(20) NOT NULL,
  `useragent` varchar(200) NOT NULL,
  `browser` varchar(50) NOT NULL,
  `UserSystem` varchar(50) NOT NULL,
  `entrydate_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`notice_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.notice_board
CREATE TABLE IF NOT EXISTS `notice_board` (
  `Notice_Id` int(11) NOT NULL AUTO_INCREMENT,
  `Header` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Permanent` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Publish_Date` datetime DEFAULT NULL,
  `Validity_From` datetime DEFAULT NULL,
  `Validity_To` datetime DEFAULT NULL,
  `Active` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Hyperlink` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `File_Type` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `File_Id` bigint(20) DEFAULT NULL,
  `Url` varchar(75) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Client_Ip` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `User_Id` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Date_Time` datetime DEFAULT NULL,
  `bold` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `blink` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `priority` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `color` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `image` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `status` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `district_id` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `base_dept_id` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `office_id` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Notice_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.notice_board_log
CREATE TABLE IF NOT EXISTS `notice_board_log` (
  `Notice_Id` int(11) DEFAULT NULL,
  `Header` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Permanent` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Publish_Date` datetime DEFAULT NULL,
  `Validity_From` datetime DEFAULT NULL,
  `Validity_To` datetime DEFAULT NULL,
  `Active` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Hyperlink` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `File_Type` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `File_Id` bigint(20) DEFAULT NULL,
  `Url` varchar(75) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Client_Ip` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `User_Id` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Date_Time` datetime DEFAULT NULL,
  `bold` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `blink` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `priority` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `color` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `image` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `status` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `district_id` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `base_dept_id` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `office_id` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.notice_file
CREATE TABLE IF NOT EXISTS `notice_file` (
  `notice_file_id` varchar(4) NOT NULL,
  `notice_id` varchar(18) NOT NULL,
  `file_name` varchar(50) NOT NULL,
  `content_type` varchar(50) NOT NULL,
  `file_data` longblob NOT NULL,
  `ipaddress` varchar(20) NOT NULL,
  `operating_System` varchar(50) NOT NULL,
  `useragent` varchar(200) NOT NULL,
  `browser` varchar(50) NOT NULL,
  `enable_status` varchar(1) NOT NULL,
  `entry_datetime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `file_description` varchar(100) NOT NULL,
  PRIMARY KEY (`notice_file_id`,`notice_id`),
  KEY `fk_notice` (`notice_id`),
  CONSTRAINT `fk_notice` FOREIGN KEY (`notice_id`) REFERENCES `notice` (`notice_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.notice_file_details
CREATE TABLE IF NOT EXISTS `notice_file_details` (
  `file_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `File_Header` varchar(500) COLLATE utf8_unicode_ci NOT NULL,
  `File_Description` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `keywords` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `upload_date` datetime DEFAULT NULL,
  `mime_type` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_code` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `file_data` longblob,
  `client_ip` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Updated` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`file_id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.notice_file_details_log
CREATE TABLE IF NOT EXISTS `notice_file_details_log` (
  `file_id` bigint(20) DEFAULT NULL,
  `File_Header` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `File_Description` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `keywords` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `upload_date` datetime DEFAULT NULL,
  `mime_type` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_code` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `file_data` longblob,
  `client_ip` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Updated` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.notice_file_log
CREATE TABLE IF NOT EXISTS `notice_file_log` (
  `notice_file_id` varchar(4) NOT NULL,
  `notice_id` varchar(18) NOT NULL,
  `file_name` varchar(50) NOT NULL,
  `content_type` varchar(50) NOT NULL,
  `file_data` longblob NOT NULL,
  `ipaddress` varchar(20) NOT NULL,
  `operating_System` varchar(50) NOT NULL,
  `useragent` varchar(200) NOT NULL,
  `browser` varchar(50) NOT NULL,
  `enable_status` varchar(1) NOT NULL,
  `entry_datetime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.notice_image
CREATE TABLE IF NOT EXISTS `notice_image` (
  `image_id` int(11) NOT NULL AUTO_INCREMENT,
  `image` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_id` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `district_id` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `date_time` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`image_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.notice_log
CREATE TABLE IF NOT EXISTS `notice_log` (
  `notice_id` varchar(18) NOT NULL,
  `Subject` varchar(500) NOT NULL,
  `Publish status` varchar(1) NOT NULL,
  `startdate` datetime NOT NULL,
  `enddate` datetime NOT NULL,
  `district_id` varchar(3) NOT NULL,
  `base_dept_id` varchar(6) NOT NULL,
  `office_id` varchar(12) NOT NULL,
  `priority_no` varchar(200) NOT NULL,
  `user_id` varchar(20) NOT NULL,
  `notice_year` year(4) NOT NULL,
  `ipaddress` varchar(20) NOT NULL,
  `useragent` varchar(200) NOT NULL,
  `browser` varchar(50) NOT NULL,
  `UserSystem` varchar(50) NOT NULL,
  `entrydate_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.office
CREATE TABLE IF NOT EXISTS `office` (
  `NewOfficeCode` varchar(12) COLLATE utf8_unicode_ci NOT NULL,
  `BaseDeptCode` varchar(4) COLLATE utf8_unicode_ci NOT NULL,
  `DistrictCodeNew` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `OfficeLevel` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeCategory` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeAddress` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CountryCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `StateCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeURL` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ContactNo` varchar(255) COLLATE utf8_unicode_ci DEFAULT 'not available',
  `Status` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Email` varchar(255) COLLATE utf8_unicode_ci DEFAULT 'Not Available',
  `Fax` varchar(255) COLLATE utf8_unicode_ci DEFAULT 'Not Available',
  PRIMARY KEY (`NewOfficeCode`),
  KEY `FK_base_department` (`BaseDeptCode`),
  KEY `FK_district` (`DistrictCodeNew`),
  CONSTRAINT `FK_base_department` FOREIGN KEY (`BaseDeptCode`) REFERENCES `basedepartment` (`dept_id`),
  CONSTRAINT `FK_district` FOREIGN KEY (`DistrictCodeNew`) REFERENCES `districts` (`District_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.officecategory
CREATE TABLE IF NOT EXISTS `officecategory` (
  `OfficeCategoryCode` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `OfficeCategoryName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`OfficeCategoryCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.officelevel
CREATE TABLE IF NOT EXISTS `officelevel` (
  `CountryCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `StateCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BaseDeptCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeLevelCode` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeLevelName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeLevelType` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.office_req
CREATE TABLE IF NOT EXISTS `office_req` (
  `office_id` varchar(10) NOT NULL,
  `filename` varchar(50) NOT NULL,
  `file_type` varchar(50) NOT NULL,
  `file_data` longblob NOT NULL,
  `ipaddress` varchar(50) NOT NULL,
  `user_agent` varchar(200) NOT NULL,
  `user_system` varchar(50) NOT NULL,
  `user_browser` varchar(50) NOT NULL,
  `approved_by_user_id` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.office_temp
CREATE TABLE IF NOT EXISTS `office_temp` (
  `NewOfficeCode` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `DistrictCodeNew` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeLevel` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BaseDeptCode` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CountryCode` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `StateCode` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeAddress` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ContactNo` varchar(50) COLLATE utf8_unicode_ci DEFAULT 'not available',
  `Email` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Fax` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeURL` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeCategory` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `isvalid` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Ip_address` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_os` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `User_browser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `useragent` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `userid` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `form` blob,
  PRIMARY KEY (`NewOfficeCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.office_temp_log
CREATE TABLE IF NOT EXISTS `office_temp_log` (
  `NewOfficeCode` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `OfficeName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DistrictCodeNew` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BaseDeptCode` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeLevel` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeCategory` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CountryCode` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `StateCode` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeAddress` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ContactNo` varchar(50) COLLATE utf8_unicode_ci DEFAULT 'not available',
  `Fax` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Email` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OfficeURL` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `isvalid` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `userid` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `useragent` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_os` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `User_browser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Ip_address` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `form` blob
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.otp_mobile
CREATE TABLE IF NOT EXISTS `otp_mobile` (
  `user_id` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `mobile_no` varchar(11) COLLATE utf8_unicode_ci DEFAULT NULL,
  `type` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_ip` varchar(16) COLLATE utf8_unicode_ci DEFAULT NULL,
  `sms_detail` varchar(900) COLLATE utf8_unicode_ci DEFAULT NULL,
  `category` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `application_id` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_id` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `email` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `sending_datetime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `delivery_status` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,
  `delivery_datetime` datetime DEFAULT NULL,
  `msgid` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `module` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.permission
CREATE TABLE IF NOT EXISTS `permission` (
  `emp_map_id` varchar(12) COLLATE utf8_unicode_ci NOT NULL,
  `ActionPermission` varchar(4) COLLATE utf8_unicode_ci NOT NULL,
  `userid` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `ipaddress` varchar(15) COLLATE utf8_unicode_ci DEFAULT NULL,
  `useragent` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `useros` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `clientbrowser` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `entrydatetime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.permission_log
CREATE TABLE IF NOT EXISTS `permission_log` (
  `emp_map_id` varchar(12) COLLATE utf8_unicode_ci NOT NULL,
  `ActionPermission` varchar(4) CHARACTER SET utf8 NOT NULL,
  `userid` varchar(15) CHARACTER SET utf8 NOT NULL,
  `ipaddress` varchar(15) CHARACTER SET utf8 DEFAULT NULL,
  `useragent` varchar(200) CHARACTER SET utf8 DEFAULT NULL,
  `useros` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `clientbrowser` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `entrydatetime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.receive_dispatch
CREATE TABLE IF NOT EXISTS `receive_dispatch` (
  `office_id` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `office_mapping_id` varchar(14) CHARACTER SET utf8 DEFAULT NULL,
  `status` varchar(3) CHARACTER SET utf8 DEFAULT NULL,
  `emp_type` varchar(3) CHARACTER SET utf8 DEFAULT NULL,
  `joined_from` varchar(15) CHARACTER SET utf8 DEFAULT NULL,
  `leave_to` varchar(15) CHARACTER SET utf8 DEFAULT NULL,
  `users_id` varchar(15) CHARACTER SET utf8 DEFAULT NULL,
  `entry_dateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `ip_address` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `client_OS` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `client_browser` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `userAgent` varchar(500) CHARACTER SET utf8 DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.role_table
CREATE TABLE IF NOT EXISTS `role_table` (
  `Role_id` int(11) DEFAULT NULL,
  `RoleName` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `WelcomePage` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='it defines the roles';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.rti_action
CREATE TABLE IF NOT EXISTS `rti_action` (
  `action_id` varchar(4) COLLATE utf8_unicode_ci NOT NULL,
  `rti_id` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `officer_mapping_id` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_detail` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_date` date DEFAULT NULL,
  `dept_status` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsUpload` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsMeeting` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `meeting_date` date DEFAULT NULL,
  `ipaddress` varchar(80) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_OS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_browser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `userAgent` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `entry_date_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `RejectReason` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`action_id`,`rti_id`),
  KEY `fk_rti_id` (`rti_id`),
  CONSTRAINT `fk_rti_id` FOREIGN KEY (`rti_id`) REFERENCES `rti_detail` (`rti_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.rti_data_for_applicant
CREATE TABLE IF NOT EXISTS `rti_data_for_applicant` (
  `rti_data_id` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `rti_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_id` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_file_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FileName` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FileType` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FileDescription` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FileData` longblob,
  `rti_prepared_emp` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `result_description` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsFileUpload` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsMeetingFix` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MeetingDate` date DEFAULT NULL,
  `IsAdditionalFees` char(1) CHARACTER SET utf8 DEFAULT NULL,
  `FeesAmount` double DEFAULT NULL,
  `ClientOS` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ClientIP` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ClientBrowser` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ClientAgent` varchar(500) CHARACTER SET utf8 DEFAULT NULL,
  `EntryDateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`rti_data_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='This tabe will contain the rti data which will be given to Applicant';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.rti_data_for_applicant_log
CREATE TABLE IF NOT EXISTS `rti_data_for_applicant_log` (
  `rti_data_id` varchar(10) CHARACTER SET utf8 NOT NULL,
  `rti_id` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `action_id` varchar(4) CHARACTER SET utf8 DEFAULT NULL,
  `action_file_id` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `FileName` varchar(60) CHARACTER SET utf8 DEFAULT NULL,
  `FileType` varchar(20) CHARACTER SET utf8 DEFAULT NULL,
  `FileDescription` varchar(20) CHARACTER SET utf8 DEFAULT NULL,
  `FileData` longblob,
  `rti_prepared_emp` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `result_description` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsFileUpload` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsMeetingFix` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MeetingDate` date DEFAULT NULL,
  `IsAdditionalFees` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FeesAmount` double DEFAULT NULL,
  `ClientOS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientIP` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientBrowser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientAgent` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='This tabe will contain the rti data which will be given to Applicant';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.rti_detail
CREATE TABLE IF NOT EXISTS `rti_detail` (
  `rti_id` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `applicant_type` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `User_ID` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Applicant_Name_en` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rti_Subject` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Is_BPL` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rti_Details` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Email_ID` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Address` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Country_Code` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Country_Name` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `State_Code` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `District_Code` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Pin_Code` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Mobile_No` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BPL_Card_No` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Gender` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Applicant_Name_hi` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BPL_Year_Issue` year(4) DEFAULT NULL,
  `BPL_Issuing_Authority` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Client_IP` varchar(16) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rti_DateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `securitycode` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsRTIFileUpload` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Client_OS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientBrowser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserAgent` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`rti_id`),
  KEY `FK_rti_detail_login` (`User_ID`),
  CONSTRAINT `FK_rti_detail_login` FOREIGN KEY (`User_ID`) REFERENCES `login` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='New rti detail table. ';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.rti_detail_old
CREATE TABLE IF NOT EXISTS `rti_detail_old` (
  `rti_id` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `applicant_type` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `User_ID` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Applicant_Name_en` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rti_Subject` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Is_BPL` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rti_Details` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Email_ID` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Address` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Country_Code` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Country_Name` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `State_Code` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `District_Code` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Pin_Code` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Mobile_No` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BPL_Card_No` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Gender` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Applicant_Name_hi` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BPL_Year_Issue` year(4) DEFAULT NULL,
  `BPL_Issuing_Authority` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Client_IP` varchar(16) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rti_DateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `securitycode` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsRTIFileUpload` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Client_OS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientBrowser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserAgent` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='New rti detail table. ';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.rti_filed_user_old
CREATE TABLE IF NOT EXISTS `rti_filed_user_old` (
  `rti_FiledUserID` int(11) NOT NULL,
  `rti_LoginUserName` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EmailID` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Gender` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Address` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PinCode` int(11) DEFAULT NULL,
  `Country` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CountryName` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `State` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `District` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Status` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MobileNo` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rti_RequestID` int(11) unsigned DEFAULT NULL,
  `IsBPL` char(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BPL_Card_No` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BPL_Issue_Year` year(4) DEFAULT NULL,
  `BPL_Issuing_Authority` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RTI_Fees` int(11) DEFAULT NULL,
  `RTI_FiledUserTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`rti_FiledUserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='this table to store information about the personal information of the user who has filed rti';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.rti_files
CREATE TABLE IF NOT EXISTS `rti_files` (
  `rti_fileID` int(11) NOT NULL,
  `fileName` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `fileType` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `fileData` longblob,
  `FileDescription` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rti_id` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BPL_RTI_FileType` char(7) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'For BPL File it will be BPL_DOC for RTI file it will be RTI_DOC',
  `FileDateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`rti_fileID`),
  KEY `FK_rti_file_rti_request` (`rti_id`),
  CONSTRAINT `FK_rti_files_rti_status` FOREIGN KEY (`rti_id`) REFERENCES `rti_status` (`rti_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='it contains rti file information';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.rti_status
CREATE TABLE IF NOT EXISTS `rti_status` (
  `rti_id` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `user_id` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `status` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'Complete, Pending',
  `DeptStatus` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_id` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `officer_maping_id` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL,
  `status_date_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `IPAddress` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_date` date DEFAULT NULL,
  `IsValid` char(1) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'If OTP is verified then it will have Y else N',
  `client_browser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `useragent` varchar(250) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_OS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsNew` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `submission_date` datetime DEFAULT NULL,
  `out_ofcr_eom` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Disposal_date` datetime DEFAULT NULL,
  `Payment_status` char(1) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'If Payment is made then Y else N',
  PRIMARY KEY (`rti_id`),
  KEY `FK_rti_status_login` (`user_id`),
  KEY `FK_rti_status_emp_office_mapping` (`officer_maping_id`),
  CONSTRAINT `FK_rti_status_emp_office_mapping` FOREIGN KEY (`officer_maping_id`) REFERENCES `emp_office_mapping` (`office_mapping_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_rti_status_login` FOREIGN KEY (`user_id`) REFERENCES `login` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.rti_status_log
CREATE TABLE IF NOT EXISTS `rti_status_log` (
  `rti_id` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `user_id` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `status` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'Complete, Pending',
  `DeptStatus` varchar(4) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_id` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `officer_maping_id` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL,
  `status_date_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `IPAddress` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `action_date` date DEFAULT NULL,
  `IsValid` char(1) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'If OTP is verified then it will have Y else N',
  `client_browser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `useragent` varchar(250) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_OS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsNew` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `submission_date` datetime DEFAULT NULL,
  `out_ofcr_eom` varchar(12) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Disposal_date` datetime DEFAULT NULL,
  `Payment_status` char(1) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.state
CREATE TABLE IF NOT EXISTS `state` (
  `cid` varchar(3) COLLATE utf8_unicode_ci NOT NULL,
  `state_id` varchar(3) COLLATE utf8_unicode_ci NOT NULL,
  `state_name_e` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `state_name_h` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `entry_datetime` datetime DEFAULT NULL,
  `client_ip` varchar(80) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_id` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`state_id`,`cid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.user_registration
CREATE TABLE IF NOT EXISTS `user_registration` (
  `RegistrationID` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `UserID` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Password` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name_hi` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name_en` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Gender` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MobileNo` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EmailID` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Address` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DistrictCode` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `District_name` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `StateCode` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Country` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Country_name` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PinCode` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserType` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsValid` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_ip` varchar(16) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_browser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_os` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `useragent` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Registration_Year` year(4) DEFAULT NULL,
  `RegistrationDateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`RegistrationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='Table for user registration';

-- Data exporting was unselected.
-- Dumping structure for table rti_nic_cg.user_registration_log
CREATE TABLE IF NOT EXISTS `user_registration_log` (
  `RegistrationID` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `UserID` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Password` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name_hi` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name_en` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Gender` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MobileNo` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EmailID` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Address` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DistrictCode` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `District_name` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `StateCode` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Country` varchar(3) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Country_name` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PinCode` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserType` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsValid` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_ip` varchar(16) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_browser` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `client_os` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `useragent` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Registration_Year` year(4) DEFAULT NULL,
  `RegistrationDateTime` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='Table for user registration';

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
