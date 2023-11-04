using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntries
{
    public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public GetEntriesQueryHandler(IMapper mapper, IEntryRepository entryRepository)
        {
            _mapper = mapper;
            _entryRepository = entryRepository;
        }

        public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();

            if (request.TodaysEntries)
            {
                query = query.Where(i=> i.CreateDate >= DateTime.Now.Date)
                    .Where(i => i.CreateDate >= DateTime.Now.AddDays(1).Date);
            }

            query = query.Include(i => i.EntryComments)
                .OrderBy(i => Guid.NewGuid())
                .Take(request.Count);

            return await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
