using Microsoft.AspNetCore.Mvc;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace LeaveManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(){
            var result = await _employeeService.GetEmployeesAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeById(int id, UpdateDto dto)
        {
            var result = await _employeeService.UpdateEmployeeByIdAsync(id, dto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            var result = await _employeeService.DeleteEmployeeByIdAsync(id);
            if (result.Success)            {
                return Ok(result.Data);}
            return BadRequest(result.Message);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateDto dto)
        {
            var result = await _employeeService.CreateEmployeeAsync(dto);
            if (result.Success)            {
                return Ok(result.Data);}
            return BadRequest(result.Message);  
    }
}
}
    
