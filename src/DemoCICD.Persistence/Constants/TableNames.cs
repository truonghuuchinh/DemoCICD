namespace DemoCICD.Persistence.Constants;
internal sealed class TableNames
{
    // ************* Plural Nouns ***************
    internal const string Actions = nameof(Actions);
    internal const string Functions = nameof(Functions);
    internal const string ActionInFunctionns = nameof(ActionInFunctionns);
    internal const string Permissions = nameof(Permissions);

    internal const string AppUsers = nameof(AppUsers);
    internal const string AppRoles = nameof(AppRoles);
    internal const string AppUserRoles = nameof(AppUserRoles);

    internal const string AppUserClaims = nameof(AppUserClaims); // IdentityUserClaims
    internal const string AppRoleClaims = nameof(AppRoleClaims); // IdentityRoleClaims
    internal const string AppUserLogins = nameof(AppUserLogins); // IdentityUserLogins
    internal const string AppUserTokens = nameof(AppUserTokens); // IdentityUserTokens

    // ************* Single Nouns ***************
    internal const string Product = nameof(Product);
}
