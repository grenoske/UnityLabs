using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerEnemy : MonoBehaviour
{
    public Slider healthBar;
    public EnemyHealthManager enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = enemyHealth.MaxHealth;
        healthBar.value = enemyHealth.CurrentHealth;
    }
}
