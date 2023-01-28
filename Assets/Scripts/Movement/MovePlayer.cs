using UnityEngine;

public class MovePlayer : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100;
    
    [SerializeField] private float speed;
    [SerializeField] private float accelerationTime;

    private Rigidbody2D rb;

    private float inputH;
    private float inputV;

    private float _health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _health = maxHealth;
    }

    void FixedUpdate()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");


        Vector2 direction = new Vector2(inputH, inputV);

        Vector2 movement = Accelerate(rb.velocity, accelerationTime, direction, 10 * speed);

        rb.velocity += movement;


        if (Mathf.Abs(inputH) <= 0 && Mathf.Abs(inputV) <= 0)
        {
            Vector2 Deacceleration = Deaccelerate(rb.velocity, accelerationTime * 1000);
            rb.velocity += Deacceleration;
        }
        
        if(_health <= 0)
            Destroy(gameObject);
    }


    Vector3 Accelerate(Vector3 velocity, float time, Vector3 direction, float maxSpeed)
    {
        Vector3 Acceleration = (direction * maxSpeed - velocity) / time;
        return Acceleration;
    }

    Vector3 Deaccelerate(Vector3 velocity, float time)
    {
        Vector3 Deacceleration = (Vector3.zero - velocity) / time;
        return Deacceleration;
    }

    public void Damage(float amount)
    {
        _health -= amount;
    }
}
