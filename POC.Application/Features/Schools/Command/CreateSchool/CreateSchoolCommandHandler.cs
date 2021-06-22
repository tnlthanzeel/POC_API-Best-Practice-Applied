﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Application.Features.Schools.Command.CreateSchool
{
    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, CreateSchoolCommandResponse>
    {
        private readonly IMapper _mapper;


        public CreateSchoolCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<CreateSchoolCommandResponse> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
