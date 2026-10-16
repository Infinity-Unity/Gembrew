using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _startingGold;
    private int _currentGold;

    public static GameManager Instance;
    public Inventory Inventory { get; private set; }
    public AlchemySystem AlchemySystem {  get; private set; }

    [SerializeField] private RecipeCatalog _allRecipeCatalog;


    private void Awake()
    {
        if (!TryInitInstance())
        {
            return;
        }

        SaveData data = SaveManager.Load();
        if (data != null)
        {
            _currentGold = data.gold;
        }
        else
        {
            _currentGold = _startingGold;
        }

        Inventory = new Inventory();
        AlchemySystem = new AlchemySystem(Inventory,_allRecipeCatalog.AllRecipes);
    }

    public RecipeCatalog AllRecipeCatalog => _allRecipeCatalog;

    private void OnValidate()
    {
        if(_allRecipeCatalog == null)
        {
            Debug.LogWarning("Ошибка в GameManager! Каталог рецептов пуст или не инициализирован. Заполните поля в испекторе!",this);
        }
    }

    private bool TryInitInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }
    }
}
