using EasyVerify.Data.Models;
using Microsoft.AspNetCore.Http.Features;

namespace EasyVerify
{
    public class IdentityAuthentication
    {
        private RequestDelegate _next;
        private readonly IConfiguration configuration;
        private readonly IFreeSql fsql;

        public IdentityAuthentication(
            RequestDelegate next,
            IConfiguration configuration,
            IFreeSql fsql
            )
        {
            _next = next;
            this.configuration = configuration;
            this.fsql = fsql;
        }

        public async Task Invoke(HttpContext context)
        {
            string account = string.IsNullOrEmpty(context.Request.Headers["account"]) ? context.Request.Cookies["Account"] : context.Request.Headers["account"];
            string password = string.IsNullOrEmpty(context.Request.Headers["password"]) ? context.Request.Cookies["Password"] : context.Request.Headers["password"];

            var ida = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<IdentityAuthenticationAttribute>();
            if (ida != null && ida.userLevel != UserLevel.Visitor)
            {

                if (ida.userLevel == UserLevel.OrdinaryUsers || ida.userLevel == UserLevel.Administrators)
                {
                    var user = await fsql.Select<UserInfo>().Where(p => p.Account == account && p.Password == password && p.UserLevel == ida.userLevel).FirstAsync();
                    if (user == null)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("当前用户未授权！");
                        return;
                    }
                }

            }

            await _next(context);

            if (!context.Request.Cookies.ContainsKey("Account") && !string.IsNullOrEmpty(account))
                context.Response.Cookies.Append("Account", account);

            if (!context.Request.Cookies.ContainsKey("Password") && !string.IsNullOrEmpty(password))
                context.Response.Cookies.Append("Password", password);

        }

    }
    public static class IdentityAuthenticationEx
    {
        public static IApplicationBuilder AddIdentityAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityAuthentication>();
        }
    }
}
