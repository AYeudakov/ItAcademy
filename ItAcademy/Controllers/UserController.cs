using System.Net;
using System.Security.Claims;
using ItAcademy.Application.Interfaces;
using ItAcademy.Application.Models;
using ItAcademy.Domain.Exceptions;
using ItAcademy.Models.BaseModels;
using Microsoft.AspNetCore.Authorization;
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
    
    [HttpPost("register")] 
    public async Task<Result> RegisterUser([FromBody] UserView userView)
    {
        if(!await _userService.CreateUserAsync(userView))
        {
            throw new UserAlreadyExistException(userView.Email);
        }

        return new Result(HttpStatusCode.Created, "Was created");
    }

    [HttpPost("login")]
    public async Task<ResultValue<string>> Login([FromBody] UserView userView)
    {
        var token = await _userService.LoginUserAsync(userView.Email, userView.Password);
        
        return new ResultValue<string>(HttpStatusCode.OK, "", token);
    }

    [Authorize]
    [HttpPut]
    public async Task<ResultValue<UserView>> Update([FromBody] UserView userView)
    {
        var currentUserEmail = User.FindFirstValue(ClaimTypes.Email)!;

        var result = await _userService.UpdateUserAsync(userView, currentUserEmail);

        return new ResultValue<UserView>(HttpStatusCode.Created, "Data was updated", result);
    }

    [Authorize]
    [HttpDelete]
    public async Task<Result> Delete()
    {
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        
        await _userService.DeleteUserAsync(email);

        return new Result(HttpStatusCode.OK, "Resource was deleted");
    }
}