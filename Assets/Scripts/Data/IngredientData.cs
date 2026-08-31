using UnityEngine;


[CreateAssetMenu(fileName = "NewIngredient", menuName = "Alchemy/Ingredients")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public string description;
    public int cost;
    public Rarity rarity;
    public Sprite icon;

    //ps дополню по нужде....
}


