using Microsoft.AspNetCore.Mvc.Filters;

namespace HttpAndMvc.Filters
{
    class ActionTestFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
    
    public class AddHeaderAttribute : ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public AddHeaderAttribute(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.HttpContext.Response.HasStarted)
            {
                context.HttpContext.Response.Headers.Add(_name, new[] { _value });
            }

            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            ;

            base.OnResultExecuted(context);
        }
    }
    
}