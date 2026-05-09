using LeaveManagementAPI.DTOs;

namespace LeaveManagementAPI.Services
{
    public interface IEmployeeService
    {
        Task<ServiceResult> GetEmployeesAsync();
        Task<ServiceResult> GetEmployeeByIdAsync(int id);
        Task<ServiceResult> UpdateEmployeeByIdAsync(int id, UpdateDto dto);
        Task<ServiceResult> DeleteEmployeeByIdAsync(int id);
    }
}