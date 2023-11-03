using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.EntryComment;
using BlazorDictionary.Common.Infrastructure;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.VoteExchangeName,
               exchangeType: DictionaryConstants.DefaultExchangeType,
               queueName: DictionaryConstants.CreateEntryCommentVoteQueueName,
               obj: new CreateEntryCommentVoteEvent()
               {
                   EntryCommentId = request.EntryCommentId,
                   VoteType = request.VoteType,
                   CreatedBy = request.CreatedBy
               });

            return await Task.FromResult(true);
        }
    }
}
