using AutoMapper;
using MediatR;
using POC.Application.Contracts.Persistence;
using POC.Application.Exceptions;
using POC.Application.Responses;
using POC.Application.Validators;
using POC.Domain.Entitities;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Application.Features.Users.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, SuccessResponse<CreateUserCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<User> _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IAsyncRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<SuccessResponse<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = new SuccessResponse<CreateUserCommandResponse>();

            await Validator<CreateUserCommandValidator>.ValidateAsync(request, result);

            if (result.Success == false)
            {
                throw new ValidationException(result.ValidationErrors);
            }

            var user = new User().Create(request.FirstName, request.LastName, request.Address, request.School, request.Gender);

            user = await _userRepository.AddAsync(user);

            result.Data = _mapper.Map<CreateUserCommandResponse>(user);

            return result;
        }
    }
}
