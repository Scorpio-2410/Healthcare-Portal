using FluentValidation;
using Healthcare_Patient_Portal.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_Patient_Portal.Features.User.Operations
{
    public class SearchUser : IRequest<SearchUserResponse>
    {
        [FromRoute] public int UserId { get; set; }
    }

    public class SearchUserResponse
    {
        public string RoleType { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly Dob { get; set; }
    }

    public class SearchUserValidator : AbstractValidator<SearchUser>
    {
        readonly HealthcarePortalContext  _context;

        public SearchUserValidator(HealthcarePortalContext context)
        {
            _context = context;
            
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).MustAsync(UserExists);
        }
        
        private async Task<bool> UserExists(int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
        }

        public class SearchUserHandler : IRequestHandler<SearchUser, SearchUserResponse>
        {
            readonly HealthcarePortalContext _context;

            public SearchUserHandler(HealthcarePortalContext context)
            {
                _context = context;
            }
            public async Task<SearchUserResponse> Handle(SearchUser request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.
                    SingleOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
                
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {request.UserId} not found.");
                }
                
                return new SearchUserResponse
                {
                    RoleType = user.RoleType,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Dob = user.Dob
                };
            }
        }
    }
}
