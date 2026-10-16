using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


// Данный тест написан с помощью AI.
public class AlchemyTester : MonoBehaviour
{
    private int _passed;
    private int _failed;
    private readonly List<UnityEngine.Object> _created = new List<UnityEngine.Object>();

    private IngredientData _mint, _water, _sage, _potion, _elixir;

    private void Start()
    {
        _mint = Ing("Mint");
        _water = Ing("Water");
        _sage = Ing("Sage");
        _potion = Ing("Potion");
        _elixir = Ing("Elixir");

        RunTests();

        foreach (var obj in _created)
        {
            Destroy(obj);
        }

        Debug.Log($"ИТОГО: прошло {_passed}, упало {_failed}");
    }

    private void RunTests()
    {
        var recipeAB = Recipe(_potion, 1, (_mint, 2), (_water, 1));
        var recipeAC = Recipe(_elixir, 1, (_mint, 2), (_sage, 1));

        // ---------- FindRecipe ----------

        var finder = Sys(new Inventory(), recipeAB);
        Check("1: точное совпадение находит рецепт", true, finder.FindRecipe(Pot((_mint, 2), (_water, 1))) == recipeAB);
        Check("2: другой порядок в котле", true, finder.FindRecipe(Pot((_water, 1), (_mint, 2))) == recipeAB);
        Check("3: лишний ингредиент -> null", true, finder.FindRecipe(Pot((_mint, 2), (_water, 1), (_sage, 1))) == null);
        Check("4: неверное количество -> null", true, finder.FindRecipe(Pot((_mint, 3), (_water, 1))) == null);
        Check("5: не хватает ингредиента -> null", true, finder.FindRecipe(Pot((_mint, 2))) == null);
        Check("6: пустой котёл -> null", true, finder.FindRecipe(Pot()) == null);
        Check("7: null вместо котла -> null", true, finder.FindRecipe(null) == null);
        Check("8: пустой список рецептов -> null", true, Sys(new Inventory()).FindRecipe(Pot((_mint, 2))) == null);
        Check("9: два рецепта не путаются", true,
            Sys(new Inventory(), recipeAB, recipeAC).FindRecipe(Pot((_mint, 2), (_sage, 1))) == recipeAC);
        Check("10: null в списке рецептов пропускается", true,
            Sys(new Inventory(), null, recipeAB).FindRecipe(Pot((_mint, 2), (_water, 1))) == recipeAB);

        // ---------- TryCraft ----------

        {
            var inv = Stock((_mint, 2), (_water, 1));
            var sys = Sys(inv, recipeAB);
            var pot = Pot((_mint, 2), (_water, 1));
            Check("11a: хватает -> true", true, sys.TryCraft(pot));
            Check("11b: остался только результат", "Potion=1", Snapshot(inv));
            Check("11c: вторая варка без запаса -> false", false, sys.TryCraft(pot));
            Check("11d: инвентарь не изменился", "Potion=1", Snapshot(inv));
        }

        {
            var inv = Stock((_mint, 5), (_water, 3));
            Sys(inv, recipeAB).TryCraft(Pot((_mint, 2), (_water, 1)));
            Check("12: лишний запас остаётся", "Mint=3, Potion=1, Water=2", Snapshot(inv));
        }

        {
            var recipeThree = Recipe(_potion, 3, (_mint, 2), (_water, 1));
            var inv = Stock((_mint, 2), (_water, 1));
            Sys(inv, recipeThree).TryCraft(Pot((_mint, 2), (_water, 1)));
            Check("13: результат в нужном количестве", "Potion=3", Snapshot(inv));
        }

        {
            // главный тест: всё или ничего
            var recipeThreeIngredients = Recipe(_potion, 1, (_mint, 2), (_water, 1), (_sage, 1));
            var inv = Stock((_mint, 2), (_water, 1)); // шалфея нет
            var ok = Sys(inv, recipeThreeIngredients).TryCraft(Pot((_mint, 2), (_water, 1), (_sage, 1)));
            Check("14a: не хватает одного из трёх -> false", false, ok);
            Check("14b: ничего не списано", "Mint=2, Water=1", Snapshot(inv));
        }

        {
            var inv = Stock((_mint, 1), (_water, 1)); // мяты 1, нужно 2
            var ok = Sys(inv, recipeAB).TryCraft(Pot((_mint, 2), (_water, 1)));
            Check("15a: не хватает количества -> false", false, ok);
            Check("15b: ничего не списано", "Mint=1, Water=1", Snapshot(inv));
        }

        {
            var inv = Stock((_mint, 2), (_water, 1));
            var ok = Sys(inv, recipeAB).TryCraft(Pot((_mint, 1)));
            Check("16a: рецепт не найден -> false", false, ok);
            Check("16b: ничего не списано", "Mint=2, Water=1", Snapshot(inv));
        }

        {
            var broken = Recipe(null, 1, (_mint, 2), (_water, 1)); // нет результата
            var inv = Stock((_mint, 2), (_water, 1));
            var ok = Sys(inv, broken).TryCraft(Pot((_mint, 2), (_water, 1)));
            Check("17a: рецепт без результата -> false", false, ok);
            Check("17b: ничего не списано", "Mint=2, Water=1", Snapshot(inv));
        }

        {
            // на текущем коде этот тест должен быть красным: TryGetValue(null) бросает исключение
            var nullSlot = Recipe(_potion, 1, (null, 1));
            var sys = Sys(new Inventory(), nullSlot);
            var pot = Pot((_mint, 1));
            Check("18: пустой слот ингредиента не роняет поиск", false, Throws(() => sys.FindRecipe(pot)));
        }
    }

