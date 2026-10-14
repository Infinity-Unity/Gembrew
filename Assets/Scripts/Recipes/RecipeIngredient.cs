using UnityEngine;


[System.Serializable]
public class RecipeIngredient
{
    [SerializeField] private IngredientData _ingredient;

    [Min(1)]
    [SerializeField] private int _amount;

    public IngredientData Ingredient => _ingredient;
    public int Amount => _amount;
}
