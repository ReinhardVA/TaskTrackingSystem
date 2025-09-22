using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.Application.Common.Exceptions;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Application.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IAppDbContext _context;
        private readonly IUserManager _userManager;
        public DeleteUserCommandHandler(IAppDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            if(entity.Id == request.Id)
            {
                throw new BadRequestException("Employee cannot delete himself");
            }

            if(entity.Id != null)
            {
                await _userManager.DeleteUserAsync(entity.Id);
            }
            _context.Users.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
