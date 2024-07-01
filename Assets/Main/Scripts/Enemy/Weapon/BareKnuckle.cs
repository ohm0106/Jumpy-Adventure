using System;
using UnityEngine;

public class BareKnuckle : Weapon , IWeaponTrigger
{
    int damage = 1;

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

    public void OnObjectEnter(Transform t, Action<Vector3> onKnockback, Action<int> onDamage)
    {
        Vector3 knockbackDirection = (t.position - transform.position).normalized;
        onKnockback.Invoke(knockbackDirection);
        onDamage.Invoke(damage);
        TriggerEffect(t.position);
    }

    public void OnObjectExit()
    {

    }

    public Weapon GetThis()
    {
        return this;
    }
}
