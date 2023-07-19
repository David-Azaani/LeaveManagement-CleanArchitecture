
using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

    public class LeaveAllocationDto
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }

        // Navigation Property
        public LeaveTypeDto LeaveType { get; set; }
        public int LeaveTypeId { get; set; }


        public int Period { get; set; }
    }

