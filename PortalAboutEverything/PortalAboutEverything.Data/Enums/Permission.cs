namespace PortalAboutEverything.Data.Enums
{
    [Flags]
    public enum Permission
    {
        None = 0,
        CanCreateGame = 1,      // 0001
        CanDeleteGame = 2,      // 0010
        CanViewPremission = 4,  // 0100
        CanEditPremission = 8,  // 1000
    }
}