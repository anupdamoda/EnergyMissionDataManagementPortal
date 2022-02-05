using System;
using System.Web.Mvc;

namespace AceSchoolPortal.Controllers
{
    internal class AccessDeniedAuthorizeAttribute : Attribute
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/AcessDenied.aspx");
            }
        }
    }
}