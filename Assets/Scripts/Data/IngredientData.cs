using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Alchemy/Ingredients")]
public class IngredientData : ScriptableObject
{
    [SerializeField] private string _ingredientName;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;
    [SerializeField] private Rarity _rarity;
    [SerializeField] private Sprite _icon;
    
    public string IngredientName => _ingredientName;
    public string Description => _description;
    public int Cost => _cost;
    public Rarity Rarity => _rarity;
    public Sprite Icon => _icon;
}


