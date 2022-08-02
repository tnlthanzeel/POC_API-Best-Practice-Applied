namespace VPMS.Application.Security.Dtos;

public record UserCreateDto(
    string UserName,
    string Password,
    string Email
    );
