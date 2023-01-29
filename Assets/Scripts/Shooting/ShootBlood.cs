using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBlood : MonoBehaviour
{
    [SerializeField] private float projectileDamage = 5f;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject shootSpawn;
    [SerializeField] private float damageToSelf;
    [SerializeField] private float startShootCooldown = 1;
    private float shootCooldown;

    private PlayersHealth _health;

    void Start()
    {
        shootCooldown = startShootCooldown;
        _health = GetComponent<PlayersHealth>();
    }

    void FixedUpdate()
    {
        if (Mouse.current.leftButton.isPressed && shootCooldown <= 0)
        {
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootSpawn.transform.position, projectileSpeed);
            shootCooldown = startShootCooldown;
        }
        else
        {
            shootCooldown-=Time.deltaTime;
        }
    }

    public void Shoot(Vector3 direction, float speed)
    {
        GetComponent<IDamageable>().Damage(damageToSelf);

        Camera.main.GetComponent<ScreenShake>().DoScreenShake(0.15f, 0.12f);

        var projectileRotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90;

        var projectileRotation =  Quaternion.Euler(0f, 0f, projectileRotationZ);

        var newProjectile = Instantiate(projectile, shootSpawn.transform.position, projectileRotation);
        newProjectile.GetComponent<ProjectileFlyStraight>().speed = speed;
        newProjectile.GetComponent<BulletCollider>().damage = projectileDamage + _health.GetMaxHealth - _health.GetHealth;
    }
}
