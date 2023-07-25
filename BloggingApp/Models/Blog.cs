using BloggingApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Models
{
    public class Blog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        [ForeignKey(nameof(AppUser.MailAddress))]
        public string UserMailAddress { get; set; }
        public virtual AppUser CreatedBy { get; set; }

    }
}
