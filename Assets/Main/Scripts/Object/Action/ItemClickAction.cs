using System.Collections;
using UnityEngine;
using DG.Tweening;
public class ItemClickAction : ObjectAction
{
    [SerializeField]
    float delay = 1f;

    public override IEnumerator CoAction()
    {
        gameObject.transform.DOScale(0, delay);
        yield return new WaitForSeconds(delay);
        // ToDo : ¿Ã∆Â∆Æ √ﬂ∞° 
    }
}
