using AutoMapper;
using MediatR;
using POC.Application.Contracts.Persistence;
using POC.Application.Responses;
using POC.Domain.Entitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Application.Features.Users.Queries.GetUserList
{
    class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, SuccessResponse<IEnumerable<UserViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<User> _userRepository;

        public GetUsersListQueryHandler(IMapper mapper, IAsyncRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<SuccessResponse<IEnumerable<UserViewModel>>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _userRepository.ListAllAsync();

            var vmList = _mapper.Map<List<UserViewModel>>(queryResult);

            return new SuccessResponse<IEnumerable<UserViewModel>>()
            {
                Data = vmList,
                TotalRecordCount = vmList.Count
            };
        }
    }
}
