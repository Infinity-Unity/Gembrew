using System.Collections.Generic;

public class AlchemySystem
{

    private Inventory _inventory = GameManager.Instance.Inventory;
    private IReadOnlyList<RecipeData> _allRecipes = GameManager.Instance.AllRecipeBook.AllRecipes;

    public RecipeData SearchRecipieItemsInPot(IReadOnlyDictionary<IngredientData,int> items)
    {
        if(items == null || items.Count == 0) return null;

        foreach (var recipe in _allRecipes)
        {
            if(IsRecipeMatch(recipe, items))
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
}
