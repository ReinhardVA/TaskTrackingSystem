using AutoMapper;
using TaskTrackingSystem.Application.Common.Mapping;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Application.WorkItems.Queries.GetWorkItemByIdQuery
{
    public class WorkItemByIdVm : IMapFrom<WorkItem>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public string? AssignedUserName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<WorkItem, WorkItemByIdVm>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.AssignedUserName, opt => opt.MapFrom(s => s.User != null ? s.User.Name : "Unassigned"));
        }

    }
}
