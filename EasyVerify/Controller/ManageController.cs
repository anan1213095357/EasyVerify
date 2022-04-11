using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyVerify.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [IdentityAuthentication(UserLevel.Administrators)]
    public class ManageController : ControllerBase
    {
        private readonly IFreeSql fsql;

        public ManageController(IFreeSql fsql)
        {
            this.fsql = fsql;
        }

        [HttpGet("userlist")]
        public async Task<object> UserList()
        {
            return new
            {
                msg = "ok",
                data = await fsql.Select<Data.Models.UserInfo>().ToListAsync()
            };
        }
        public async Task<object> SetUserExpirationTime(int id, DateTime dateTime)
        {
            return new
            {
                msg = "ok",
                //result = await fsql.Update<Data.Models.UserInfo>()
                //.Set(p => p.ExpirationTime, dateTime)
                //.Where(p => p.Id == id)
                //.ExecuteAffrowsAsync()
            };
        }
        public async Task<object> DelUser(int id)
        {
            return new
            {
                msg = "ok",
                result = await fsql.Delete<Data.Models.UserInfo>()
                .Where(p => p.Id == id)
                .ExecuteAffrowsAsync()
            };
        }
        public async Task<object> CreateUser([FromBody] Data.Dtos.UserInfo userInfo)
        {
            return new
            {
                msg = "ok",
                result = await fsql.Insert(new Data.Models.UserInfo
                {
                    Account = userInfo.Account,
                    Password = userInfo.Password,
                    Phone = userInfo.Phone,
                    UserLevel = UserLevel.OrdinaryUsers
                }).ExecuteAffrowsAsync()
            };
        }

    }
}
