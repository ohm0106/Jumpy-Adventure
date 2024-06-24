using UnityEngine;
using System;

public class PlayerEvent : MonoBehaviour
{
    // 이벤트 선언
    public event Action<ItemType, int> OnAddItem;
    public event Action<ItemType, int> OnDeleteItem;
    public event Action<EffectType> OnStartEffect;
    public event Action<EffectType> OnStopEffect;
    public event Action<bool> OnMovePlayer;


    public void AddItem(ItemType t, int amount = 1)
    {
        OnAddItem?.Invoke(t, amount);
    }

    public void DeleteItem(ItemType t, int amount = 1)
    {
        OnDeleteItem?.Invoke(t, amount);
    }

    public void StartEffect(EffectType effect)
    {
        OnStartEffect?.Invoke(effect);
    }

    public void StopEffect(EffectType effect)
    {
        OnStopEffect?.Invoke(effect);
    }

    public void SetMovePlayer(bool isMove)
    {
        OnMovePlayer?.Invoke(isMove);
    }


    public void SetInteractiveState(bool isInteractive)
    {
        
    }
}