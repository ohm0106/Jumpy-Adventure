using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
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
}
