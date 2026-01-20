using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using INF714.Data;
using INF714.Data.Providers.Interfaces;

namespace INF714.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserProvider _userProvider;

        public UsersController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var user = new User();
            user.Id = Guid.NewGuid();
            await _userProvider.Create(user);
            return CreatedAtAction(nameof(Get), new { userId = user.Id }, user);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> Get(Guid userId)
        {
            return Ok();
        }

        [HttpPatch("{userId}")]
        public async Task<ActionResult> Update(Guid userId, string name, uint? level)
        {
            return Ok();
        }

        [HttpPut("{userId}/name")]
        public async Task<ActionResult> UpdateName(Guid userId, [Required] string value)
        {
            return Ok();
        }
    }
}
