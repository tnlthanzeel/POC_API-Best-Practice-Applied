using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VPMS.Application.Contracts.Security;
using VPMS.Application.PersistanceInterfaces;
using VPMS.Application.Security.Dtos;
using VPMS.Application.Security.Validators;
using VPMS.Domain.Entities.IdentityUserEntities;
using VPMS.SharedKernel.Exceptions;

namespace VPMS.Application.Security;

public class SecurityService : ISecurityService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenBuilder _tokenBuilder;
    private readonly IUserSecurityRespository _userSecurityRepository;

    public SecurityService(SignInManager<ApplicationUser> signInManager, ITokenBuilder tokenBuilder, IUserSecurityRespository userSecurityRepository)
    {
        _signInManager = signInManager;
        _tokenBuilder = tokenBuilder;
        _userSecurityRepository = userSecurityRepository;
    }

    public async Task<ResponseResult<AuthenticatedUserDto>> AuthenticateUser(AuthenticateUserDto model, CancellationToken token)
    {
        var user = await _userSecurityRepository.FindByEmail(model.Email);

        if (user is null) return new ResponseResult<AuthenticatedUserDto>(new UnauthorizedException("Invalid username or password"));

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);

        if (signInResult.Succeeded is false) return new ResponseResult<AuthenticatedUserDto>(new UnauthorizedException("Invalid username or password"));

        var jwtToken = await _tokenBuilder.GenerateJwtTokenAsync(user, token);

        var userClaims = await _userSecurityRepository.GetUserClaims(user.Id, token);

        AuthenticatedUserDto authuser = new()
        {
            BearerToken = jwtToken,
            Claims = userClaims,
            IsAuthenticated = signInResult.Succeeded,
            UserId = user.Id,
            UserName = user.UserName
        };

        return new ResponseResult<AuthenticatedUserDto>(authuser);
    }

    public async Task<ResponseResult<UserDto>> CreateUser(UserCreateDto model, CancellationToken token)
    {
        var validationResult = await Validator<UserCreateDtoValidator>.ValidateAsync(model, token);

        if (validationResult is { IsValid: false }) return new ResponseResult<UserDto>(validationResult.Errors);

        var responseResult = await _userSecurityRepository.CreateUser(model.UserName, model.Password, model.Email, token);

        if (responseResult.Success is false) return responseResult;

        return new ResponseResult<UserDto>(responseResult.Data);
    }

    public async Task<ResponseResult<UserDto>> Getuser(Guid id, CancellationToken token)
    {
        var appUser = await _userSecurityRepository.GetUser(id);

        if (appUser is null) return new ResponseResult<UserDto>(new NotFoundException(nameof(id), "User", id));

        UserDto user = new(appUser.Id, appUser.Email, appUser.UserName);

        return new ResponseResult<UserDto>(user);
    }
}
