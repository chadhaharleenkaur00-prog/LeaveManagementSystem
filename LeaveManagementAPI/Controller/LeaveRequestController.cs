using LeaveManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using LeaveManagementAPI.Services;
using LeaveManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveRequestById(int id)
        {
            var result = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest(LeaveRequestDTO dto)
        {
            var result = await _leaveRequestService.CreateLeaveRequestAsync(dto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetLeaveRequestById), new { id = ((LeaveRequest)result.Data).Id }, result);
            }
            return BadRequest(result);
        }

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
    }
}