using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRecipeCatalog", menuName = "Alchemy/RecipeCatalog")]
public class RecipeCatalog : ScriptableObject
{
    [SerializeField] private List<RecipeData> _allRecipes;

    public IReadOnlyList<RecipeData> AllRecipes => _allRecipes;

    private void OnValidate()
    {
        if (_allRecipes == null || _allRecipes.Count == 0)
        {
            Debug.LogWarning("Ошибка в RecipeCatalog! Список рецептов пуст или не инициализирован. Заполните поля в испекторе!", this);
        }
    }
}
