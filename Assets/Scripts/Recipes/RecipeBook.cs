using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRecipeBook", menuName = "Alchemy/RecipeBook")]
public class RecipeBook : ScriptableObject
{
    [SerializeField] private List<RecipeData> _allRecipes;

    public IReadOnlyList<RecipeData> AllRecipes => _allRecipes;
}
