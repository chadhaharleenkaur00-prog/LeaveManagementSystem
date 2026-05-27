using Microsoft.EntityFrameworkCore;
using LeaveManagementAPI.Models;

namespace LeaveManagementAPI.Data{
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options){

            }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType { Id = 1, TypeName = "Sick Leave", TotalDays = 10 },
                new LeaveType { Id = 2, TypeName = "Casual Leave", TotalDays = 12 },
                new LeaveType { Id = 3, TypeName = "Annual Leave", TotalDays = 15 }
            );
        }

    }
}