using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Final_Demo.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserPasswordHash { get; set; }
        public string UserRole { get; set; } = "Customer";
    }
}
