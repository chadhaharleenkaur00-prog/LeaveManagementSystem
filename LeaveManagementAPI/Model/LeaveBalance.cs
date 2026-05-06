namespace LeaveManagementAPI.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } = null!;
        public int UsedDays { get; set; } = 0;
        public int RemainingDays { get; set; }
    }
}