using System.ComponentModel.DataAnnotations;
namespace LeaveManagementAPI.Models {
    public class Employee{
        [Required]
        public int Id { get; set;}

        [Required]
        public string Name { get; set;} = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string Role { get; set; } = "Employee";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
    }
}