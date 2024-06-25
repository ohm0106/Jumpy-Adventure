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
    public event Action<bool> OnTeleportPlayer;
    public event Action<int,int> OnPlayerHP;
    public event Action OnDeadPlayer;

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

    public void SetMoveTeleportPlayer(bool isTeleport)
    {
        OnTeleportPlayer?.Invoke(isTeleport);
    }

    public void SetInteractiveState(bool isInteractive)
    {
        
    }

    public void SetPlayerDead()
    {
        OnDeadPlayer?.Invoke();
    }

    public void GetCurrentPlayerHP (int hp, int maxHp)
    {
        OnPlayerHP?.Invoke(hp, maxHp);
    }
}