using BlazorDictionary.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.ViewModels.RequestModels
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public LoginUserCommand()
        {
        }

        public LoginUserCommand(string email, string password)
        {
            EmailAddress = email;
            Password = password;
        }

        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
