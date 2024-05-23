using CRUD.Core.Interface;
using CRUD.Core.Model;
using CRUD.Helpers;
using CRUD.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace CRUD.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUser _IUser;
    private readonly JwtServices _JwtServices;

    public UserController(IUser iUser, JwtServices jwtServices)
    {
        _IUser = iUser;
        _JwtServices = jwtServices;
    }

    [HttpGet]
    public async Task<UserModel> GetItem(int Id)
    {
        return await _IUser.GetItem(Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetLogin(string Username, string Password)
    {
        var user = await _IUser.GetLogin(Username);

        var jwt = "";
        if(user != null && Password == user.Password){
            jwt = _JwtServices.Generate(user.RecordId);
            Response.Cookies.Append("jwt", jwt, new CookieOptions{
                HttpOnly = true
            });

            user.jwtToken = jwt;
        }

        return Ok(new {
            jwtToken = user != null ? jwt : "",
            name = user != null ? user.FirstName : "",
        }); 
    }

    [HttpGet]
    public async Task<List<UserModel>> GetAll()
    {
        var jwt = Request.Cookies["jwt"];
        var token = _JwtServices.Verify(jwt);
        int userId = int.Parse(token.Issuer);

        Console.WriteLine("AAA");
        Console.WriteLine(userId);
        Console.WriteLine("AAA");

        return await _IUser.GetAll();
    }

    [HttpPost]
    public async Task<UserModel> AddUpdate(UserModel model)
    {
        return await _IUser.AddUpdate(model);
    }

    [HttpPost]
    public async Task<UserModel> UpdateLogin(UserModel model)
    {
        return await _IUser.UpdateLogin(model);
    }    
}
