using AutoMapper;
using FluentValidation.Results;
using MediatR;
using POC.Application.Contracts.Persistence;
using POC.Application.Exceptions;
using POC.Application.Responses;
using POC.Application.Validators;
using POC.Domain.Entitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Application.Features.Users.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<User> _userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IAsyncRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result = new SuccessResponse<ValidationResult>();

            var eventToUpdate = await _userRepository.GetByIdAsync(request.Id);

            if (eventToUpdate == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }


            await Validator<UpdateUserCommandValidator>.ValidateAsync(request, result);

            if (result.Success == false)
            {
                throw new ValidationException(result.ValidationErrors);
            }

            _mapper.Map(request, eventToUpdate);

            await _userRepository.UpdateAsync(eventToUpdate);

            return Unit.Value;
        }
    }
}
