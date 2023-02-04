using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyMobHealthManager : HealthManager
{
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private GameObject deathMarker;
    
    [SerializeField] private GameObject myHealthBar;

    private Image staggerBarFill;

    private VampireFinisher vampireFinisher;
    private Material startMaterial;


    protected override void Awake()
    {
        startMaterial = transform.Find("Sprite").GetComponent<SpriteRenderer>().material;
        vampireFinisher = FindObjectOfType<VampireFinisher>();
        var newHealthBar = Instantiate(myHealthBar, transform.position, Quaternion.identity);
        newHealthBar.GetComponent<FollowOtherObject>().SetObjectToFollow(gameObject);
        healthBarFill = newHealthBar.GetComponent<FollowOtherObject>().GetHealthImageFill();
        staggerBarFill = newHealthBar.GetComponent<FollowOtherObject>().GetStaggerImageFill();

        base.Awake();
    }

    public override void Damage(float amount)
    {
        StartCoroutine("FlashWhite", 0.05f);

        if(vampireFinisher.GetSuckingBlood())
            return;
        base.Damage(amount);
        var particles = Instantiate(hitParticles, transform.position, Quaternion.identity);
        particles.Play();
        vampireFinisher.IncreaseRage(amount);
    }

    [SerializeField] private Material whiteColorMaterial;
    IEnumerator FlashWhite(float time)
    {
        transform.Find("Sprite").GetComponent<SpriteRenderer>().material = whiteColorMaterial;

        yield return new WaitForSeconds(time);

        transform.Find("Sprite").GetComponent<SpriteRenderer>().material = startMaterial;
    }

    public void DamageWhileBloodSuck(float amount)
    {
        base.Damage(amount * WaveManager.Instance.GetCurrentDifficultySetting.damageMultiplier);
    }

    public void RunEnemyDeath()
    {
        Destroy(gameObject);
        var particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        particles.Play();
        WaveManager.Instance.EnemyMobDeath();
    }
    
    public void SetDeathMarker(bool newStatus)
    {
        deathMarker.SetActive(newStatus);
    }
    
    protected override void UpdateHealth()
    {
        base.UpdateHealth();
        if(currentHealth <= 0 )
            RunEnemyDeath();
    }


    public Image GetStaggerBar() { return staggerBarFill; }
}
