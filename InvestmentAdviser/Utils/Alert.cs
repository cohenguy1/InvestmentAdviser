using System.Web;
using System.Text;
using System.Web.UI;
using System;
using System.Collections.Specialized;

/// <summary>
/// A JavaScript alert
/// </summary>
public static class Alert
{


    /// <summary>
    /// This method prepares an Html form which holds all data in hidden field in the addetion to form submitting script.
    /// </summary>
    /// <param name="url">The destination Url to which the post and redirection will occur, the Url can be in the same App or ouside the App.</param>
    /// <param name="data">A collection of data that will be posted to the destination Url.</param>
    /// <returns>Returns a string representation of the Posting form.</returns>
    /// <Author>Samer Abu Rabie</Author>
    private static String PreparePOSTForm(string url, NameValueCollection data)
    {
        //Set a name for the form
        string formID = "PostForm";

        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
        foreach (string key in data)
        {
            strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + data[key] + "\">");
        }
        strForm.Append("</form>");

        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." + formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");

        //Return the form and the script concatenated. (The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }
    /// <summary>
    /// POST data and Redirect to the specified url using the specified page.
    /// </summary>
    /// <param name="page">The page which will be the referrer page.</param>
    /// <param name="destinationUrl">The destination Url to which the post and redirection is occuring.</param>
    /// <param name="data">The data should be posted.</param>
    /// <Author>Samer Abu Rabie</Author>
    public static void RedirectAndPOST(Page page, string destinationUrl, NameValueCollection data)
    {
        //Prepare the Posting form
        string strForm = PreparePOSTForm(destinationUrl, data);

        //Add a literal control the specified page holding the Post Form, this is to submit the Posting form with the request.
        page.Controls.Add(new LiteralControl(strForm));
    }

    /// <summary>
    /// Shows a client-side JavaScript alert in the browser.
    /// </summary>
    /// <param name="message">The message to appear in the alert.</param>
    public static void Show(string message)
    {
        // Cleans the message to allow single quotation marks
        string cleanMessage = message.Replace("'", "\\'");
        string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
        // Gets the executing web page
        Page page = HttpContext.Current.CurrentHandler as Page;

        // Checks if the handler is a Page and that the script isn't allready on the Page
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        {
            page.ClientScript.RegisterClientScriptBlock(typeof(Alert), "alert", script);
        }
    }

}