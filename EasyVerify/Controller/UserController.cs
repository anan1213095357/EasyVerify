using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyVerify.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [IdentityAuthentication(UserLevel.Visitor)]
    public class UserController : ControllerBase
    {
        private readonly IFreeSql fsql;

        public UserController(IFreeSql fsql)
        {
            this.fsql = fsql;
        }

        [HttpPost("register")]
        public async Task<object> Register([FromBody] Data.Dtos.UserInfo userInfo)
        {
            var ret = await fsql.Insert(new Data.Models.UserInfo
            {
                Account = userInfo.Account,
                Password = userInfo.Password,
                Phone = userInfo.Phone,
                UserLevel = UserLevel.OrdinaryUsers
            }).ExecuteAffrowsAsync();
            return new { msg = "ok", code = ret };
        }
    }
}
