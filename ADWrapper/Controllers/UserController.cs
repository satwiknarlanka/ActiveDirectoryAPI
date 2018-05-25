using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADWrapper.BusinessLogic.Interfaces;
using ADWrapper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADWrapper.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IBUser _bUser;
        private readonly IBGroup _bGroup;
        public UserController(IBUser bUser, IBGroup bGroup)
        {
            _bUser = bUser;
            _bGroup = bGroup;
        }

        /// <summary>
        /// Retrieve basic user details
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns User details</returns>
        /// <response code="200">User Details</response>
        /// <response code="404">User is not found in AD</response> 
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(AdUser),200)]
        [ProducesResponseType(404)]
        public IActionResult GetUserDetails(string username)
        {
            var user = _bUser.GetUserDetails(username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Check if user is a valid AD user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <response code="200">Valid AD user</response>
        /// <response code="404">User is not found in AD</response> 
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{username}/validity")]
        public IActionResult CheckUserValidity(string username)
        {
            if (_bUser.IsValidUser(username))
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Check if a user is the member of a group
        /// </summary>
        /// <param name="username"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        /// <response code="200">User is a member of this group</response>
        /// <response code="404">User is not a member of this group</response> 
        /// <response code="400">Username or groupName is not valid</response> 
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpGet("{username}/memberof/{groupName}")]
        public IActionResult UserInGroup(string username, string groupName)
        {
            if (!_bUser.IsValidUser(username) || !_bGroup.IsValidGroup(groupName))
            {
                return BadRequest();
            }
            if (_bGroup.IsUserGroupMember(groupName, username))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}