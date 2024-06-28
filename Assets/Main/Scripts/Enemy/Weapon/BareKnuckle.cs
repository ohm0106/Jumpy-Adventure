using UnityEngine;

public class BareKnuckle : Weapon , IPlayerTrigger
{
    int damage = 2;

    void Start()
    {
        StopUse();
    }

    public override void Use()
    {
        Debug.Log("Using Bare Knuckle");
        GetComponent<Collider>().enabled = true;
        
    }
    public override void StopUse()
    {
        Debug.Log("Stop Using Bare Knuckle");
        GetComponent<Collider>().enabled = false;
    }
    public void OnPlayerEnter(Player player)
    {
        Vector3 knockbackDirection = (player.transform.position - transform.position).normalized;
        player.ApplyKnockback(knockbackDirection);
        player.Damage(damage);
    }

    public void OnPlayerExit(Player player)
    {
     
    }

 
}
