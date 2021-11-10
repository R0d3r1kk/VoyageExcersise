using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VoyageExcercise.Interfaces;

namespace VoyageExcercise.Helpers
{
    public class UserLoginNotificationEventHandler : INotificationHandler<AppUserLoginEvent>
    {
        private readonly ILogger<UserLoginNotificationEventHandler> _logger;
        public UserLoginNotificationEventHandler(ILogger<UserLoginNotificationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(AppUserLoginEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CurrenUser with Account:{notification.Account} has been successfully setup");

            return Task.FromResult(true);
        }
    }
}
