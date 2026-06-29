using DashboardFinanzas.API.Models;

namespace DashboardFinanzas.API.Services;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}
