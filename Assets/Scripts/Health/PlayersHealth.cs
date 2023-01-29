using UnityEngine;

public class PlayersHealth : MonoBehaviour, IDamageable
{
    private HealthBar _healthBar;
    public bool isDashing;

    void Start()
    {
        isDashing = false;

        _healthBar = UIManager.Instance.GetPlayerHealthBar;
    }

    void FixedUpdate()
    {
        if(_healthBar.GetHealth <= 0)
            Destroy(gameObject);
    }


    public void Damage(float amount)
    {
        if (isDashing)
            return;

        _healthBar.Damage(amount);
    }
}
