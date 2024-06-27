using System.Collections;
using UnityEngine;

public class ObjectAction : MonoBehaviour
{
    protected bool isAction;

    protected virtual void Start()
    {
        // init hear
    }

    public virtual void StartAction()
    {
        if (!isAction)
        {
            isAction = true;
            StartCoroutine(CoAction());

        }
        
    }
    public Coroutine StartActionReturn()
    {
        if (!isAction)
        {
            isAction = true;
           return  StartCoroutine(CoAction());

        }
        return null;
    }

    public virtual IEnumerator CoAction()
    {
        yield return null;
    }

    public virtual void ReleaseAction()
    {
        if (isAction)
            isAction = false;
        StartCoroutine(CoRelease());
    }

    public virtual IEnumerator CoRelease()
    {
        yield return null;
    }
}
