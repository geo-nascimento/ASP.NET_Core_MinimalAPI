using MinCatalogoAPI.Models;

namespace MinCatalogoAPI.Services;

public interface ITokenService
{
    string GerarToken(string key, string issuer, string audience, UserModel user);
}
