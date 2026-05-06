using LeaveManagementAPI.Data;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Models;

namespace LeaveManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> RegisterAsync(RegisterDto dto)
        {
            if (_context.Employees.Any(e => e.Email == dto.Email))
                return new ServiceResult 
                { 
                    Success = false, 
                    Message = "Email already exists" 
                };

            var employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Designation = dto.Designation,
                Role = "Employee"
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new ServiceResult 
            { 
                Success = true, 
                Message = "Employee registered successfully!" 
            };
        }
    }
}