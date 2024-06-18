using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TreeShake : ObjectAction
{
    [SerializeField]
    float shakeDuration = 1.0f; // ��鸮�� ���� �ð�
    [SerializeField]
    float shakeStrength = 1.0f; // ��鸮�� ����
    [SerializeField]
    int shakeVibrato = 10; // ���� Ƚ��
    [SerializeField]
    float shakeRandomness = 90.0f; // ��鸮�� ������

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