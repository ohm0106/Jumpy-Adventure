using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    Dictionary<ItemType, int> invenDic;

    PlayerEvent pEvent;

    void Start()
    {
        invenDic = new Dictionary<ItemType, int>();
        pEvent = GetComponent<PlayerEvent>();
    }
    void OnEnable()
    {
        pEvent = gameObject.GetComponent<PlayerEvent>();
        pEvent.OnAddItem += AddItem;
        pEvent.OnDeleteItem += DeleteItem;
    }

    void OnDisable()
    {
        pEvent.OnAddItem -= AddItem;
        pEvent.OnDeleteItem -= DeleteItem;
    }

    void AddItem(ItemType t , int amount)
    {
        if (invenDic.ContainsKey(t))
            invenDic[t] += amount;
        else
            invenDic.Add(t, amount);
    }

    void DeleteItem(ItemType t, int amount)
    {
        if (invenDic.ContainsKey(t))
            invenDic[t] -= amount;
        else
            invenDic.Add(t, amount);

    }

}
