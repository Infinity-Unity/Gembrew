using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _startingGold;
    private int _currentGold;

    public static GameManager Instance;
    public Inventory Inventory { get; private set; }
    public AlchemySystem AlchemySystem {  get; private set; }

    [SerializeField] private RecipeBook _allRecipeBook;


    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SaveData data = SaveManager.Load();
        if (data != null) _currentGold = data.gold;
        else _currentGold = _startingGold;

        Inventory = new Inventory();
        AlchemySystem = new AlchemySystem();
    }

    public RecipeBook AllRecipeBook => _allRecipeBook;

}
