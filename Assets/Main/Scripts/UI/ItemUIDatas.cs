using UnityEngine;


[CreateAssetMenu(fileName = "ItemUIData", menuName = "Inventory/ItemUIData")]
public class ItemUIData : ScriptableObject
{
    [SerializeField]
    Item[] items;

    public Sprite GetSprite(ItemType itemType)
    {
        foreach (var item in items)
        {
            if (item.itemType == itemType)
            {
                return item.itemSprite;
            }
        }
        return null; 
    }
}