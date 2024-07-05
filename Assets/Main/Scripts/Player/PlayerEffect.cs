using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField]
    EffectObject walkEffect;

    PlayerEvent eventController;

    [SerializeField]
    GameObject effects;

    void Start()
    {
        ObjectPoolManager.Instance.CreatePool(walkEffect, 10, effects.transform);
    }

    void OnEnable()
    {
        if(eventController == null)
            eventController = GetComponent<PlayerEvent>();

        eventController.OnStartEffect += StartEffectFromType;
        eventController.OnStopEffect += StopEffectFromType;

    }

    void OnDisable()
    {
        if (eventController == null)
            eventController = GetComponent<PlayerEvent>();
        eventController.OnStartEffect -= StartEffectFromType;
        eventController.OnStopEffect -= StopEffectFromType;
    }

    public void StartEffectFromType(EffectType t)
    {
        switch (t)
        {
            case EffectType.WALK:
                if (walkEffect.gameObject != null)
                {
                    //Instantiate(walkEffect.effect, , Quaternion.identity, effects.transform);
                    EffectObject effect = ObjectPoolManager.Instance.GetObject<EffectObject>();
                    effect.transform.position = transform.position + (transform.forward * -0.3f);
                    effect.transform.rotation = Quaternion.identity;
                }
                break;
        }

    }

    public void StopEffectFromType(EffectType t)
    {
        switch (t)
        {
            case EffectType.WALK:
                
                break;
        }

    }

}


public enum EffectType
{
    Click,
    WALK,
    HIT
}