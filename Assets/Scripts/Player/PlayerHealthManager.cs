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
    public GameObject healEffect;
    // Start is called before the first frame update
    void Start()
    {
        ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        playerCurrentHealth = ProjectUpdater.PlayerHP;
    }

    // Update is called once per frame
    void OnUpdate()
    {
        if(playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            Scene scene = SceneManager.GetActiveScene();
            ProjectUpdater.PlayerHP = 100;
            SceneManager.LoadScene(scene.name);
        }    
        
    }

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void RestorePlayerHealth(int healthToRestore)
    {
        GameObject effectInstance = Instantiate(healEffect, gameObject.transform.position, gameObject.transform.rotation);
        playerCurrentHealth += healthToRestore;

        if (playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }

        Destroy(effectInstance, 1.5f);
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

