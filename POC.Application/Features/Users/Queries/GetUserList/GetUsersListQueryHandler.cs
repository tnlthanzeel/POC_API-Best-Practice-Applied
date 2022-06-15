using AutoMapper;
using MediatR;
using POC.Application.Contracts.Persistence;
using POC.Application.Responses;
using POC.Domain.Entitities;

namespace POC.Application.Features.Users.Queries.GetUserList
{
    class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, ResponseResult<IEnumerable<UserViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<User> _userRepository;

        public GetUsersListQueryHandler(IMapper mapper, IAsyncRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseResult<IEnumerable<UserViewModel>>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _userRepository.ListAllAsync();

            var vmList = _mapper.Map<List<UserViewModel>>(queryResult);

            return new ResponseResult<IEnumerable<UserViewModel>>(vmList, vmList.Count);
        }
    }
}
