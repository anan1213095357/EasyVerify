namespace EasyVerify
{
    public enum UserLevel
    {
        Administrators,
        OrdinaryUsers,
        Visitor
    }
    public class IdentityAuthenticationAttribute : Attribute
    {
        public UserLevel userLevel { get; set; }

        public IdentityAuthenticationAttribute(UserLevel userLevel)
        {
            this.userLevel = userLevel;
        }

    }
}
