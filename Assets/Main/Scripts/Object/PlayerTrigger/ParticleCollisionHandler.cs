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
          
            HandlePlayerCollision(player); // ToDo : 플레이어 무적 상태 및 넉백이 아닌 점프 효과 추가 할 것. 
           
        }
    }

    private void HandlePlayerCollision(Player player)
    {
        player.Damage(damage);
        player.OnFire();
    }
}
