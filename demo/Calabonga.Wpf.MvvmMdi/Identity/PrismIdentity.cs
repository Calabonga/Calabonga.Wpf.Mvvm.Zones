using System.Security.Principal;

namespace Calabonga.Wpf.MvvmMdi.Identity;

/// <summary>
/// This is a PrismIdentity
/// </summary>
public class PrismIdentity : IIdentity
{

    #region constructors

    public PrismIdentity(string name, string[] roles, SecurityToken? securityToken, int id)
        : this(name, roles, securityToken)
    {
        Id = id;
    }


    protected PrismIdentity(string name, string[] roles, SecurityToken? securityToken)
    {
        Name = name;
        Roles = roles;
        SecurityToken = securityToken;
    }

    #endregion

    /// <summary>
    /// First login indication
    /// </summary>
    public bool IsFirstLogin { get; set; }

    /// <summary>
    /// Id for current
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// AuthenticationToken for access to server
    /// </summary>
    public SecurityToken? SecurityToken { get; }

    /// <summary>
    /// Gets the name of the current user.
    /// </summary>
    /// <returns>
    /// The name of the user on whose behalf the code is running.
    /// </returns>
    public string Name { get; }

    /// <summary>
    /// Gets the Role list of the current user.
    /// </summary>
    /// <returns>
    /// The array of the role names.
    /// </returns>
    public string[] Roles { get; private set; }

    /// <summary>
    /// Gets the type of authentication used.
    /// </summary>
    /// <returns>
    /// The type of authentication used to identify the user.
    /// </returns>
    public string AuthenticationType { get; set; }

    /// <summary>
    /// Gets a value that indicates whether the user has been authenticated.
    /// </summary>
    /// <returns>
    /// true if the user was authenticated; otherwise, false.
    /// </returns>
    public bool IsAuthenticated => !string.IsNullOrEmpty(SecurityToken?.AccessToken);
}