    // ---------- Помощники ----------

    private void Check(string testName, object expected, object actual)
    {
        if (expected.Equals(actual))
        {
            _passed++;
            Debug.Log($"PASS {testName}: ожидалось {expected}, получено {actual}");
        }
        else
        {
            _failed++;
            Debug.LogError($"FAIL {testName}: ожидалось {expected}, получено {actual}");
        }
    }

    private IngredientData Ing(string ingredientName)
    {
        var data = ScriptableObject.CreateInstance<IngredientData>();
        data.name = ingredientName;
        _created.Add(data);
        return data;
    }

    // Поля RecipeData приватные, а сеттеров нет (так и надо: рецепты правятся только в Inspector).
    // Поэтому тест записывает их через reflection, чтобы не зависеть от ассетов проекта.
    private RecipeData Recipe(IngredientData result, int resultAmount, params (IngredientData ingredient, int amount)[] lines)
    {
        var recipe = ScriptableObject.CreateInstance<RecipeData>();
        _created.Add(recipe);

        var list = new List<RecipeIngredient>();
        foreach (var line in lines)
        {
            var recipeIngredient = new RecipeIngredient();
            Set(recipeIngredient, "_ingredient", line.ingredient);
            Set(recipeIngredient, "_amount", line.amount);
            list.Add(recipeIngredient);
        }

        Set(recipe, "_ingredients", list);
        Set(recipe, "_resultIngredient", result);
        Set(recipe, "_amountResultIngredient", resultAmount);
        return recipe;
    }

    private static void Set(object target, string fieldName, object value)
    {
        FieldInfo field = target.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field == null)
        {
            throw new Exception($"Нет поля {fieldName} у {target.GetType().Name}. Переименовал поле?");
        }
        field.SetValue(target, value);
    }

    private static AlchemySystem Sys(Inventory inventory, params RecipeData[] recipes)
    {
        return new AlchemySystem(inventory, new List<RecipeData>(recipes));
    }

    private static Dictionary<IngredientData, int> Pot(params (IngredientData ingredient, int amount)[] items)
    {
        var pot = new Dictionary<IngredientData, int>();
        foreach (var item in items)
        {
            pot[item.ingredient] = item.amount;
        }
        return pot;
    }

    private static Inventory Stock(params (IngredientData ingredient, int amount)[] items)
    {
        var inventory = new Inventory();
        foreach (var item in items)
        {
            inventory.Add(item.ingredient, item.amount);
        }
        return inventory;
    }

    // Снимок всего инвентаря одной строкой, отсортированный по имени.
    // Сравнивая строки, мы ловим и потерянные, и неожиданно появившиеся предметы.
    private static string Snapshot(Inventory inventory)
    {
        var parts = new List<string>();
        foreach (var pair in inventory.Items)
        {
            parts.Add($"{pair.Key.name}={pair.Value}");
        }
        parts.Sort(StringComparer.Ordinal);
        return string.Join(", ", parts);
    }

    private static bool Throws(Action action)
    {
        try
        {
            action();
            return false;
        }
        catch (Exception)
        {
            return true;
        }
    }
}