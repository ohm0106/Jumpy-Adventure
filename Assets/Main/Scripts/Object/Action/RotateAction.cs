using System.Collections;
using UnityEngine;

public class RotateAction : ObjectAction
{

    [SerializeField]
    Vector3 rotateDir;

    [SerializeField]
    float speed = 0.5f;

    public override IEnumerator CoAction()
    {
        while (isAction)
        {
            Quaternion deltaRotation = Quaternion.Euler(rotateDir * speed);
            this.transform.rotation = this.transform.rotation * deltaRotation;
            yield return null;
        }

    }
}
