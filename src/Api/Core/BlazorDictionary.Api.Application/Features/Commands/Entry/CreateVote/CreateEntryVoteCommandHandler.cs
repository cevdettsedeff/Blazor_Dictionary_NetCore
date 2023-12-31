﻿using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Infrastructure;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Commands.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.VoteExchangeName,
               exchangeType: DictionaryConstants.DefaultExchangeType,
               queueName: DictionaryConstants.CreateEntryVoteQueueName,
               obj: new CreateEntryVoteEvent()
               {
                   EntryId = request.EntryId,
                   CreatedBy = request.CreatedById,
                   VoteType = request.VoteType
               });

            return await Task.FromResult(true);
        }
    }
}
