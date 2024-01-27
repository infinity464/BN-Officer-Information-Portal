namespace Infinity.Bnois.Configuration.Models
{
    public interface ILoggedInUser
    {
        string UserId { get; }
        string[] UserRoles { get; }
        string[] RoleIds { get; }
         int[] UserFeatureCodes { get; }

    }
}
