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
    public class UsersController(IUserProvider userProvider, IInventoryProvider inventoryProvider) : ControllerBase
    {
        private readonly IUserProvider _userProvider = userProvider;
        private readonly IInventoryProvider _inventoryProvider = inventoryProvider;

        #region Inventory requests

        [HttpGet("{userId}/items")]
        public async Task<ActionResult> GetInventory(Guid userId)
        {
            var user = await _userProvider.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            var inventory = await _inventoryProvider.Get(userId);
            return Ok(inventory);
        }

        [HttpPut("{userId}/items/{itemId}")]
        public async Task<ActionResult> PutItem(Guid userId, uint itemId, [Required] string name, [Required] uint amount)
        {
            await _inventoryProvider.Put(userId, itemId, name, amount);

            return Ok();
        }

        [HttpDelete("{userId}/items/{itemId}")]
        public async Task<ActionResult> DeleteItem(Guid userId, uint itemId)
        {
            await _inventoryProvider.Delete(userId, itemId);

            return Ok();
        }

        #endregion

        #region Core user requests

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
            var user = await _userProvider.Get(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPatch("{userId}")]
        public async Task<ActionResult> Update(Guid userId, string name, uint? level)
        {
            var user = await _userProvider.Get(userId);
            if (user == null)
            {
                return NotFound();
            }
            if (name != null)
            {
                user.Name = name;
            }
            if (level != null)
            {
                user.Level = level.GetValueOrDefault();
            }
            return Ok();
        }

        [HttpPut("{userId}/name")]
        public async Task<ActionResult> UpdateName(Guid userId, [Required] string value)
        {
            return Ok();
        }

        #endregion
    }
}
