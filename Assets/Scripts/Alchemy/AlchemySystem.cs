using System.Collections.Generic;

public class AlchemySystem
{
    private IReadOnlyList<RecipeData> _allRecipes = GameManager.Instance.AllRecipeBook.AllRecipes;
    private Inventory _inventory = GameManager.Instance.Inventory;

    private RecipeData SearchRecipieItemsInPot(IReadOnlyDictionary<IngredientData,int> potItems)
    {
        if(potItems == null || potItems.Count == 0) return null;

        foreach (var recipe in _allRecipes)
        {
            if(IsRecipeMatch(recipe, potItems))
            {
                return recipe;
            }
        }
        return null;
    }

    private bool IsRecipeMatch(RecipeData recipeData,IReadOnlyDictionary<IngredientData,int> potItems)
    {
        if(recipeData.Ingredients.Count != potItems.Count) return false;

        foreach(var recipeIngredient in recipeData.Ingredients)
        {
            var ingredient = recipeIngredient.Ingredient;
            
            if (!potItems.TryGetValue(ingredient, out int amountInPot) || recipeIngredient.Amount != amountInPot)
            {
                return false;
            }
        }
        return true;
    }

    public bool TryCraft(IReadOnlyDictionary<IngredientData, int> potItems)
    {
        if (HasInInventory(potItems))
        {

        }


        return true;
    }
    private bool HasInInventory(IReadOnlyDictionary<IngredientData, int> potItems)
    {
        if(potItems == null || potItems.Count == 0) return false;

        foreach(var items in potItems)
        {
            if (!_inventory.Has(items.Key, items.Value))
            {
                return false;
            }
        }
        return true;
    }
}
