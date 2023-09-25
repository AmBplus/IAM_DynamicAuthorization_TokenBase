using AccessManagement.Data;
using MediatR;
using Base.Shared.ResultUtility;


namespace AccessManagement.Services.Query;
public record GetUserByUsernameQueryRequest(string Username) : IRequest<ResultOperation<GetUserDtoQueryResponse>>;

    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQueryRequest, ResultOperation<GetUserDtoQueryResponse>>
{

    private readonly IAccessManagementDbContext _dbContext;

        public GetUserByUsernameHandler(IAccessManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

   

    public  async  Task<ResultOperation<GetUserDtoQueryResponse>>  Handle(GetUserByUsernameQueryRequest request, CancellationToken cancellationToken)
        {
            var user =  _dbContext.Users.FirstOrDefault(x => x.UserName == request.Username);
            if(user == null) {
                return new GetUserDtoQueryResponse( null, null).ToFailedResult("کاربر یافت نشد");
            }
            return new GetUserDtoQueryResponse(user.Email,user.UserName).ToSuccessResult("عملیات با موفقیت انجام شد");
        }
   }

