namespace Calabonga.Wpf.MvvmMdi.Identity;

/// <summary>
/// This is a ApplicationUser
/// </summary>
public class ApplicationUser : PrismIdentity
{

    public ApplicationUser(string name, string[] roles, SecurityToken securityToken, int userId, bool isFirstLogin) :
        base(name, roles, securityToken)
    {

        Id = userId;
        IsFirstLogin = isFirstLogin;
    }
}