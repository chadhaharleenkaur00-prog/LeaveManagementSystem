using LeaveManagementAPI.Services;  
using Microsoft.AspNetCore.Mvc;
using LeaveManagementAPI.DTOs;      
using LeaveManagementAPI.Models;
using LeaveManagementAPI.Data;

namespace LeaveManagementAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveBalanceController : ControllerBase
    {
        private readonly ILeaveBalanceService _leaveBalanceService;

        public LeaveBalanceController(ILeaveBalanceService leaveBalanceService)
        {
            _leaveBalanceService = leaveBalanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaveBalances()
        {
            var result = await _leaveBalanceService.GetLeaveBalancesAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveBalanceById(int id)
        {
            var result = await _leaveBalanceService.GetLeaveBalanceByIdAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateLeaveBalance(LeaveBalanceDTO dto)
        {
            var result = await _leaveBalanceService.CreateLeaveBalanceAsync(dto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetLeaveBalanceById), new { id = ((LeaveBalance)result.Data).Id }, result);
            }
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveBalance(UpdateLeaveBalanceDTO dto)
        {
            dto.Id = dto.Id;
            var result = await _leaveBalanceService.UpdateLeaveBalanceAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveBalance(int id)
        {
            var result = await _leaveBalanceService.DeleteLeaveBalanceAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
    
}