using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IPlayerTrigger
{
    [SerializeField]
    int damage = 20;

    Rigidbody rigid;
    [SerializeField]
    float speed = 1000f;

    [SerializeField]
    float lifeT = 2f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.AddForce(transform.forward * speed);

        Destroy(gameObject, lifeT);
    }

    public void OnPlayerEnter(Player player)
    {
        Vector3 knockbackDirection = (player.transform.position - transform.position).normalized;
        player.ApplyKnockback(knockbackDirection);
        player.Damage(damage);
    }

    public void OnPlayerExit(Player player)
    {
        // 필요 시 추가 처리
    }
}
