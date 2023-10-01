using MediatR;
using Base.Shared.ResultUtility;
using AccessManagement.Data;

namespace AccessManagement.Services.Query;
public record GetUserByIdQueryRequest(Guid Id) : IRequest<ResultOperation<GetUserDtoQueryResponse>>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, ResultOperation<GetUserDtoQueryResponse>>
{
    private readonly IAccessManagementDbContext _dbContext;

    public GetUserByIdQueryHandler(IAccessManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // Implementation 
    public async Task<ResultOperation<GetUserDtoQueryResponse>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.Id);
        if (user == null)
        {
            return new GetUserDtoQueryResponse(null, null).ToFailedResult("کاربر یافت نشد");
        }
        return new GetUserDtoQueryResponse(user.Email, user.UserName).ToSuccessResult("عملیات با موفقیت انجام شد");

    }
}
