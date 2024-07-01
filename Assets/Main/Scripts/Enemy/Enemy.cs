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
    protected EnemyDecorator enemyDecorator;
    bool isKnockedBack = false;
    Vector3 knockbackDirection;
    float knockbackTime = 1f;
    float knockbackCounter = 0;

    CharacterController characterController;
    Vector3 velocity;
    [SerializeField]
    float gravity = -9.81f;

    public void Start()
    {
        currentHealth = maxHealth;
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
        }
    }

    public virtual void ChangeState(IEnemyState newState)
    {
        if (currentState != null && newState.GetType() == currentState.GetType())
        {
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
        if (isKnockedBack)
        {
            knockbackCounter -= Time.deltaTime;
            if (knockbackCounter <= 0)
            {
                isKnockedBack = false;
            }
            else
            {
                Vector3 horizontalKnockback = new Vector3(knockbackDirection.x, 0, knockbackDirection.z);
                characterController.Move(horizontalKnockback * Time.deltaTime);
                return;
            }
        }

        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
        }

        characterController.Move(velocity * Time.deltaTime);

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

    protected abstract void Attack();

    public abstract void PerformAttack();

    public abstract float GetAttackCoolTime();

    protected abstract void Damage(int hp);

    public void ApplyKnockback(Vector3 direction)
    {
        isKnockedBack = true;
        knockbackDirection = new Vector3(direction.x, 0, direction.z);
        knockbackCounter = knockbackTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log("check!!! Trigger");
        //TODO : ³Ë¹é State ·Î ¹Ù²Ü °Í. 
        IWeaponTrigger weaponTrigger = other.GetComponent<IWeaponTrigger>();
        if (weaponTrigger != null && enemyDecorator.GetWeapon() != weaponTrigger.GetThis())
        {
            anim.SetTrigger("doHit");
            Debug.Log("check!!! Trigger");
            weaponTrigger.OnObjectEnter(transform, (Vector3 v) => { ApplyKnockback(v); }, (int hp) => { Damage(hp); });
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Debug.Log("check!!! Trigger");
        IWeaponTrigger weaponTrigger = other.GetComponent<IWeaponTrigger>();
        if (weaponTrigger != null)
        {
            weaponTrigger.OnObjectExit();
        }
    }
}
 