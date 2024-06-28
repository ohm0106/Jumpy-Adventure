using UnityEngine;

public enum EnemyType
{
    Sliver,
    Gold,
    Platinum,
    Diamond
}

public abstract class Enemy : MonoBehaviour
{
    int maxHealth = 100;
    protected int currentHealth;
    protected IEnemyState currentState;
    protected Animator anim;
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void ChangeState(IEnemyState newState)
    {
        if(newState.Equals(currentState))
        {
            Debug.Log("already state");
            return;
        }
        if (currentState != null)
        {
            currentState.Exit(this);
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute(this);
        }
    }

    public Animator GetAnimator()
    {
        return anim;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public abstract EnemyType GetEnemyType();

    public IEnemyState GetCurrentState()
    {
        return currentState;
    }
    public abstract void Attack();

    public abstract void PerformAttack();

    public abstract float GetAttackCoolTime();
}

