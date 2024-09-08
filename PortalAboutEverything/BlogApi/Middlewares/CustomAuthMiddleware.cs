namespace BlogApi.Middlewares
{
    public class CustomAuthMiddleware
    {
        private const string PASSWORD = "123";
        private readonly RequestDelegate _next;

        public CustomAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.ToString().StartsWith("/Hubs/BlogComment/negotiate")
                && context.Request.Method == "POST")
            {
                var authKey = context.Request.Headers["authKey"].FirstOrDefault();
                if (authKey is null || authKey != PASSWORD)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Bad password");
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}
