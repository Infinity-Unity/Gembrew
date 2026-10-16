
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

    private void OnValidate()
    {
        if (_ingredients == null || _ingredients.Count == 0)
        {
            Debug.LogWarning("Ошибка в RecipeData! Список рецептов пуст или не инициализирован. Заполните поля в испекторе!", this);
        }

        if(_resultIngredient == null)
        {
            Debug.LogWarning("Ошибка в RecipeData! Не добавлен конечный ингредиент. Заполните поля в испекторе!", this);
        }
    }

}
