using System.ComponentModel.DataAnnotations;

namespace eedbackApp.Models
{
    public class Feedback
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Feedback comments are required.")]
        public string Comments { get; set; }
    }
}