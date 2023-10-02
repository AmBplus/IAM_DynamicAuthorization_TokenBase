namespace AccessManagement.Services
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpires { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }


     
    }
}
