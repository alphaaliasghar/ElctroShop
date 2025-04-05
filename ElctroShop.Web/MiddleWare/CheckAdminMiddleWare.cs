namespace ElctroShop.Web.MiddleWare
{
    public class CheckAdminMiddleWare
    {
        private RequestDelegate _next;

        public CheckAdminMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.ToString().ToLower().Trim();

            if (path.StartsWith("/admin"))
            {
                string result = context.User.FindFirst("IsAdmin")?.Value ?? "";
                if (string.IsNullOrEmpty(result) || bool.Parse(result) == false)
                {
                    context.Response.Redirect(location: "/home/AccessDenied");
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
