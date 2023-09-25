using AccessManagement.Data;
using MediatR;
using Base.Shared.ResultUtility;
using static AccessManagement.Services.Query.GetRoleByIdHandler;

namespace AccessManagement.Services.Query;
public record GetUserByEmailQueryRequest(string Email) : IRequest<ResultOperation<GetUserDtoQueryResponse>>;


    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQueryRequest, ResultOperation<GetUserDtoQueryResponse>>
    {
        private readonly IAccessManagementDbContext _dbContext;

        public GetUserByEmailHandler(IAccessManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Implementation 
        public async Task<ResultOperation<GetUserDtoQueryResponse>> Handle(GetUserByEmailQueryRequest request, CancellationToken cancellationToken)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == request.Email);
            if (user == null)
            {
                return new GetUserDtoQueryResponse(null, null).ToFailedResult("کاربر یافت نشد");
            }
            return new GetUserDtoQueryResponse(user.Email, user.UserName).ToSuccessResult("عملیات با موفقیت انجام شد");

        }
    }

