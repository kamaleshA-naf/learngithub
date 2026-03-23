using Microsoft.AspNetCore.Mvc.Filters;

namespace ExceptionFilterAPI.Misc
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
             context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
            {
                Message = "An error occurred while processing your request.",
                Details = context.Exception.Message
            })
            {
                StatusCode = 500
            };
        }
    }
}
