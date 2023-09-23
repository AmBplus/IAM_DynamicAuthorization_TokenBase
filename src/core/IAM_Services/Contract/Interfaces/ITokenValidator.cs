using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AccessManagement.Contract
{
    public interface ITokenValidator
    {
        Task Execute(TokenValidatedContext context);
    }

}
