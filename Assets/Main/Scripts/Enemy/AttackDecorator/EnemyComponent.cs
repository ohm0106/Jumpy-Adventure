using UnityEngine;

public abstract class EnemyComponent : MonoBehaviour
{
    public abstract void Attack();

    public abstract void StopAttack();
    public abstract Weapon GetWeapon();
}