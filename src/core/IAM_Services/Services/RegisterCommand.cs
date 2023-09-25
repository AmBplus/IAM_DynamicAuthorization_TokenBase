
using AccessManagement.Entities;
using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace AccessManagement.Services;

    public record RegisterCommandRequest: IRequest<ResultOperation<int>>
    {
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "LastName is required")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }


}
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest , ResultOperation<int>>
    {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RegisterCommandHandler(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<ResultOperation<int>> Handle(RegisterCommandRequest model, CancellationToken cancellationToken)
        {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return ResultOperation<int>.ToFailedResult( "User already exists!", 400);

        var user = CreateUser(
            model.Email,
            Guid.NewGuid().ToString(),
            model.Username
        );
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return ResultOperation<int>.ToFailedResult(message: "User creation failed! Please check user details and try again." ,StatusCodes.Status500InternalServerError);

         return ResultOperation<int>.ToSuccessResult( message:"User created successfully!", 200 );
    }
    private ApplicationUser CreateUser(string? email, string securityStamp, string username)
    {
        try
        {
            var user = Activator.CreateInstance<ApplicationUser>();
            user.Email = email;
            user.SecurityStamp = securityStamp;
            user.UserName = username;
            return user;
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively ");

        }
    }
}
    
      

        
    

