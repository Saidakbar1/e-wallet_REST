using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace e_wallet.REST_API.Services
{

    public class SigningSecurityKey : ISigningSecurityKey
    {
        SymmetricSecurityKey securityKey;
        string signingAlgorithm;

        public SigningSecurityKey(string symetricKey, string signingAlgorithm)
        {
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symetricKey));
            this.signingAlgorithm = signingAlgorithm;
        }

        public string SigningAlgorithm => signingAlgorithm;

        public SecurityKey GetKey()
        {
            return securityKey;
        }
    }
}
