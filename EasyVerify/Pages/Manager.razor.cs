using Microsoft.AspNetCore.Components;

namespace EasyVerify.Pages
{
    public partial class Manager
    {
        [Inject]
        public IFreeSql fsql { get; set; }

        List<Data.View.UserInfo> userInfos { get; set; } = new();

        protected override void OnInitialized()
        {
            try
            {
                userInfos = fsql
                   .Select<Data.Models.Equipment,Data.Models.UserInfo>()
                   .LeftJoin((Equipment, UserInfo) => UserInfo.Account == Equipment.UserInfo_Account)
                   .ToList((Equipment, UserInfo) => new Data.View.UserInfo
                   {
                       Account = UserInfo.Account,
                       Password = UserInfo.Password,
                       Frozen = UserInfo.Frozen,
                       Id = UserInfo.Id,
                       Name = UserInfo.Name,
                       UserLevel = UserInfo.UserLevel,
                       MachineCode = Equipment.MachineCode,
                       ExpirationTime = Equipment.ExpirationTime,
                       Phone = UserInfo.Phone
                   });

                userInfos.ForEach(x =>
                {
                    x.PropertyChanged += async (s, e) =>
                    {
                        var user = s as Data.View.UserInfo;
                        var upValue = user?.GetType().GetProperties().First(p => p.Name.Contains(e.PropertyName!));
                        if (new[] { "MachineCode", "ExpirationTime" }.Contains(e.PropertyName))
                        {
                            if (upValue?.PropertyType == typeof(DateTime))
                                await fsql.Ado.ExecuteArrayAsync($"UPDATE equipment SET {e.PropertyName} = '{upValue?.GetValue(user):yyyy-MM-dd HH:mm:ss}' WHERE Id = {x.EquipmentId}");
                            else
                                await fsql.Ado.ExecuteArrayAsync($"UPDATE equipment SET {e.PropertyName} = '{upValue?.GetValue(user)}' WHERE Id = {x.EquipmentId}");
                        }
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
