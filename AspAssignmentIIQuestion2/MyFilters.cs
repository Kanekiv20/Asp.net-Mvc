using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspAssignmentIIQuestion2
{
    public class MyFilters: ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext.HttpContext.Session["username"] == null)
			{
				filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = "Login", controller = "Bank" }));
			}
			base.OnActionExecuting(filterContext);

		}
	}
}