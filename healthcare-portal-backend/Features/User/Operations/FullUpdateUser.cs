﻿using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Healthcare_Patient_Portal.Models;
using Microsoft.EntityFrameworkCore;


namespace Healthcare_Patient_Portal.Features.User.Operations
{
    public class FullUpdateUser : IRequest<bool>
    {
        [FromRoute] public int UserId { get; set; }

        public string RoleType { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly Dob { get; set; }
    }

    public class FullUpdateUserValidator : AbstractValidator<FullUpdateUser>
    {
        private readonly HealthcarePortalContext _context;

        public FullUpdateUserValidator(HealthcarePortalContext context)
        {
            _context = context;

            CascadeMode = CascadeMode.Stop;
            
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).MustAsync(UserExists);
            RuleFor(x => x.RoleType).NotEmpty().Matches("^[a-zA-Z'\\s]+$");
            RuleFor(x => x.FirstName).NotEmpty().Matches("^[a-zA-Z'\\s]+$");
            RuleFor(x => x.LastName).NotEmpty().Matches("^[a-zA-Z'\\s]+$");
            RuleFor(x => x.Dob).NotEmpty();
            
        }
        
        async Task<bool> UserExists(int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
        }
        
        public class FullUpdateUserHandler : IRequestHandler<FullUpdateUser, bool>
        {
            readonly HealthcarePortalContext _context;

            public FullUpdateUserHandler(HealthcarePortalContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(FullUpdateUser request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.UserId);
                
                if(user == null)return false;
                
                user.RoleType = request.RoleType;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Dob = request.Dob;
                
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
    } 
}
