using AutoMapper;
using MediatR;
using POC.Application.Contracts.Persistence;
using POC.Application.Exceptions;
using POC.Application.Responses;
using POC.Application.Validators;
using POC.Domain.Entitities;

namespace POC.Application.Features.Users.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseResult<CreateUserCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<User> _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IAsyncRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseResult<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // request.Gender = 0;
            //request.Grade.Name = nullv;

            var validationResult = await Validator<CreateUserCommandValidator>.ValidateAsync(request);

            if (validationResult.IsValid == false)
            {
                return new ResponseResult<CreateUserCommandResponse>(validationResult.Errors);
            }

            //throw new Exception("this is an internal server error");
            var badRequestException = new BadRequestException(nameof(request.Grade.Name), "student first name is already taken");

            var notfound = new NotFoundException("Id", "student ID", 1);

           // return new ResponseResult<CreateUserCommandResponse>(badRequestException);

            var user = new User().Create(request.FirstName, request.LastName, request.Address, request.School, request.Gender);

            user = await _userRepository.AddAsync(user);

            var mappedObj = _mapper.Map<CreateUserCommandResponse>(user);

            var result = new ResponseResult<CreateUserCommandResponse>(mappedObj);

            return result;
        }
    }
}
