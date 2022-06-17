using AutoMapper;
using MediatR;
using POC.Application.Contracts.Persistence;
using POC.Application.Exceptions;
using POC.Application.Responses;
using POC.Domain.Entitities;

namespace POC.Application.Features.Users.Queries.GetUserDetail;

class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, ResponseResult<UserDetailViewModel>>
{
    private readonly IAsyncRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public GetUserDetailQueryHandler(IAsyncRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ResponseResult<UserDetailViewModel>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        var userDetail = await _userRepository.GetByIdAsync(request.UserId);

        if (userDetail is null) return new ResponseResult<UserDetailViewModel>(new NotFoundException(nameof(request.UserId), nameof(User), request.UserId));

        var userDetailViewModel = _mapper.Map<UserDetailViewModel>(userDetail);

        var result = new ResponseResult<UserDetailViewModel>(userDetailViewModel);

        return result;
    }
}
