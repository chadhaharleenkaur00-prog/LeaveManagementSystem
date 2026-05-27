using LeaveManagementAPI.Data;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LeaveManagementAPI.Services{

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
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
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Email == dto.Email);
            if (employee == null || !BCrypt.Net.BCrypt.Verify(dto.Password, employee.PasswordHash))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password",
                    Token = null,
                    Name = null,
                    EmployeeId = 0,
                    Email = null,
                    Designation = null,
                    Role = null
                };
            } 
            else 
            {
                var token = GenerateJwtToken(employee);
                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token,
                    Name = employee.Name,  
                    EmployeeId = employee.Id,   
                    Email = employee.Email,
                    Designation = employee.Designation,
                    Role = employee.Role
                };
            } 

        }
        private string GenerateJwtToken(Employee employee)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Role, employee.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
}
}