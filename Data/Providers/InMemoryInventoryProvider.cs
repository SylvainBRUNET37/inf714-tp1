using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace INF714.Data.Providers
{
    public class InMemoryInventoryProvider : Interfaces.IInventoryProvider
    {
        private readonly Dictionary<Guid, Dictionary<string, int>> _inventories = [];

        public InMemoryInventoryProvider()
        {

        }

        public Task<Dictionary<string, int>> Get(Guid userId)
        {
            if (_inventories.TryGetValue(userId, out var items))
            {
                return Task.FromResult(items);
            }

            return Task.FromResult(new Dictionary<string, int>());
        }
    }
}
