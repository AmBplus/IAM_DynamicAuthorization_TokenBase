﻿using AccessManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace AccessManagement.Services
{
    public class RevokeTokenRequest : IRequest
    {
        public string UserId { get; set; }
    }
    // Revoke token handler
public class RevokeTokenHandler : IRequestHandler<RevokeTokenRequest>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public RevokeTokenHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Handle(RevokeTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null) { return; }   
            user.RefreshToken = null;

            await userManager.UpdateAsync(user);

           
        }
    }
}
