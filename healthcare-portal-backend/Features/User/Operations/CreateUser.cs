using FluentValidation;
using MediatR;
using Healthcare_Patient_Portal.Models;

namespace Healthcare_Patient_Portal.Features.User.Operations
{
    public class CreateUser : IRequest<CreateUserResponse>
    {
        public string RoleType { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly Dob { get; set; }
    }

    public class CreateUserResponse
    {
        public int UserId { get; set; }
    }

    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        readonly HealthcarePortalContext _context;
        public CreateUserValidator(HealthcarePortalContext context)
        {
            _context = context;

            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.RoleType).NotEmpty().Matches("^[a-zA-Z'\\s]+$");
            RuleFor(x => x.FirstName).NotEmpty().Matches("^[a-zA-Z'\\s]+$");
            RuleFor(x => x.LastName).NotEmpty().Matches("^[a-zA-Z'\\s]+$");
            RuleFor(x => x.Dob).NotEmpty();
        }

        public class CreateUserHandler : IRequestHandler<CreateUser, CreateUserResponse> 
        {
            readonly HealthcarePortalContext _context;
            public CreateUserHandler(HealthcarePortalContext context)
            {
                _context= context;
            }

            public async Task<CreateUserResponse> Handle(CreateUser request, CancellationToken cancellationToken)
            {
                var user = new Healthcare_Patient_Portal.Models.User
                {
                    RoleType = request.RoleType,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Dob = request.Dob,
                };
                
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new() { UserId = user.UserId };
            }
        }
    }
}
