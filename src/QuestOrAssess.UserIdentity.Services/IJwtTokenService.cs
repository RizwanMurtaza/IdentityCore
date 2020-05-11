namespace QuestOrAssess.UserIdentity.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userName);
    }
}