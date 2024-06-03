using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Controllers.ActionFilterAttributes
{
    public class HasPermission : ActionFilterAttribute
    {
        private Permission _permission;

        public HasPermission(Permission permission)
        {
            _permission = permission;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService<AuthService>();
            var userPermission = authService!.GetUserPermission();

            if (!userPermission.HasFlag(_permission))
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
