namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class Contact 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => string.Concat(FirstName, " ", LastName);
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public bool IsPrimary { get; set; }
        public virtual User User { get; set; }
    }
}
