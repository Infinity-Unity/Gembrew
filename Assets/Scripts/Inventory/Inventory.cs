using System;
using System.Collections.Generic;

public class Inventory
{
    private Dictionary<IngredientData, int> _items = new Dictionary<IngredientData, int>();

    public event Action<IngredientData,int> Changed;

    public void Add(IngredientData ingredient, int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        if (!_items.TryAdd(ingredient, amount))
        {
            _items[ingredient] += amount;
        }

        Changed?.Invoke(ingredient, _items[ingredient]);
    }

    public int GetAmount(IngredientData ingredient)
    {
        _items.TryGetValue(ingredient, out var amount);
        return amount;
    }

    public bool TryRemove(IngredientData ingredient, int amount)
    {
        if(amount <= 0)
        {
            return false;
        }

        if (_items.TryGetValue(ingredient, out var current))
        {
            if (current < amount) return false;

            int diff = current - amount;
            if (diff == 0)
            {
                _items.Remove(ingredient);
            }
            else
            {
                _items[ingredient] = diff;
            }

            
            Changed?.Invoke(ingredient,diff);
            return true;
        }

        return false;
    }

    public bool Has(IngredientData ingredient, int amount)
    {
        if (amount <= 0) return false;
        return GetAmount(ingredient) >= amount;
    }

    public IReadOnlyDictionary<IngredientData, int> Items => _items;

    public void ClearInventory()
    {
        _items.Clear();
    }
    
}
