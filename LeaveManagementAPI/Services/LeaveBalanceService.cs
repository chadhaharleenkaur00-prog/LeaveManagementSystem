using LeaveManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using LeaveManagementAPI.Services;
using LeaveManagementAPI.DTOs;      
using LeaveManagementAPI.Data;

namespace LeaveManagementAPI.Services
{
    public class LeaveBalanceService : ILeaveBalanceService
    {
        private readonly AppDbContext _context;

        public LeaveBalanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> GetLeaveBalancesAsync()
        {
            if (_context.LeaveBalances is null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "No leave balances found"
                };
            }
            else
            {
                var leaveBalances = await _context.LeaveBalances.ToListAsync();
                return new ServiceResult
                {
                    Success = true,
                    Message = "Leave balances retrieved successfully",
                    Data = leaveBalances
                };
            }
        }

        public async Task<ServiceResult> GetLeaveBalanceByIdAsync(int id)
        {
            if (_context.LeaveBalances is null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "No leave balances found"
                };
            }
            else
            {
                var leaveBalance = await _context.LeaveBalances.FindAsync(id);
                if (leaveBalance is null)
                {
                    return new ServiceResult
                    {
                        Success = false,
                        Message = "Leave balance not found"
                    };
                }
                return new ServiceResult
                {
                    Success = true,
                    Message = "Leave balance retrieved successfully",
                    Data = leaveBalance
                };
            }
        }

        public async Task<ServiceResult> CreateLeaveBalanceAsync(LeaveBalanceDTO dto)
        {
            var existingLeaveBalance = await _context.LeaveBalances.FirstOrDefaultAsync(lb => lb.EmployeeId == dto.EmployeeId && lb.LeaveTypeId == dto.LeaveTypeId);
            if (existingLeaveBalance != null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Leave balance already exists for this employee and leave type"
                };
            }
            var leaveBalance = new LeaveBalance
            {
                EmployeeId = dto.EmployeeId,
                LeaveTypeId = dto.LeaveTypeId,
                RemainingDays = dto.RemainingDays
            };

            _context.LeaveBalances.Add(leaveBalance);
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                Success = true,
                Message = "Leave balance created successfully",
                Data = leaveBalance
            };
        }
        public async Task<ServiceResult> UpdateLeaveBalanceAsync(UpdateLeaveBalanceDTO dto)
        {
            var existingLeaveBalance = await _context.LeaveBalances.FindAsync(dto.Id);
            if (existingLeaveBalance == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Leave balance not found"
                };
            }

            existingLeaveBalance.RemainingDays = dto.RemainingDays;
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                Success = true,
                Message = "Leave balance updated successfully",
                Data = existingLeaveBalance
            };
        }
        public async Task<ServiceResult> DeleteLeaveBalanceAsync(int id)
        {
            var existingLeaveBalance = await _context.LeaveBalances.FindAsync(id);
            if (existingLeaveBalance == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Leave balance not found"
                };
            }

            _context.LeaveBalances.Remove(existingLeaveBalance);
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                Success = true,
                Message = "Leave balance deleted successfully"
            };
         }   
    }   
}   

        