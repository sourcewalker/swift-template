using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}