using Base.Shared.ResultUtility;
using AccessManagement.Data;
using MediatR;

namespace AccessManagement.Services.Query;



//public class GetPaginateRolesQueryHandler : IRequestHandler<GetPaginateRolesQueryRequest, ResultOperation<PaginateResultDto<GetRoleResponseDto>>>
//{

//    // Implementation
//    public async Task<ResultOperation<PaginateResultDto<GetRoleResponseDto>>> Handle(GetPaginateUsersQueryRequest request, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }
//}

public record GetPaginateRolesQueryRequest(int Page, int PageSize) : IRequest<ResultOperation<PaginateResultDto<GetRoleResponseDto>>>;

public class GetPaginateRolesQueryHandler : IRequestHandler<GetPaginateRolesQueryRequest, ResultOperation<PaginateResultDto<GetRoleResponseDto>>>
{

    private readonly IAccessManagementDbContext _dbContext;

    public GetPaginateRolesQueryHandler(IAccessManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

  
    public async Task<ResultOperation<PaginateResultDto<GetRoleResponseDto>>> Handle(GetPaginateRolesQueryRequest request, CancellationToken cancellationToken)
    {
        var pagedRoles = _dbContext.Roles.Skip(request.Page * request.PageSize)
                                   .Take(request.PageSize)
                                   .Select(x => new GetRoleResponseDto(x.Id ,x.Name))
                                   .ToList();

        if (pagedRoles  == null)
        {
            return new PaginateResultDto<GetRoleResponseDto>
               (request.Page, request.PageSize, request.PageSize, null)
               .ToSuccessResult("نقشی وجود ندارد");
        }

        return new PaginateResultDto<GetRoleResponseDto>
             (request.Page, request.PageSize, request.PageSize, pagedRoles)
             .ToSuccessResult("کاربری وجود ندارد");
    }
}
