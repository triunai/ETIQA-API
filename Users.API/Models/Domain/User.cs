namespace Users.API.Models.Domain
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        // Skillsets property, if want to manipulate further make one to many relationship ( one user has many skills you brainlet)
        // public ICollection<BlogPost> BlogPosts { get; set; }
        public string Hobby { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public bool isVisible { get; set; }

        // many to many begins here
        public ICollection<Skillset> Skillset { get; set; }


    }
}
