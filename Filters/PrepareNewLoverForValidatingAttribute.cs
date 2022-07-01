using AsparagusLoversProject.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Mvc.Filters;

namespace AsparagusLoversProject.Filters
{
    public class PrepareNewLoverForValidatingAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
             ModelStateDictionary modelState = filterContext.ModelState;
             
            if (string.IsNullOrEmpty(modelState["LoverFname"].RawValue.ToString()) == true)
                modelState.ClearValidationState("LoverFname");

            if (string.IsNullOrEmpty(modelState["LoverEMail"].RawValue.ToString()) == true)
                modelState.ClearValidationState("LoverEMail");


        }
    }
}
