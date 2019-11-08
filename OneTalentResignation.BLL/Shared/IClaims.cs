using System.Collections.Generic;
using System.Security.Claims;

namespace OneTalentResignation.BLL.Shared
{
    public interface IClaims
    {
        Claim this[string claimName] { get; }

        string UserId { get; }

        string Role { get; }

        List<string> Roles { get; }
    }
}
