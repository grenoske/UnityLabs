using Core.Services.Updater;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour, IDisposable
{
    public int playerMaxHealth;
    public int playerCurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void OnUpdate()
    {
        if(playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }    
        
    }

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
    private void OnDestroy()
    {
        Dispose();
    }
}

