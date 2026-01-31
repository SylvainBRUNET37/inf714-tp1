using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INF714.Data.Providers.Interfaces
{
    public interface IInventoryProvider
    {
        Task<Dictionary<string, int>> Get(Guid userId);
    }
}
