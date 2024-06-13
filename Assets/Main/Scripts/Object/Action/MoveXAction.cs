using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MoveXAction : ObjectAction
{
    [SerializeField]
    float degreeX = 5f;
    [SerializeField]
    float delay = 3f;

    public override IEnumerator CoAction()
    {
       
        while (isAction)
        {
            gameObject.transform.DOLocalMoveX( degreeX, delay);
            yield return new WaitForSeconds(delay);
            gameObject.transform.DOLocalMoveX( -degreeX, delay);
            yield return new WaitForSeconds(delay);
        }

    }
}
