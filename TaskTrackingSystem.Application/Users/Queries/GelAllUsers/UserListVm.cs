namespace TaskTrackingSystem.Application.Users.Queries.GelAllUsers
{
    public class UserListVm
    {
        public IList<UserLookUpDto> Users { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

    }
}
