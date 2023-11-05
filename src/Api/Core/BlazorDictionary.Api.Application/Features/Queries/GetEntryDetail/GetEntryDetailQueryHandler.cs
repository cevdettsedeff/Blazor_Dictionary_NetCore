﻿using BlazorDictionary.Api.Application.Features.Queries.GetEntryComments;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Models.Page;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntryDetail
{
    public class GetEntryDetailQueryHandler: IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
    {
        private readonly IEntryRepository _entryRepository;

        public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
        {

            var query = _entryRepository.AsQueryable();

            query = query.Include(i => i.EntryFavorites)
                .Include(i => i.CreatedBy)
                .Include(i => i.EntryVotes)
                .Where(i => i.Id == request.EntryId);

            var list = query.Select(i => new GetEntryDetailViewModel
            {
                Id = i.Id,
                Subejct = i.Subject,
                Content = i.Content,
                IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
                FavoritedCount = i.EntryFavorites.Count,
                CreatedDate = i.CreateDate,
                VoteType = request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
                ? i.EntryVotes.FirstOrDefault(z => z.CreatedById == request.UserId).VoteType
                : Common.Models.VoteType.None
            }); 

            return await list.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
