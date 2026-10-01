using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    
    private Dictionary<IngredientData, int> _items = new Dictionary<IngredientData, int>();

    public void Add(IngredientData ingredient, int amount)
    {
        if (amount <= 0) return;

        if (_items.TryAdd(ingredient, amount) == false)
        {
            _items[ingredient] += amount;
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
                return true;
            }

            _items[ingredient] -= amount;
            return true;
        }

        return false;

    }
}
