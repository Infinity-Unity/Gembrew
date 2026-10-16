using System.Collections.Generic;

public class AlchemySystem
{
    private readonly Inventory _inventory;
    private readonly IReadOnlyList<RecipeData> _recipes;

    public AlchemySystem(Inventory inventory, IReadOnlyList<RecipeData> recipes)
    {
        _inventory = inventory;
        _recipes = recipes;
    }

    public bool TryCraft(IReadOnlyDictionary<IngredientData, int> potItems)
    {
        RecipeData recipe = FindRecipe(potItems);
        if (recipe == null || !HasInInventory(potItems) || recipe.ResultIngredient == null)
        {
            return false;
        }

        foreach (var item in potItems)
        {
            _inventory.TryRemove(item.Key, item.Value);
        }

        _inventory.Add(recipe.ResultIngredient,recipe.AmountResultIngredient);

        return true;
    }

    public RecipeData FindRecipe(IReadOnlyDictionary<IngredientData,int> potItems)
    {
        if(potItems == null || potItems.Count == 0)
        {
            return null;
        }

        foreach (var recipe in _recipes)
        {
            if (!IsRecipeValid(recipe))
            {
                continue;
            }

            if (IsRecipeMatch(recipe, potItems))
            {
                return recipe;
            }
        }
        return null;
    }

    private bool IsRecipeMatch(RecipeData recipeData,IReadOnlyDictionary<IngredientData,int> potItems)
    {
        if(recipeData.Ingredients == null || recipeData.Ingredients.Count != potItems.Count)
        {
            return false;
        }

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
    

    private bool HasInInventory(IReadOnlyDictionary<IngredientData, int> potItems)
    {
        if(potItems == null || potItems.Count == 0)
        {
            return false;
        }

        foreach(var items in potItems)
        {
            if (!_inventory.Has(items.Key, items.Value))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsRecipeValid(RecipeData recipe)
    {
        if (recipe == null || recipe.ResultIngredient == null) return false;
        if (recipe.Ingredients == null || recipe.Ingredients.Count == 0) return false;

        foreach (var line in recipe.Ingredients)
        {
            if (line == null || line.Ingredient == null) return false;
        }
        return true;
    }
}
