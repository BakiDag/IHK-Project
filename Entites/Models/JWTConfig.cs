namespace Domain.Models
{
    public class JWTConfig
    {
        public string Key { get; set; } //must match with appsetting
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}