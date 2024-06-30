using UnityEngine;
using System.Collections;

public interface IEnemyState
{
    void Enter(Enemy enemy);
    void Execute(Enemy enemy);
    void Exit(Enemy enemy);
}

public class IdleState : IEnemyState
{
    public void Enter(Enemy enemy) {         
        Debug.Log("Enter Idle State"); }
    public void Execute(Enemy enemy) { Debug.Log("Idle..."); }
    public void Exit(Enemy enemy) { Debug.Log("Exit Idle State"); }
}

public class PatrolState : IEnemyState
{
    public void Enter(Enemy enemy) {
        enemy.GetAnimator().SetBool("isWalk", true);
    }
    public void Execute(Enemy enemy) {
        //Debug.Log("Patrolling..."); 
    }
    public void Exit(Enemy enemy) { 
        enemy.GetAnimator().SetBool("isWalk", false);
    }
}
public class ChaseState : IEnemyState
{
    public void Enter(Enemy enemy) {
        enemy.GetAnimator().SetBool("isWalk", true); 
    
    }
    public void Execute(Enemy enemy) { 
        //Debug.Log("Chasing...!");
        }
    public void Exit(Enemy enemy) { enemy.GetAnimator().SetBool("isWalk", false); }
}

public class AttackState : IEnemyState
{
    float attackCooldown = 2.0f;
    float lastAttackTime = 0;
    public void Enter(Enemy enemy)
    {
        attackCooldown = enemy.GetAttackCoolTime();
    }
    public void Execute(Enemy enemy) { 
        if(Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            enemy.GetAnimator().SetTrigger("doAttack");
            enemy.PerformAttack();
        }
    }
    public void Exit(Enemy enemy) { Debug.Log("Exit Attack State"); }
}

public class DeathState : IEnemyState
{
    public void Enter(Enemy enemy) {
        enemy.GetAnimator().SetTrigger("doDeath");
    }
    public void Execute(Enemy enemy) {}
    public void Exit(Enemy enemy) 
    { 
        GameObject.Destroy(enemy); // todo
    }
}
