using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public  string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string TimeInterval { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? LinkdinProfile { get; set; }

        [MaxLength(256)]
        public string? GithubProfile { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
