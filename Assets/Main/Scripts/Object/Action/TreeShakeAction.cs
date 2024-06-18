using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TreeShake : ObjectAction
{
    [SerializeField]
    float shakeDuration = 1.0f; // Èçµé¸®´Â Áö¼Ó ½Ã°£
    [SerializeField]
    float shakeStrength = 1.0f; // Èçµé¸®´Â °­µµ
    [SerializeField]
    int shakeVibrato = 10; // Áøµ¿ È½¼ö
    [SerializeField]
    float shakeRandomness = 90.0f; // Èçµé¸®´Â ·£´ý¼º

    bool isHaveApple = true;

    void Start()
    {
       
    }

    public override IEnumerator CoAction()
    {
        if (isHaveApple)
        {
            ShakeTree();
            isHaveApple = false;
        }
       
        yield return null;
    }


    public void ShakeTree()
    {
        transform.DOShakeRotation(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
    }
}