using System.Security.Principal;

namespace Calabonga.Wpf.MvvmMdi.Identity;

/// <summary>
/// This is a PrismPrincipal
/// </summary>
public class PrismPrincipal : IPrincipal
{

    #region property Identity

    /// <summary>
    /// The Identity property
    /// </summary>
    public PrismIdentity Identity
    {
        get
        {
            return _identity ?? new AnonymousIdentity();
        }
        set
        {
            _identity = value;
        }
    }

    /// <summary>
    /// The backing field for Identity property 
    /// </summary>
    private PrismIdentity _identity;

    #endregion

    #region IPrincipal


    /// <summary>
    /// Determines whether the current principal belongs to the specified role.
    /// </summary>
    /// <returns>
    /// true if the current principal is a member of the specified role; otherwise, false.
    /// </returns>
    /// <param name="role">The name of the role for which to check membership. </param>
    public bool IsInRole(string role)
    {
        return _identity.Roles.Contains(role);
    }

    /// <summary>
    /// Gets the identity of the current principal.
    /// </summary>
    /// <returns>
    /// The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.
    /// </returns>
    IIdentity IPrincipal.Identity
    {
        get { return _identity; }
    }

    #endregion
}