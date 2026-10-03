using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientCellUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Color[] _rarityColors;
    
    

    public void Setup(IngredientData data,int amount)
    {

       
        if(data.Icon == null)
        {
            _icon.sprite = null;
            _icon.color = _rarityColors[(int)data.Rarity];
        }
        else
        {
            _icon.color = Color.white;
            _icon.sprite = data.Icon;
        }

        _countText.text = amount.ToString();

    }

}
