
using System.Threading;
using System.Globalization;

///<summary>
/// Summary description for BasePage
///</summary>
public class BasePage : System.Web.UI.Page
{   
    protected override void InitializeCulture()
    {
        //string language = "en-GB";
        string language = "en-GB";
        if (Request.UserLanguages != null)
        {
            //language = Request.UserLanguages[0];   // Commented by Naveen
           // language = "hi-IN";                 // Added by Naveen
            language = "en-GB";                 // added by naveen to test english default
            if (Session["language"] != null)
            {
                language = Session["language"].ToString();
            }
            else
            {
                //Session["language"] = "hi-IN";
                Session["language"] = "en-GB";   // added by naveen to test english default
                //Session["language"] = Request.UserLanguages[0];
            }
        }
        else
        {
           // Session["language"] = "en-GB";
            Session["language"] = "hi-IN";    // added by naveen to test english default
        }
        if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("hin"))
        {
            language = "hi-IN";
            Session["language"] = "hi-IN";
        }
        else if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("eng"))
        {
            language = "en-GB";
            Session["language"] = "en-GB";
        }

        Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
       // base.InitializeCulture();            // added by Naveen 
    }
    
}
