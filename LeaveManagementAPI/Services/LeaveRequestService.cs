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
                var leaveRequests = await _context.LeaveRequests
                    .Include(lr => lr.Employee)
                    .Include(lr => lr.LeaveType)
                    .Select(lr => new
                    {
                        lr.Id,
                        lr.EmployeeId,
                        Employee = new
                        {
                            lr.Employee.Name
                        },
                        LeaveType = new
                        {
                            lr.LeaveType.TypeName
                        },
                        lr.StartDate,
                        lr.EndDate,
                        lr.Reason,
                        lr.Status,
                        lr.CreatedAt
                    })
                    .ToListAsync();
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
                var leaveRequests = await _context.LeaveRequests
                    .Where(lr => lr.EmployeeId == id)
                    .Select(lr => new
                    {
                        lr.Id,
                        lr.EmployeeId,
                        LeaveType = new
                        {
                            lr.LeaveType.TypeName
                        },
                        lr.StartDate,
                        lr.EndDate,
                        lr.Reason,
                        lr.Status,
                        lr.CreatedAt
                    })
                    .ToListAsync();

                if(!leaveRequests.Any())
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
                    Message = "Leave requests retrieved successfully",
                    Data = leaveRequests
                };
            }
        }

        public async Task<ServiceResult> CreateLeaveRequestAsync(LeaveRequestDTO dto)
        {
            Console.WriteLine("CreateLeaveRequestAsync HIT");
            var existingLeaveReq = await _context.LeaveRequests.Where(lr => lr.EmployeeId == dto.EmployeeId && lr.StartDate == dto.StartDate && lr.EndDate == dto.EndDate).ToListAsync();
            if(existingLeaveReq.Any())            
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
                Status = "Pending",
                ManagerId = await _context.Employees.Where(e => e.Id == dto.EmployeeId).Select(e => e.ManagerId).FirstOrDefaultAsync()
            };
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
            // var leaveDays = (dto.EndDate - dto.StartDate).Days + 1;

            // var leaveBalance = await _context.LeaveBalances
            //     .FirstOrDefaultAsync(lb =>
            //         lb.EmployeeId == dto.EmployeeId &&
            //         lb.LeaveTypeId == dto.LeaveTypeId);

            // if (leaveBalance != null)
            // {
            //     leaveBalance.RemainingDays -= leaveDays;
            //     leaveBalance.UsedDays += leaveDays;

            //     await _context.SaveChangesAsync();
            // }
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
            if (updateDto.Status != "Approved" && updateDto.Status != "Rejected")
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Invalid leave request status"
                };
            }
            if (updateDto.Status == "Approved")
            {
                var leaveDays = (existingLeaveRequest.EndDate - existingLeaveRequest.StartDate).Days + 1;

                var leaveBalance = await _context.LeaveBalances
                    .FirstOrDefaultAsync(lb =>
                        lb.EmployeeId == existingLeaveRequest.EmployeeId &&
                        lb.LeaveTypeId == existingLeaveRequest.LeaveTypeId);

                if (leaveBalance != null)
                {
                    leaveBalance.RemainingDays -= leaveDays;
                    leaveBalance.UsedDays += leaveDays;
                }
            }

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
        public async Task<ServiceResult> GetLeaveRequestsByManagerIdAsync(int managerId)
        {
            var leaveRequests = await _context.LeaveRequests
                .Where(lr => lr.ManagerId == managerId)
                .Select(lr => new
                {
                    lr.Id,
                    lr.EmployeeId,
                    Employee = new
                    {
                        lr.Employee.Name
                    },
                    LeaveType = new
                    {
                        lr.LeaveType.TypeName
                    },
                    lr.StartDate,
                    lr.EndDate,
                    lr.Reason,
                    lr.Status,
                    lr.CreatedAt
                })
                .ToListAsync();

            return new ServiceResult
            {
                Success = true,
                Message = "Manager leave requests retrieved successfully",
                Data = leaveRequests
            };
        }
        public async Task<ServiceResult> GetLeaveRequestByRequestIdAsync(int reqId)
        {
            var leaveRequests = await _context.LeaveRequests
                .Where(lr => lr.Id == reqId)
                .Select(lr => new
                {
                    lr.Id,
                    lr.EmployeeId,
                    LeaveType = new
                    {
                        lr.LeaveType.TypeName
                    },
                    lr.StartDate,
                    lr.EndDate,
                    lr.Reason,
                    lr.Status,
                    lr.CreatedAt
                })
                .ToListAsync();
            if (!leaveRequests.Any())
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
                Message = "Employee leave requests retrieved successfully",
                Data = leaveRequests
            };
        }
    }
    
}        
                
