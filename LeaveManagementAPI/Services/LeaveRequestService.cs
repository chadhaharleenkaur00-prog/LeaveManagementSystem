using LeaveManagementAPI.Data;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementAPI.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly AppDbContext _context;

        public LeaveRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> GetLeaveRequestsAsync()
        {
            if (_context.LeaveRequests is null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "No leave requests found"
                };
            }
            else
            {
                var leaveRequests = await _context.LeaveRequests.ToListAsync();
                return new ServiceResult
                {
                    Success = true,
                    Message = "Leave requests retrieved successfully",
                    Data = leaveRequests
                };
            }
        }

        public async Task<ServiceResult> GetLeaveRequestByIdAsync(int id)
        {
            if(_context.LeaveRequests is null)
            {
                return new ServiceResult
                { 
                    Success = false,
                    Message = "No leave requests found"  
                };
            }
            else {
                var leaveRequest = await _context.LeaveRequests.FindAsync(id);
                if(leaveRequest is null)
                {
                    return new ServiceResult
                    {
                        Success = false,
                        Message = "Leave request not found"
                    };
                }
                return new ServiceResult
                {
                    Success = true,
                    Message = "Leave request retrieved successfully",
                    Data = leaveRequest
                };
            }
        }

        public async Task<ServiceResult> CreateLeaveRequestAsync(LeaveRequestDTO dto)
        {
            var existingLeaveReq = await _context.LeaveRequests.FirstOrDefaultAsync(lr => lr.EmployeeId == dto.EmployeeId && lr.StartDate == dto.StartDate && lr.EndDate == dto.EndDate);
            if(existingLeaveReq != null)            
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Leave request already exists for the given dates"
                };
            }

            var leaveBalanceExceeds = await _context.LeaveTypes.Where(lt => lt.Id == dto.LeaveTypeId).Select(lt => lt.TotalDays).FirstOrDefaultAsync() < (dto.EndDate - dto.StartDate).Days + 1;
            if(leaveBalanceExceeds)            
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Leave balance exceeded for the selected leave type"
                };
            }

            var leaveRequest = new LeaveRequest
            {
                EmployeeId = dto.EmployeeId,
                LeaveTypeId = dto.LeaveTypeId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Reason = dto.Reason,
                Status = "Pending"
            };
        
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
            return new ServiceResult
            {
                Success = true,
                Message = "Leave request created successfully",
                Data = leaveRequest
            };
    
        }
        public async Task<ServiceResult> UpdateLeaveRequestStatusAsync(UpdateLeaveRequestStatusDTO updateDto)
        {
            var existingLeaveRequest = await _context.LeaveRequests.FindAsync(updateDto.id);
            if (existingLeaveRequest == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Leave request not found"
                };
            }

            existingLeaveRequest.Status = updateDto.Status;
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                Success = true,
                Message = "Leave request updated successfully",
                Data = existingLeaveRequest
            };
        }                   
        public async Task<ServiceResult> DeleteLeaveRequestAsync(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Leave request not found"
                };
            }

            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                Success = true,
                Message = "Leave request deleted successfully"
            };
        }
    }
}        
                