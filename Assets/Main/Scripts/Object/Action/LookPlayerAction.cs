using System.Collections;
using UnityEngine;
using DG.Tweening;
public class LookPlayerAction : ObjectAction
{

    [SerializeField]
    float rotationSpeed = 2.0f;

    public override IEnumerator CoAction()
    {
        Player player = FindObjectOfType<Player>();


        while (isAction)
        {
          
                /*Quaternion dir = Quaternion.LookRotation(player.transform.position);
                transform.rotation = dir;*/

                Vector3 direction = player.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            

            yield return null;
        }

    }

}