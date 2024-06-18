using System.Collections;
using UnityEngine;
using DG.Tweening;
public class ItemClickAction : ObjectAction
{
    [SerializeField]
    ItemType type;

    [SerializeField]
    float delay = 1f;

    void Start()
    {
            
    }

    
    void SendItemType()
    {
        FindAnyObjectByType<PlayerEvent>().SetItem(type);
    }

    public override IEnumerator CoAction()
    {
        yield return StartCoroutine(base.CoAction());

        gameObject.transform.DOScale(0, delay);
        yield return new WaitForSeconds(delay);

        SendItemType();
        // ToDo : ¿Ã∆Â∆Æ √ﬂ∞° 
    }
}
