using System.Net;
using ItAcademy.Application.Interfaces;
using ItAcademy.Application.Models;
using ItAcademy.Domain.Exceptions;
using ItAcademy.Models.BaseModels;
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
    public async Task<Result> RegisterUser([FromBody] UserView userView)
    {
        if(!await _userService.CreateUserAsync(userView))
        {
            // TODO: Log info
            throw new UserAlreadyExistException(userView.Email);
        }

        return new Result(HttpStatusCode.Created, "Was created");
    }

    [HttpGet]
    public async Task<ResultValue<string>> Login([FromQuery]string email, [FromQuery]string password)
    {
        var token = await _userService.LoginUserAsync(email, password);
        
        return new ResultValue<string>(HttpStatusCode.OK, "", token);
    }
}