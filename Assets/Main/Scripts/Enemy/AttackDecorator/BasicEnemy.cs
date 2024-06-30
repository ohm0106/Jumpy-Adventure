using UnityEngine;

public class BasicEnemy : EnemyComponent
{
    public override void Attack()
    {
        //Debug.Log("Basic Enemy attacks!");
    }

    public override Weapon GetWeapon()
    {
        return null;
    }

    public override void StopAttack()
    {
        //Debug.Log("Stop Basic Enemy attacks!");
    }
}
