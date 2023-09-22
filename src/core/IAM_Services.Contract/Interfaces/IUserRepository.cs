using AccessManagement.Entities;


namespace AccessManagement.Contract;
public record GetUserRequest(Guid UserId);
public record ValidateUserRequest(string UserName, string Password);
public record GetLoginCodeRequest(string PhoneNumber );
public record LoginWithPhoneNumberCodeRequest(string PhoneNumber ,string Code);
public record FindUserWithPhoneNumberRequest(string PhoneNumber );
public record RegisterUserRequest(RegisterApplicationUserDto RegisterApplicationUser)
{
    public RegisterUserRequest(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; }
}

public record LogOutTokenBaseRequest(Guid UserId);
    public interface IUserRepository
    {
        public ApplicationUser GetUser(GetUserRequest request);

        public bool ValidateUser(ValidateUserRequest request);

        public string GetCode(GetLoginCodeRequest request);

        public LoginDto Login(LoginWithPhoneNumberCodeRequest request);

        public ApplicationUser FindUserWithPhoneNumber(FindUserWithPhoneNumberRequest request);

        public ApplicationUser RegisterUser(RegisterUserRequest registerRequest);
        public void Logout(LogOutTokenBaseRequest request);
    }



