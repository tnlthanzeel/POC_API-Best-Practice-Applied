using AutoMapper;
using MediatR;
using POC.Application.Contracts.Persistence;
using POC.Application.Exceptions;
using POC.Application.Responses;
using POC.Application.Validators;
using POC.Domain.Entitities;

namespace POC.Application.Features.Users.Command.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseResult<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<User> _userRepository;

    public UpdateUserCommandHandler(IMapper mapper, IAsyncRepository<User> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<ResponseResult<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var eventToUpdate = await _userRepository.GetByIdAsync(request.Id);

        if (eventToUpdate == null)
        {
            throw new NotFoundException(nameof(request.Id), nameof(User), request.Id);
        }

        var validationResult = await Validator<UpdateUserCommandValidator>.ValidateAsync(request);

        if (validationResult.IsValid == false)
        {
            return new ResponseResult<Unit>(validationResult.Errors);
        }

        _mapper.Map(request, eventToUpdate);

        await _userRepository.UpdateAsync(eventToUpdate);

        return new ResponseResult<Unit>(Unit.Value);
    }
}
