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
    }

}