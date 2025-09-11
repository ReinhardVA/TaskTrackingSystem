using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.Application.Common.Exceptions;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserByIdVm>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserByIdVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserByIdVm> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }
            return _mapper.Map<UserByIdVm>(user);
        }
    }
}
