using UnityEngine;

public abstract class EnemyDecorator : EnemyComponent
{
    protected EnemyComponent decoratedEnemy;

    public EnemyDecorator(EnemyComponent enemy)
    {
        this.decoratedEnemy = enemy;
    }

    public override void Attack()
    {
        if (decoratedEnemy != null)
        {
            decoratedEnemy.Attack();
        }
    }

    public override void StopAttack()
    {
        if (decoratedEnemy != null)
        {
            decoratedEnemy.StopAttack();
        }
    }
}
