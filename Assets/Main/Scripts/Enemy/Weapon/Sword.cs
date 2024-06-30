using System;
using UnityEngine;

public class Sword : Weapon, IWeaponTrigger
{
    int damage = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StopUse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Use()
    {
        Debug.Log("Using Sword");
        GetComponent<Collider>().enabled = true;
    }

    public override void StopUse()
    {
        Debug.Log("Stop Using Sword");
        GetComponent<Collider>().enabled = false;
    }

    public void OnObjectEnter(Transform t, Action<Vector3> onKnockback, Action<int> onDamage)
    {
        Vector3 knockbackDirection = (t.position - transform.position).normalized;
        onKnockback.Invoke(knockbackDirection);
        onDamage.Invoke(damage);
    }

    public void OnObjectExit()
    {
        //throw new NotImplementedException();
    }
    public Weapon GetThis()
    {
        return this;
    }
}

