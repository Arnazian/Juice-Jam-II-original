using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float bossMaxHealth;
    [SerializeField] private Slider bossHealthSlider;
    
    private float bossCurHealth;

    private BossStateController bossStateController;
    private void Start()
    {
        if (bossHealthSlider == null)
            bossHealthSlider = FindObjectsOfType<Slider>()[1];
        bossStateController = GetComponent<BossStateController>(); 
        bossCurHealth = bossMaxHealth;
        bossHealthSlider.maxValue = bossMaxHealth;
        bossHealthSlider.value = bossCurHealth; 
    }
    public void Damage(float damage)
    {
        bossCurHealth -= damage;
        bossHealthSlider.value = bossCurHealth;
        bossStateController.CheckStageThreshold();

        if (bossCurHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public float GetHealth => bossCurHealth;
    public void SetHealth(float newHealth) { bossCurHealth = newHealth; }




}
    
