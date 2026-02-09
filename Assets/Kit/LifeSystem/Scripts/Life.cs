using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] float startingLife = 1f;

    [Header("Debug")]
    [SerializeField] float debugHitDamage = 0.1f;
    [SerializeField] bool debugReciveHit;


    [SerializeField] float currentLife;

    [SerializeField] public UnityEvent <float> onLifeChanged;
    [SerializeField] public UnityEvent onDeath;

    private void OnValidate()
    {
        if (debugReciveHit)
        {
            debugReciveHit = false;
            OnHitRecived(debugHitDamage);
        }
    }

    private void Awake()
    {
        currentLife = startingLife;
    }

    public void OnHitRecived(float damage)
    {
        if(currentLife > 0f)
        {
            currentLife -= damage;
            onLifeChanged.Invoke(currentLife);
            if (currentLife <= 0f)
            {
                onDeath.Invoke();
            }
        }
    }

    internal void RecoverHealth(float healthRecovery)
    {
        if(currentLife > 0f)
        {
            currentLife += healthRecovery;
            currentLife = Mathf.Clamp01(currentLife);
            onLifeChanged.Invoke(currentLife);
        }
    }
}
