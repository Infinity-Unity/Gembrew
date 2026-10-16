using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewIngredientCatalog", menuName = "Alchemy/IngredientCatalog")]
public class IngredientCatalog : ScriptableObject
{
    [SerializeField] private List<IngredientData> _ingredients;

    public IReadOnlyList<IngredientData> Ingredients => _ingredients;
}
