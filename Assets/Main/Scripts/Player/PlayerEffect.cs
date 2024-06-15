using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField]
    EffectSetting walkEffect;

    PlayerEvent eventController;

    [SerializeField]
    GameObject effects;

    void Awake()
    {
        eventController = GetComponent<PlayerEvent>();
    }

    void OnEnable()
    {
        eventController.OnStartEffect += StartEffectFromType;
        eventController.OnStopEffect += StopEffectFromType;

    }

    void OnDisable()
    {
        eventController.OnStartEffect -= StartEffectFromType;
        eventController.OnStopEffect -= StopEffectFromType;
    }

    public void StartEffectFromType(EffectType t)
    {
        switch (t)
        {
            case EffectType.WALK:
                if (walkEffect.effect != null)
                {
                   Instantiate(walkEffect.effect, transform.position + (transform.forward * -0.3f), Quaternion.identity, effects.transform);
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

[System.Serializable]
public class EffectSetting
{
    public EffectType type;
    public GameObject effect;
}

public enum EffectType
{
    Click,
    WALK
}