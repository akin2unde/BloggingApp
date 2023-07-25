using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class UserVM
    {
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string MailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //public virtual List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
