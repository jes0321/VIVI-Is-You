using System.Collections;
using System.Collections.Generic;
using GGMPool;
using UnityEngine;

public class EffectPlayer : MonoBehaviour, IPoolable
{


    private ParticleSystem _particle;
    private float _duration;
    private WaitForSeconds _particleDuration;


    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
        _duration = _particle.main.duration;
        _particleDuration = new WaitForSeconds(_duration); 
    }

    public void SetPositionAndPlay(Vector3 position)
    {
        transform.position = position;
        _particle.Play();
        StartCoroutine(DelayAndGotoPoolCoroutine());
    }

    private IEnumerator DelayAndGotoPoolCoroutine()
    {
        yield return _particleDuration;
        //PoolManager.Instance.Push(this);
    }

    public PoolTypeSO PoolType { get; }
    public GameObject GameObject { get; }

    public void SetUpPool(Pool pool)
    {
        //
    }

    public void ResetItem()
    {
        _particle.Stop();
        _particle.Simulate(0); 
    }
}
