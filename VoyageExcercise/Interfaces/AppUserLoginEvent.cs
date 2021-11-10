using System;
using MediatR;

namespace VoyageExcercise.Interfaces
{
    public class AppUserLoginEvent : INotification
    {
        public string Account { get; }

        public AppUserLoginEvent(string account)
        {
            Account = account;
        }
    }
}
