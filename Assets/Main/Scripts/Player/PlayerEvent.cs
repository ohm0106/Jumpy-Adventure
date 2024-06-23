using UnityEngine;
using System;

public class PlayerEvent : MonoBehaviour
{
    public event Action<ItemType,int> OnAddItem;

    public event Action<ItemType, int> OnDeleteItem;

    public event Action<EffectType> OnStartEffect;

    public event Action<EffectType> OnStopEffect;

    public event Action<bool> OnMovePlayer;

    // 상호작용 시 다른 상호작용 오브젝트들 멈춰야 할듯? 

    public void AddItem(ItemType t,int amount = 1)
    {
        OnAddItem?.Invoke(t,amount);
    }
    public void DeleteItem(ItemType t, int amount = 1)
    {
        OnAddItem?.Invoke(t, amount);
    }


    public void StartEffect(EffectType effect)
    {
        OnStartEffect?.Invoke(effect); 
    }

    public void StopEffect(EffectType effect)
    {
        OnStartEffect?.Invoke(effect);
    }

    public void SetMovePlayer(bool isMove)
    {
        OnMovePlayer?.Invoke(isMove);
    }

    public void SetInterativeState(bool isInteractive)
    {

    }
}
