using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace INF714.Data.Providers
{
    using Inventory = Dictionary<uint, Item>;

    public class InMemoryInventoryProvider : Interfaces.IInventoryProvider
    {
        private readonly Dictionary<uint, string> _items = new()
        {
            { 0, "Epee" },
            { 1, "Bouclier" }
        };

        // UserId -> ItemIds
        private readonly Dictionary<Guid, Inventory> _inventories = [];

        public Task<Inventory> Get(Guid userId)
        {
            if (_inventories.TryGetValue(userId, out var items))
            {
                return Task.FromResult(items);
            }

            return Task.FromResult(new Inventory());
        }

        public Task Put(Guid userId, uint itemId, string name, uint amount)
        {
            if (!_items.ContainsKey(itemId))
            {
                throw new ArgumentException($"Item with ID {itemId} doesn't exist.");
            }

            if (amount == 0)
            {
                return Task.CompletedTask;
            }

            if (!_inventories.TryGetValue(userId, out Inventory userInventory))
            {
                userInventory = [];
                _inventories[userId] = userInventory;
            }

            UpdateItemData(itemId, name);
            AddItem(userInventory, itemId, amount);

            return Task.CompletedTask;
        }

        private void UpdateItemData(uint itemId, string name)
        {
            _items[itemId] = name;

            foreach (var inventory in _inventories.Values)
            {
                if (inventory.TryGetValue(itemId, out var item))
                {
                    item.Name = name;
                }
            }
        }

        private void AddItem(Inventory userInventory, uint itemId, uint amount)
        {
            if (userInventory.TryGetValue(itemId, out var item))
            {
                item.Amount += amount;
            }
            else
            {
                userInventory[itemId] = new Item
                {
                    Name = _items[itemId],
                    Amount = amount
                };
            }
        }
    }
}
