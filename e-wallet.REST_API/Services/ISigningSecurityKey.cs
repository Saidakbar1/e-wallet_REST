using Microsoft.IdentityModel.Tokens;

namespace e_wallet.REST_API.Services
{
    public interface ISigningSecurityKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
