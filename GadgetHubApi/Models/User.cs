using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GadgetHub.Models
{
    public class User
    {
        public string? ProfilePicture { get; set; }
        public string? FullName { get; set; }
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
