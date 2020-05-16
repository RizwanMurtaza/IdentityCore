namespace UserIdentity.Services.Authentication
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userName);
    }
}