using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CredoMobile.WebUI.Filters
{
    /// <summary>
    /// This filter prevents the site from redirecting to external hosts
    /// </summary>
    public class OpenRedirectFilter : ActionFilterAttribute
    {

        private const string homePageRoute = "HomePage";
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            var result = filterContext.Result as RedirectResult;
            if (result != null)
            {
                var request = filterContext.HttpContext.Request;
                if (!UrlHostEqualsServerHost(result.Url, request))
                {
                   
                    filterContext.Result = new RedirectToRouteResult(homePageRoute, null);

                }


            }



        }


        private bool UrlHostEqualsServerHost(string url, HttpRequestBase requestBase)
        {
            if (!url.Contains("://"))
                url = "http://" + url;

            return (requestBase.Url != null && (new Uri(url)).Host == requestBase.Url.Host);
        }

    }
}