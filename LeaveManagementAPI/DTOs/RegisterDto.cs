namespace LeaveManagementAPI.DTOs
{
    public class RegisterDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
    }
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
    public class UpdateDto
    {
        public int id { get; set; } = 0;    
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
    }
    public class LeaveTypeDTO
    {
        public string TypeName { get; set; } = string.Empty;
        public int TotalDays { get; set; }
    }
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
    public class LeaveRequestDTO
    {
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
    public class UpdateLeaveRequestStatusDTO
    {
        public int id { get; set; }
        public string Status { get; set; }
    }
    public class LeaveBalanceDTO
    {
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public int RemainingDays { get; set; }
    }
    public class UpdateLeaveBalanceDTO
    {
        public int Id { get; set; }
        public int RemainingDays { get; set; }
    }
}

