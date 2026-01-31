using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INF714.Data.Providers.Interfaces
{
    using Inventory = Dictionary<uint, Item>;

    public interface IInventoryProvider
    {
        Task<Inventory> Get(Guid userId);

        Task Put(Guid userId, uint itemId, Item item);
    }
}
