using LeaveManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using LeaveManagementAPI.Services;
using LeaveManagementAPI.DTOs;

namespace LeaveManagementAPI.Services
{
    public interface ILeaveBalanceService
    {
        Task<ServiceResult> GetLeaveBalancesAsync();
        Task<ServiceResult> GetLeaveBalanceByIdAsync(int id);
        Task<ServiceResult> GetLeaveBalanceByEmployeeIdAsync(int employeeId);
        Task<ServiceResult> CreateLeaveBalanceAsync(LeaveBalanceDTO dto);
        Task<ServiceResult> UpdateLeaveBalanceAsync(UpdateLeaveBalanceDTO dto);
        Task<ServiceResult> DeleteLeaveBalanceAsync(int id);
    }
}