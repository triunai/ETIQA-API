namespace Users.API.Models.Domain
{
    public class Skillset
    {
        public Guid SkillsetId { get; set; }
        public string SkillName { get; set; }
        
        // many to many begins here
        public ICollection<User> User { get; set; }
    }
}
