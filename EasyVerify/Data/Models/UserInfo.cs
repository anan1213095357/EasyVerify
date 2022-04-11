

using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace EasyVerify.Data.Models
{
    [Table(Name = "user_info")]
    [Index("uk_account", "Account", true)]
    public class UserInfo
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool Frozen { get; set; }
        public UserLevel UserLevel { get; set; }

    }
}
