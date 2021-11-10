using System;
using MediatR;

namespace VoyageExcercise.Interfaces
{
    public class UserLoginCommand: IRequest<bool>
    {

        public string account { get; private set; }
        public string password { get; private set; }
        public string token { get; private set; }

        /// <summary>
        /// UserLoginCommand Contsructor
        /// </summary>
        /// <param name="_account">Email</param>
        /// <param name="_password">Password</param>
        /// <param name="_token">Código de verificación</param>
        public UserLoginCommand(string _account, string _password, string _token)
        {
            this.account = _account;
            this.password = _password;
            this.token = _token;
        }
    }
}
