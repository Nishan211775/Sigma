using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel
{
    public class CandidateViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(256)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string TimeInterval { get; set; } = string.Empty;

        [MaxLength(256)]
        [Url(ErrorMessage = "Invalid LinkedIn profile URL.")]
        public string? LinkdinProfile { get; set; }

        [MaxLength(256)]
        [Url(ErrorMessage = "Invalid GitHub profile URL.")]
        public string? GithubProfile { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
