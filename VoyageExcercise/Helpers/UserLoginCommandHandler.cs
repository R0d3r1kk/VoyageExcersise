using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using VoyageExcercise.DAL;
using VoyageExcercise.Interfaces;

namespace VoyageExcercise.Helpers
{
    public class UserLoginCommandHandler: IRequestHandler<UserLoginCommand, bool>
    {
        /// <summary>
        /// Database Context
        /// </summary>
        private readonly AppDBContext _context;

        private readonly AppUserHelper _userhelper;

        private readonly IMediator _mediator;


        /// <summary>
        /// UserLoginCommandHandler Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public UserLoginCommandHandler(AppDBContext context, IMediator mediator, IConfiguration config)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userhelper = new AppUserHelper(_context, config);
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            //checking the token is not null
            if (string.IsNullOrEmpty(request.token))
                return false;
            //doing the login through the db
            var appUser =  _userhelper.GetAppUserInfo(request.account, request.password);
            if (appUser == null)
                return false;

            //publishing the notification
            _userhelper.SetUserLoginRecord(_mediator, new AppUserLoginEvent(appUser.account));

            return true;
        }
    }
}
