﻿using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        
        public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            Domain.Models.User dbUser = null;

            if(request.UserId != Guid.Empty)
                dbUser = await _userRepository.GetByIdAsync(request.UserId);

            else if(!string.IsNullOrEmpty(request.UserName))
                dbUser = await _userRepository.GetSingleAsync(i => i.UserName == request.UserName);

            //ToDo: if both are empty, throw new exception.

            return _mapper.Map<UserDetailViewModel>(dbUser);
        }
    }
}
