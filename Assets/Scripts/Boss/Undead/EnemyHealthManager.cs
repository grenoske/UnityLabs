using Core.Services.Updater;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDisposable
{
    public int MaxHealth;
    public int CurrentHealth;
    public string deadTag = "Dead";
    // Start is called before the first frame update
    void Start()
    {
        ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        CurrentHealth = MaxHealth;
    }

    void OnUpdate()
    {
        if (CurrentHealth <= 0)
        {
            gameObject.tag = deadTag;
        }

    }

    public void HurtEnemy(int damageToGive)
    {
        CurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }

    
    public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
    private void OnDestroy()
    {
        Dispose();
    }
}
