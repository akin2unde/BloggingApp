using BloggingApp.Models;

namespace BloggingApp.ViewModels
{
    public class UserWrapperVM
    {
        public AppUser User { get; set; }

        public string LoginError { get; set; }

    }
}
