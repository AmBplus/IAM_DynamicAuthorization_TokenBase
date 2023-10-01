using Base.Shared.ResultUtility;
using AccessManagement.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Query;



public record GetPaginateUsersQueryRequest(int Page, int PageSize) 
    : IRequest<ResultOperation<PaginateResultDto<GetUserDtoQueryResponse>>> ;

public class GetPaginateUsersHandler : IRequestHandler
    <GetPaginateUsersQueryRequest,
    ResultOperation<PaginateResultDto<GetUserDtoQueryResponse>>
    >

{
    IAccessManagementDbContext context;

    public GetPaginateUsersHandler(IAccessManagementDbContext context)
    {
        this.context = context;
    }

  public async  Task<ResultOperation<PaginateResultDto<GetUserDtoQueryResponse>>> Handle(GetPaginateUsersQueryRequest request, CancellationToken cancellationToken)
    {


            var pagedUsers = context.Users?.Skip(request.Page  * request.PageSize)
                                  .Take(request.PageSize)
                              .Select(x => new GetUserDtoQueryResponse(x.Email, x.UserName))
                              .ToList();

       if(pagedUsers == null)
        {
             return new PaginateResultDto<GetUserDtoQueryResponse>
                (request.Page, request.PageSize,request.PageSize,null)
                .ToSuccessResult("کاربری وجود ندارد");
        }
       
        return new PaginateResultDto<GetUserDtoQueryResponse>
             (request.Page, request.PageSize, request.PageSize, pagedUsers)
             .ToSuccessResult("عملیات با موفقیت انجام شد");

    }
}

