using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using Xabe.FFmpeg;

namespace PortalAboutEverything.Models.Blog
{
    public class MessageViewModel
    {
        public string Name { get; set; }
        
        public DateTime CurrentTime { get; set; }

        [Required(ErrorMessage = "The message must be not nullable.")]
        
        public string Message { get; set; }
        
        public int LikeCount { get; set; }
        
        public int DislikeCount { get; set; }

        [ComputerMoodRate]
        [Display(Name = "mood rate")]
        
        public int MoodRate { get; set; }

        public int Number_1 { get; set; }

        [CorrectSymbols]
        public int Number_2 { get; set; }

        [MaxBlogImageSize(400,400)]
        public IFormFile? Cover {  get; set; }

    }
}
