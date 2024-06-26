using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    // so ¿˙¿Â     

    Dictionary<ItemType, InventoryUI> slots;

    [SerializeField]
    ItemUIData itemUIData;

    InventoryUI[] inventoryUIs;

    private void Start()
    {

        inventoryUIs = GetComponentsInChildren<InventoryUI>();
        slots = new Dictionary<ItemType, InventoryUI>();
        

    }
  

    private void OnEnable()
    {
        GameCanvasManager.Instance.playerEvent.OnAddItem += AddItemInventory;
        GameCanvasManager.Instance.playerEvent.OnDeleteItem += DeleteItemInventory;
    }

    private void OnDisable()
    {
        GameCanvasManager.Instance.playerEvent.OnAddItem -= AddItemInventory;
        GameCanvasManager.Instance.playerEvent.OnDeleteItem -= DeleteItemInventory;
    }

    void AddItemInventory(ItemType t, int amount = 1)
    {
        Sprite sprite = itemUIData.GetSprite(t);
        if(sprite != null)
        {
            if (!slots.ContainsKey(t))
            {
                foreach (var slot in inventoryUIs)
                {
                    if (slot.IsEmpty())
                    {
                        slots.Add(t, slot);
                        slot.SetImageAndText(sprite, amount);
                        break;
                    }
                }
            }
            else
            {
                slots[t].SetText(amount);
            }
           
        }
    }

    void DeleteItemInventory(ItemType t, int amount = 1)
    {
        if (slots.ContainsKey(t))
        {
            if (slots[t].SetText(amount))
            {
                slots.Remove(t);
            }
        }
        else
        {
            Debug.Log("not exist");
        }

    }
}
