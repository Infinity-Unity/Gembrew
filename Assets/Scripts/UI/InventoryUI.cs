using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private IngredientCellUI _cellPrefab;
    [SerializeField] private Transform _cellsParent;

    private Inventory _inventory;
    private Dictionary<IngredientData, IngredientCellUI> _inventoryItemsUI;


    private void Start()
    {
        _inventoryItemsUI = new Dictionary<IngredientData, IngredientCellUI>();
        _inventory = GameManager.Instance.Inventory;
        _inventory.Changed += UpdateCell;
        Refresh();
    }

    private void OnDestroy()
    {
        if( _inventory != null )
        {
            _inventory.Changed -= UpdateCell;
        }
    }

    private void Refresh()
    {
       _inventoryItemsUI.Clear();

        foreach(Transform child in _cellsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var pair in _inventory.Items)
        {
            UpdateCell(pair.Key,pair.Value);
        }
    }


    private void UpdateCell(IngredientData data,int amount)
    {
        if (_inventoryItemsUI.TryGetValue(data,out var cell))
        {
            if(amount > 0)
            {
                cell.Setup(data, amount);
            }
            else
            {
                Destroy(cell.gameObject);
                _inventoryItemsUI.Remove(data);             
            }
        }
        else
        {
            var newCell = Instantiate(_cellPrefab, _cellsParent);
            newCell.Setup(data, amount);
            _inventoryItemsUI.Add(data, newCell);
        }
    }
}

