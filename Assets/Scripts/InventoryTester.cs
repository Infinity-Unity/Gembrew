using System;
using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] private IngredientData mint;
    [SerializeField] private IngredientData sage;

    [SerializeField] private GameObject emerald;

    private int _passed;
    private int _failed;

    private void Start()
    {
        var inv = new Inventory();
       /* // 1. Пустой склад
        Check("1: GetAmount на пустом складе", 0, inv.GetAmount(mint));

        // 2. Простое добавление
        inv = new Inventory();
        inv.Add(mint, 3);
        Check("2: Add(3)", 3, inv.GetAmount(mint));

        // 3. Добавление к существующему
        inv = new Inventory();
        inv.Add(mint, 3);
        inv.Add(mint, 2);
        Check("3: Add(3) + Add(2)", 5, inv.GetAmount(mint));

        // 4. Add с нулём
        inv = new Inventory();
        inv.Add(mint, 0);
        Check("4: Add(0)", 0, inv.GetAmount(mint));

        // 5. Add с отрицательным
        inv = new Inventory();
        inv.Add(mint, -5);
        Check("5: Add(-5)", 0, inv.GetAmount(mint));

        // 6. Списание части
        inv = new Inventory();
        inv.Add(mint, 3);
        Check("6a: TryRemove(2) вернул", true, inv.TryRemove(mint, 2));
        Check("6b: осталось", 1, inv.GetAmount(mint));

        // 7. Списание ровно всего запаса
        inv = new Inventory();
        inv.Add(mint, 3);
        Check("7a: TryRemove(3) вернул", true, inv.TryRemove(mint, 3));
        Check("7b: осталось", 0, inv.GetAmount(mint));

        // 8. Не хватает
        inv = new Inventory();
        inv.Add(mint, 3);
        Check("8a: TryRemove(5) вернул", false, inv.TryRemove(mint, 5));
        Check("8b: запас не изменился", 3, inv.GetAmount(mint));

        // 9. Ингредиента нет вообще
        inv = new Inventory();
        Check("9: TryRemove без Add", false, inv.TryRemove(mint, 1));

        // 10. Списание нуля
        inv = new Inventory();
        inv.Add(mint, 3);
        Check("10a: TryRemove(0) вернул", false, inv.TryRemove(mint, 0));
        Check("10b: запас не изменился", 3, inv.GetAmount(mint));

        // 11. Списание отрицательного
        inv = new Inventory();
        inv.Add(mint, 3);
        Check("11a: TryRemove(-2) вернул", false, inv.TryRemove(mint, -2));
        Check("11b: запас не изменился", 3, inv.GetAmount(mint));

        // 12. Два ингредиента не влияют друг на друга
        inv = new Inventory();
        inv.Add(mint, 3);
        inv.Add(sage, 4);
        Check("12a: TryRemove(mint, 3) вернул", true, inv.TryRemove(mint, 3));
        Check("12b: шалфей остался", 4, inv.GetAmount(sage));
        Check("12c: мяты нет", 0, inv.GetAmount(mint));

        // 13. Проверки на существование
        // 13. Has: склад с одним ингредиентом (Has ничего не меняет, склад можно переиспользовать)
        inv = new Inventory();
        inv.Add(mint, 3);

        Check("13a: Has(mint, 2), запас 3", true, inv.Has(mint, 2));
        Check("13b: Has(mint, 3), запас 3", true, inv.Has(mint, 3));
        Check("13c: Has(mint, 5), запас 3", false, inv.Has(mint, 5));
        Check("13d: Has(sage, 1), шалфея нет", false, inv.Has(sage, 1));
        Check("13e: Has(mint, 0)", false, inv.Has(mint, 0));
        Check("13f: Has(mint, -1)", false, inv.Has(mint, -1));

*/

        int calls = 0;
        inv.Changed += () => calls++;


        inv.Add(mint, 3);
        inv.Add(mint, 3);
        inv.Add(mint, 1);
        inv.Add(mint, 1);
        inv.Add(mint, 0);

        Debug.Log($"Count calls: {calls}.");


        Debug.Log($"ИТОГО: прошло {_passed}, упало {_failed}");

        
        



    }

    private void Check(string testName, object expected, object actual)
    {
        if (expected.Equals(actual))
        {
            _passed++;
            Debug.Log($"PASS {testName}");
        }
        else
        {
            _failed++;
            Debug.LogError($"FAIL {testName}: ожидалось {expected}, получено {actual}");
        }
    }
}