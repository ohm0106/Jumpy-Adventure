using UnityEngine;
using System;

public class PlayerEvent : MonoBehaviour
{
    public event Action<ItemType> OnSetItem;

    public event Action<EffectType> OnStartEffect;

    public event Action<EffectType> OnStopEffect;

    public event Action<bool> OnMovePlayer;

    public void SetItem(ItemType t)
    {
        OnSetItem?.Invoke(t);
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
}
