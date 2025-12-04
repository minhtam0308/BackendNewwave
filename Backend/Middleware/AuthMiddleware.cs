namespace Backend.Middlware
{
    public class AuthMiddleware
    {
        private RequestDelegate _delegrate;

        public AuthMiddleware(RequestDelegate delegrate)
        {
            _delegrate = delegrate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("request " + context.Request.Path);
            await _delegrate(context);
            //await context.Response.WriteAsync("middle");
            Console.WriteLine("response  " + context.Response.StatusCode);

        }
    }
    public static class AuthMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
