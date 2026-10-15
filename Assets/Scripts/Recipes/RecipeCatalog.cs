using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRecipeBook", menuName = "Alchemy/RecipeCatalog")]
public class RecipeCatalog : ScriptableObject
{
    [SerializeField] private List<RecipeData> _allRecipes;

    public IReadOnlyList<RecipeData> AllRecipes => _allRecipes;
}
