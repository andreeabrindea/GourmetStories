using ErrorOr;
using GourmetStories.Contracts.GourmetStories;
using GourmetStories.Services;
using GourmetStories.Models;
using Microsoft.AspNetCore.Mvc;

namespace GourmetStories.Controllers;

public class UsersController(IUserService userService, TokenProvider tokenProvider) : ApiController
{
    private readonly IPasswordHasher _passwordHasher = new PasswordHasher();

    [HttpPost]
    public IActionResult CreateUser(CreateUserRequest request)
    {
        var user = Models.User.Create(
            request.Username,
            _passwordHasher.Hash(request.Password),
            request.Email,
            Guid.NewGuid()
        );

        if (user.IsError)
        {
            return Problem(user.Errors);
        }
        var createUserResult = userService.CreateUser(user.Value);
        return createUserResult.Match(
            _ => CreatedNewUser(user.Value),
            Problem);
    }

    public IActionResult GetUser(Guid id)
    {
        ErrorOr<User> getUserResult = userService.GetUser(id);
        return getUserResult.Match(
            user => Ok(MapUserResponse(user)),
            Problem);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertUser(Guid id, UpsertUserRequest request)
    {
        var user = Models.User.Create(
            request.Username,
            request.Password,
            request.Email,
            id
        );

        if (user.IsError)
        {
            return Problem(user.Errors);
        }

        ErrorOr<UpsertUserResult> updateUserResult = userService.UpsertUser(user.Value);
        return updateUserResult.Match(
            updated => NoContent(),
            Problem);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteUser(Guid id)
    {
        ErrorOr<Deleted> deleteUserResult = userService.DeleteUser(id);
        return deleteUserResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpGet]
    public IActionResult LoginUser([FromQuery]LoginRequest loginRequest)
    {
        ErrorOr<User> getUserResult = userService.GetUserByEmail(loginRequest.Email);
        return getUserResult.Match(
            value =>
            {
                User user = value;
                if (_passwordHasher.Verify(loginRequest.Password, user.Password))
                {
                    return Ok(tokenProvider.Create(user));
                }
                else
                {
                    return Problem(statusCode: 401, title: "Invalid credentials.");
                }
            },
            Problem
        );

    }

    private static User MapUserResponse(User user)
    {
        return Models.User.Create(
            user.Username,
            user.Password,
            user.Email,
            user.Id
        ).Value;
    }

    private CreatedAtActionResult CreatedNewUser(User user)
    {
        return CreatedAtAction(
            actionName: nameof(GetUser),
            routeValues: new { id = user.Id },
            value: tokenProvider.Create(user)
        );
    }
}