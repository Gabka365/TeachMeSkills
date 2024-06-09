using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Controllers.ActionFilterAttributes
{
    public class HasRoleOrHigherAttribute : ActionFilterAttribute
    {
        private UserRole _role;

        public HasRoleOrHigherAttribute(UserRole role)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<AuthService>();
            if (!authService.HasRoleOrHigher(_role))
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
