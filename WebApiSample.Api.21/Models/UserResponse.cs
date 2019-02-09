using System.ComponentModel.DataAnnotations;

namespace HourRegistration.DataAccess.Models
{
    public class UserResponse
    {
        [Required]
        public User User { get; set; }

        [Required]
        public bool IsCreated { get; set; }

        public string FailedMessage { get; set; }

    }
}