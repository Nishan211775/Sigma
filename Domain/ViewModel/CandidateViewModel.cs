using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel
{
    public class CandidateViewModel
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public required string FirstName { get; set; }

        [MaxLength(500)]
        public required string LastName { get; set; }

        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        [MaxLength(256)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }

        [MaxLength(256)]
        public required string TimeInterval { get; set; }

        [MaxLength(256)]
        [Url(ErrorMessage = "Invalid LinkedIn profile URL.")]
        public string? LinkdinProfile { get; set; }

        [MaxLength(256)]
        [Url(ErrorMessage = "Invalid GitHub profile URL.")]
        public string? GithubProfile { get; set; }

        public required string Comment { get; set; }
    }
}
