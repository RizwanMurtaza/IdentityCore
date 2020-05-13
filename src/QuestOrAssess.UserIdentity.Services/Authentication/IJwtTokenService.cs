namespace QuestOrAssess.UserIdentity.Services.Authentication
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userName);
    }
}