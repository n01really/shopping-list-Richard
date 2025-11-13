using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Models;
using System.Security.Cryptography.X509Certificates;

namespace ShoppingList.Application.Services;

public class ShoppingListService : IShoppingListService
{
    private ShoppingItem[] _items;
    private int _nextIndex;

    public ShoppingListService() 
    {
        // Initialize with demo data for UI demonstration
        // TODO: Students can remove or comment this out when running unit tests
        _items = GenerateDemoItems();
        _nextIndex = 4; // We have 4 demo items initialized
    }

    public IReadOnlyList<ShoppingItem> GetAll()
    {
        // TODO: Students - Return all items from the array (up to _nextIndex)
        
        return _items[.._nextIndex];

    }

    public ShoppingItem? GetById(string id)
    {
        // Search through items up to _nextIndex
        for (int i = 0; i < _nextIndex; i++)
        {
            if (_items[i]?.Id == id)
            {
                return _items[i];
            }
        }
        return null; // Not found
    }

    public ShoppingItem? Add(string name, int quantity, string? notes) //needs to change
    {
        // Check if array is full and resize if needed
        if (_nextIndex >= _items.Length)
        {
            Array.Resize(ref _items, _items.Length * 2);
        }

        var shoppingitem = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Quantity = quantity,
            Notes = notes,
            IsPurchased = false
        };
        
        // Store in array and increment index
        _items[_nextIndex] = shoppingitem;
        _nextIndex++;
        
        return shoppingitem;
    }

    public ShoppingItem? Update(string id, string name, int quantity, string? notes)
    {
        // TODO: Students - Implement this method
        var item = GetById(id);
        if (item == null)
        {
            return null;
        }

        item.Name = name;
        item.Quantity = quantity;
        item.Notes = notes;


        // Return the updated item, or null if not found
        return item;
    }

    public bool Delete(string id)
    {
        
        int indexToDelete = -1;
        for (int i = 0; i < _nextIndex; i++)
        {
            if (_items[i]?.Id == id)
            {
                indexToDelete = i;
                break;
            }
        }

        
        if (indexToDelete == -1)
        {
            return false;
        }

        
        for (int i = indexToDelete; i < _nextIndex - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        
        _items[_nextIndex - 1] = null!;
        _nextIndex--;

        return true;
    }

    public IReadOnlyList<ShoppingItem> Search(string query)
    {
        // TODO: Students - Implement this method
        // Return the filtered items
        var shoppingItems = new List<ShoppingItem>();
        for (int i = 0; i <= _nextIndex; i++)
        {
            if (_items[i] != null)
            {
                shoppingItems.Add(_items[i]);
            }
        }
        return shoppingItems;
    }

    public int ClearPurchased()
    {
        // TODO: Students - Implement this method
        // Return the count of removed items
        return 0;
    }

    public bool TogglePurchased(string id)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false if item not found
        return false;
    }

    public bool Reorder(IReadOnlyList<string> orderedIds)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false otherwise
        return false;
    }

    private ShoppingItem[] GenerateDemoItems()
    {
        var items = new ShoppingItem[5];
        items[0] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Dishwasher tablets",
            Quantity = 1,
            Notes = "80st/pack - Rea",
            IsPurchased = false
        };
        items[1] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Ground meat",
            Quantity = 1,
            Notes = "2kg - origin Sweden",
            IsPurchased = false
        };
        items[2] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Apples",
            Quantity = 10,
            Notes = "Pink Lady",
            IsPurchased = false
        };
        items[3] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Toothpaste",
            Quantity = 1,
            Notes = "Colgate",
            IsPurchased = false
        };
        return items;
    }
}