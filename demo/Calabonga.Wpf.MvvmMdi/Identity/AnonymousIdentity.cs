namespace Calabonga.Wpf.MvvmMdi.Identity;

/// <summary>
/// Anonymous Identity for unauthenticated users
/// </summary>
public class AnonymousIdentity : PrismIdentity
{
    public AnonymousIdentity() : base("Anonymous", [], null)
    {
    }
}