using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADWrapper.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADWrapper.Controllers
{
    [Produces("application/json")]
    [Route("api/Group")]
    public class GroupController : Controller
    {
        private readonly IBGroup _bGroup;
        public GroupController(IBGroup bGroup)
        {
            _bGroup = bGroup;
        }

        /// <summary>
        /// Retrieve group members
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>Returns group members</returns>
        /// <response code="200">Group members</response>
        /// <response code="404">Group is not found in AD</response> 
        [HttpGet("{groupName}")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetGroupMembers(string groupName)
        {
            var group = _bGroup.GetGroupMembersList(groupName);
            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        /// <summary>
        /// Check if group is a valid AD group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        /// <response code="200">Valid AD group</response>
        /// <response code="404">Group is not found in AD</response> 
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{groupName}/validity")]
        public IActionResult CheckGroupValidity(string groupName)
        {
            if (_bGroup.IsValidGroup(groupName))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}