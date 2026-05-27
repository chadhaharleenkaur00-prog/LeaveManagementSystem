using LeaveManagementAPI.Data;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> GetEmployeesAsync()
        {
            if (_context.Employees is null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "No employees found"
                };
            }
            else
            {
                var employees = await _context.Employees.ToListAsync();
                return new ServiceResult
                {
                    Success = true,
                    Message = "Employees retrieved successfully",
                    Data = employees
                };
            }
        }

        public async Task<ServiceResult> GetEmployeeByIdAsync(int id)
        {
            if(_context.Employees is null)
            {
                return new ServiceResult
                { 
                    Success = false,
                    Message = "No employees found"  
                };
            }
            else {
                var employee = await _context.Employees.FindAsync(id);
                if(employee is null)
                {
                    return new ServiceResult
                    {
                        Success = false,
                        Message = "Employee not found"
                    };
                }
                return new ServiceResult
                {
                    Success = true,
                    Message = "Employee retrieved successfully",
                    Data = employee
                };
            }
        }
        public async Task<ServiceResult> UpdateEmployeeByIdAsync(int id, UpdateDto dto)
        {
            if(_context.Employees is null)
            {
                return new ServiceResult{
                    Success = false,
                    Message = "No Employees found"
                };
            }
            else
            {
                var employee = await _context.Employees.FindAsync(id);
                if(employee is null)
                {
                    return new ServiceResult 
                    {
                        Success = false,
                        Message = "Employee not found"
                    };
                }
                else
                {
                    employee.Name = dto.Name;
                    employee.Email = dto.Email;
                    employee.PasswordHash = dto.Password;
                    employee.Designation = dto.Designation;

                    await _context.SaveChangesAsync();

                    return new ServiceResult
                    {
                        Success = true,
                        Message = "Employee details have been updated",
                        Data = employee
                    };
                }
            }
        }
        public async Task<ServiceResult> DeleteEmployeeByIdAsync(int id)
        {
            if(_context.Employees is null)
            {
                return new ServiceResult{
                    Success = false,
                    Message = "No Employees found"
                };
            }
            else
            {
                var employee = await _context.Employees.FindAsync(id);
                if(employee is null)
                {
                    return new ServiceResult 
                    {
                        Success = false,
                        Message = "Employee not found"
                    };
                }
                else
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();

                    return new ServiceResult
                    {
                        Success = true,
                        Message = "Employee has been deleted",
                        Data = employee
                    };
                }
            }
    }
    public async Task<ServiceResult> CreateEmployeeAsync(CreateDto dto)
        {
            if(_context.Employees.Any(e => e.Email == dto.Email))
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }   
                var employee = new Employee
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Designation = dto.Designation,
                    ManagerId = dto.ManagerId,
                    Role = dto.Designation,
                };

                if (dto.ManagerId.HasValue)

                {
                    var managerExists = await _context.Employees
                        .AnyAsync(e => e.Id == dto.ManagerId.Value && e.Role == "Manager");

                    if (!managerExists)
                    {
                        return new ServiceResult
                        {
                            Success = false,
                            Message = "Selected manager is invalid"
                        };
                    }
                }

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                var leaveBalances = new List<LeaveBalance>
                {
                    new LeaveBalance { EmployeeId = employee.Id, LeaveTypeId = 1, RemainingDays = 10 },
                    new LeaveBalance { EmployeeId = employee.Id, LeaveTypeId = 2, RemainingDays = 12 },
                    new LeaveBalance { EmployeeId = employee.Id, LeaveTypeId = 3, RemainingDays = 15 }
                };

                _context.LeaveBalances.AddRange(leaveBalances);
                await _context.SaveChangesAsync();

                return new ServiceResult
                {
                    Success = true,
                    Message = "Employee has been created",
                    Data = employee
                };
            

}
}
}