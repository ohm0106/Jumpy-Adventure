using UnityEngine;

public class DamageTrap : MonoBehaviour, IPlayerTrigger
{
    [SerializeField]
    int damage = 5;
    [SerializeField]
    float knockbackF = 2.0f;
    [SerializeField]
    float delay = 0.2f;

    public void OnPlayerEnter(Player player)
    {
        Vector3 knockbackDirection = (player.transform.position - transform.position).normalized;
        player.ApplyKnockback(knockbackDirection, knockbackF, delay);
        player.Damage(damage);
    }

    public void OnPlayerExit(Player player)
    {
        // 필요 시 추가 처리
    }
}