using System.Collections;
using UnityEngine;
using DG.Tweening;
public class MoveYAction : ObjectAction
{
    [SerializeField]
    float degreeY = 5f;
    [SerializeField]
    float delay = 3f;

    public override IEnumerator CoAction()
    {
        float Y = transform.position.y;
        while (isAction)
        {
            gameObject.transform.DOMoveY(Y + degreeY, delay);
            yield return new WaitForSeconds(delay);
            gameObject.transform.DOMoveY(Y , delay);
            yield return new WaitForSeconds(delay);
        }

    }
}
