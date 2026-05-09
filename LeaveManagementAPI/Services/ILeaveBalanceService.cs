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
        Task<ServiceResult> CreateLeaveBalanceAsync(LeaveBalanceDTO dto);
        Task<ServiceResult> UpdateLeaveBalanceAsync(int id, LeaveBalanceDTO dto);
        Task<ServiceResult> DeleteLeaveBalanceAsync(int id);
    }
}