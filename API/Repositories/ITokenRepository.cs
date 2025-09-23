using System;
using Microsoft.AspNetCore.Identity;

namespace API.Repositories;

public interface ITokenRepository
{
    string CreateJwtToken(IdentityUser user, List<string> roles);
}
