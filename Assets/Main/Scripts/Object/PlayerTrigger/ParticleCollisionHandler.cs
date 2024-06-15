using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
   
    
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
        player.Damage(10);
    }
}
