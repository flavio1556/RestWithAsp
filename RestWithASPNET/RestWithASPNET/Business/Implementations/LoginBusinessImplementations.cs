using RestWithASPNET.Business.Interfaces;
using RestWithASPNET.Configurations;
using RestWithASPNET.Configurations.Services;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Repository.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestWithASPNET.Business.Implementations
{
    public class LoginBusinessImplementations : IloginBusiness
    {
        private const string DATE_FORMART = "yyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementations(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var acessToken = _tokenService.GenerateAcessToken(claims);
            var refreshToken = _tokenService.GenerateRefrashToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);
            DateTime CreateDate = DateTime.Now;
            DateTime expirationDate = CreateDate.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfor(user);
            return new TokenVO(
                true,
                CreateDate.ToString(DATE_FORMART),
                expirationDate.ToString(DATE_FORMART),
                acessToken,
                refreshToken
                );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var acessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(acessToken);
            var username = principal.Identity.Name;

            var user = _repository.ValidateCredentials(username);

            if (user == null ||
                user.RefreshToken != refreshToken || 
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            acessToken = _tokenService.GenerateAcessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefrashToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);
            DateTime CreateDate = DateTime.Now;
            DateTime expirationDate = CreateDate.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfor(user);
            return new TokenVO(
                true,
                CreateDate.ToString(DATE_FORMART),
                expirationDate.ToString(DATE_FORMART),
                acessToken,
                refreshToken
                );
        }

        public bool RevokeToken(string userName)
        {
           return _repository.RevokeToken(userName);
        }
    }
}
