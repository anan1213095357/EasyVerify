using Microsoft.AspNetCore.Components;

namespace EasyVerify.Pages
{
    public partial class Index
    {
        [Inject]
        public IFreeSql fsql { get; set; }

        List<Data.View.UserInfo> userInfos { get; set; } = new();

        protected override void OnInitialized()
        {
            try
            {
                userInfos = fsql
                    .Select<Data.Models.UserInfo>()
                    .ToList()
                    .Select(p =>
                    {
                        var user = new Data.View.UserInfo();
                        user.GetType().GetProperties().ToList().ForEach(x =>
                            x.SetValue(user, p.GetType().GetProperties().First(p => p.Name.Contains(x.Name)).GetValue(p)));
                        return user;
                    })
                    .ToList();

                userInfos.ForEach(x =>
                {
                    x.PropertyChanged += async (s, e) =>
                    {
                        var user = s as Data.View.UserInfo;
                        var upValue = user?.GetType().GetProperties().First(p => p.Name.Contains(e.PropertyName!));
                        if (upValue?.PropertyType == typeof(DateTime))
                            await fsql.Ado.ExecuteArrayAsync($"UPDATE user_info SET {e.PropertyName} = '{upValue?.GetValue(user):yyyy-MM-dd HH:mm:ss}' WHERE Id = {x.Id}");
                        else
                            await fsql.Ado.ExecuteArrayAsync($"UPDATE user_info SET {e.PropertyName} = '{upValue?.GetValue(user)}' WHERE Id = {x.Id}");

                    };

                });

            }
            catch (Exception ex)
            {

                throw;
            }

            base.OnInitialized();
        }

    }
}
