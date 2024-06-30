using UnityEngine;

public class BasicAttackDecorator : EnemyDecorator
{
    public BasicAttackDecorator(EnemyComponent enemy) : base(enemy) { }

    [SerializeField]
    Weapon rightBareKnuckles;

    [SerializeField]
    Weapon leftBareKnuckles;

    void Start()
    {
        
    }

    public override void Attack()
    {
        base.Attack();
        Perform();
    }

    public override void StopAttack()
    {
        base.StopAttack();
        StopPerform();
    }

    private void Perform()
    {
        if(rightBareKnuckles != null)
            rightBareKnuckles.Use();

        if (leftBareKnuckles != null)
            leftBareKnuckles.Use();
    }
    private void StopPerform()
    {
        if(rightBareKnuckles != null)
            rightBareKnuckles.StopUse();

        if (leftBareKnuckles != null)
            leftBareKnuckles.StopUse();
    }

    public override Weapon GetWeapon()
    {
        return rightBareKnuckles;
    }
}
