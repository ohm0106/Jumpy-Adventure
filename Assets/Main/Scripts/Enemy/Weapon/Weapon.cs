using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    GameObject hitEffectPrefab;

    public abstract void Use();
    public abstract void StopUse();

    protected void TriggerEffect(Vector3 position)
    {
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, position, Quaternion.identity);
        }
    }
}
