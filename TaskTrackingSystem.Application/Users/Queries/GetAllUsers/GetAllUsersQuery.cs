using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.Application.Common.Interfaces;

namespace TaskTrackingSystem.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<UserListVm>
    {
        public int Page {  get; set; }
        public int PageSize { get; set; }

        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, UserListVm>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            public GetAllUsersQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<UserListVm> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Users.AsQueryable();

                var totalCount = await query.CountAsync(cancellationToken);

                var users = await query
                    .OrderBy(u => u.Name)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<UserLookUpDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new UserListVm
                {
                    Users = users,
                    TotalCount = totalCount,
                    CurrentPage = request.Page,
                    PageSize = request.PageSize
                };

            }
        }
    }
}
