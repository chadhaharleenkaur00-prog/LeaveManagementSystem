using LeaveManagementAPI.Models;
using LeaveManagementAPI.DTOs;

namespace LeaveManagementAPI.Services
{
    public interface ILeaveRequestService
    {
        Task<ServiceResult> GetLeaveRequestsAsync();
        Task<ServiceResult> GetLeaveRequestByIdAsync(int id);
        Task<ServiceResult> CreateLeaveRequestAsync(LeaveRequestDTO dto);
        Task<ServiceResult> UpdateLeaveRequestStatusAsync(UpdateLeaveRequestStatusDTO updateDto);
        Task<ServiceResult> DeleteLeaveRequestAsync(int id);
        Task<ServiceResult> GetLeaveRequestsByManagerIdAsync(int managerId);
        Task<ServiceResult> GetLeaveRequestByRequestIdAsync(int leaveRequestId);
    }
}