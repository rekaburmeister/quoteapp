using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Helpers
{
    public static class PdfHelper
    {
        public static string FormatBasePdf(string html)
        {
            html = string.Format(@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width"", initial-scale=""1.0"">
    <link href=""bootstrap.min.css"" rel=""stylesheet""/>
<link href=""site.css"" rel=""stylesheet""/>
</head>
<body>{0}</body></html>", html);
            html = html.Replace("src=\"/Content/Images/", "src=\"");
            return html;
        }
    }
}