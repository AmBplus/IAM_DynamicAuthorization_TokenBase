using MediatR;
using Base.Shared.ResultUtility;
using AccessManagement.Data;

namespace AccessManagement.Services.Query;
public record GetRoleByIdQueryRequest(Guid Id) : IRequest<ResultOperation<GetRoleResponseDto>>;

public  class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQueryRequest, ResultOperation<GetRoleResponseDto>>
{

    private readonly IAccessManagementDbContext _dbContext;

    public GetRoleByIdHandler(IAccessManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultOperation<GetRoleResponseDto>> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
    {

        var result = _dbContext.Roles.FirstOrDefault(x => x.Id == request.Id);
        if (result == null)
        {
            return new GetRoleResponseDto(Guid.Empty, null).ToFailedResult("نقش پیدا نشد");
        }
        return new GetRoleResponseDto(result.Id, result.Name).ToSuccessResult("عملیات با موفقیت انجام شد");

    }
}



