using AccessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Contract;
    public interface IUserTokenRepository
    {
        public void SaveToken(SaveTokenRequest request);

        public ApplicationUserToken FindRefreshToken(FindRefreshTokenRequest request);

        public void DeleteToken(DeleteTokenRequest request);

        public bool CheckExistToken(CheckExitTokenRequest request);
    }
public record SaveTokenRequest(ApplicationUserToken UserToken);
public record ApplicationUserTokenDto();
public record FindRefreshTokenRequest(string RefreshToken);

public record DeleteTokenRequest(string RefreshToken);
public record CheckExitTokenRequest(string Token);