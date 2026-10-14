
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Alchemy/Recipe")]
public class RecipeData : ScriptableObject
{
    [SerializeField] private string _recipeName;
    [SerializeField] private List<RecipeIngredient> _ingredients;
    [SerializeField] private IngredientData _resultIngredient;
    [Min(1)]
    [SerializeField] private int _amountResultIngredient;


    public string RecipeName => _recipeName;
    public IReadOnlyList<RecipeIngredient> Ingredients => _ingredients;
    public IngredientData ResultIngredient => _resultIngredient;
    public int AmountResultIngredient => _amountResultIngredient;



}
