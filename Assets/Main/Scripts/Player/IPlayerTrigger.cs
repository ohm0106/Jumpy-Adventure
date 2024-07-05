using System;
using UnityEngine;


public interface IPlayerTrigger
{
    void OnPlayerEnter(Player player);
    void OnPlayerExit(Player player);
}



public interface IWeaponTrigger
{
    void OnObjectEnter(Transform t,Action<Vector3> onKnockback, Action<int> onDamage);
    void OnObjectExit();
    public Weapon GetThis();
}

