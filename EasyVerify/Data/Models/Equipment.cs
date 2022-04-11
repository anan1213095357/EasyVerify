

using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace EasyVerify.Data.Models
{
    [Table(Name = "equipment")]
    [Index("uk_machineCode", "MachineCode", true)]
    public class Equipment
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserInfo_Account { get; set; }
        public string MachineCode { get; set; }
        public bool Frozen { get; set; }
        public DateTime ExpirationTime { get; set; }

    }
}
