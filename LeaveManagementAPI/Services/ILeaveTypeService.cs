using LeaveManagementAPI.DTOs;

namespace LeaveManagementAPI.Services{
    public interface ILeaveTypeService{
        Task<ServiceResult> AddLeaveTypeAsync(LeaveTypeDTO dto);
        Task<ServiceResult> GetAllLeaveTypesAsync();
        Task<ServiceResult> GetLeaveTypeAsync(int id);
        Task<ServiceResult> UpdateLeaveTypeAsync(int id, LeaveTypeDTO dto);
        Task<ServiceResult> DeleteLeaveTypeAsync(int id);
    }
}