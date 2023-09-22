using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AccessManagement.Contract;

namespace AccessManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{

    private readonly IConfiguration configuration;
    private readonly IUserRepository userRepository;;
    private readonly IUserTokenRepository userTokenRepository;


    public AccountsController(IConfiguration configuration
        , IUserRepository userRepository
        , IUserTokenRepository userTokenRepository
        )
    {
        this.configuration = configuration;
        this.userRepository = userRepository;
        this.userTokenRepository = userTokenRepository;
    }

    [HttpPost]
    public IActionResult Post(string PhoneNumber, string SmsCode)
    {
        var loginResult = userRepository.Login(new LoginWithPhoneNumberCodeRequest(PhoneNumber, SmsCode));
        if (loginResult.IsSuccess == false)
        {
            return Ok(new LoginResultDto()
            {
                IsSuccess = false,
                Message = loginResult.Message
            });
        }
       // var token = CreateToken();

        return Ok(new LoginResultDto()
        {
            IsSuccess = true,
            Data = token,
        });
    }


    [HttpPost]
    [Route("RefreshToken")]
    public IActionResult RefreshToken(string Refreshtoken)
    {
        var usertoken = userTokenRepository.FindRefreshToken(Refreshtoken);
        if (usertoken == null)
        {
            return Unauthorized();
        }
        if (usertoken.RefreshTokenExp < DateTime.Now)
        {
            return Unauthorized("Token Expire");
        }

        var token = CreateToken(usertoken.User);
        userTokenRepository.DeleteToken(Refreshtoken);

        return Ok(token);
    }


    [Route("GetSmsCode")]
    [HttpGet]
    public IActionResult GetSmsCode(string PhoneNumber)
    {
        var smsCode = userRepository.GetCode(PhoneNumber);
        //smsCode پیامک کنید به همین شماره
        return Ok();
    }

    [Authorize]
    [HttpGet]
    [Route("Logout")]
    public IActionResult Logout()
    {
        var user = User.Claims.First(p => p.Type == "UserId").Value;
        userRepository.Logout(Guid.Parse(user));
        return Ok();
    }


    private LoginDataDto CreateToken(User user)
    {
        SecurityHelper securityHelper = new SecurityHelper();


        var claims = new List<Claim>
                {
                    new Claim ("UserId", user.Id.ToString()),
                    new Claim ("Name",  user?.Name??""),
                };
        string key = configuration["JWtConfig:Key"];
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenexp = DateTime.Now.AddMinutes(int.Parse(configuration["JWtConfig:expires"]));
        var token = new JwtSecurityToken(
            issuer: configuration["JWtConfig:issuer"],
            audience: configuration["JWtConfig:audience"],
            expires: tokenexp,
            notBefore: DateTime.Now,
            claims: claims,
            signingCredentials: credentials
            );
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        var refreshToken = Guid.NewGuid();

        userTokenRepository.SaveToken(new Models.Entities.UserToken()
        {
            MobileModel = "Iphone pro max",
            TokenExp = tokenexp,
            TokenHash = securityHelper.Getsha256Hash(jwtToken),
            User = user,
            RefreshToken = securityHelper.Getsha256Hash(refreshToken.ToString()),
            RefreshTokenExp = DateTime.Now.AddDays(30)
        });

        return new LoginDataDto()
        {
            Token = jwtToken,
            RefreshToken = refreshToken.ToString()
        };


    }
}
}



 