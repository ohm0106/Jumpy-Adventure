using UnityEngine;

public class Water : MonoBehaviour, IPlayerTrigger
{

    [SerializeField]
    GameObject paticlePrefab;

    public void OnPlayerEnter(Player player)
    {
        Instantiate(paticlePrefab, player.transform.position, player.transform.rotation);
        player.Damage(100);
        
    }

    public void OnPlayerExit(Player player)
    {
        // 필요 시 추가 처리
    }
}