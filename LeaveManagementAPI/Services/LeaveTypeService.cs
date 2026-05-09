using LeaveManagementAPI.Data;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Models;
using LeaveManagementAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementAPI.Services{
    public class LeaveTypeService : ILeaveTypeService{

        private readonly AppDbContext _context;

        public LeaveTypeService(AppDbContext context){
            _context = context;
        }
    
        public async Task<ServiceResult> AddLeaveTypeAsync(LeaveTypeDTO dto){

            var existingTypeName = await _context.LeaveTypes.AnyAsync(l => l.TypeName == dto.TypeName);
            if (existingTypeName)
            {
                return new ServiceResult{
                    Success = false,
                    Message = "Leave Type already exists"
                };
            }
            else{
                var leaveType = new LeaveType{
                    TypeName = dto.TypeName,
                    TotalDays = dto.TotalDays
                };
                _context.LeaveTypes.Add(leaveType);
                await _context.SaveChangesAsync();

                return new ServiceResult{
                    Success = true,
                    Message = "Leave Type added successfully",
                    Data = leaveType
                };
            }
        }

        public async Task<ServiceResult> GetAllLeaveTypesAsync(){
            if(_context.LeaveTypes is null){
                return new ServiceResult{
                    Success = false,
                    Message = "No Leave Types found"
                };
            }
            else{
                var leaveTypesList = await _context.LeaveTypes.ToListAsync();
                return new ServiceResult{
                    Success = true,
                    Message = "Leave types fetched successfully!",
                    Data = leaveTypesList
                };
            }
        }
        
        public async Task<ServiceResult> GetLeaveTypeAsync(int id){
            if(_context.LeaveTypes is null){
                return new ServiceResult{
                    Success = false,
                    Message = "No Leave Types found"
                };
            }
            else{
                var leaveType = await _context.LeaveTypes.FindAsync(id);
                if (leaveType == null)
                {
                    return new ServiceResult{
                        Success = false,
                        Message = "Leave type not found"
                    };
                }
                return new ServiceResult{
                    Success = true,
                    Message = "Leave type fetched successfully!",
                    Data = leaveType
                };
            }
        }

        public async Task<ServiceResult> UpdateLeaveTypeAsync(int id, LeaveTypeDTO dto){
            if(_context.LeaveTypes is null){
                return new ServiceResult{
                    Success = false,
                    Message = "No Leave Types found"
                };
            }
            else{
                var leaveType = await _context.LeaveTypes.FindAsync(id);
                if (leaveType == null)
                {
                    return new ServiceResult{
                        Success = false,
                        Message = "Leave type not found"
                    };
                }
                leaveType.TypeName = dto.TypeName;
                leaveType.TotalDays = dto.TotalDays;
                await _context.SaveChangesAsync();

                return new ServiceResult{
                    Success = true,
                    Message = "Leave type updated successfully!",
                    Data = leaveType
                };
            }
        }

        public async Task<ServiceResult> DeleteLeaveTypeAsync(int id){
        
            if(_context.LeaveTypes is null){
                return new ServiceResult{
                    Success = false,
                    Message = "No Leave Types found"
                };
            }
            else{
                var leaveType = await _context.LeaveTypes.FindAsync(id);
                if (leaveType == null)
                {
                    return new ServiceResult{
                        Success = false,
                        Message = "Leave type not found"
                    };
                }
                _context.LeaveTypes.Remove(leaveType);
                await _context.SaveChangesAsync();

                return new ServiceResult{
                    Success = true,
                    Message = "Leave type removed successfully!",
                    Data = leaveType
                };
            }
        }
    }
}