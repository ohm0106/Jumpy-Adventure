using UnityEngine;

[RequireComponent(typeof(BasicAttackDecorator))]
public class SpikeSmallEnemy : Enemy
{
    Transform target;
    float attackRange = 0.85f;
    float chaseRange = 2.5f;
    float moveSpeed = 2.0f;

    EnemyDecorator enemyDecorator;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        enemyDecorator = GetComponent<EnemyDecorator>();
        target = FindObjectOfType<Player>().gameObject.transform; // todo
        ChangeState(new PatrolState());
    }



    void MoveTowardsTarget()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned.");
            return;
        }

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * (moveSpeed * 2f));
        }

    }

    void Update()
    {
        base.Update();

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= attackRange)
        {
            Attack();
        }
        else if (distance > attackRange  && distance <= chaseRange)
        {
            Chase();
        }
        else if (distance > chaseRange + 0.5f)
        {
            GetComponent<EnemyMovement>().SetMovement(true);
            ChangeState(new PatrolState());
        }

        if (currentState is ChaseState)
        {
            MoveTowardsTarget();
        }
        else
        {
            
        }
    }

    public void Chase()
    {
        GetComponent<EnemyMovement>().SetMovement(false);
        ChangeState(new ChaseState());
    }

    public override void Attack()
    {
        ChangeState(new AttackState());
    }

    public override void PerformAttack()
    {
        enemyDecorator.Attack();
    }

    public override EnemyType GetEnemyType()
    {
        return EnemyType.Sliver;
    }

    public override float GetAttackCoolTime()
    {
        return 4f;
    }
}
