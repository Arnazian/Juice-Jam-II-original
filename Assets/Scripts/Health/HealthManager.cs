using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour, IDamageable
{
    public Image healthBarFill;
    public float maxHealth;

    protected float currentHealth;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealth();
    }

    public virtual void Damage(float amount)
    {
        currentHealth -= Mathf.Round(amount);
        UpdateHealth();
    }

    public virtual void Heal(float amount)
    {
        currentHealth += Mathf.Round(amount);
        UpdateHealth();
    }

    public void SetHealth(float newValue)
    {
        currentHealth = newValue;
        UpdateHealth();
    }

    protected virtual void UpdateHealth()
    {
        var fillAmount = currentHealth / maxHealth;
        healthBarFill.fillAmount = fillAmount;
    }
}