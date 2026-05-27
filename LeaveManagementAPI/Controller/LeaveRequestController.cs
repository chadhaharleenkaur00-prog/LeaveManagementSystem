using LeaveManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using LeaveManagementAPI.Services;
using LeaveManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LeaveManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaveRequests()
        {
            var result = await _leaveRequestService.GetLeaveRequestsAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetLeaveRequestById(int employeeId)
        {
            var result = await _leaveRequestService.GetLeaveRequestByIdAsync(employeeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestDTO dto)
        {
            var result = await _leaveRequestService.CreateLeaveRequestAsync(dto);
            if (result.Success)
            {
                var leaveRequest = (LeaveRequest)result.Data!;
                return CreatedAtAction(
                    nameof(GetLeaveRequestByRequestId),
                    new { leaveRequestId = leaveRequest.Id },
                    result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("status")]
        public async Task<IActionResult> UpdateLeaveRequestStatus(UpdateLeaveRequestStatusDTO updateDto)
        {
            var result = await _leaveRequestService.UpdateLeaveRequestStatusAsync(updateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("manager/{managerId}")]
        public async Task<IActionResult> GetLeaveRequestsByManagerId(int managerId)
        {
            var result = await _leaveRequestService.GetLeaveRequestsByManagerIdAsync(managerId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            var result = await _leaveRequestService.DeleteLeaveRequestAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpGet("request/{leaveRequestId}")]
        public async Task<IActionResult> GetLeaveRequestByRequestId(int leaveRequestId)
        {
            var result = await _leaveRequestService.GetLeaveRequestByRequestIdAsync(leaveRequestId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

    }
}
