using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewIngredientCatalog", menuName = "Alchemy/IngredientCatalog")]
public class IngredientCatalog : ScriptableObject
{
    [SerializeField] private List<IngredientData> _ingredients;

    public IReadOnlyList<IngredientData> Ingredients => _ingredients;

    private void OnValidate()
    {
        if(_ingredients == null || _ingredients.Count == 0)
        {
            Debug.LogWarning("Ошибка в IngredientCatalog! Список ингредиентов пуст или не инициализирован. Заполните поля в инспекторе!", this);
        }  
    }
}
