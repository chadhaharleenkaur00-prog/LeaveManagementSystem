using Microsoft.AspNetCore.Mvc;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Services;
using LeaveManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace LeaveManagementAPI.Controller{

    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTypesController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypesController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetLeaveTypes()
        {
            var result = await _leaveTypeService.GetAllLeaveTypesAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveType(int id)
        {
            var result = await _leaveTypeService.GetLeaveTypeAsync(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return result.Message.Contains("not found") ? NotFound(result.Message) : BadRequest(result.Message);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateLeaveType(LeaveTypeDTO dto)
        {
            var result = await _leaveTypeService.AddLeaveTypeAsync(dto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetLeaveType), new { id = ((LeaveType)result.Data).Id }, result.Data);
            }
            return BadRequest(result.Message);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveType(int id, LeaveTypeDTO dto)
        {
            var result = await _leaveTypeService.UpdateLeaveTypeAsync(id, dto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return result.Message.Contains("not found") ? NotFound(result.Message) : BadRequest(result.Message);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveType(int id)
        {
            var result = await _leaveTypeService.DeleteLeaveTypeAsync(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return result.Message.Contains("not found") ? NotFound(result.Message) : BadRequest(result.Message);
        }

    }
}