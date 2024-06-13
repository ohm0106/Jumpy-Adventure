using System.Collections;
using UnityEngine;
using DG.Tweening;
public class LookPlayerAction : ObjectAction
{
    public override IEnumerator CoAction()
    {
        Player player = FindObjectOfType<Player>();
        while (isAction)
        {
            /*Quaternion dir = Quaternion.LookRotation(player.transform.position);
            transform.rotation = dir;*/

            transform.LookAt(player.transform);
            transform.rotation = new Quaternion(0,transform.rotation.y, 0, transform.rotation.w);
            
            yield return null;
        }

    }
}