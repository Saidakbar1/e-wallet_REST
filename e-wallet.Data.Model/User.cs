using Microsoft.AspNetCore.Identity;

namespace e_wallet.Data.Model
{
    public class User : IdentityUser<Guid>
    {
        public bool Identified { get; set; }
        
        public double Balance { get; set; }

    }
}