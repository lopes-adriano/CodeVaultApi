using CodeVaultApi.Models;

namespace CodeVaultApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
