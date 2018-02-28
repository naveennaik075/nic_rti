using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_feedback
/// </summary>
public class bl_feedback
{
    string userid, feedback, subject, specify;
    public string Feedback { get { return feedback; } set { feedback = value; } }
    public string Userid { get { return userid; } set { userid = value; } }
    public string Subject { get { return subject; } set { subject = value; } }
    public string Specify { get { return specify; } set { specify = value; } }
}