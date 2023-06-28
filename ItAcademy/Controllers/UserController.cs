using System.Net;
using ItAcademy.Application.Interfaces;
using ItAcademy.Application.Models;
using ItAcademy.Domain.Exceptions;
using ItAcademy.Models.BaseModels;
using ItAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItAcademy.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost] 
    public Result RegisterUser([FromBody] UserView userView)
    {
        if(!_userService.CreateUser(userView))
        {
            // 
            throw new UserAlreadyExistException(userView.Email);
        }

        return new Result(HttpStatusCode.Created, "Was created");
    }

    [HttpGet]
    public ResultValue<string> Login([FromQuery]string email, [FromQuery]string password)
    {
        
        var token = _userService.LoginUser(email, password);
        
        return new ResultValue<string>(HttpStatusCode.OK, "", token);
    }
}