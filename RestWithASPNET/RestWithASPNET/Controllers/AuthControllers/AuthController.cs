using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business.Interfaces;
using RestWithASPNET.Data.VO;
using System;

namespace RestWithASPNET.Controllers.AuthControllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IloginBusiness _loginBusiness;

        public AuthController(IloginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }
        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Invalid client Request");
            try
            {
                var  token = _loginBusiness.ValidateCredentials(user);
                if(token == null) return Unauthorized();
                return Ok(token);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
        [HttpPost, Route("refresh")]
        public IActionResult Refrash([FromBody] TokenVO tokenvO)
        {
            if (tokenvO == null) return BadRequest("Invalid client Request");
            try
            {
                var token = _loginBusiness.ValidateCredentials(tokenvO);
                if (token == null) return BadRequest("Invalid client Request"); 
                return Ok(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet, Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {

            try
            {
                var username = User.Identity.Name;
                var result = _loginBusiness.RevokeToken(username);
                if (!result) return BadRequest("Invalid client Request");
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
