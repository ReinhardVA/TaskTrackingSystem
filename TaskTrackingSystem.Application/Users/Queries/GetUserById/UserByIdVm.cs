using AutoMapper;
using TaskTrackingSystem.Application.Common.Mapping;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Application.Users.Queries.GetUserById
{
    public class UserByIdVm : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserByIdVm>().
                ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role.ToString()));
        }
    }
}
