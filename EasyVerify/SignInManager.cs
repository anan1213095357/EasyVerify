using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;

namespace EasyVerify
{
    public class SignInManager : BackgroundService
    {
        private readonly IFreeSql fsql;

        public SignInManager(IFreeSql fsql)
        {
            this.fsql = fsql;
        }

        public Dictionary<Data.View.UserInfo, DateTime> UserDict { get; set; } = new();
        
        public bool Login(Data.View.UserInfo user)
        {
            var u = fsql.Select<Data.Models.UserInfo>().Where(p => p.Account == user.Account && p.Password == user.Password).First();
            if (u != null)
            {
                user.UserLevel = u.UserLevel;
                user.Frozen = u.Frozen;
                user.Account = u.Account;
                user.Password = u.Password;
                if (UserDict.ContainsKey(user))
                    UserDict.Remove(user);
                UserDict.Add(user, DateTime.Now.AddHours(3));
                return true;
            }
            return false;
        }
        public bool LogOut(Data.View.UserInfo user)
        {
            UserDict.Remove(user);
            user = null;
            return true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var user in UserDict.Where(p => DateTime.Now > p.Value))
                {
                    UserDict.Remove(user.Key);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
