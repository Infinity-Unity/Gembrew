using UnityEngine;
using System.Collections.Generic;



public class Test : MonoBehaviour
{
    
    private void Start()
    {
        Dictionary<string, int> inventory = new Dictionary<string, int>();

        inventory.Add("Crystal Shard", 5);
        
        inventory.Add("Herb", 3);

        string ingredientName = "Crystal Shard";
        int amountToAdd = 3;

        if (inventory.ContainsKey(ingredientName))
        {
            inventory[ingredientName] += amountToAdd;
        }
        else
        {
            inventory.Add(ingredientName, amountToAdd);
        }
    }
}
