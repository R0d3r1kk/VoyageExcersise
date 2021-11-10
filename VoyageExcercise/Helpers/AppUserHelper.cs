using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Interfaces;
using VoyageExcercise.Models;

namespace VoyageExcercise.Helpers
{
    public class AppUserHelper
    {


        public string SQLAddUser = "INSERT INTO AppUser (" +
            "id," +
            "name," +
            "account," +
            "password," +
            "date_created " +
            ")" +
            "VALUES(" +
                "@id," +
                "@name," +
                "@account," +
                "@password," +
                "@date_created" +
            ");";

        /// <summary>
        /// Database Context
        /// </summary>
        private readonly AppDBContext _context;
        private readonly IConfiguration _config;

        public AppUserHelper(AppDBContext context, IConfiguration config)
        {
            this._context = context;
            this._config = config;
        }

        public async Task<AppUser> AddUser(UserRequest request)
        {
            try
            {
                //linq
                /*var u = _context.AppUser.Add(new AppUser()
                {
                    account = request.account,
                    name = request.name,
                    password = request.password,
                    date_created = DateTime.Now
                });

                await _context.SaveChangesAsync();
                return u.Entity;*/


                //Dapper
                using var connection = new SqliteConnection(_config.GetConnectionString("cs"));
                var user = new AppUser()
                {
                    account = request.account,
                    name = request.name,
                    password = request.password,
                    date_created = DateTime.Now
                };
                await connection.QueryAsync<AppUser>(SQLAddUser, user);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteUser(int user_id)
        {
            try
            {
                var u = GetUser(user_id);

                if (u != null)
                {
                    _context.AppUser.Remove(u);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<AppUser> EditUser(UserRequest request, int user_id)
        {
            try
            {
                if (request == null || user_id < 0)
                    return null;

                //check if the transaction exist
                var u = GetUser(user_id);
                if (u != null)
                {
                    
                    u.name = request.name != u.name ? request.name : u.name;
                    u.account = request.account != u.account ? request.account : u.account;
                    u.password = request.password != u.password ? request.password : u.password;

                    _context.AppUser.Update(u);
                    await _context.SaveChangesAsync();

                    return u;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public AppUser GetUser(int id)
        {
            try
            {
                var user = _context.AppUser.FirstOrDefault(u => u.id == id);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public AppUser GetAppUserInfo(string account, string password)
        {
            try
            {
                var user = _context.AppUser.FirstOrDefault(u => u.account == account && u.password == password);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<AppUser> GetUsers(int page, int pagesize)
        {
            try
            {
                var users = _context.AppUser.Skip(page * pagesize).Take(pagesize).ToList();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AppUser>();
            }
        }

        public void SetUserLoginRecord(IMediator mediator, AppUserLoginEvent _event)
        {
            mediator.Publish(_event);
        }
    }
}
