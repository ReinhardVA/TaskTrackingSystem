using AutoMapper;
using TaskTrackingSystem.Application.Common.Mapping;
using TaskTrackingSystem.Domain.Entities;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.Users.Queries.GelAllUsers
{
    public class UserLookUpDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLookUpDto>()
                .ForMember(d => d.Role, opt => opt.MapFrom(d => d.Role));
        }
    }
}
