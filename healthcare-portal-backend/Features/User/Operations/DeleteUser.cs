using Healthcare_Patient_Portal.Models;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_Patient_Portal.Features.User.Operations
{
    public class DeleteUser : IRequest<bool>
    {
        [FromHeader] public int UserId { get; set; }
    }

    public class DeleteUserValidator : AbstractValidator<DeleteUser>
    {
        readonly HealthcarePortalContext _context;

        public DeleteUserValidator(HealthcarePortalContext context)
        {
            _context = context;
            
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
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
                    return false;
                }
                
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
                
                return true;
            }
        }
    }
}
