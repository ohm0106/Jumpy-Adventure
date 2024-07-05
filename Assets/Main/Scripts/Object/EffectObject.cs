using UnityEngine;
using System.Collections;

[System.Serializable]
public class EffectObject : MonoBehaviour, IPoolable
{
    public EffectType type;

    private ParticleSystem particleSystem;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnSpawn()
    {
        if (particleSystem != null)
        {
            Debug.Log("Test");
            particleSystem.Clear();
            particleSystem.Play();

            StartCoroutine(CoCheckParticle());
        }
    }

    IEnumerator CoCheckParticle()
    {
        yield return new WaitUntil(() => !particleSystem.IsAlive(true));

        ObjectPoolManager.Instance.ReturnObject(this);
    }

    #region IPoolable

    public void OnDespawn()
    {
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }
    }

    #endregion
}
