using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Helpers;
using VoyageExcercise.Interfaces;
using VoyageExcercise.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VoyageExcercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        //Database context
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        private readonly AppUserHelper _userhelper;

        public UserController(IMediator mediator, AppDBContext context, IConfiguration config)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userhelper = new AppUserHelper(_context, config);
        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] LoginRequest request)
        {
            //Entity mapping conversion
            var command = new UserLoginCommand(request.account, request.password, request.token);

            bool flag = await _mediator.Send(command);

            if (flag)
            {
                return Ok(new
                {
                    code = "20001",
                    msg = $"{request.account} Lofin successfull",
                    data = request
                });
            }
            else
            {
                return Unauthorized(new
                {
                    code = "20001",
                    msg = $"{request.account} Lofin failed",
                    data = request
                });
            }
        }

        // <summary>
        /// Get all the users from db.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// page = 0
        /// pagesize = 10
        /// </remarks>
        /// <param name="page">Number of the page</param>
        /// <param name="pagesize">Number of elements of that page</param>
        /// <returns>A user List</returns>    
        [HttpPost("GetAllUsers")]
        public List<AppUser> GetAllTransactions(int page, int pagesize)
        {
            return _userhelper.GetUsers(page, pagesize);
        }

        /// <summary>
        /// Get a user from db.
        /// </summary>
        /// <returns>A User data model</returns>
        /// <response code="200">Returns the user data model</response>
        /// <response code="400">If the user id is incorrect</response>
        /// <response code="404">If the user doesn't exist</response>
        [HttpPost("GetUser")]
        public IActionResult GetTransaction(int user_id)
        {
            if (user_id <= -1)
            {
                return BadRequest();
            }

            var user = _userhelper.GetUser(user_id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(user));
        }

        /// <summary>
        /// Add a user to the db.
        /// </summary>
        /// <returns>A User data model</returns>
        /// <response code="201">If the user was created returns the user data model</response>
        /// <response code="304">If the user was not created</response>
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserRequest request)
        {
            var user = await _userhelper.AddUser(request);

            if (user != null)
                return Created("Transaction", JsonConvert.SerializeObject(user));
            else
                return StatusCode(304);//Not Modified

        }

        /// <summary>
        /// Edit a user from db.
        /// </summary>
        /// <returns>A User data model</returns>
        /// <response code="201">If the user was edite returns the user data model</response>
        /// <response code="304">If the user was not edited</response>
        [HttpPut("EditUser")]
        public async Task<IActionResult> EditTransaction(UserRequest request, int user_id)
        {
            var user = await _userhelper.EditUser(request, user_id);

            if (user != null)
                return Ok(JsonConvert.SerializeObject(user));
            else
                return StatusCode(304);//Not Modified

        }

        /// <summary>
        /// Delete a user from db.
        /// </summary>
        /// <returns>A user data model</returns>
        /// <response code="201">If the user was deleted returns the user data model</response>
        /// <response code="304">If the user was not deleted</response>
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteTransaction(int user_id)
        {
            var t = await _userhelper.DeleteUser(user_id);

            if (t)
                return Ok(t);
            else
                return StatusCode(304);//Not Modified

        }
    }
}
