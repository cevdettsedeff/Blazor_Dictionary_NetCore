using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Projections.UserService.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string GenerateConfirmationLink(Guid confirmationId)
        {
            var baseUrl = _configuration["ConfirmationBaseUrl"] + confirmationId;

            return baseUrl;
        }

        public Task SendEmail(string toEmailAddress, string content)
        {
            // Sende email process

            _logger.LogInformation($"Email sent to {toEmailAddress} with content of {content}");

            return Task.CompletedTask;
        }
    }
}
