using MediatR;
using TaskTrackingSystem.Application.Common.Exceptions;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Entities;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.Users.Commands.Update
{
    public class UpsertUserCommand : IRequest<Guid>
    {
        public Guid? Id;
        public required string Name;
        public required string Email;
        public required Role role;
    }

    public class UpsertUserCommandHandler : IRequestHandler<UpsertUserCommand, Guid>
    {
        private readonly IAppDbContext _context;
        public UpsertUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(UpsertUserCommand request, CancellationToken cancellationToken)
        {
            User entity;

            if (request.Id.HasValue)
            {
                entity = await _context.Users.FindAsync(request.Id.Value);
            }
            else
            {
                throw new NotFoundException(nameof(User), request.Id);
                //entity = new User();
                //_context.Users.Add(entity);
            }

            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.Role = request.role;

            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

}
