using System.Security.Claims;

namespace CodeVaultApi.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetUsername(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        }
    }
}
