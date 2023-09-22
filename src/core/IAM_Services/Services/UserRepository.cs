using AccessManagement.Data;
using AccessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Contract;


namespace AccessManagement.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IAccessManagementDbContext context;
        public UserRepository(IAccessManagementDbContext context)
        {
            this.context = context;
        }


   

  


     

       


        public ApplicationUser GetUser(GetUserRequest request)
        {
            var user = context.Users.SingleOrDefault(p => p.Id == request.UserId);
            return user;
        }

        public bool ValidateUser(ValidateUserRequest request)
        {
            var user = context.Users.FirstOrDefault();
            return user != null ? true : false;
        }

        public string GetCode(GetLoginCodeRequest request)
        {
            Random random = new Random();
            string code = random.Next(1000, 9999).ToString();
            ApplicationSmsCode smsCode = new ApplicationSmsCode()
            {
                Code = code,
                InsertDate = DateTime.Now,
                PhoneNumber = request.PhoneNumber,
                RequestCount = 0,
                Used = false,
            };
            context.Add(smsCode);
            context.SaveChanges();

            return code;
        }

        public LoginDto Login(LoginWithPhoneNumberCodeRequest request)
        {
            var smsCode = context.SmsCodes.Where(p => p.PhoneNumber == request.PhoneNumber
    && p.Code == request.Code).FirstOrDefault();
            if (smsCode == null)
            {
                return new LoginDto
                {
                    IsSuccess = false,
                    Message = "کد وارد شده صحیح نیست!",

                };
            }
            else
            {
                if (smsCode.Used == true)
                {
                    return new LoginDto
                    {
                        IsSuccess = false,
                        Message = "کد وارد شده صحیح نیست!",

                    };
                }
                ////
                ///
                smsCode.RequestCount++;

                smsCode.Used = true;
                context.SaveChanges();
                var user = FindUserWithPhoneNumber(new FindUserWithPhoneNumberRequest(request.PhoneNumber));
                if (user != null)
                {
                    throw new NotImplementedException();
                    return new LoginDto
                    {
                        IsSuccess = true,
                        
                        //User = t,
                    };
                }
                else
                {
                   
                    return new LoginDto
                    {
                        IsSuccess = false,
                        User = null,
                    };
                }

            }

        }

        public ApplicationUser FindUserWithPhoneNumber(FindUserWithPhoneNumberRequest request)
        {
            var user = context.Users.SingleOrDefault(p => p.PhoneNumber.Equals(request.PhoneNumber));
            return user;
        }

        public ApplicationUser RegisterUser(RegisterUserRequest registerRequest)
        {
            ApplicationUser user = new ApplicationUser()
            {
                PhoneNumber = registerRequest.RegisterApplicationUser.PhoneNumber,
                IsActive = true,
            };
            return user;
        }

        public void Logout(LogOutTokenBaseRequest request)
        {
            var userToken = context.UserTokens.Where(p => p.UserId == request.UserId).ToList();
            foreach (var token in userToken)
            {
                context.UserTokens.Remove(token);
            }
            context.SaveChanges();
        }
    }


}
