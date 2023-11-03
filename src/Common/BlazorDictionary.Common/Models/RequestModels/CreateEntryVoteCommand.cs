﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Models.RequestModels
{
    public class CreateEntryVoteCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid CreatedById { get; set; }

        public CreateEntryVoteCommand(Guid entryId, VoteType voteType, Guid createdById)
        {
            EntryId = entryId;
            VoteType = voteType;
            CreatedById = createdById;
        }
    }
}
