﻿using BlazorDictionary.Common.Events.User;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Projections.UserService.Services
{
    public class UserService
    {
        private string connStr;

        public UserService(IConfiguration configuration)
        {
            connStr = configuration.GetConnectionString("SqlConnection");
        }

        public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent @event)
        {
            var guid = Guid.NewGuid();


            using var connection = new SqlConnection(connStr);

            await connection.ExecuteAsync("INSERT INTO EMAILCONFIRMATION (Id, CreateDate, OldEmailAddress, NewEmailAddress) VALUES (@Id, GETDATE(), @OldEmailAddress, @NewEmailAddress)",
                    new
                    {
                        Id = guid,
                        OldEmailAddress = @event.OldEmailAddress,
                        NewEmailAddress = @event.NewEmailAddress
                    });

            return guid;
        }
    }
}
