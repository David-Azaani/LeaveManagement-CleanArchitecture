using AutoMapper;
using LeaveManagement.Domain;

namespace LeaveManagement.Application.MappingProfiles;

    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>();
        }
    }

