using Healthcare_Patient_Portal.Models;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_Patient_Portal.Features.User.Operations
{
    public class DeleteUser : IRequest<bool>
    {
        [FromRoute] public int UserId { get; set; }
    }

    public class DeleteUserValidator : AbstractValidator<DeleteUser>
    {
        readonly HealthcarePortalContext _context;

        public DeleteUserValidator(HealthcarePortalContext context)
        {
            _context = context;
            
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).MustAsync(UserExists);
        }
        private async Task<bool> UserExists(int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
        }

        public class DeleteUserHandler : IRequestHandler<DeleteUser, bool>
        {
            readonly HealthcarePortalContext _context;
            public DeleteUserHandler(HealthcarePortalContext context)
            {
                _context= context;
            }

            public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.UserId);
                
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {request.UserId} not found.");
                }
                
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
                
                return true;
            }
        }
    }
}
