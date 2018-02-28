using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_RTI_RequestFiles
/// </summary>
public class bl_RTI_RequestFiles
{
	public bl_RTI_RequestFiles()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    // To insert in the table rti_files
    string rti_fileID, fileName, fileType, fileDescription, rti_request_id, bpl_rti_FileType;
    Byte[] fileData;

    public string RTI_fileID { get { return rti_fileID; } set { rti_fileID = value; } }
    public string RTI_fileName { get { return fileName; } set { fileName = value; } }
    public string RTI_fileType { get { return fileType; } set { fileType = value; } }
    public Byte[] RTI_fileData { get { return fileData; } set { fileData = value; } }
    public string FileDescription { get { return fileDescription; } set { fileDescription = value; } }
    public string RTI_Request_id { get { return rti_request_id; } set { rti_request_id = value; } }
    public string BPL_RTI_FileType { get { return bpl_rti_FileType; } set { bpl_rti_FileType = value; } }
}