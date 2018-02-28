using System;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Net.NetworkInformation;
using System.IO;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Threading;
using System.Linq;
using System.Net;
using System.Collections.Specialized;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{
    public Utilities()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Generates MD5 Hash for given string
    /// </summary>
    public string GenerateMd5Hash(string input)
    {
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }


    /// <summary>
    /// Compares and verifies MD5 hash of two strings
    /// </summary>
    public bool VerifyMd5Hash(string input, string hash)
    {
        string hashOfInput = GenerateMd5Hash(input);
        StringComparer comparer = StringComparer.Ordinal;

        if (0 == comparer.Compare(hashOfInput, hash))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected Random rGen = new Random();
    protected string[] strCharacters = { "A","B","C","D","E","F","G",
    "H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y",
    "Z","1","2","3","4","5","6","7","8","9","0","a","b","c","d","e","f","g","h",
    "i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};

    /// <summary>
    /// This method is used to generate random lowercase password with given length
    /// </summary>
    public string GenPassLowercase(int i)
    {
        int p = 0;
        string strPass = "";
        for (int x = 0; x <= i; x++)
        {
            p = rGen.Next(0, 35);
            strPass += strCharacters[p];
        }
        return strPass.ToLower();
    }

    /// <summary>
    /// This method is used to generate random uppercase password with given length
    /// </summary>
    public string GenPassWithCap(int i)
    {
        int p = 0;
        string strPass = "";
        for (int x = 0; x <= i; x++)
        {
            p = rGen.Next(0, 60);
            strPass += strCharacters[p];
        }
        return strPass.ToUpper();
    }
    /// <summary>
    /// This method is used to generate random string with given length
    /// </summary>
    public string GenRendomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
    protected static Hashtable handlerPages = new Hashtable();

    /// <summary>
    /// This method is used to show Java Script Message box with specified message
    /// </summary>
    public static void MessageBoxShow(string Message)
    {
        if (!(handlerPages.Contains(HttpContext.Current.Handler)))
        {

            Page currentPage = (Page)HttpContext.Current.Handler;

            if (!((currentPage == null)))
            {
                Queue messageQueue = new Queue();
                messageQueue.Enqueue(Message);
                handlerPages.Add(HttpContext.Current.Handler, messageQueue);
                currentPage.Unload += new EventHandler(CurrentPageUnload);
            }

        }
        else
        {
            Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
            queue.Enqueue(Message);
        }
    }

    private static void CurrentPageUnload(object sender, EventArgs e)
    {
        Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
        if (queue != null)
        {
            StringBuilder builder = new StringBuilder();
            int iMsgCount = queue.Count;
            builder.Append("<script language='javascript'>");
            string sMsg;
            while ((iMsgCount > 0))
            {
                iMsgCount = iMsgCount - 1;
                sMsg = System.Convert.ToString(queue.Dequeue());
                sMsg = sMsg.Replace("\"", "'");
                builder.Append("alert( \"" + sMsg + "\" );");
            }

            builder.Append("</script>");
            handlerPages.Remove(HttpContext.Current.Handler);
            HttpContext.Current.Response.Write(builder.ToString());
        }

    }

    /// <summary>
    /// This method is used to show Java Script Message box with the specified message and redirect to given URL after clients click OK
    /// </summary>

    public static void MessageBoxShow_Redirect(string Message, string location)
    {
        if (!(handlerPages.Contains(HttpContext.Current.Handler)))
        {

            Page currentPage = (Page)HttpContext.Current.Handler;

            if (!((currentPage == null)))
            {
                Queue messageQueue = new Queue();
                messageQueue.Enqueue(Message);
                messageQueue.Enqueue(location);
                handlerPages.Add(HttpContext.Current.Handler, messageQueue);
                currentPage.Unload += new EventHandler(CurrentPageUnload_Redirect);
            }

        }
        else
        {
            Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
            //queue.Enqueue(Message);
        }
    }

    private static void CurrentPageUnload_Redirect(object sender, EventArgs e)
    {
        string[] sMsg = new string[3];
        Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
        if (queue != null)
        {
            StringBuilder builder = new StringBuilder();
            int iMsgCount = queue.Count;
            builder.Append("<script language='javascript'>");

            while ((iMsgCount != 0))
            {
                sMsg[iMsgCount] = System.Convert.ToString(queue.Dequeue());
                sMsg[iMsgCount] = sMsg[iMsgCount].Replace("\"", "'");
                iMsgCount = iMsgCount - 1;
            }
            
            builder.Append("alert( \"" + sMsg[2] + "\" ); location = \"" + sMsg[1] + "\"");
            builder.Append("</script>");
            handlerPages.Remove(HttpContext.Current.Handler);
            HttpContext.Current.Response.Write(builder.ToString());
        }

    }

    /// <summary>
    /// This function returns client IP Address
    /// </summary>
    /// <param name="Page"></param>
    /// <returns>Client Ip Address</returns>
    public string GetClientIpAddress(Page Current_Page_Name)
    {
        string strIpAddress;
        try
        {
            strIpAddress = Current_Page_Name.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null)
            {
                strIpAddress = Current_Page_Name.Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        catch
        {
            strIpAddress = Current_Page_Name.Request.ServerVariables["REMOTE_ADDR"];
        }
        return strIpAddress;
    }

    /// <summary>
    /// This Method Returns Sever's MAC Address
    /// </summary>
    /// <returns>Physical Address</returns>
    public string GetServersMacAddress()
    {

        // initial value
        string macAddress = null;

        try
        {
            // Get the network adapters
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            if ((adapters != null) && (adapters.Length > 0))
            {
                foreach (NetworkInterface adapter in adapters)
                {
                    // Get the mac address for this machine
                    macAddress = adapter.GetPhysicalAddress().ToString();
                    break;
                }
            }
        }
        catch
        {
            macAddress = "Unable To Get Clients MAC Address";
        }
        return macAddress;
    }

    /// <summary>
    /// This Method Exports The Given Grid View to Excel File Without Formatting
    /// </summary>
    /// <returns>Excel File</returns>
    public static void ExporttoExcel_Withoutformatting(GridView gv)
    {
        try
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename=cgnicin.xls"));
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            gv.AllowPaging = false;
            gv.DataBind();
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid 
                    System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
                    table.GridLines = GridLines.Both;
                    table.CaptionAlign = TableCaptionAlign.Right;
                    table.Caption = "Generated By http://cg.nic.in on " + System.DateTime.Now.ToString("dd MMMM yyyy") + " at " + System.DateTime.Now.ToString("h:mm:ss tt");
                    //  add the header row to the table 
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table 
                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter 
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response 
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception e)
        {
            Gen_Error_Rpt.Write_Error("Class:Utilitie | Method: ExporttoExcel_Withoutformatting | Error:", e);
        }
    }

    /// <summary>
    /// This Method Exports The Given Grid View to Excel File With Formatting
    /// </summary>
    /// <returns>Excel File</returns>
    public static void ExporttoExcel_WithFormatting(Page Current_Page_Name, GridView gv)
    {
        try
        {
            Current_Page_Name.Response.Clear();
            Current_Page_Name.Response.AddHeader("content-disposition", "attachment;filename=cgnicin.xls");
            gv.AllowPaging = false;
            gv.Caption = "Generated By http://cg.nic.in on " + System.DateTime.Now.ToString("dd MMMM yyyy") + " at " + System.DateTime.Now.ToString("h:mm:ss tt");
            gv.CaptionAlign = TableCaptionAlign.Right;
            gv.DataBind();
            Current_Page_Name.Response.Charset = "";

            // If you want the option to open the Excel file without saving than
            // comment out the line below
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Current_Page_Name.Response.ContentType = "application/vnd.xls";

            using (System.IO.StringWriter stringWrite = new System.IO.StringWriter())
            {
                using (System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite))
                {
                    gv.RenderControl(htmlWrite);

                    Current_Page_Name.Response.Write(stringWrite.ToString());

                    Current_Page_Name.Response.End();
                }
            }
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception e)
        {
            Gen_Error_Rpt.Write_Error("Class:Utilitie | Method: ExporttoExcel_WithFormatting | Error:", e);
        }
    }

    /// <summary> 
    /// Replace any of the contained controls with literals 
    /// </summary> 
    /// <param name="control"></param> 
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }

    /// <summary>
    /// This Method Exports The Given Grid View to PDF
    /// </summary>
    /// <returns>PDF File</returns>
    public static void ExporttoPDF(Page Current_Page_Name, GridView gv)
    {
        try
        {
            Current_Page_Name.Response.ContentType = "application/pdf";
            Current_Page_Name.Response.AddHeader("content-disposition", "attachment;filename=cgnicin.pdf");
            Current_Page_Name.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);


            //iTextSharp.text.pdf.BaseFont STF_Mangal = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\Windows\Fonts\Mangal.ttf", "Identity-H", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            //iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(STF_Mangal, 12, iTextSharp.text.Font.NORMAL);


            gv.AllowPaging = false;
            gv.DataBind();
            gv.RenderControl(hw);

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Current_Page_Name.Response.OutputStream);

            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Current_Page_Name.Response.Write(pdfDoc);
            Current_Page_Name.Response.Flush();
            Current_Page_Name.Response.End();
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception e)
        {
            Gen_Error_Rpt.Write_Error("Class:Utilitie | Method: ExporttoPDF | Error:", e);
        }
    }

    /// <summary>
    /// This Method Exports The Given Grid View to Word File
    /// </summary>
    /// <returns>Word File</returns>
    public static void ExporttoWord(Page Current_Page_Name, GridView gv)
    {
        try
        {
            Current_Page_Name.Response.Clear();
            Current_Page_Name.Response.Buffer = true;
            Current_Page_Name.Response.AddHeader("content-disposition", "attachment;filename=cgnicin.doc");
            Current_Page_Name.Response.Charset = "";
            Current_Page_Name.Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gv.AllowPaging = false;
            gv.Caption = "Generated By http://cg.nic.in on " + System.DateTime.Now.ToString("dd MMMM yyyy") + " at " + System.DateTime.Now.ToString("h:mm:ss tt");
            gv.CaptionAlign = TableCaptionAlign.Right;
            gv.DataBind();
            gv.RenderControl(hw);
            Current_Page_Name.Response.Output.Write(sw.ToString());
            Current_Page_Name.Response.Flush();
            Current_Page_Name.Response.End();
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception e)
        {
            Gen_Error_Rpt.Write_Error("Class:Utilitie | Method: ExporttoWord | Error:", e);
        }
    }

    public static void MessageBox_UpdatePanel(UpdatePanel UpdatePanelID, string MessgToShow)
    {
        string message = "alert('" + MessgToShow + "');";
        Guid gd = Guid.NewGuid();
        ScriptManager.RegisterStartupScript(UpdatePanelID, UpdatePanelID.GetType(), gd.ToString(), message, true);
    }

    /// <summary>
    /// This Method Shows Javascript Message Box Under Update Panel and Redirect to Given URL
    /// </summary>
    /// <returns>Messagebox</returns>
    public static void MessageBox_UpdatePanel_Redirect(UpdatePanel UpdatePanelID, string MessgToShow, string url)
    {
        string message = "alert('" + MessgToShow + "'); location = '" + url + "';";
        Guid gd = Guid.NewGuid();
        ScriptManager.RegisterStartupScript(UpdatePanelID, UpdatePanelID.GetType(), gd.ToString(), message, true);
    }




    public static string System_Info(Page pg)
    {
        string info = "";
        try
        {
            //HttpBrowserCapabilities browse = pg.Request.Browser;
            // string info =  browse.Browser + " "+ browse.Version +"OS:" + browse.Platform;
            //info = browse.Platform;
            String userAgent = pg.Request.UserAgent;

            if (userAgent.IndexOf("Windows NT 10.0") > 0)
            {

                info = "win10.0";
            }
            else
            if (userAgent.IndexOf("Windows NT 6.3") > 0)
            {
                info = "Win8.1";
            }
            else if (userAgent.IndexOf("Windows NT 6.2") > 0)
            {
                info = "Win8";
            }
            else if (userAgent.IndexOf("Windows NT 6.1") > 0)
            {
                info = "Win7";
            }
            else if (userAgent.IndexOf("Windows NT 6.0") > 0)
            {
                info = "WinVista";
            }
            else if (userAgent.IndexOf("Windows NT 5.2") > 0)
            {
                info = "Win2003,WinXP64";
            }
            else if (userAgent.IndexOf("Windows NT 5.1") > 0)
            {
                info = "WinXP";
            }
            else if (userAgent.IndexOf("Windows NT 5.01") > 0)
            {
                info = "Win2000SP1";
            }
            else if (userAgent.IndexOf("Windows NT 5.0") > 0)
            {
                info = "Win2000";
            }
            else if (userAgent.IndexOf("Windows NT 4.0") > 0)
            {
                info = "WinNT4.0";
            }
            else if (userAgent.IndexOf("Win 9x 4.90") > 0)
            {
                info = "WinMe";
            }
            else if (userAgent.IndexOf("Windows 98") > 0)
            {
                info = "Win98";
            }
            else if (userAgent.IndexOf("Windows 95") > 0)
            {
                info = "Win95";
            }
            else if (userAgent.IndexOf("Windows CE") > 0)
            {
                info = "WinCE";
            }
            else
            {
                info = "Others";
            }
        }
        catch
        { info = "Uknown"; }
        return info;
    }

    public string Encrypt_AES(string clearText, string EncryptionKey)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public string Decrypt_AES(string cipherText, string EncryptionKey)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    /// <summary>
    /// AES 265 Bit Encryption
    /// </summary>
    /// <param name="plain_text"></param>
    /// <param name="encryption_key"></param>
    /// <param name="invect"></param>
    /// <returns>cipher text</returns>
    public string Aes_Encrypt_256(string plain_text, string encryption_key, string invect)
    {
        var sToEncrypt = plain_text;
        var myRijndael = new RijndaelManaged()
        {
            Padding = PaddingMode.Zeros,
            Mode = CipherMode.CBC,
            KeySize = 256,
            BlockSize = 128
        };

        var key = Encoding.ASCII.GetBytes(encryption_key);
        var IV = Encoding.ASCII.GetBytes(invect);
        var encryptor = myRijndael.CreateEncryptor(key, IV);
        var msEncrypt = new MemoryStream();
        var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        var toEncrypt = Encoding.ASCII.GetBytes(sToEncrypt);

        csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
        csEncrypt.FlushFinalBlock();

        var encrypted = msEncrypt.ToArray();
        return (Convert.ToBase64String(encrypted));
    }
    /// <summary>
    /// Decrypt AES Cipher Text
    /// </summary>
    /// <param name="cipher_text"></param>
    /// <param name="encryption_key"></param>
    /// <param name="invect"></param>
    /// <returns>Plain Text</returns>
    public static string Aes_Decrypt_256(string cipher_text, string encryption_key, string invect)
    {
        var sEncryptedString = cipher_text;
        var myRijndael = new RijndaelManaged()
        {
            Padding = PaddingMode.Zeros,
            Mode = CipherMode.CBC,
            KeySize = 256,
            BlockSize = 128
        };

        var key = Encoding.ASCII.GetBytes(encryption_key);
        var IV = Encoding.ASCII.GetBytes(invect);

        var decryptor = myRijndael.CreateDecryptor(key, IV);
        var sEncrypted = Convert.FromBase64String(sEncryptedString);
        var fromEncrypt = new byte[sEncrypted.Length];
        var msDecrypt = new MemoryStream(sEncrypted);
        var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

        var strmReader = new StreamReader(csDecrypt);
        var str = strmReader.ReadToEnd();
        return str; // (Encoding.ASCII.GetString(str));
    }
    public bool Check_IpRange(string ip)
    {
        bool flag = false;
        try
        {
            IPAddress cip = IPAddress.Parse(ip);

            IPAddressRange ipr_local_v4 = new IPAddressRange(IPAddress.Parse("127.0.0.0"), IPAddress.Parse("127.0.0.255"));
            IPAddressRange ipr_local_v6 = new IPAddressRange(IPAddress.Parse("::1"), IPAddress.Parse("::1"));
            IPAddressRange ipr_nicnet_local = new IPAddressRange(IPAddress.Parse("10.0.0.0"), IPAddress.Parse("10.255.255.255"));
            IPAddressRange ipr_nicnet_public = new IPAddressRange(IPAddress.Parse("164.100.0.0"), IPAddress.Parse("164.100.255.255"));

            if (ipr_local_v4.IsInRange(cip))
            {
                flag = true;
            }
            else if (ipr_local_v6.IsInRange(cip))
                flag = true;
            else if (ipr_nicnet_local.IsInRange(cip))
                flag = true;
            else if (ipr_nicnet_public.IsInRange(cip))
                flag = true;
        }
        catch { }
        return flag;
    }
    public static void RedirectWithData(NameValueCollection data, string url)
    {
        HttpResponse response = HttpContext.Current.Response;
        response.Clear();

        StringBuilder s = new StringBuilder();
        s.Append("<html>");
        s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
        s.AppendFormat("<form name='form' action='{0}' method='post'>", url);
        foreach (string key in data)
        {
            s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", key, data[key]);
        }
        s.Append("</form></body></html>");
        response.Write(s.ToString());
        response.End();
    }
}