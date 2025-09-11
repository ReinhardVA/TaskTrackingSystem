using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Enums;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Application.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required Role Role { get; set; }
    }
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = request.Role,
            };
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
