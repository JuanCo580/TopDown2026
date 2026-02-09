using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] Life life;
    [SerializeField] Image imageFill;

    private void OnEnable()
    {
        life.onLifeChanged.AddListener(OnLifeChanged);
        life.onDeath.AddListener(OnDeath);
    }

    private void OnDisable()
    {
        life.onLifeChanged.RemoveListener(OnLifeChanged);
        life.onDeath.RemoveListener(OnDeath);
    }

    void OnLifeChanged(float newLife)
    {
        imageFill.fillAmount = newLife;
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }
}
