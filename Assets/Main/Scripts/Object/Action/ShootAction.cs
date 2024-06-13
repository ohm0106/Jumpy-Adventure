using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ShootAction : ObjectAction
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform shootPos;

    [SerializeField]
    float delay;

    public override IEnumerator CoAction()
    {
        while (isAction)
        {
            GameObject bulletTemp = GameObject.Instantiate(bulletPrefab, shootPos.position , shootPos.rotation);
            yield return new WaitForSeconds(delay);
        }
   
    }
}
