using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    
    private Dictionary<IngredientData, int> _items = new Dictionary<IngredientData, int>();

    public event Action Changed;

    public void Add(IngredientData ingredient, int amount)
    {
        if (amount <= 0) return;

        if (_items.TryAdd(ingredient, amount) == false)
        {
            _items[ingredient] += amount;
            Changed?.Invoke();
        }
        else
        {
            Changed?.Invoke();
        }

    }

    public int GetAmount(IngredientData ingredient)
    {
        _items.TryGetValue(ingredient, out var amount);
        return amount;
    }

    public bool TryRemove(IngredientData ingredient, int amount)
    {

        if(amount <= 0 ) return false;

        if (_items.TryGetValue(ingredient, out var current))
        {
            if (current < amount) return false;

            int diff = current - amount;
            if (diff <= 0)
            {
                _items.Remove(ingredient);
                Changed?.Invoke();
                return true;
            }

            _items[ingredient] -= amount;
            Changed?.Invoke();
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
    
}
