using AutoMapper;
using MediatR;
using POC.Application.Contracts.Persistence;
using POC.Application.Exceptions;
using POC.Application.Responses;
using POC.Domain.Entitities;

namespace POC.Application.Features.Users.Command.DeleteUserCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseResult>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<User> _userRepository;

    public DeleteUserCommandHandler(IMapper mapper, IAsyncRepository<User> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ResponseResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userToDelete = await _userRepository.GetByIdAsync(request.Id);

        if (userToDelete == null) return new ResponseResult(new NotFoundException(nameof(request.Id), nameof(User), request.Id));


        await _userRepository.DeleteAsync(userToDelete);

        return new ResponseResult();
    }
}
