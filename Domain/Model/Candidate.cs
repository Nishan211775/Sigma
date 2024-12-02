using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public required string FirstName { get; set; }

        [MaxLength(500)]
        public required string LastName { get; set; }

        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        [MaxLength(256)]
        public required string Email { get; set; }

        [MaxLength(256)]
        public required string TimeInterval { get; set; }

        [MaxLength(256)]
        public string? LinkdinProfile { get; set; }

        [MaxLength(256)]
        public string? GithubProfile { get; set; }

        public required string Comment { get; set; }
    }
}
