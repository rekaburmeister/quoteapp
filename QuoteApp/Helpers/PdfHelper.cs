using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteApp.Helpers
{
    public static class PdfHelper
    {
        public static string FormatBasePdf(string html, string basePath)
        {
            basePath = basePath.Replace('\\', '/');
            html = string.Format(@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width"", initial-scale=""1.0"">
    <link href=""{0}Content/bootstrap.css"" rel=""stylesheet""/>
<link href=""{0}Content/site.css"" rel=""stylesheet""/>
</head>
<body>{1}</body></html>", basePath, html);
            html = html.Replace("src=\"/Content/Images/", string.Format("src=\"{0}/Content/Images/", basePath));
            return html;
        }
    }
}