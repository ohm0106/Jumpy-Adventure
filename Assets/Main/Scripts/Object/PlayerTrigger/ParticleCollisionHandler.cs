using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    [SerializeField]
    int damage = 10;
    
    void OnParticleCollision(GameObject other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
          
            HandlePlayerCollision(player); // ToDo : �÷��̾� ���� ���� �� �˹��� �ƴ� ���� ȿ�� �߰� �� ��. 
           
        }
    }

    private void HandlePlayerCollision(Player player)
    {
        player.Damage(damage);
        player.OnFire();
    }
}
