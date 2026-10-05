using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private IngredientCellUI _cellPrefab;
    [SerializeField] private Transform _cellsParent;

    private Inventory _inventory;


    private void Start()
    {
        _inventory = GameManager.Instance.Inventory;
        _inventory.Changed += Refresh;
        Refresh();

    }

    private void OnDestroy()
    {
        if( _inventory != null )
        {
            _inventory.Changed -= Refresh;
        }
    }

    private void Refresh()
    {
        foreach(Transform child in _cellsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var pair in _inventory.Items)
        {
            var cell = Instantiate(_cellPrefab, _cellsParent);
            cell.Setup(pair.Key, pair.Value);
        }
    }
}

