using UnityEngine;


public class AlchemyTester : MonoBehaviour
{

    private Inventory testPotItems = new Inventory();
    [SerializeField] private IngredientCatalog ingredientsCatalog;

    void Start()
    {

    }

    [ContextMenu("Add Ingredient in Pot")]
    public void AddIngredient()
    {
        int amount = 2;
        foreach (var item in ingredientsCatalog.Ingredients)
        {
            testPotItems.Add(item, amount);
            Debug.Log($"Успешно добавлено {item.IngredientName} в количестве {amount} штук");
        }

    }

    [ContextMenu("TryCraft")]
    public void Craft()
    {
        bool succes = GameManager.Instance.AlchemySystem.TryCraft(testPotItems.Items);
        string text = succes ? "Попытка крафта успешно завершилась" : "Попытка крафта провалена";
        Debug.Log(text);
    }

    [ContextMenu("Delete item in Pot")]
    public void DeleteItem()
    {
        testPotItems.ClearInventory();
        Debug.Log("Все предметы из котла успешно удалены!");
    }

}
