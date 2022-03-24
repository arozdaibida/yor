using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace yor_auth_api.Model
{
    public class JwtSettings
    {
        public bool ValidateIssuer { get; set; }

        public string ValidIssuer { get; set; }

        public bool ValidateAudience { get; set; }

        public string ValidAudience { get; set; }

        public string Secret { get; set; }

        public int TokenLifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}
