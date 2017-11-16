using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageSharingWithAuth.Filters
{
    public class RefererHeaderAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var context = filterContext.HttpContext.Request.RequestContext;
            UrlHelper Url = new UrlHelper(context);
            if(filterContext.HttpContext != null)
            {
                if (filterContext.HttpContext.Request.UrlReferrer == null)
                {
                    throw new System.Web.HttpException("Invalid Submission");
                }
                if (Url.IsLocalUrl(filterContext.HttpContext.Request.UrlReferrer.AbsoluteUri))
                {
                    throw new System.Web.HttpException("This form was not submitted by this site.");
                }
            }
        }
    }
}